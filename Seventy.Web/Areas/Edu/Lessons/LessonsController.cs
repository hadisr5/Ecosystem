using AutoMapper;
using System.Linq;
using Seventy.Data;
using System.Threading;
using Seventy.Service.Users;
using Seventy.ViewModel.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Seventy.Service.Core.Files;
using Seventy.Service.EDU.Lesson;
using Seventy.ViewModel.EDU.Lesson;
using Seventy.Service.EDU.Course;
using Seventy.WebFramework.Filters;
using Seventy.Common.Enums;

// ReSharper disable Mvc.ViewNotResolved
// ReSharper disable Mvc.PartialViewNotResolved

namespace Seventy.Web.Areas.Edu.Lessons
{
    [Area("Edu")]
    public class LessonsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;
        private static IFilesService _filesService;
        private readonly ILessonService _lessonService;
        private readonly ICourseService _courseService;

        private static int? _userId;

        public LessonsController(ILessonService lessonService, IMapper mapper, IUserManager userManager,
            IFilesService filesService, ICourseService courseService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _lessonService = lessonService;
            _filesService = filesService;
            _courseService = courseService;
            _userId = _userManager.GetCurrentUserID();
        }


        #region /Edu/Lessons/Index == درس ها
        [HttpGet]
        [Route("/Edu/Lessons/Index")]
        [UserAccess(eAccessControl.LessonsIndex, eAccessType.None, 1)]
        [Menu(eMenu.LessonsManagement, eModule.OnlineTraining, 5)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, int id = 0)
        {
            if (id == 0)
                return View();

            var model = await _lessonService.GetByIDAsync(cancellationToken, id);

            return View(_mapper.Map<LessonEditViewModel>(model));
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/Lessons/Index")]
        [UserAccess(Common.Enums.eAccessControl.LessonsIndex2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, DomainClass.EDU.Lesson.Lesson model, IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                TempData["err"] = "model is not valid";

                return View(_mapper.Map<LessonEditViewModel>(model));
            }

            model.RegUserID = _userManager.GetCurrentUserID();

            if (file != null)
            {
                if (file.FileName.Count() >= 40)
                {
                    TempData["err"] = "طول نام فایل انتخابی طولانی است.";
                    return RedirectToAction("Index");
                }
                var fileResult = await _filesService.UploadFileAsync(new FilesViewModel
                {
                    UploadFile = file,
                    Title = file.FileName,
                    RegUserID = model.RegUserID
                }, cancellationToken);

                model.PicFileID = fileResult.ResultID;
            }

            if (model.ID == 0 || model.ID == null)
            {
                model.ID = null;

                var insert = await _lessonService.InsertAsync(model, cancellationToken);

                if (insert != null)
                {
                    TempData["success"] = "با موفقیت افزوده شد";

                    return RedirectToAction("Index");
                }

                TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

                return View(_mapper.Map<LessonEditViewModel>(model));
            }

            var update = await _lessonService.UpdateAsync(model, cancellationToken);

            if (update != null)
            {
                TempData["success"] = "با موفقیت بروز رسانی شد";

                return RedirectToAction("Index");
            }

            TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

            return View(_mapper.Map<LessonEditViewModel>(model));
        }

        [Route("/Edu/Lessons/List")]
        [UserAccess(Common.Enums.eAccessControl.LessonsList, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> List(int page)
        {
            var model = await _lessonService
                .GetAllPaginatedAsync(new GenericPagingParameters
                {
                    PageNumber = page,
                    PageSize = 10
                }, q => q.IsActive,
                   a => a.OrderByDescending(b => b.ID));

            return PartialView("List", model);
        }

        [Route("/Edu/Lessons/Remove")]
        [UserAccess(Common.Enums.eAccessControl.LessonsDelete, Common.Enums.eAccessType.None, 1)]
        public async Task<string> Delete(CancellationToken cancellationToken, int id)
        {
            var entity = await _lessonService.GetByIDAsync(cancellationToken, id);

            if (entity == null)
                return "آیتم مورد نظر یافت نشد";

            if (await _lessonService.DeleteAsync(entity, cancellationToken))
                return "done";

            return "خطا در زمان حذف داده";
        }
        #endregion

        #region CheckFile  ==بارگزاری فایل
        [UserAccess(Common.Enums.eAccessControl.LessonsCheckFile, Common.Enums.eAccessType.None, 1)]
        public static async Task<string> CheckFile(CancellationToken cancellationToken, int? fileId)
        {
            if (fileId == null || _userId == null)
                return string.Empty;

            var file = await _filesService.CheckUserSignUpToContent((int)_userId, (int)fileId, cancellationToken);

            return file.File;
        }
        #endregion

        #region /Edu/Lessons/ListOfTrainingCourses == لیست دوره های آموزشی

        //ثبت دوره آموزشی
        [HttpGet]
        [Route("/Edu/Lessons/ListOfTrainingCourses")]
        [UserAccess(Common.Enums.eAccessControl.LessonsListOfTrainingCourse, Common.Enums.eAccessType.None, 1)]
        public IActionResult ListOfTrainingCourses(CancellationToken cancellationToken)
        {
            return View();
        }

        [Route("/Edu/Lessons/ListTrainingCourse")]
        [UserAccess(Common.Enums.eAccessControl.LessonsListTrainingCourse, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> ListTrainingCourse(int page)
        {
            var model = await _courseService
                .GetCustomCourseAsync(Common.Enums.CourseEnum.Long, new GenericPagingParameters
                {
                    PageNumber = page,
                    PageSize = 10
                }, null,
                   a => a.OrderByDescending(b => b.ID));

            //var model = await _courseService
            //.GetAllPaginatedAsync(new GenericPagingParameters
            //{
            //    PageNumber = page,
            //    PageSize = 10
            //}, null,
            //   a => a.OrderByDescending(b => b.ID));

            return PartialView("ListTrainingCourse", model);
        }

        #endregion

    }
}