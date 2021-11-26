using Microsoft.AspNetCore.Mvc;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Areas.Core.Home
{
    [Area("Core")]
    public class HomeController : Controller
    {
        [Route("core", Name = "core")]
        [UserAccess(Common.Enums.eAccessControl.HomeIndex2, Common.Enums.eAccessType.None, 1)]
        public IActionResult Index()
        {
            return View();
        }

    }
}