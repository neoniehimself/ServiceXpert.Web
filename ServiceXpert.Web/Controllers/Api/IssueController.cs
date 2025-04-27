using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.Abstractions.Interfaces.Services;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Shared;
using DomainEnums = ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Web.Controllers.Api
{
    [Route("api/issues")]
    [ApiController]
    public class IssueController(IIssueService issueService) : ControllerBase
    {
        private const int MaxTabContentPageSize = 10;
        private readonly IIssueService issueService = issueService;

        [HttpPost]
        public async Task<ActionResult<string>> CreateIssueAsync(IssueDataObjectForCreate issue)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            var issueId = await this.issueService.CreateAsync(issue);
            return string.Concat(nameof(DomainEnums.IssuePreFix.SXP) + "-" + issueId);
        }

        [HttpGet]
        public async Task<ActionResult<(IEnumerable<Issue>, Pagination)>> GetPagedAllByStatusAsync(string statusCategory, int pageNumber = 1, int pageSize = MaxTabContentPageSize)
        {
            if (pageSize > MaxTabContentPageSize)
            {
                pageSize = MaxTabContentPageSize;
            }

            return await this.issueService.GetPagedAllByStatusAsync(statusCategory, pageNumber, pageSize);
        }

        [HttpGet("{issueKey}")]
        public async Task<ActionResult<Issue>> GetByIssueKeyAsync(string issueKey)
        {
            var issue = await this.issueService.GetByIssueKey(issueKey);
            return issue != null ? issue : NotFound();
        }

        [HttpPut("{issueKey}")]
        public async Task<ActionResult> UpdateByIssueKeyAsync(string issueKey, IssueDataObjectForUpdate issue)
        {
            await this.issueService.UpdateByIssueKeyAsync(issueKey, issue);
            return NoContent();
        }
    }
}
