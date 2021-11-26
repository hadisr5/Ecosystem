using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Areas.Edu.Error
{
    [Area("Edu")]
    [Authorize(Policy = "user")]
    public class ErrorHandeling : Controller
    {
        [Route("/Edu/ErrorHandeling/Erorr400")]
        [UserAccess(Common.Enums.eAccessControl.ErrorHandelingErorr400, Common.Enums.eAccessType.View, 1,true)]
        public IActionResult Erorr400()
        {
            return View();
        }
        [UserAccess(Common.Enums.eAccessControl.ErrorHandelingErorr401, Common.Enums.eAccessType.View, 1, true)]
        public IActionResult Erorr401()
        {
            return View();
        }
        [UserAccess(Common.Enums.eAccessControl.ErrorHandelingErorr402, Common.Enums.eAccessType.View, 3, true)]
        public IActionResult Erorr402()
        {
            return View();
        }
        [UserAccess(Common.Enums.eAccessControl.ErrorHandelingErorr403, Common.Enums.eAccessType.View, 4, true)]
        public IActionResult Erorr403()
        {
            return View();
        }
        [UserAccess(Common.Enums.eAccessControl.ErrorHandelingErorr404, Common.Enums.eAccessType.View, 4, true)]
        public IActionResult Erorr404()
        {
            return View();
        }
    }
}
