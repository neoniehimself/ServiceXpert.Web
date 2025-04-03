using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ServiceXpert.Domain.Shared;

namespace ServiceXpert.Web.Controllers
{
    public class SxpController(ICompositeViewEngine compositeViewEngine) : Controller
    {
        private readonly ICompositeViewEngine compositeViewEngine = compositeViewEngine;

        [NonAction]
        protected async Task<string> RenderViewToHtmlStringAsync(string viewName, object model, ViewDataDictionary? viewData = null)
        {
            var viewResult = this.compositeViewEngine.GetView(null, viewName, false);

            if (!viewResult.Success)
            {
                viewResult = this.compositeViewEngine.FindView(this.ControllerContext, viewName, false);

                if (!viewResult.Success)
                {
                    throw new InvalidOperationException($"View '{viewName}' not found.");
                }
            }

            using var writer = new StringWriter();

            viewData ??= new ViewDataDictionary(new EmptyModelMetadataProvider(), this.ModelState);
            viewData.Model = model;

            var viewContext = new ViewContext(
                this.ControllerContext,
                viewResult.View,
                viewData,
                this.TempData,
                writer,
                new HtmlHelperOptions()
            );

            await viewResult.View.RenderAsync(viewContext);
            return writer.ToString();
        }

        [NonAction]
        protected static ViewDataDictionary GetPaginationViewDataDictionary(Pagination pagination, ModelStateDictionary modelState)
        {
            int startPage = Math.Max(1, pagination.CurrentPage - 2);
            int endPage = Math.Min(pagination.TotalPageCount, startPage + 4);

            if (endPage - startPage < 4)
            {
                startPage = Math.Max(1, endPage - 4);
            }

            return new ViewDataDictionary(new EmptyModelMetadataProvider(), modelState)
            {
                Model = pagination,
                ["PaginationStartIndex"] = ((pagination.CurrentPage - 1) * pagination.PageSize) + 1,
                ["PaginationEndIndex"] = Math.Min(pagination.CurrentPage * pagination.PageSize, pagination.TotalCount),
                ["PaginationStartPage"] = startPage,
                ["PaginationEndPage"] = endPage
            };
        }
    }
}
