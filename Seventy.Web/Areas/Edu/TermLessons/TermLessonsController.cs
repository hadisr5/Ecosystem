using AutoMapper;
using Seventy.Data;
using System.Threading;
using Seventy.Service.Users;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Seventy.Service.EDU.Term;
using Seventy.DomainClass.Core;
using System.Collections.Generic;
using Seventy.Service.EDU.Course;
using Seventy.Service.EDU.Lesson;
using Seventy.DomainClass.EDU.Term;
using Seventy.DomainClass.EDU.Course;
using Seventy.Service.EDU.CourseGroup;
using Seventy.ViewModel.EDU.TermLesson;
using Seventy.Service.Core.UserProfiles;
using Seventy.WebFramework.Filters;
using Seventy.Common.Enums;

// ReSharper disable Mvc.ViewNotResolved
// ReSharper disable Mvc.PartialViewNotResolved

namespace Seventy.Web.Areas.Edu.TermLessons
{
    [Area("Edu")]
    public class TermLessonsController : Controller
    {
        private readonly IMapper _mapper;
        private static ITermService _termService;
        private readonly IUserManager _userManager;
        private static ILessonService _lessonService;
        private static ICourseService _courseService;
        private static ITermLessonService _termLessonService;
        private static IUserProfilesService _userProfilesService;
        private static ICourseGroupsService _courseGroupsService;

        public TermLessonsController(IUserManager userManager, ITermLessonService termLessonService,
            ITermService termService, ICourseService courseService, ICourseGroupsService courseGroupsService,
            ILessonService lessonService, IUserProfilesService userProfilesService, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _termLessonService = termLessonService;
            _termService = termService;
            _lessonService = lessonService;
            _courseService = courseService;
            _userProfilesService = userProfilesService;
            _courseGroupsService = courseGroupsService;
        }

        [HttpGet]
        [Route("/Edu/TermLessons/Index")]
        [UserAccess(Common.Enums.eAccessControl.TermLessonsIndex, Common.Enums.eAccessType.None, 1)]
        [Menu(eMenu.AllocateLessonToTerm, eModule.OnlineTraining, 3)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, int id = 0)
        {
            if (id == 0)
                return View();

            var model = await _termLessonService.GetByIDAsync(cancellationToken, id);

            return View(_mapper.Map<TermLessonEditModel>(model));
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/TermLessons/Index")]
        [UserAccess(Common.Enums.eAccessControl.TermLessonsIndex2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, TermLesson model)
        {
            if (!ModelState.IsValid || model.LessonID == 0
                || model.CourseGroupID == 0 || model.CourseID == 0 || model.TermID == 0)
            {
                TempData["err"] = "model is not valid";

                return View(model);
            }

            model.RegUserID = _userManager.GetCurrentUserID();

            if (model.ID == 0 || model.ID == null)
            {
                model.ID = null;

                var insert = await _termLessonService.InsertAsync(model, cancellationToken);

                if (insert != null)
                {
                    TempData["success"] = "با موفقیت افزوده شد";

                    return RedirectToAction("Index");
                }

                TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

                return View(model);
            }

            var update = await _termLessonService.UpdateAsync(model, cancellationToken);

            if (update != null)
            {
                TempData["success"] = "با موفقیت بروز رسانی شد";

                return RedirectToAction("Index");
            }

            TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

            return View(model);
        }

        [Route("/Edu/TermLessons/List")]
        [UserAccess(Common.Enums.eAccessControl.TermLessonsList, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> List(int page)
        {
            var model = await _termLessonService
                .GetAllPaginatedAsync(new GenericPagingParameters
                {
                    PageNumber = page,
                    PageSize = 10
                }, q => q.IsActive);

            return PartialView("List", model);
        }

        [Route("/Edu/TermLessons/Remove")]
        [UserAccess(Common.Enums.eAccessControl.TermLessonsDelete, Common.Enums.eAccessType.None, 1)]
        public async Task<string> Delete(CancellationToken cancellationToken, int id)
        {
            var entity = await _termLessonService.GetByIDAsync(cancellationToken, id);

            if (entity == null)
                return "آیتم مورد نظر یافت نشد";

            if (await _termLessonService.DeleteAsync(entity, cancellationToken))
                return "done";

            return "خطا در زمان حذف داده";
        }
        [UserAccess(Common.Enums.eAccessControl.TermLessonsGetAllTerm, Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<Term> GetAllTerm()
        {
            return _termService.TableNoTracking();
        }
        [UserAccess(Common.Enums.eAccessControl.TermLessonsGetAllLessoon, Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<DomainClass.EDU.Lesson.Lesson> GetAllLesson()
        {
            return _lessonService.TableNoTracking();
        }
        [UserAccess(Common.Enums.eAccessControl.TermLessonsGetAllCourse, Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<DomainClass.EDU.Course.Course> GetAllCourse()
        {
            return _courseService.TableNoTracking();
        }
        [UserAccess(Common.Enums.eAccessControl.TermLessonsGetAllCourseType, Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<CourseGroups> GetAllCourseGroup()
        {
            return _courseGroupsService.TableNoTracking();
        }
        [UserAccess(Common.Enums.eAccessControl.TermLessonsGetAllTeacher, Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<UserProfiles> GetAllTeacher()
        {
            return _userProfilesService.TableNoTracking();
        }
    }
}