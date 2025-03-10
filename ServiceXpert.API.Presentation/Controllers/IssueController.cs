using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServiceXpert.API.Application.Abstractions.Interfaces.Services;
using ServiceXpert.API.Application.DataTransferObjects.Issues;

namespace ServiceXpert.API.Presentation.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IIssueService issueService;

        public IssueController(IIssueService issueService)
        {
            this.issueService = issueService;
        }

        [HttpGet("{issueKey}")]
        public async Task<ActionResult<Issue>> GetByIDAsync(string issueKey)
        {
            var issue = await this.issueService.GetByIDAsync(issueKey);
            return issue != null ? Ok(issue) : NotFound(issueKey);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Issue>>> GetAllAsync()
        {
            var issues = await this.issueService.GetAllAsync();
            return Ok(issues);
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync(IssueForCreate issueForCreateRequest)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            var issueID = await this.issueService.AddAsync(issueForCreateRequest);
            return Ok(issueID);
        }

        [HttpDelete("{issueKey}")]
        public async Task<ActionResult> DeleteByIDAsync(string issueKey)
        {
            await this.issueService.DeleteByIDAsync(issueKey);
            return NoContent();
        }

        [HttpPatch("{issueKey}")]
        public async Task<ActionResult> PatchUpdateAsync(string issueKey, JsonPatchDocument<IssueForUpdate> patchDocument)
        {
            if (!await this.issueService.IsExistsByIDAsync(issueKey))
            {
                return NotFound(issueKey);
            }

            var issueID = this.issueService.GetIssueID(issueKey);
            var result = await this.issueService.ConfigureForUpdateAsync(issueID, patchDocument, this.ModelState);

            IssueForUpdate issueForUpdateRequest = result.Item1;
            ModelStateDictionary modelState = result.Item2;

            if (!modelState.IsValid)
            {
                return BadRequest(modelState);
            }

            if (!TryValidateModel(issueForUpdateRequest))
            {
                return BadRequest(modelState);
            }

            await this.issueService.UpdateByIDAsync(issueKey, issueForUpdateRequest);

            return NoContent();
        }

        [HttpGet("IssuePriorities")]
        public ActionResult<IEnumerable<string>> GetIssuePrioritiesAsync()
        {
            return Ok(this.issueService.GetIssuePriorities());
        }
    }
}
