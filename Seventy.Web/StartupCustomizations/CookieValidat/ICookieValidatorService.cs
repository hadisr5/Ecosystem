using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;

namespace Seventy.Web.StartupCustomizations.CookieValidat
{
    public interface ICookieValidatorService
    {
        Task ValidateAsync(CookieValidatePrincipalContext context);
    }
}
