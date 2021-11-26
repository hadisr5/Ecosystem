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
using Seventy.Service.Users;
using Seventy.ViewModel.Core.Users;

namespace Seventy.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserRolesController : Controller
    {
        private static IUserManager _UserManager;
        private static IRolePermissionsService _RolePermissionsService;
        private static IRolesService _RolesService;
        private static IPermissionsService _PermissionService;
        private static IMapper _mapper;
        public UserRolesController(IUserManager UserManager, IRolePermissionsService RolePermissionsService
            , IRolesService RolesService, IPermissionsService PermissionService, IMapper mapper)
        {
            _UserManager = UserManager;
            _RolePermissionsService = RolePermissionsService;
            _mapper = mapper;
            _RolesService = RolesService;
            _PermissionService = PermissionService;
        }
        [HttpGet]
        [Route("/Admin/UserRolesController/Index")]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, int ID = 0)
        {
            if (ID != 0)
            {
                var model = await _RolePermissionsService.GetByIDAsync(cancellationToken, ID);
                return View(_mapper.Map<RolePermissionsViewModel>(model));
            }
            ViewBag.AllRole = _RolesService.TableNoTracking();
            return View();
        }
        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Admin/UserRolesController/Index")]
        public async Task<IActionResult> Index(RolePermissions model, string[] Checked, Microsoft.AspNetCore.Http.IFormCollection _formCollection, CancellationToken cancellationToken)
        {
            ViewBag.AllRole = _RolesService.Table();

            if (!ModelState.IsValid)
            {
                TempData["err"] = "model is not valid";
                return View(_mapper.Map<RolePermissionsViewModel>(model));
            }
            model.RegUserID = _UserManager.GetCurrentUserID();

            List<RolePermissions> _ActiveRolePerm = new List<RolePermissions>();
            foreach (var item in _formCollection.Keys)
            {
                if (item.StartsWith("Perm_"))
                {
                    var PermValue = _formCollection[item];
                    var PermID = Convert.ToInt32(item.Replace("Perm_", ""));
                    var _RolePermissions = new RolePermissions()
                    {
                        RoleID = model.RoleID,
                        PermissionID = PermID,
                        RegUserID = _UserManager.GetCurrentUserID(),
                    };
                    if (PermValue == "on")
                        _ActiveRolePerm.Add(_RolePermissions);
                }
            }
            var MyActivePerms = _RolePermissionsService.Table().Where(q => q.RoleID == model.RoleID);
            if (MyActivePerms.Count() > 0)
                await _RolePermissionsService.DeleteRangeAsync(MyActivePerms.ToList(), cancellationToken);

            var Insert = await _RolePermissionsService.InsertRangeAsync(_ActiveRolePerm, cancellationToken);
            if (Insert)
            {
                TempData["success"] = "با موفقیت ثبت شد";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";
                return View(_mapper.Map<RolePermissionsViewModel>(model));
            }
        }
        [Route("/Admin/UserRolesController/List")]
        public IActionResult List(int RoleId)
        {
            var MyActivePerms = _RolePermissionsService.TableNoTracking().Where(q => q.RoleID == RoleId);
            var model = MyActivePerms.Select(q => q.PermissionID).ToList();
            var AllPerms = _PermissionService.TableNoTracking();
            var Groups = AllPerms.GroupBy(q => q.Section);
            ViewBag.AllPermissions = AllPerms;
            ViewBag.MyActivePerms = model;
            return PartialView("List");
        }
    }
}
