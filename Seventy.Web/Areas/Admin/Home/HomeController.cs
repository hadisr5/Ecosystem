using Microsoft.AspNetCore.Mvc;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Areas.Admin.Home
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        [Route("admin",Name = "admin")]
        [UserAccess(Common.Enums.eAccessControl.HomeIndex , Common.Enums.eAccessType.None, 1)]
        public IActionResult Index()
        {
            return View();
        }

    }
}