using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seventy.DomainClass.EDU;
using Seventy.DomainClass.EDU.Course;
using Seventy.Service.EDU.CourseCategory;
using Seventy.Service.Users;
using Seventy.ViewModel.EDU;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Seventy.Service.Core.Files;
using Seventy.Service.EDU.Main;
using Seventy.WebFramework.Filters;
using Seventy.Service.Core.UserRole;
using System.Security.Cryptography.X509Certificates;
using Seventy.Service.Core.Roles;

namespace Seventy.Web.Areas.Edu.Home
{
    [Area("Edu")]
    [Authorize(Policy = "user")]
    public class HomeController : Controller
    {
        private static IUserManager _um;
        private readonly IUserRoleService _userRoleService;
        private readonly IRolesService _rolesService;
        private static ICourseCategoryService _ccs;
        private static AutoMapper.IMapper _mapper;
        private readonly IMainService _mainService;
        private static IFilesService _filesService;
        private static int? _userId;

        public HomeController(ICourseCategoryService ccs, IRolesService rolesService,
            IUserRoleService userRoleService, IUserManager um, 
            IMapper mapper, IMainService mainService,
            IFilesService filesService)
        {
            _ccs = ccs;
            _um = um;
            _mapper = mapper;
            _mainService = mainService;
            _filesService = filesService;
            _userId = _um.GetCurrentUserID();
            _userRoleService = userRoleService;
            _rolesService = rolesService;
        }
        [Route("/Edu/getAllCourseCategory")]
        [UserAccess(Common.Enums.eAccessControl.HomeGetAllCourseCategory, Common.Enums.eAccessType.None, 1)]
        public async Task<string> GetAllCourseCategory(int pageNo, int pageSize = 20)
        {
            var all = await _ccs.GetAllPaginatedAsync(new Data.GenericPagingParameters()
            {
                PageNumber = pageNo,
                PageSize = pageSize
            });
            if (all == null) return "";
            var filter = all.Where(q => q.IsActive == true);
            var res = "";
            res += all.TotalPages + "_%_";
            foreach (var item in filter)
            {
                if (res.Length > 0)
                    res += "_$_";
                res += item.ID + "_|_";
                res += item.PrimaryCat + "_|_";
                res += item.SecondaryCat + "_|_";
                res += item.Description;
            }
            return res;
        }

        [Route("edu", Name = "edu")]
        [UserAccess(Common.Enums.eAccessControl.HomeIndex3, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var data = await _mainService
                .GetStudentHomeDataAsync(cancellationToken);

            var userRole = (await _userRoleService.GetAllPaginatedAsync(new Data.GenericPagingParameters
            { PageNumber = 1, PageSize = 10 }, x => x.UserID == _userId)).FirstOrDefault();

            ViewBag.UserRoleTitle = "";

            if (userRole != null)
            {

                ViewBag.UserRoleTitle = (await _rolesService.GetByIDAsync(cancellationToken, userRole.RoleID)).Title;

            }

            

            return View(data);
        }

        #region CourseCategory
        [HttpGet]
        [Route("/Edu/CourseCategory")]
        [UserAccess(Common.Enums.eAccessControl.HomeCourseCategory, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> CourseCategory(CancellationToken cancellationToken, int id = 0)
        {
            //edit
            if (id != 0)
            {
                ViewBag.ID = id;
                var curCouseCategory = await _ccs.GetByIDAsync(cancellationToken, id);
                var model = _mapper.Map<CourseCategoryViewModel>(curCouseCategory);
                return View(model);
            }
            else
            {
                ViewBag.ID = 0;
            }
            return View();
        }
        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/CourseCategory")]
        [UserAccess(Common.Enums.eAccessControl.HomeCourseCategory2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> CourseCategory(CourseCategory model, CancellationToken cancellationToken)
        {
            var model2 = _mapper.Map<CourseCategoryViewModel>(model);

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("PrimaryCat", "Model is not Valid");
                return View(model2);
            }
            var curUser = await _um.GetCurrentUserAsync(cancellationToken);
            if (model.ID == 0)
            {
                model.RegUserID = curUser.ID;
                var result = await _ccs.InsertAsync(model, cancellationToken);
                if (result != null)
                {
                    ViewBag.success = "با موفقیت ذخیره شد";
                    return View();
                }
                else
                {
                    ModelState.AddModelError("PrimaryCat", "در هنگام ذخیره ، خطایی رخ داده است");
                    return View(model2);
                }
            }
            else
            {
                var exist = await _ccs.GetByIDAsync(cancellationToken, model.ID);
                if (exist == null)
                {
                    ModelState.AddModelError("PrimaryCat", "ردیف انتخابی شما در پایگاه داده یافت نشد");
                    return View(model);
                }

                exist.Description = model.Description;
                exist.PrimaryCat = model.PrimaryCat;
                exist.SecondaryCat = model.SecondaryCat;
                exist.RegUserID = curUser.ID;
                var result = await _ccs.UpdateAsync(exist, cancellationToken);
                if (result != null)
                {
                    ViewBag.success = "با موفقیت به روز رسانی شد";
                    return View();
                }
                else
                {
                    ModelState.AddModelError("PrimaryCat", "در هنگام به روز رسانی ، خطایی رخ داده است");
                    return View(model2);
                }
            }
        }
        [HttpPost]
        [Route("/Edu/RemoveCourseCategory")]
        [UserAccess(Common.Enums.eAccessControl.HomeRemoveCourseCategory, Common.Enums.eAccessType.None, 1)]
        public async Task<string> RemoveCourseCategory(int entityId, CancellationToken cancellationToken)
        {
            var curEntity = await _ccs.GetByIDAsync(cancellationToken, entityId);
            if (curEntity == null)
                return "این دسته بندی حذف شده است";

            var curUser = await _um.GetCurrentUserAsync(cancellationToken);
            if (curEntity.RegUserID != curUser.ID)
                return "این موجودی توسط شما ساخته نشده است";

            curEntity.IsActive = false;
            var result = await _ccs.UpdateAsync(curEntity, cancellationToken);
            if (result != null)
                return "done";
            else
                return "حذف با مشکل مواجه شد";
        }
        [UserAccess(Common.Enums.eAccessControl.HomeCheckFile, Common.Enums.eAccessType.None, 1)]
        public static async Task<string?> CheckFile(CancellationToken cancellationToken, int? fileId)
        {
            if (fileId == null || _userId == null)
                return string.Empty;

            var file = await _filesService.CheckUserSignUpToContent((int)_userId, (int)fileId, cancellationToken);

            return file.File;
        }
        #endregion
    }
}