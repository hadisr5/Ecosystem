using Microsoft.AspNetCore.Mvc;
using Seventy.Service.Users;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Areas.Core.Notifications
{
    [Area("Core")]
    [Route("Core/[controller]/[action]/{id?}")]
    public class NotificationController : Controller
    {
        private readonly IUserManager _userManager;
        public NotificationController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [Route("/core/notif")]
        [UserAccess(Common.Enums.eAccessControl.NotificationIndex, Common.Enums.eAccessType.None, 1)]
        public IActionResult Index()
        {
            return View();
        }
    }
}