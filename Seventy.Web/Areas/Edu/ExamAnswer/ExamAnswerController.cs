using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Seventy.Common.Utilities;
using Seventy.Data;
using Seventy.DomainClass.EDU.Exam;
using Seventy.Service.EDU.Exam;
using Seventy.Service.EDU.ExamAnswerSheet;
//using Seventy.Service.EDU.ExamAnswerSheet;
using Seventy.Service.EDU.ExamQuestions;
using Seventy.Service.EDU.ExamUser;
using Seventy.Service.EDU.QuestionOptions;
using Seventy.Service.EDU.Questions;
using Seventy.Service.Users;
using Seventy.ViewModel.EDU;
using Seventy.ViewModel.EDU.Exam.ExamAnswerSheet;
using Seventy.WebFramework.Filters;
using System;
namespace Seventy.Web.Areas.Edu.ExamAnswer
{
    [Area("Edu")]
    public class ExamAnswerController : Controller
    {
        private static IUserManager _UserManager;
        private static IMapper _mapper;
        //private static IExamAnswerSheetService _ExamAnswerSheet;
        private static IExamService _ExamService;
        private static IExamQuestionsService _ExamQuestions;
        private static IQuestionsService _qs;
        private IQuestionOptionsService _qops;
        private static IExamUserService _ExamUserService;
        private static IExamAnswerSheetService _ExamAnswerSheetService;
        public ExamAnswerController(IUserManager UserManager, IMapper mapper,/*IExamAnswerSheetService ExamAnswerSheet*/ IExamService ExamService
            , IExamQuestionsService ExamQuestions, IQuestionsService qs, IExamUserService ExamUserService, IExamAnswerSheetService examAnswerSheetService, IQuestionOptionsService qops)
        {
            _ExamAnswerSheetService = examAnswerSheetService;
            _qops = qops;
            _UserManager = UserManager;
            _mapper = mapper;
            _ExamService = ExamService;
            _ExamQuestions = ExamQuestions;
            _qs = qs;
            _ExamUserService = ExamUserService;
        }

