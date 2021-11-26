using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Seventy.Service.Users;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Seventy.Web.StartupCustomizations.CookieValidat
{
    public class CookieValidatorService : ICookieValidatorService
    {
        private readonly IUserManager _userService;

        public CookieValidatorService(IUserManager userService)
        {
            _userService = userService;
        }

        public async Task ValidateAsync(CookieValidatePrincipalContext context)
        {
            var userPrincipal = context.Principal;

            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
            if (claimsIdentity?.Claims == null || !claimsIdentity.Claims.Any())
            {
                // this is not our issued cookie
                await HandleUnauthorizedRequest(context);
                return;
            }

            if (claimsIdentity.FindFirst(ClaimTypes.UserData) == null)
            {
                await HandleUnauthorizedRequest(context);
                return;
            }
            var userIdString = claimsIdentity.FindFirst(ClaimTypes.UserData).Value;
            if (!int.TryParse(userIdString, out int userId))
            {
                // this is not our issued cookie
                await HandleUnauthorizedRequest(context);
                return;
            }
            var user = _userService.GetByID(userId);
            if (user == null)
            {
                // user has changed his/her password/roles/stat/IsActive
                await HandleUnauthorizedRequest(context);
            }

            // await _UserService.UpdateUserLastActivityDateAsync(userId).ConfigureAwait(false);
        }

        private async Task HandleUnauthorizedRequest(CookieValidatePrincipalContext context)
        {
            context.RejectPrincipal();
            await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        }
    }

}
