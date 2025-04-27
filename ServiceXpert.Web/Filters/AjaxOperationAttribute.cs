using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ServiceXpert.Web.Filters
{
    public class AjaxOperationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers["X-Requested-With"].Equals("XMLHttpRequest"))
            {
                context.Result = new BadRequestObjectResult(new
                {
                    message = "Bad Request: Direct URL access not allowed."
                });
            }
            base.OnActionExecuting(context);
        }
    }
}
