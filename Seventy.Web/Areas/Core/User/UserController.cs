using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Seventy.Service.Users;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Areas.Core.User
{
    [Area("Core")]
    [Route("Core/[controller]/[action]/{id?}")]
    public class UserController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly INotif _notifManager;
        public UserController(IUserManager userManager, INotif notif)
        {
            _userManager = userManager;
            _notifManager = notif;
        }

        [Route("/core/user")]
        [UserAccess(Common.Enums.eAccessControl.UserIndex, Common.Enums.eAccessType.None, 1)]
        public IActionResult Index()
        {
            return View();
        }
        [Route("/core/changePass")]
        [UserAccess(Common.Enums.eAccessControl.UserChangePassword, Common.Enums.eAccessType.None, 1)]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [Route("/core/MessageList")]
        [UserAccess(Common.Enums.eAccessControl.UserMessageList, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> MessageList(CancellationToken cancellationToken)
        {
            var notifs = await _notifManager.getUserNotification(cancellationToken, true, 3);
            return View(notifs);
        }

        [HttpPost]
        [UserAccess(Common.Enums.eAccessControl.UserChangePasswordPost, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> ChangePasswordPost(Seventy.ViewModel.Core.Users.ChangePassword model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ChangePassword", model);
            }
            var loginResult = await _userManager.changePassword(model, cancellationToken);
            switch (loginResult)
            {
                case Service.Core.Users.ChangePassResult.Success:
                    return RedirectToAction("index", new { success = "رمز شما با موفقیت تغییر یافت" });
                case Service.Core.Users.ChangePassResult.Error:
                    ModelState.AddModelError("ConfirmedPassword", "خطایی رخ داده است");
                    return View("ChangePassword");
                case Service.Core.Users.ChangePassResult.OldPassError:
                    ModelState.AddModelError("OldPassword", "رمز قدیمی نادرست است");
                    return View("ChangePassword");
                case Service.Core.Users.ChangePassResult.ConfirmPassError:
                    ModelState.AddModelError("ConfirmedPassword", "رمز جدید و تایید رمز جدید مطابقت ندارند");
                    return View("ChangePassword");
                default:
                    break;
            }
            return View(model);
        }

    }
}