        [HttpGet]
        [Route("/Edu/StartExam")]
        [UserAccess(Common.Enums.eAccessControl.ExamAnswerStartExam, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> StartExam(CancellationToken cancellationToken, int ExamID = 0)
        {
            var user = await _UserManager.GetCurrentUserAsync(cancellationToken);

            //update starttime in ExamUser
            var examUser = (await _ExamUserService.GetByUserAndExamIdAsync(cancellationToken, ExamID, user.ID.Value))?.FirstOrDefault();
            var now = DateTime.Now;
            if (examUser != null && examUser.StartTime == null)
            {
                examUser.StartTime = now;
                await _ExamUserService.UpdateAsync(examUser, cancellationToken);
            }

            var exam = await _ExamService.GetByIDAsync(cancellationToken, ExamID);
            if (exam == null)
            {
                TempData["Message"] = "امتحان مورد نظر یافت نشد";
                return View(new List<QuestionsViewModel>());
            }

            if (user == null)
            {
                TempData["Message"] = "خطا در اطلاعات کاربر، لطفا ابتدا لاگین کنید.";
                return View(new List<QuestionsViewModel>());
            }

            var canStartExam = _ExamUserService.AssignedBefore(ExamID, user.ID.Value);
            if (!canStartExam)
            {
                TempData["Message"] = "شما اجازه شرکت مجدد در امتحان را ندارید";
                return View(new List<QuestionsViewModel>());
            }


            if (exam.StartDate > now)
            {
                TempData["Message"] = $"آزمون شروع نشده است . تاریخ شروع: {exam.StartDate}";
                return View(new List<QuestionsViewModel>());
            }

            if (exam.EndDate < now)
            {
                TempData["Message"] = $"آزمون به اتمام رسیده . تاریخ اتمام: {exam.EndDate}";
                return View(new List<QuestionsViewModel>());
            }

            var AllQuestions = await _ExamQuestions.GetQuestionsByExamAsync(ExamID);

            foreach (var item in AllQuestions)
            {
                if (item.MultiOption)
                {
                    var options = _qops.TableNoTracking().Where(x => x.QuestionId == item.ID);
                    var mapmodel = _mapper.Map<List<QuestionOptionsViewModel>>(options);
                    item.AnswerOptions = mapmodel;


                }
            }
            DateTime? time = null;
            if (examUser.StartTime != now)
            {
                time =now.AddSeconds((exam.Time*60) - (int)((now - examUser.StartTime).Value.TotalSeconds)); //مدت زمان به دقیقه
            }
            else
            {
                time =now.AddMinutes(exam.Time); //مدت زمان به دقیقه
            }
            if (time !=null && time>now)
            {
                ViewBag.ExamID = ExamID;
                ViewBag.ExamTitle = exam.Title;
                ViewBag.ExamTime = time;
                ViewBag.ExamLesson = AllQuestions.Count() == 0 ? "" : AllQuestions.FirstOrDefault().LessonTitle ?? "";
                return View(AllQuestions.ToList());
            }
            else
            {

                TempData["Message"] = "شما اجازه شرکت مجدد در امتحان را ندارید";
                return View(new List<QuestionsViewModel>());
            }
        }



        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/StartExam")]
        [UserAccess(Common.Enums.eAccessControl.ExamAnswerStartExam2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> StartExam(Microsoft.AspNetCore.Http.IFormCollection _formCollector, CancellationToken cancellationToken)
        {
            var User = await _UserManager.GetCurrentUserAsync(cancellationToken);
            var ExamID = System.Convert.ToInt32(_formCollector["ExamID"]);
            var allKeys = _formCollector.Keys.ToList();

            var exam = await _ExamService.GetByIDAsync(cancellationToken, ExamID);

            var examUser = (await _ExamUserService.GetByUserAndExamIdAsync(cancellationToken, ExamID, User.ID.Value)).FirstOrDefault();

            if (examUser != null)
            {
                var timeout = DateTime.Now.ToPersianDateTime(true).Subtract(examUser.StartTime.Value).TotalMinutes > exam.Time;
                if (timeout)
                {
                    TempData["err"] = "زمان آزمون به پایان رسیده است. پاسخنامه شما ثبت نگردید";
                    return RedirectToAction("Index", "Home");
                }
            }

            List<ExamAnswerSheetViewModel> answers = new List<ExamAnswerSheetViewModel>();
            foreach (var item in allKeys.Where(q => q.StartsWith("Q_")).ToList())
            {
                var QuestionID = System.Convert.ToInt32(item.Replace("Q_", ""));
                var Answer = _formCollector[item];
                var AnswerSheet = new ExamAnswerSheetViewModel()
                {
                    Answer = Answer,
                    QuestionID = QuestionID,
                    ExamID = ExamID,
                    UserID = User.ID.Value,
                    RegUserID = User.ID,
                };
                answers.Add(AnswerSheet);

            }

            var isInserted = await _ExamAnswerSheetService.SaveExamAnswersheetAsync(answers, cancellationToken);

            if (isInserted == null)
                throw new System.ArgumentException(" _ExamAnswerSheet can not insert", "original");


            TempData["success"] = "با تشکر از شرکت در آزمون ، پس از بررسی توسط استاد نمره در کارنامه قرار خواهد گرفت";
            return RedirectToAction("Index", "Home");
        }
        [UserAccess(Common.Enums.eAccessControl.ExamAnswerIsExamFull, Common.Enums.eAccessType.None, 1)]
        public async Task<bool> isExamFullMultiOption(int ExamID)
        {
            var Questions = (await _ExamQuestions.GetQuestionsByExamAsync(ExamID)).ToList();
            foreach (var item in Questions)
            {
                if (!item.MultiOption)
                {
                    return false;
                }
            }
            return true;
        }







    }
}
