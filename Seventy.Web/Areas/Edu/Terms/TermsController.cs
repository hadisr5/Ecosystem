using AutoMapper;
using Seventy.Data;
using System.Threading;
using Seventy.ViewModel.EDU;
using Seventy.Service.Users;
using System.Threading.Tasks;
using Seventy.Service.EDU.Term;
using Microsoft.AspNetCore.Mvc;
using Seventy.Service.EDU.Course;
using System.Collections.Generic;
using Seventy.DomainClass.EDU.Term;
using Seventy.DomainClass.EDU.Course;
using Seventy.Service.EDU.CourseGroup;
using Seventy.WebFramework.Filters;
using Seventy.Common.Enums;

// ReSharper disable Mvc.ViewNotResolved
// ReSharper disable Mvc.PartialViewNotResolved

namespace Seventy.Web.Areas.Edu.Terms
{
    [Area("Edu")]
    public class TermsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;
        private readonly ITermService _termService;
        private static ICourseService _courseService;
        private static ICourseGroupsService _courseGroupsService;

        public TermsController(IUserManager userManager, ITermService termService, ICourseService courseService,
            ICourseGroupsService courseGroupsService, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _termService = termService;
            _courseService = courseService;
            _courseGroupsService = courseGroupsService;
        }

        [HttpGet]
        [Route("/Edu/Terms/Index")]
        [UserAccess(Common.Enums.eAccessControl.TermsIndex , Common.Enums.eAccessType.None, 1)]
        [Menu(eMenu.TermManagement, eModule.OnlineTraining, 4)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, int id = 0)
        {
            if (id == 0)
                return View();

            var model = await _termService.GetByIDAsync(cancellationToken, id);

            return View(_mapper.Map<TermEditModel>(model));
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/Terms/Index")]
        [UserAccess(Common.Enums.eAccessControl.TermsIndex2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, Term model)
        {
            if (!ModelState.IsValid)
            {
                TempData["err"] = "model is not valid";

                return View(model);
            }

            model.RegUserID = _userManager.GetCurrentUserID();

            if (model.ID == 0 || model.ID == null)
            {
                model.ID = null;

                var insert = await _termService.InsertAsync(model, cancellationToken);

                if (insert != null)
                {
                    TempData["success"] = "با موفقیت افزوده شد";

                    return RedirectToAction("Index");
                }

                TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

                return View(model);
            }

            var update = await _termService.UpdateAsync(model, cancellationToken);

            if (update != null)
            {
                TempData["success"] = "با موفقیت بروز رسانی شد";

                return RedirectToAction("Index");
            }

            TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

            return View(model);
        }

        [Route("/Edu/Terms/List")]
        [UserAccess(Common.Enums.eAccessControl.TermsList, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> List(int page)
        {
            var model = await _termService
                .GetAllPaginatedAsync(new GenericPagingParameters
                {
                    PageNumber = page,
                    PageSize = 10
                }, q => q.IsActive);

            return PartialView("List", model);
        }

        [Route("/Edu/Terms/Remove")]
        [UserAccess(Common.Enums.eAccessControl.TermsDelete, Common.Enums.eAccessType.None, 1)]
        public async Task<string> Delete(CancellationToken cancellationToken, int id)
        {
            var entity = await _termService.GetByIDAsync(cancellationToken, id);

            if (entity == null)
                return "آیتم مورد نظر یافت نشد";

            if (await _termService.DeleteAsync(entity, cancellationToken))
                return "done";

            return "خطا در زمان حذف داده";
        }
        [UserAccess(Common.Enums.eAccessControl.TermsGetAllCourse, Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<DomainClass.EDU.Course.Course> GetAllCourse()
        {
            return _courseService.TableNoTracking();
        }
        [UserAccess(Common.Enums.eAccessControl.TermsGetAllCourseGroup, Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<CourseGroups> GetAllCourseGroup()
        {
            return _courseGroupsService.TableNoTracking();
        }
    }
}