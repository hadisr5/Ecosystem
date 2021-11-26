using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Seventy.DomainClass.Core;
using Seventy.Service.Core.Permissions;
using Seventy.Service.Core.RolePermissions;
using Seventy.Service.Core.Roles;
using Seventy.Service.Core.Roles.DefaultRoleAccess;
using Seventy.Service.Core.UserAccess;
using Seventy.Service.Core.UserRole;
using Seventy.Service.Users;
using Seventy.ViewModel.Core;
using Seventy.ViewModel.Core.Users;

namespace Seventy.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleDefaultAccessesController : Controller
    {
        private static IUserManager _UserManager;
        private static IRolePermissionsService _RolePermissionsService;
        private readonly IDefaultRoleAccessService _defaultRoleAccessService;
        private readonly IUserAccessService _userAccessService;
        private readonly IUserRoleService _userRoleService;
        private static IRolesService _RolesService;
        private readonly IAccessService _accessService;
        private static IPermissionsService _PermissionService;
        private static IMapper _mapper;

        public RoleDefaultAccessesController(IUserManager UserManager, IRolePermissionsService RolePermissionsService
            , IDefaultRoleAccessService defaultRoleAccessService, IUserAccessService userAccessService,
            IUserRoleService userRoleService, IRolesService RolesService,
            IAccessService accessService, IPermissionsService PermissionService, IMapper mapper)
        {
            _UserManager = UserManager;
            _RolePermissionsService = RolePermissionsService;
            _defaultRoleAccessService = defaultRoleAccessService;
            _userAccessService = userAccessService;
            _userRoleService = userRoleService;
            _mapper = mapper;
            _RolesService = RolesService;
            _accessService = accessService;
            _PermissionService = PermissionService;
        }

        [HttpGet]
        [Route("/Admin/RoleDefaultAccesses")]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            return View();
        }


        [HttpPost]
        [Route("/Admin/RoleDefaultAccesses")]
        public async Task<IActionResult> Index(int[] accessList, int roleId,
            CancellationToken cancellationToken)
        {
            try
            {
                if (accessList == null || accessList.Length == 0)
                {
                    throw new Exception("هیچ دسترسی انتخاب نشده است");
                }

                var role = await _RolesService.GetByIDAsync(cancellationToken, roleId);

                if (role == null)
                {
                    throw new Exception("نقش وجود ندارد");
                }

                // VALIDATION

                var foundCount = _accessService.Table()
                    .Count(r => r.ID.HasValue && accessList.Contains(r.ID.Value));
                if (foundCount != accessList.Length)
                {
                    throw new Exception("کد دسترسی های ارسالی اشتباه است");
                }

                List<DefaultRoleAccess> defaultRoleAccessList = new List<DefaultRoleAccess>();

                var userId = _UserManager.GetCurrentUserID();
                foreach (var accessId in accessList)
                {
                    defaultRoleAccessList.Add(new DefaultRoleAccess
                    {
                        AccessID = accessId,
                        RoleID = roleId,
                        RegUserID = userId,
                        RegDate = DateTime.Now,
                        IsActive = true,
                    });
                }

                var role_DefaultAcceses = _defaultRoleAccessService.TableNoTracking()
                    .Where(c => c.RoleID == roleId && c.IsActive==true).ToList();

                await _defaultRoleAccessService.DeleteRangeAsync(role_DefaultAcceses, cancellationToken);
                await _defaultRoleAccessService.InsertRangeAsync(defaultRoleAccessList, cancellationToken);

                return View();
            }
            catch (Exception e)
            {
                TempData["err"] = e.Message;
                return StatusCode(500, e.Message);
            }
        }

        #region call by ajax

        [HttpGet]
        [Route("/Admin/RoleDefaultAccesses/GetDefaultAccessByRoleId")]
        public async Task<IActionResult> GetDefaultAccessByRoleId(int roleId, CancellationToken cancellationToken)
        {
            return View("GetDefaultAccessByRoleId", roleId);
        }

        #endregion
    }
}