using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Seventy.Service.Core.UserAccess;
using System.Threading;
using Seventy.Service.Core.UserRole;
using Seventy.Service.Users;
using Seventy.ViewModel.Core;
using Microsoft.AspNetCore.Authorization;
using Seventy.Service.Core.Roles;
using Seventy.Service.Core.Roles.DefaultRoleAccess;
using Seventy.WebFramework.Filters;
using Seventy.Service.Core.UserPermissionGroup;
using Seventy.Service.Core.AccessPermissionGroup;
using Seventy.Service.Core.PermissionGroup;

namespace Seventy.Web.Areas.Admin.Permission.api
{
    //[Route("api/[controller]")]
    [ApiController]
    [Area("Admin")]
    public class PermissionController : ControllerBase
    {
        private readonly IUserAccessService userAccessService;
        private readonly IUserRoleService userRoleService;
        private readonly IUserManager userManager;
        private readonly IRolesService rolesService;
        private readonly IDefaultRoleAccessService defaultRoleAccessService;
        private readonly IUserPermissionGroupService userPermissionGroupService;
        private readonly IAccessPermissionGroupService accessPermissionGroupService;
        private readonly IAccessPermissionGroupService permissionGroupService1;
        private readonly IPermissionGroupService permissionGroupService;

        public PermissionController(IUserAccessService userAccessService,
            IUserRoleService userRoleService,
            IUserManager userManager,
            IRolesService rolesService,
            IDefaultRoleAccessService defaultRoleAccessService,
            IUserPermissionGroupService userPermissionGroupService,
            IAccessPermissionGroupService  accessPermissionGroupService,
            IPermissionGroupService permissionGroupService)
        {
            this.userAccessService = userAccessService;
            this.userRoleService = userRoleService;
            this.userManager = userManager;
            this.rolesService = rolesService;
            this.defaultRoleAccessService = defaultRoleAccessService;
            this.userPermissionGroupService = userPermissionGroupService;
            this.accessPermissionGroupService = accessPermissionGroupService;
            this.permissionGroupService = permissionGroupService;
        }

