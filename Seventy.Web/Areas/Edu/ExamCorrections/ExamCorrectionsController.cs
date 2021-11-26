using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading;
using Seventy.Service.Users;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Seventy.Service.EDU.Exam;
using Seventy.Service.EDU.ExamAnswerSheet;
using Seventy.Service.EDU.ExamQuestions;
using Seventy.Service.EDU.ExamUser;
using Seventy.Service.EDU.Lesson;
using Seventy.Service.EDU.Questions;
using Seventy.ViewModel.EDU;
using Seventy.ViewModel.EDU.Exam.ExamAnswerSheet;
using Seventy.WebFramework.Filters;

// ReSharper disable Mvc.ViewNotResolved
// ReSharper disable Mvc.PartialViewNotResolved

namespace Seventy.Web.Areas.Edu.ExamCorrections
{
    [Area("Edu")]
    public class ExamCorrectionsController : Controller
    {
        private static IUserManager _UserManager;
        private static IMapper _mapper;
        private static IExamService _ExamService;
        private static ILessonService _lessonService;
        private static IExamQuestionsService _ExamQuestions;
        private static IQuestionsService _qs;
        private static IExamUserService _ExamUserService;
        private static IExamAnswerSheetService _ExamAnswerSheetService;

        public ExamCorrectionsController(/*IExamAnswerSheetService ExamAnswerSheet*/
            IUserManager UserManager, IMapper mapper, IExamService ExamService
            , IExamQuestionsService ExamQuestions, IQuestionsService qs, IExamUserService ExamUserService,
            IExamAnswerSheetService examAnswerSheetService, ILessonService lessonService)
        {
            _ExamAnswerSheetService = examAnswerSheetService;
            _UserManager = UserManager;
            _mapper = mapper;
            _ExamService = ExamService;
            _ExamQuestions = ExamQuestions;
            _qs = qs;
            _ExamUserService = ExamUserService;
            _lessonService = lessonService;
        }
        [HttpGet]
        [Route("/Edu/ExamCorrections/Index")]
        [UserAccess(Common.Enums.eAccessControl.ExamCorrectionsIndex , Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, int examId, int userId)
        {
            var exam = await _ExamService.GetByIDAsync(cancellationToken, examId);

            if (exam == null)
            {
                TempData["Message"] = "امتحان مورد نظر یافت نشد";
                return View(new List<QuestionsViewModel>());
            }

            var allQuestions = await _ExamAnswerSheetService
                .GetExamAnswerSheetByUser(examId, userId);

            var lesson = await _lessonService.GetByIDAsync(cancellationToken, exam.LessonID);

            ViewBag.ExamID = examId;
            ViewBag.UserID = userId;
            ViewBag.ExamTitle = exam.Title;
            ViewBag.ExamLesson = lesson.Title;

            return View(allQuestions);
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/ExamCorrections/Index")]
        [UserAccess(Common.Enums.eAccessControl.ExamCorrectionsIndex2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Index(Microsoft.AspNetCore.Http.IFormCollection _formCollector, CancellationToken cancellationToken)
        {
            var user = await _UserManager.GetCurrentUserAsync(cancellationToken);

            var examId = System.Convert.ToInt32(_formCollector["ExamID"]);

            var allKeys = _formCollector.Keys.ToList();

            var answers = new List<ExamAnswerSheetViewModel>();

            foreach (var item in allKeys.Where(q => q.StartsWith("A_")).ToList())
            {
                var questionId = Convert.ToInt32(item.Replace("A_", ""));

                var achievedBarom = _formCollector[item];

                var getItem =await _ExamAnswerSheetService
                    .GetByIDAsync(cancellationToken, questionId);

                getItem.AchievedBarom = Convert.ToDouble(achievedBarom);

                answers.Add(new ExamAnswerSheetViewModel
                {
                    UserID = getItem.UserID,
                    ID = getItem.ID,
                    QuestionID = getItem.QuestionID,
                    ExamID = getItem.ExamID,
                    Answer = getItem.Answer,
                    AchievedBarom = getItem.AchievedBarom
                });
            }

            var isInserted = await _ExamAnswerSheetService.EvaluateExamByTeacherAsync(answers, cancellationToken);

            if (isInserted == null)
                throw new System.ArgumentException(" _ExamAnswerSheet can not insert", "original");

            TempData["success"] = "با موفقیت انجام شد";

            return RedirectToAction("Index", "Home");
        }
    }
}