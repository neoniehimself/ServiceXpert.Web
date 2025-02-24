using Microsoft.AspNetCore.Mvc;
using PropLoader;
using ServiceXpert.API.Application.Abstractions.Interfaces.Services;
using ServiceXpert.API.Application.DataTransferObjects;
using ServiceXpert.API.Domain.Entities;

namespace ServiceXpert.API.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IIssueService issueService;

        public IssueController(IIssueService issueService)
        {
            this.issueService = issueService;
        }

        [HttpGet("{issueKey}")]
        public async Task<ActionResult<IssueResponse>> GetByIDAsync(string issueKey)
        {
            var issue = await this.issueService.GetByIDAsync(issueKey, new IncludeOptions<Issue>(i => i.IssueStatus!));
            return issue != null ? Ok(issue) : NotFound(issueKey);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IssueResponse>>> GetAllAsync()
        {
            var issues = await this.issueService.GetAllAsync(new IncludeOptions<Issue>(i => i.IssueStatus!));
            return Ok(issues);
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync(IssueForCreateRequest issueForCreateRequest)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            var issueID = await this.issueService.AddAsync(issueForCreateRequest);
            var issue = await this.issueService.GetByIDAsync(issueID);

            return Created(
                this.Url.Action(nameof(GetByIDAsync), new { issue!.IssueKey }),
                issue
            );
        }

        [HttpDelete("{issueKey}")]
        public async Task<ActionResult> DeleteByIDAsync(string issueKey)
        {
            await this.issueService.DeleteByIDAsync(issueKey);
            return NoContent();
        }

        // TODO: Implement Update EndPoint
    }
}
