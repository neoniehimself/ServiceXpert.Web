using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.Abstractions.Interfaces.Services;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Web.Filters;
using ServiceXpert.Web.ViewModels;
using System.Net;
using SxpEnums = ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Web.Controllers
{
    [Route("Issues")]
    public class IssueController : Controller
    {
        private const int MaxTabContentPageSize = 10;

        private readonly IIssueService issueService;

        public IssueController(IIssueService issueService)
        {
            this.issueService = issueService;
        }

        [AjaxOperation]
        [HttpGet(nameof(InitializeCreateIssue))]
        public IActionResult InitializeCreateIssue()
        {
            return PartialView("_CreateIssueModal", new CreateIssueViewModel()
            {
                IssuePriorities = Enum.GetValues(typeof(SxpEnums.IssuePriority))
                    .Cast<SxpEnums.IssuePriority>().ToDictionary(p => (int)p, p => p.ToString())
            });
        }

        [AjaxOperation]
        [HttpPost(nameof(CreateIssue))]
        public async Task<IActionResult> CreateIssue(IssueDataObjectForCreate dataObject)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            try
            {
                var entityId = await this.issueService.CreateAsync(dataObject);

                return Json(new
                {
                    issueKey = string.Concat(nameof(SxpEnums.IssuePreFix.SXP) + "-" + entityId)
                });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [AjaxOperation]
        [HttpGet(nameof(GetTabContent))]
        public async Task<IActionResult> GetTabContent(string tab, int pageNumber = 1, int pageSize = MaxTabContentPageSize)
        {
            if (pageSize > MaxTabContentPageSize)
            {
                pageSize = MaxTabContentPageSize;
            }

            try
            {
                var (issues, paginationMetaData) = await this.issueService.GetPagedAllByStatusAsync(tab, pageNumber, pageSize);

                int startPage = Math.Max(1, paginationMetaData.CurrentPage - 2);
                int endPage = Math.Min(paginationMetaData.TotalPageCount, startPage + 4);

                if (endPage - startPage < 4)
                {
                    startPage = Math.Max(1, endPage - 4);
                }

                this.ViewData["PaginationStartPage"] = startPage;
                this.ViewData["PaginationEndPage"] = endPage;

                return PartialView("~/Views/Issue/_TabContent.cshtml", new IssueViewModel()
                {
                    Issues = issues.ToList(),
                    Metadata = paginationMetaData
                });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}