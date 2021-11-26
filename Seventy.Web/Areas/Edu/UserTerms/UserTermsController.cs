using Seventy.Service.Users;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Seventy.Service.EDU.Term;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Areas.Edu.UserTerms
{
    [Area("Edu")]
    public class UserTermsController : Controller
    {
        private static IUserManager _userManager;
        private static ITermService _termService;

        public UserTermsController(ITermService termService, IUserManager userManager)
        {
            _termService = termService;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("/Edu/UserTerms")]
        [UserAccess(Common.Enums.eAccessControl.UserTermsIndex, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var user = await _userManager.GetCurrentUserAsync(cancellationToken);

            var data = await _termService.GetUserTerms(user).ToListAsync(cancellationToken);

            return View(data);
        }
    }
}