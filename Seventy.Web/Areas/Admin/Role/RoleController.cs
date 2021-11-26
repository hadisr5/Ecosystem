using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Seventy.DomainClass.Core;
using Seventy.Service.Core.Roles;
using Seventy.Service.Core.UserRole;
using Seventy.Service.Users;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly IUserRoleService _userRoleService;
        private static IUserManager _UserManager;
        private static IRolesService _RoleService;
        private static IMapper _mapper;
        public RoleController(IUserRoleService userRoleService, IUserManager UserManager, IRolesService RoleService, IMapper mapper)
        {
            _userRoleService = userRoleService;
            _UserManager = UserManager;
            _RoleService = RoleService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("/Admin/Role/RoleManagement")]
        [UserAccess(Common.Enums.eAccessControl.ShowRoleManagement, Common.Enums.eAccessType.View, 1)]
        public async Task<IActionResult> RoleManagement(CancellationToken cancellationToken, int ID = 0)
        {
            if (ID != 0)
            {
                var model = await _RoleService.GetByIDAsync(cancellationToken, ID);
                return View(_mapper.Map<RolesViewModel>(model));
            }
            return View();
        }
        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Admin/Role/RoleManagement")]
        [UserAccess(Common.Enums.eAccessControl.AddOrEditRoles, Common.Enums.eAccessType.None, 2)]
        public async Task<IActionResult> RoleManagement(Roles model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                TempData["err"] = "model is not valid";
                return View(_mapper.Map<RolesViewModel>(model));
            }
            model.RegUserID = _UserManager.GetCurrentUserID();
            if (model.ID == 0 || model.ID == null)
            {
                var Insert = await _RoleService.InsertAsync(model, cancellationToken);
                if (Insert != null)
                {
                    TempData["success"] = "با موفقیت افزوده شد";
                    return RedirectToAction("RoleManagement");
                }
                else
                {
                    TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";
                    return View(_mapper.Map<RolesViewModel>(model));
                }
            }
            else
            {
                var update = await _RoleService.UpdateAsync(model, cancellationToken);
                if (update != null)
                {
                    TempData["success"] = "با موفقیت به روز رسانی شد";
                    return RedirectToAction("RoleManagement");
                }
                else
                {
                    TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";
                    return View(_mapper.Map<RolesViewModel>(model));
                }
            }
        }
        [Route("/Admin/Role/RoleManagementList")]
        [UserAccess(Common.Enums.eAccessControl.GetRoleManagementList, Common.Enums.eAccessType.View, 3)]
        public async Task<IActionResult> RoleManagementList(int Page, CancellationToken cancellationToken)
        {
            var model = await _RoleService.GetAllPaginatedAsync(new Data.GenericPagingParameters()
            {
                PageNumber = Page,
                PageSize = 10,
            }, q => q.IsActive == true);

            ViewBag.TotalPage = model.TotalPages;
            return PartialView("RoleManagementList", _mapper.Map<List<RolesViewModel>>(model.ToList()));
        }



        /// <summary>
        /// این در صفحه مدریت دسترسی کاربران
        /// اسفتاده شده
        /// </summary>
        /// <param name="Page"></param>
        /// <returns></returns>
        [Route("/Admin/Role/RoleManagementUserId")]
        [UserAccess(Common.Enums.eAccessControl.GetRoleManagementList2, Common.Enums.eAccessType.View, 4)]
        public async Task<IActionResult> RoleManagementList(int Page, int userId, CancellationToken cancellationToken)
        {
            var model = await _RoleService.GetAllPaginatedAsync(new Data.GenericPagingParameters()
            {
                PageNumber = Page,
                PageSize = 10,
            }, q => q.IsActive == true);

            ViewBag.UserId = userId;
            ViewBag.TotalPage = model.TotalPages;
            return PartialView("RoleManagementListByUserId", _mapper.Map<List<RolesViewModel>>(model.ToList()));
        }

        [Route("/Admin/Role/Remove")]
        [UserAccess(Common.Enums.eAccessControl.RemoveRole, Common.Enums.eAccessType.None, 5)]
        public async Task<string> Remove(int ID, CancellationToken cancellationToken)
        {
            var Entity = await _RoleService.GetByIDAsync(cancellationToken, ID);
            if (Entity == null)
                return "این نقش یافت نشد";

            if (await _RoleService.DeleteAsync(Entity, cancellationToken))
                return "done";
            else
                return "خطا در زمان حذف داده...";
        }
    }
}
