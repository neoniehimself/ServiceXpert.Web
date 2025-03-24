using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.Abstractions.Interfaces.Services;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Web.Filters;
using ServiceXpert.Web.Helpers;
using ServiceXpert.Web.ViewModels;
using System.Net;
using System.Text;
using NewtonsoftJson = Newtonsoft.Json;
using SxpEnums = ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Web.Controllers
{
    [Route("issues")]
    public class IssueController(
        IHttpClientFactory httpClientFactory,
        IIssueService issueService)
        : Controller
    {
        private const int MaxTabContentPageSize = 10;
        private readonly IHttpClientFactory httpClientFactory = httpClientFactory;
        private readonly IIssueService issueService = issueService;

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

            var httpClient = this.httpClientFactory.CreateClient(ApiSettings.ClientName);
            var serializedObject = NewtonsoftJson.JsonConvert.SerializeObject(dataObject);

            var requestContent = new StringContent(serializedObject, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{httpClient.BaseAddress}/Issues", requestContent);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            var issueKey = response.Content.ReadAsStringAsync().Result;

            return Json(new
            {
                issueKey
            });
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