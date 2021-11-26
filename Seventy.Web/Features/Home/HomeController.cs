using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Seventy.Service.Core.Users;
using Seventy.Service.Users;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Features.Home
{
    public class HomeController : Controller
    {
        private readonly INotif _notifManager;
        public HomeController(INotif notifManager)
        {
            _notifManager = notifManager;
            //TempData.Put("notifs", UserManager.getUserNotification());
        }
        [UserAccess(Common.Enums.eAccessControl.ShowMainPage, Common.Enums.eAccessType.View, 1)]
        public IActionResult Index() => View();

        [Route("getNotifications")]
        [UserAccess(Common.Enums.eAccessControl.GetNotifications, Common.Enums.eAccessType.None, 2)]
        public async Task<IActionResult> GetNotifications(CancellationToken cancellationToken, bool isRead = false)
        {
            string st = "";
            int count = 0;
            // if (_NotifManager.GetCurrentUserAsync() != null)
            var notifs = await _notifManager.getUserNotification(cancellationToken, isRead);
            if (notifs == null) return Json(new { count = count, list = st }); ;
            foreach (var item in notifs)
            {
                var type = "";
                string color = "info";
                switch (item.MsgType)
                {
                    case "1":
                        type = "ft-message-circle"; break;
                    case "2":
                        type = "ft-message-circle";
                        color = "danger";
                        break;
                    case "3":
                        type = "ft-message-circle";
                        color = "warning";
                        break;
                    default:
                        type = "ft-message-circle"; break;
                }
                count++;
                st += $"<a href=\"javascript:void(0)\">" +
                       $"<div class=\"media\">" +
                           $"<div class=\"media-left align-self-center\"><i class=\"{type} {color} font-medium-4 mt-2\"></i></div>" +
                           $"<div class=\"media-body\">" +
                               $"<h6 class=\"media-heading {color}\">{item.MsgTitle}</h6>" +
                               $"<p class=\"notification-text font-small-3 text-muted text-bold-600\">{item.Description}!</p><small>" +
                                   $"<time class=\"media-meta text-muted\" datetime=\"{item.RegDate}\">{Helper.GetPersianDate(item.RegDate)}</time>" +
                               $"</small>" +
                           $"</div>" +
                       $"</div>" +
                   $"</a>";
            }
            return Json(new { count = count, list = st });
        }
    }
}