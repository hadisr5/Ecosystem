using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Areas.Edu.Error
{
    [Area("Edu")]
    [Authorize(Policy = "user")]
    public class ErrorController : Controller
    {
        [Route("/Edu/Error/Erorr400")]
        [UserAccess(Common.Enums.eAccessControl.Erorr400, Common.Enums.eAccessType.View, 1,true)]
        public IActionResult Erorr400()
        {
            return View();
        }

        [Route("/Edu/Error/Erorr401")]
        [UserAccess(Common.Enums.eAccessControl.Erorr401, Common.Enums.eAccessType.View, 2, true)]
        public IActionResult Erorr401()
        {
            return View();
        }

        [Route("/Edu/Error/Erorr402")]
        [UserAccess(Common.Enums.eAccessControl.Erorr402, Common.Enums.eAccessType.View, 3, true)]
        public IActionResult Erorr402()
        {
            return View();
        }

        [Route("/Edu/Error/Erorr403")]
        [UserAccess(Common.Enums.eAccessControl.Erorr403, Common.Enums.eAccessType.View, 4, true)]
        public IActionResult Erorr403()
        {
            return View();
        }

        [Route("/Edu/Error/Erorr404")]
        [UserAccess(Common.Enums.eAccessControl.Erorr404, Common.Enums.eAccessType.View, 5, true)]
        public IActionResult Erorr404()
        {
            return View();
        }
    }
}
