using Microsoft.AspNetCore.Mvc;
using Seventy.DomainClass.Core;
using Seventy.Service.Users;
using Seventy.WebFramework.Api;
using Seventy.WebFramework.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seventy.Web.Areas.Admin.Controllers
{
    public class CourceController : Controller
    {
        private readonly UserManager userManager;

        public CourceController(UserManager userManager)
        {
            this.userManager = userManager;
        }
        [UserAccess(Common.Enums.eAccessControl.CourseIndex , Common.Enums.eAccessType.None,1)]
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
