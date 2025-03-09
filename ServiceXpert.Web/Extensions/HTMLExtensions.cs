using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceXpert.Web.Extensions
{
    public static class HTMLExtensions
    {
        public static string IsActive(this IHtmlHelper htmlHelper, string controller, string action)
        {
            // Get route data
            var routeData = htmlHelper.ViewContext.RouteData;

            // Get controller and page
            var routeAction = routeData.Values["action"]!.ToString();
            var routeController = routeData.Values["controller"]!.ToString();

            return (controller == routeController) &&
                   (action == routeAction)
                   ? "active" : "";
        }
    }
}
