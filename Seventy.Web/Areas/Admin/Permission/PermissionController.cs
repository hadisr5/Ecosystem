using Microsoft.AspNetCore.Mvc;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Areas.Admin.Permission
{
    [Area("Admin")]
    public class PermissionController : Controller
    {
        [HttpGet]
        [Route("/Admin/Permission/UserAccessAndRole")]
        [UserAccess(Common.Enums.eAccessControl.UserAccessAndRoleView, Common.Enums.eAccessType.View, 1)]
        public IActionResult UserAccessAndRole() => View();

        [HttpGet]
        [Route("/Admin/Permission/DefaultRoleAccess")]
        [UserAccess(Common.Enums.eAccessControl.DefaultRoleAccessView, Common.Enums.eAccessType.View, 2)]
        public IActionResult DefaultRoleAccess() => View();

        [HttpGet]
        [Route("/Admin/Permission/AccessPermissionGroup")]
        [UserAccess(Common.Enums.eAccessControl.AccessPermissionGroupView, Common.Enums.eAccessType.View, 3)]
        public IActionResult AccessPermissionGroup() => View();
    }
}
