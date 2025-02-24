using Microsoft.AspNetCore.Mvc;
using ServiceXpert.API.Application.Abstractions.Interfaces.Services;

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
        public async Task<ActionResult> GetByID(string issueKey)
        {
            var issue = await this.issueService.GetByIDAsync(issueKey);
            return issue != null ? Ok(issue) : NotFound(issueKey);
        }
    }
}
