using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Seventy.Common.Enums;
using Seventy.DomainClass.EDU.TrainingWeek;
using Seventy.Service.Core.Files;
using Seventy.Service.EDU.Lesson;
using Seventy.Service.EDU.Term;
using Seventy.Service.EDU.TrainingWeek;
using Seventy.Service.EDU.TrainingWeekContent;
using Seventy.Service.EDU.UserTrainingWeekContent;
using Seventy.Service.Users;
using Seventy.ViewModel.EDU;
using Seventy.WebFramework.Filters;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Web.Areas.Edu.ClassContent
{
    [Area("EDU")]
    public class ClassContentController : Controller
    {
        private static IWebHostEnvironment _env;
        private readonly IUserManager _userManager;
        private readonly ILessonService _lessonService;
        private readonly ITermLessonService _termLessonService;
        private static IFilesService _filesService;
        private readonly ITrainingWeekService _trainingWeekService;
        private readonly IUserTrainingWeekContentService _userTrainingWeekContentService;
        private readonly ITrainingWeekContentService _TrainingWeekContentService;
        private static int? _userId;
        public ClassContentController(IWebHostEnvironment env, IUserManager userManager, ILessonService lessonService
            , ITrainingWeekService trainingWeekService, IUserTrainingWeekContentService userTrainingWeekContentService
            , IFilesService filesService, ITrainingWeekContentService TrainingWeekContentService
            , ITermLessonService termLessonService
            )
        {
            _env = env;
            _userManager = userManager;
            _lessonService = lessonService;
            _trainingWeekService = trainingWeekService;
            _userId = _userManager.GetCurrentUserID();
            _filesService = filesService;
            _TrainingWeekContentService = TrainingWeekContentService;
            _userTrainingWeekContentService = userTrainingWeekContentService;
            _termLessonService = termLessonService;
        }
        [Route("/Edu/ClassContent/Index")]
        [UserAccess(Common.Enums.eAccessControl.ShowClassContentView, Common.Enums.eAccessType.View, 1)]
        public async Task<IActionResult> Index(CancellationToken cancellation, int weekId = 0)
        {
            var trainingWeek = await _trainingWeekService.GetByIDAsync(cancellation, weekId);
            var termLesson = (await _termLessonService.GetByTermAndLesson(trainingWeek.TermID, trainingWeek.LessonID)).FirstOrDefault();
            var watchedContent = await _userTrainingWeekContentService.GetByUserIDAsync(_userId.Value);

            var classContentViewModel = new ClassContentViewModel()
            {
                UserID = _userId.Value,
                CourseID = termLesson.CourseID,
                CourseGroupID = termLesson.CourseGroupID,
                TermID = termLesson.TermID,
                LessonID = termLesson.LessonID,
                WeekID = weekId,
                WatchedContent = watchedContent
            };

            return View(classContentViewModel);
        }

        [HttpPost]
        [Route("/Edu/ClassContent/SaveUserProgress")]
        [UserAccess(Common.Enums.eAccessControl.ClassContentSaveUserProgress, Common.Enums.eAccessType.View, 1)]
        public async Task<string> SaveUserProgress(CancellationToken cancellationToken
            , int contentID, int lessonID, int weekID
            , int courseID, int courseGroupID)
        {
            var trainingWeek = await _trainingWeekService.GetByIDAsync(cancellationToken, weekID);


            var userTrainingWeekContent = new UserTrainingWeekContent
            {
                UserID = _userId.Value,
                Progress = 100,
                Result = true,
                ContentID = contentID,
                CourseGroupID = courseGroupID,
                CourseID = courseID,
                IsActive = true,
                LessonID = lessonID,
                TermID = trainingWeek.TermID,
                TrainingWeekID = weekID,
                RegUserID = _userId.Value
            };

            var result = await _userTrainingWeekContentService.InsertAsync(userTrainingWeekContent, cancellationToken);

            if (result != null)
                return "done";
            else
                return "ثبت با مشکل مواجه شد";
        }


        #region TrainingWeekContentList => محتوای آموزشی و ویدئو
        [Route("/Edu/ClassContent/TrainingWeekContentList")]
        [UserAccess(Common.Enums.eAccessControl.ClassContentTrainingWeekContentList, Common.Enums.eAccessType.View, 1)]

        public async Task<IActionResult> TrainingWeekContentList(CancellationToken cancellation, string ContentType = "HTML", int lessonid = 2)
        {
            var model = await _TrainingWeekContentService.GetByLessonIdAndTypeAsync(
                (ContentType == "HTML" ? ContentTypeEnum.Html : ContentType == "ویدیو" ? ContentTypeEnum.Video : ContentTypeEnum.Library),
                lessonid,
                cancellation);



            if (ContentType == "HTML")
            {
                return PartialView("TrainingWeekContentList", model);
            }
            else if (ContentType == "ویدیو")
            {
                return PartialView("TrainingWeekContentList2", model);
            }
            else //if(ContentType == "کتابخانه")
            {
                return PartialView("TrainingWeekContentList3", model);
            }
        }
        #endregion

        #region CheckFile  ==بارگزاری فایل
        [UserAccess(Common.Enums.eAccessControl.ClassContentCheckFile, Common.Enums.eAccessType.View, 1)]
        public static async Task<string> CheckFile(CancellationToken cancellationToken, int? fileId)
        {
            if (fileId == null || _userId == null)
                return string.Empty;

            var file = await _filesService.CheckUserSignUpToContent((int)_userId, (int)fileId, cancellationToken);

            return file.File;
        }

        [UserAccess(Common.Enums.eAccessControl.ClassContentGetFileContent, Common.Enums.eAccessType.View, 1)]
        public static async Task<string> GetFileContent(CancellationToken cancellationToken, string filePath)
        {
            try
            {
                var fileContent = await System.IO.File.ReadAllTextAsync(_env.WebRootPath + filePath.Replace("/", @"\"), cancellationToken);
                return fileContent;
            }
            catch (System.Exception ex)
            {
                return "";
            }
        }
        #endregion

    }
}
