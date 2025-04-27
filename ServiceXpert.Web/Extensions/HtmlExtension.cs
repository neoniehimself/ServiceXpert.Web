using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceXpert.Web.Extensions;
public static class HtmlExtension
{
    public static string IsActive(this IHtmlHelper htmlHelper, string controller, string action)
    {
        var routeData = htmlHelper.ViewContext.RouteData;

        var routeAction = routeData.Values["action"]!.ToString();
        var routeController = routeData.Values["controller"]!.ToString();

        var routePath = htmlHelper.ViewContext.HttpContext.Request.Path.Value?.Trim('/').ToLower();

        bool isActive = (controller.Equals(routeController?.ToLower(), StringComparison.OrdinalIgnoreCase) &&
                         action.Equals(routeAction?.ToLower(), StringComparison.OrdinalIgnoreCase)) ||
                        (controller.Equals(routePath, StringComparison.OrdinalIgnoreCase));

        return isActive ? "active" : "";
    }
}
