using Seventy.Data;
using System.Threading;
using Seventy.Service.Users;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Seventy.DomainClass.Core;
using Seventy.Service.EDU.Lesson;
using System.Collections.Generic;
using Seventy.Service.Core.UserProfiles;
using Seventy.Service.EDU.TeacherLesson;
using Seventy.WebFramework.Filters;
using Seventy.Common.Enums;

// ReSharper disable Mvc.ViewNotResolved
// ReSharper disable Mvc.PartialViewNotResolved

namespace Seventy.Web.Areas.Edu.Teachers
{
    [Area("Edu")]
    public class TeachersController : Controller
    {
        private readonly IUserManager _userManager;
        private static ILessonService _lessonService;
        private static IUserProfilesService _userProfilesService;
        private readonly ITeacherLessonService _teacherLessonService;

        public TeachersController(ITeacherLessonService teacherLessonService, IUserManager userManager,
            ILessonService lessonService, IUserProfilesService userProfilesService)
        {
            _userManager = userManager;
            _lessonService = lessonService;
            _userProfilesService = userProfilesService;
            _teacherLessonService = teacherLessonService;
        }

        [HttpGet]
        [Route("/Edu/Teachers/Index")]
        [UserAccess(Common.Enums.eAccessControl.TeachersIndex , Common.Enums.eAccessType.None, 1)]
        [Menu(eMenu.LessonManagement, eModule.OnlineTraining, 2)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, int id = 0)
        {
            var Teachers = await _userManager.GetByRole(Service.Core.Users.RoleSelect.Teacher);
            ViewBag.Teachers = Teachers ?? new List<ViewModel.Core.Users.UserListViewModel>();
            if (id == 0)
            {
                return View();
            }
            var model = await _teacherLessonService.GetByIDAsync(cancellationToken, id);
            return View(model);
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/Teachers/Index")]
        [UserAccess(Common.Enums.eAccessControl.TeachersIndex2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, DomainClass.EDU.Teacher.TeacherLesson model)
        {
            if (!ModelState.IsValid || model.LessonID == 0 || model.TeacherID == 0)
            {
                TempData["err"] = "model is not valid";

                return View(model);
            }

            model.RegUserID = _userManager.GetCurrentUserID();

            if (model.ID == 0 || model.ID == null)
            {
                model.ID = null;

                var insert = await _teacherLessonService.InsertAsync(model, cancellationToken);

                if (insert != null)
                {
                    TempData["success"] = "با موفقیت افزوده شد";

                    return RedirectToAction("Index");
                }

                TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

                return View(model);
            }

            var update = await _teacherLessonService.UpdateAsync(model, cancellationToken);

            if (update != null)
            {
                TempData["success"] = "با موفقیت بروز رسانی شد";

                return RedirectToAction("Index");
            }

            TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

            return View(model);
        }

        [Route("/Edu/Teachers/List")]
        [UserAccess(Common.Enums.eAccessControl.TeachersList, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> List(int page)
        {
            var model = await _teacherLessonService
                .GetAllPaginatedAsync(new GenericPagingParameters
                {
                    PageNumber = page,
                    PageSize = 10
                }, q => q.IsActive);

            return PartialView("List", model);
        }

        [Route("/Edu/Teachers/Remove")]
        [UserAccess(Common.Enums.eAccessControl.TeachersDelete, Common.Enums.eAccessType.None, 1)]
        public async Task<string> Delete(CancellationToken cancellationToken, int id)
        {
            var entity = await _teacherLessonService.GetByIDAsync(cancellationToken, id);

            if (entity == null)
                return "آیتم مورد نظر یافت نشد";

            if (await _teacherLessonService.DeleteAsync(entity, cancellationToken))
                return "done";

            return "خطا در زمان حذف داده";
        }
        [UserAccess(Common.Enums.eAccessControl.TeachersGetAllLesson, Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<DomainClass.EDU.Lesson.Lesson> GetAllLesson()
        {
            return _lessonService.TableNoTracking();
        }
        [UserAccess(Common.Enums.eAccessControl.TeachersGetAllUser, Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<UserProfiles> GetAllUser()
        {
            return _userProfilesService.TableNoTracking();
        }
    }
}