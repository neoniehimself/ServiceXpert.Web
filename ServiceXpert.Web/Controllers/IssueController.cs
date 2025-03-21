using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.Abstractions.Interfaces.Services;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Web.ViewModels;
using SxpEnums = ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Web.Controllers
{
    [Route("issues")]
    public class IssueController : Controller
    {
        private readonly IIssueService issueService;

        public IssueController(IIssueService issueService)
        {
            this.issueService = issueService;
        }

        [HttpGet(nameof(InitializeCreateIssue))]
        public IActionResult InitializeCreateIssue()
        {
            return PartialView("_CreateIssueModal", new CreateIssueViewModel()
            {
                IssuePriorities = Enum.GetValues(typeof(SxpEnums.IssuePriority))
                    .Cast<SxpEnums.IssuePriority>().ToDictionary(p => (int)p, p => p.ToString())
            });
        }

        [HttpPost(nameof(CreateIssue))]
        public async Task<IActionResult> CreateIssue(IssueDataObjectForCreate dataObject)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            var entityId = await this.issueService.CreateAsync(dataObject);
            return Json(new
            {
                issueKey = string.Concat(nameof(SxpEnums.IssuePreFix.SXP) + "-" + entityId)
            });
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet(nameof(GetTabContent))]
        public async Task<IActionResult> GetTabContent(string tab)
        {
            var issues = await this.issueService.GetAllAsync(tab);
            var issuesConverted = issues.ToList();

            return PartialView("~/Views/Issue/_TabContent.cshtml", new IssueViewModel()
            {
                Issues = issuesConverted
            });
        }
    }
}