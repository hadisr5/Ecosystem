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
using Seventy.Service.Core.UserAccess;
using Seventy.Service.Core.UserRole;
using Seventy.Service.Users;
using Seventy.ViewModel.Core;
using Seventy.ViewModel.Core.Users;

namespace Seventy.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolePermissionsController : Controller
    {
        private static IUserManager _UserManager;
        private static IRolePermissionsService _RolePermissionsService;
        private readonly IUserAccessService _userAccessService;
        private readonly IUserRoleService _userRoleService;
        private static IRolesService _RolesService;
        private readonly IAccessService _accessService;
        private static IPermissionsService _PermissionService;
        private static IMapper _mapper;

        public RolePermissionsController(IUserManager UserManager, IRolePermissionsService RolePermissionsService
            , IUserAccessService userAccessService, IUserRoleService userRoleService, IRolesService RolesService,
            IAccessService accessService, IPermissionsService PermissionService, IMapper mapper)
        {
            _UserManager = UserManager;
            _RolePermissionsService = RolePermissionsService;
            _userAccessService = userAccessService;
            _userRoleService = userRoleService;
            _mapper = mapper;
            _RolesService = RolesService;
            _accessService = accessService;
            _PermissionService = PermissionService;
        }

        [HttpGet]
        [Route("/Admin/RolePermissions/RolePermissionsManagement")]
        public async Task<IActionResult> RolePermissionsManagement(CancellationToken cancellationToken, int ID = 0)
        {
            if (ID != 0)
            {
                var model = await _RolePermissionsService.GetByIDAsync(cancellationToken, ID);
                return View(_mapper.Map<RolePermissionsViewModel>(model));
            }

            ViewBag.AllRole = _RolesService.TableNoTracking();
            return View();
        }
        
        


        [HttpPost]
        [Route("/Admin/RolePermissions/RolePermissionsManagement")]
        public async Task<IActionResult> RolePermissionsManagement(int[] roles, int[] accessList, int type, int userId,
            CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["err"] = "فرم صحیح ارسال نشده است";
                    return View();
                }

                int? currentUserId = _UserManager.GetCurrentUserID();

                if (currentUserId.HasValue == false)
                {
                    throw new Exception("کاربر وارد نشده است");
                }

                var user = _UserManager.GetByID(userId);
                if (user == null)
                {
                    throw new Exception("کاربر یافت نشد");
                }

                List<UserRole> userRoleList = new List<UserRole>();
                List<UserAccess> userAccessList = new List<UserAccess>();

                if (type == 1) //Role
                {
                    if (roles == null || roles.Length == 0)
                    {
                        throw new Exception("هیچ نقشی انتخاب نشده است");
                    }

                    // VALIDATION
                    var foundCount = _RolesService.Table()
                        .Count(r => r.ID.HasValue && roles.Contains(r.ID.Value));
                    if (foundCount!=roles.Length)
                    {
                        throw new Exception("کد نقش های ارسالی اشتباه است");
                    }


                    foreach (var roleId in roles)
                    {
                        var rp = new UserRole
                        {
                            RoleID = roleId,
                            RegUserID = currentUserId,
                            UserID = userId,
                            IsActive = true,
                            RegDate = DateTime.Now,
                        };
                        userRoleList.Add(rp);
                    }
                }
                else if (type == 2) //access
                {
                    // VALIDATION
                    
                    var foundCount = _accessService.Table()
                        .Count(r => r.ID.HasValue && accessList.Contains(r.ID.Value));
                    if (foundCount!=accessList.Length)
                    {
                        throw new Exception("کد دسترسی های ارسالی اشتباه است");
                    }

                    foreach (var permissionId in accessList)
                    {
                        var rp = new UserAccess
                        {
                            AccessID = permissionId,
                            UserID = userId,
                            RegUserID = currentUserId,
                            IsActive = true,
                            RegDate = DateTime.Now,
                        };
                        userAccessList.Add(rp);
                    }
                }
                else
                {
                    throw new Exception("نوع شناسایی نشد");
                }


                #region Insert

                if (type == 1) //Role
                {
                    var userRoles = _userRoleService.Table().Where(r => r.UserID == userId).ToList();
                    await _userRoleService.DeleteRangeAsync(userRoles, cancellationToken);

                    await _userRoleService.InsertRangeAsync(userRoleList, cancellationToken);
                }
                else
                {
                    //access
                    var userAccesses = _userAccessService.Table().Where(r => r.UserID == userId).ToList();
                    await _userAccessService.DeleteRangeAsync(userAccesses, cancellationToken);

                    await _userAccessService.InsertRangeAsync(userAccessList, cancellationToken);
                }

                #endregion

                return View();
            }
            catch (Exception e)
            {
                TempData["err"] = e.Message;
                return StatusCode(500,e.Message);
            }
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Admin/RolePermissions/RolePermissionsManagementBackup")]
        public async Task<IActionResult> RolePermissionsManagementBackup(RolePermissions model, string[] Checked,
            Microsoft.AspNetCore.Http.IFormCollection _formCollection, CancellationToken cancellationToken)
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
                return RedirectToAction("RolePermissionsManagement");
            }
            else
            {
                TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";
                return View(_mapper.Map<RolePermissionsViewModel>(model));
            }
        }

        [Route("/Admin/RolePermissions/RolePermissionsManagementList")]
        public IActionResult RolePermissionsManagementList(int RoleId)
        {
            var MyActivePerms = _RolePermissionsService.TableNoTracking().Where(q => q.RoleID == RoleId);
            var model = MyActivePerms.Select(q => q.PermissionID).ToList();
            var AllPerms = _PermissionService.TableNoTracking();
            var Groups = AllPerms.GroupBy(q => q.Section);
            ViewBag.AllPermissions = AllPerms;
            ViewBag.MyActivePerms = model;
            return PartialView("RolePermissionsManagementList");
        }


        #region Called by Ajax

        
        /// <summary>
        /// این در صفحه مدریت دسترسی کاربران
        /// اسفتاده شده
        /// </summary>
        /// <param name="Page"></param>
        /// <returns></returns>
        [Route("/Admin/RolePermissions/AccessListWithUserAccesses")]
        public async Task<IActionResult> AccessListWithUserAccesses(int Page,int userId)
        {
            try
            {
                var model = await _accessService.GetAllPaginatedAsync(new Data.GenericPagingParameters()
                {
                    PageNumber = Page,
                    PageSize = 10,
                }, q => q.IsActive == true);

                ViewBag.UserId=userId;
                ViewBag.TotalPage = model.TotalPages;
                return PartialView("AccessListWithUserAccesses", _mapper.Map<System.Collections.Generic.List<AccessViewModel>>(model.ToList()));
                //return PartialView("AccessListWithUserAccesses", model.ToList());

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return View();
        }

        #endregion
    }
    
    
    
    
}