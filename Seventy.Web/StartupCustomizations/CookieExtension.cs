using Microsoft.AspNetCore.Http;
using System;

namespace Seventy.Web.StartupCustomizations
{
    public static class CookieExtension
    {
        public static string GetCookie(this IHttpContextAccessor httpContext , string key)
        {
            try
            {
                string output = httpContext.HttpContext.Request.Cookies[key];
                return output;
            }
            catch (Exception){}
            return null;
        }

        public static bool SetCookie(this IHttpContextAccessor httpContext,  string key, string value, int? expireTime)
        {
            try
            {
                CookieOptions option = new CookieOptions();

                if (expireTime.HasValue)
                    option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
                else
                    option.Expires = DateTime.Now.AddMilliseconds(10);

                httpContext.HttpContext.Response.Cookies.Append(key, value, option);
                return true;
            }
            catch { }
            return false;
        }
        public static bool RemoveCookie(this IHttpContextAccessor httpContext, string key)
        {
            try
            {
                httpContext.HttpContext.Response.Cookies.Delete(key);
            }
            catch (Exception){}
            return false;
                    
        }
    }
}