        [HttpGet]
        [Route("/Admin/api/Permission/GetPermissions")]
        [UserAccess(Common.Enums.eAccessControl.GetPermissionsInAllocationForUsers, Common.Enums.eAccessType.Api, 1)]
        public async Task<IActionResult> GetPermissions([DataSourceRequest] DataSourceRequest request, int userId, string permissionType, CancellationToken cancellationToken)
        {
            DataSourceResult result = new DataSourceResult();
            if (userId == 0)
                return Ok(result);
            switch (permissionType)
            {
                case "access":
                    result = await userAccessService.GetAccessesForUserAsync_DataSourceResult(userId, request);
                    break;
                case "accessgroup":
                    result = await userPermissionGroupService.GetPermissionGroupForUserAsync_DataSourceResult(userId, request);
                    break;
                case "role":
                    result = await userRoleService.GetRolesForUserAsync_DataSourceResult(userId, request);
                    break;
                default:
                    break;
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("/Admin/api/Permission/GetUserList")]
        [UserAccess(Common.Enums.eAccessControl.GetUserListInAllocationForUsers, Common.Enums.eAccessType.Api, 2)]
        public async Task<IActionResult> GetUserList([DataSourceRequest] DataSourceRequest request, CancellationToken cancellationToken)
        {
            return Ok(await userManager.GetAllUsers_DataSourceResult(request));
        }

        [HttpPost]
        [Route("/Admin/api/Permission/SavePermission")]
        [UserAccess(Common.Enums.eAccessControl.SavePermissionInAllocationForUsers, Common.Enums.eAccessType.Api, 3)]
        public async Task<IActionResult> SavePermission(SavePermissionViewModel viewModel, CancellationToken cancellationToken)
        {
            int result = 0;
            switch (viewModel.AccessType)
            {
                case "access":
                    result = await userAccessService.ChangeUserAccessesAsync(viewModel, cancellationToken);
                    break;
                case "accessgroup":
                    result = await userPermissionGroupService.ChangeUserGroupAccessAsync(viewModel, cancellationToken);
                    break;
                case "role":
                    result = await userRoleService.ChangeUserRolesAsync(viewModel, cancellationToken);
                    break;
                default:
                    break;
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("/Admin/api/Permission/GetRoleList")]
        [UserAccess(Common.Enums.eAccessControl.GetRolesInAllocationForDefaultAccess, Common.Enums.eAccessType.Api, 4)]
        public async Task<IActionResult> GetRoleList([DataSourceRequest] DataSourceRequest request, CancellationToken cancellationToken)
        {
            return Ok(await rolesService.GetAllRoles_DataSourceResult(request));
        }

        [HttpGet]
        [Route("/Admin/api/Permission/GetDefaultRoleAccesses")]
        [UserAccess(Common.Enums.eAccessControl.GetDefaultRoleAccessesInAllocationRole, Common.Enums.eAccessType.Api, 5)]
        public async Task<IActionResult> GetDefaultRoleAccesses([DataSourceRequest] DataSourceRequest request, int roleId, CancellationToken cancellationToken)
        {
            DataSourceResult result = new DataSourceResult();
            if (roleId == 0)
                return Ok(result);
            result = await defaultRoleAccessService.GetAccessesForRoleAsync_DataSourceResult(roleId, request);
            return Ok(result);
        }

        [HttpPost]
        [Route("/Admin/api/Permission/SaveDefaultRoleAccesses")]
        [UserAccess(Common.Enums.eAccessControl.SaveDefaultRoleAccessesInAllocationRole, Common.Enums.eAccessType.Api, 6)]
        public async Task<IActionResult> SaveDefaultRoleAccesses(SaveDefaultRoleAccessesViewModel viewModel, CancellationToken cancellationToken)
        {
            int result = await defaultRoleAccessService.ChangeDefaultRoleAccessesAsync(viewModel, cancellationToken);
            return Ok(result);
        }



        [HttpGet]
        [Route("/Admin/api/Permission/GetGroupAccessList")]
        [UserAccess(Common.Enums.eAccessControl.GetGroupAccessInAllocationForDefaultAccess, Common.Enums.eAccessType.Api, 7)]
        public async Task<IActionResult> GetGroupAccessList([DataSourceRequest] DataSourceRequest request, CancellationToken cancellationToken)
        {
            return Ok(await permissionGroupService.GetAllPermissionGroup_DataSourceResult(request));
        }

        [HttpGet]
        [Route("/Admin/api/Permission/GetAccessesForPermissionGroup")]
        [UserAccess(Common.Enums.eAccessControl.GetAccessesForPermissionGroupInAllocationUserPermissionGroup, Common.Enums.eAccessType.Api, 8)]
        public async Task<IActionResult> GetAccessesForPermissionGroup([DataSourceRequest] DataSourceRequest request, int permissionGroupId, CancellationToken cancellationToken)
        {
            DataSourceResult result = new DataSourceResult();
            if (permissionGroupId == 0)
                return Ok(result);
            result = await accessPermissionGroupService.GetAccessesForPermissionGroupAsync_DataSourceResult(permissionGroupId, request);
            return Ok(result);
        }

        [HttpPost]
        [Route("/Admin/api/Permission/SaveAccessPermissionGroup")]
        [UserAccess(Common.Enums.eAccessControl.SaveAccessPermissionGroupInAllocationGroupAccess, Common.Enums.eAccessType.Api, 9)]
        public async Task<IActionResult> SaveAccessPermissionGroup(SaveAccessPermissionGroupViewModel viewModel, CancellationToken cancellationToken)
        {
            int result = await accessPermissionGroupService.ChangeAccessPermissionGroupAsync(viewModel, cancellationToken);
            return Ok(result);
        }
    }
}
