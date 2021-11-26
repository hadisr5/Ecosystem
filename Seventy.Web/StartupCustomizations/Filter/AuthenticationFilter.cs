using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Seventy.DomainClass.Core;
using Seventy.Service.Users;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Seventy.Web.StartupCustomizations.Filter
{
    public class AuthenticationFilter : IAsyncActionFilter
    {
        private readonly IUserManager _userManager;
        private readonly IHttpContextAccessor _httpContext;

        public AuthenticationFilter(IUserManager userManager, IHttpContextAccessor httpContext)
        {
            _userManager = userManager;
            _httpContext = httpContext;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Users? user = null;
            if (_httpContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var _userID = _httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrEmpty(_userID))
                {
                   user = _userManager.GetByID(Convert.ToInt32(_userID));
                }
            }

            if (context.HttpContext.User.Identity.IsAuthenticated == false || user == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Account" }, { "action", "AccessDenied" } });
            }
            else
            {
                await next();
            }
        }
    }

}
