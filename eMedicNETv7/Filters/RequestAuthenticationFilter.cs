using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eMedicNETv7.Filters
{
    public class RequestAuthenticationFilter : IActionFilter
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public RequestAuthenticationFilter(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controllerAndAction = context.ActionDescriptor.DisplayName;
            var ctx = _contextAccessor.HttpContext;
            if (ctx != null)
            {
                var session = ctx.Session.GetString("UserNM");
                if (session == null && controllerAndAction != null && !controllerAndAction.Contains("AuthController.Index"))
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Auth" }));
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
