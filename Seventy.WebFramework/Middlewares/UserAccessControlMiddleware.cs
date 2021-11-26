using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Seventy.Service.Core.UserAccess;
using Seventy.Service.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.WebFramework.Middlewares
{
    public static class UserAccessControlMiddlewareExtension
    {
        public static void UseUserAccessControlHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<UserAccessControlMiddleware>();
        }
    }

    public class UserAccessControlMiddleware
    {
        private readonly RequestDelegate _next;        

        public UserAccessControlMiddleware(RequestDelegate next)
        {
            _next = next;            
        }

        public async Task Invoke(HttpContext context, IUserAccessService userAccess)
        {
            var hasAccess = await userAccess.CheckForAccess(context.Request.Path);
            if (hasAccess)
                await _next.Invoke(context);
            else
                await context.Response.WriteAsync("Forbidden Access");
        }
    }
}


