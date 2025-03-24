using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.Abstractions.Interfaces.Services;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Domain.Entities;
using SxpEnums = ServiceXpert.Domain.Shared.Enums;

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
            return string.Concat(nameof(SxpEnums.IssuePreFix.SXP) + "-" + issueId);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Issue>>> GetAllAsync(string status, int pageNumber = 1, int pageSize = MaxTabContentPageSize)
        {
            if (pageSize > MaxTabContentPageSize)
            {
                pageSize = MaxTabContentPageSize;
            }

            var (issues, paginationMetaData) = await this.issueService.GetPagedAllByStatusAsync(status, pageNumber, pageSize);

            throw new NotImplementedException();
        }
    }
}
