using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Seventy.Common.Enums;
using Seventy.Data;
using Seventy.DomainClass.EDU.Exam;
using Seventy.Service.EDU.Course;
using Seventy.Service.EDU.Exam;
using Seventy.Service.EDU.ExamQuestions;
using Seventy.Service.EDU.Questions;
using Seventy.Service.Users;
using Seventy.ViewModel.EDU;
using Seventy.WebFramework.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Web.Areas.Edu.ExamQuestion
{
    [Area("Edu")]
    public class ExamQuestionController : Controller
    {
        private static IUserManager _um;
        private static IQuestionsService _qs;
        private static ICourseService _cs;
        private static IExamService _es;
        private static IExamQuestionsService _eqs;
        private static IMapper _mapper;
        public ExamQuestionController(ICourseService cs, IQuestionsService qs, IUserManager um, IExamService es,IMapper mapper, IExamQuestionsService eqs)
        {
            _cs = cs;
            _es = es;
            _qs = qs;
            _um = um;
            _eqs = eqs;
            _mapper = mapper;
        }

        [Route("/Edu/getNeedReviewExams")]
        [UserAccess(Common.Enums.eAccessControl.ExamQuestionGetNeedReview , Common.Enums.eAccessType.None, 1)]
        [Menu(eMenu.WaitningExamsForReview, eModule.OnlineTraining, 3)]
        public async Task<string> getNeedReviewExams(int pageNo, int pageSize = 10)
        {
            var all = _es.GetAvailableExams(q=>q.IsActive == true);
            var pagination = await PagedList<ExamViewModel>.ToPagedList(all, pageNo, pageSize);
            if (pagination == null) return "";
            var filter = pagination.Where(q => q.IsActive == true);
            var res = "";
            res += pagination.TotalPages + "_%_";
            foreach (var item in filter)
            {
                if (res.Length > 0)
                    res += "_$_";
                res += item.ID + "_|_";
                res += item.Title + "_|_";
                //res += await GetCourceNamebyId(item.CourseID) + "_|_";
                res += item.StartDate + "_|_";
                res += item.EndDate + "_|_"; 
                res += item.QuestionCount + "_|_";
                res += (item.RandomQuestionsOrder ? "بله" : "خیر") + "_|_";
                res += (item.RandomQuestionOptionsOrder ? "بله" : "خیر") + "_|_";
                res += item.Barom + "_|_";
                res += item.Description + "_|_";
            }
            return res;
        }
        [Route("/Edu/ExamList")]
        [UserAccess(Common.Enums.eAccessControl.ExamExamList, Common.Enums.eAccessType.None, 1)]
        public ActionResult ExamList()
        {
            return View();
        }
        [UserAccess(Common.Enums.eAccessControl.ExamQuestionGetAllCourse, Common.Enums.eAccessType.None, 1)]
        public static async Task<IEnumerable<DomainClass.EDU.Course.Course>> GetAllCourse(CancellationToken cancellationToken)
        {
            var curUser = await _um.GetCurrentUserAsync(cancellationToken);
            var find = _cs.TableNoTracking();
            var filter = find.Where(q => q.RegUserID == curUser.ID && q.IsActive == true);
            return filter;
        }
        [UserAccess(Common.Enums.eAccessControl.ExamQuestionGetCourseNameById, Common.Enums.eAccessType.None, 1)]
        public static async Task<string> GetCourceNamebyId(int id, CancellationToken cancellationToken)
        {
            var find = await _cs.GetByIDAsync(cancellationToken,id);
            return find != null ? find.Title : "یافت نشد";
        }
        [HttpGet]
        [Route("/Edu/ExamQuestion")]
        [UserAccess(Common.Enums.eAccessControl.ExamQuestionExamQuestion, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> ExamQuestion(CancellationToken cancellationToken, int id = 0)
        {
            if(id != 0)
            {
                ViewBag.ID = id;
                var cur = await _es.GetByIDAsync(cancellationToken,id);
                var questionEntitys = _eqs.TableNoTracking();
                ViewBag.Questions = questionEntitys.Where(q => q.ExamID== id).ToList();
                return View(_mapper.Map<ExamViewModel>(cur));
            }
            else{
                ViewBag.ID = 0;
            }
            return View();
        }
        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/ExamQuestion")]
        [UserAccess(Common.Enums.eAccessControl.ExamQuestionExamQuestion2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> ExamQuestion(DomainClass.EDU.Exam.Exam model,string questions, CancellationToken cancellationToken)
        {
            var model2 = _mapper.Map<ExamViewModel>(model);
            if (!ModelState.IsValid)
            {                
                ModelState.AddModelError("CourseID", "Model is not Valid");
                return View(model2);  
            }
            var curUser = await _um.GetCurrentUserAsync(cancellationToken);

            var examQuestion = new List<ExamQuestions>();
            var spl = questions.Split("$");
            foreach (var item in spl)
            {
                var d = item.Split("|");
                var qid = Convert.ToInt32(d[0]);
                var qBarom = Convert.ToInt32(d[1]);
                examQuestion.Add(new ExamQuestions()
                {
                    Barom = qBarom,
                    QuestionID = qid,
                    RegUserID = curUser.ID,
                });
            }
            if (model.ID == 0)
            {
                model.RegUserID = curUser.ID;
                if (await _es.InsertExamWithQuestionsAsync(model, examQuestion,cancellationToken))
                {
                    TempData["success"] = "با موفقیت ذخیره شد";
                    return View();
                }
                else
                {
                    ModelState.AddModelError("CourseID", "در هنگام ذخیره ، خطایی رخ داده است");
                    return View(model2);
                }
            }
            else
            {
                var exist = await _es.GetByIDAsync(cancellationToken, model.ID);
                if (exist == null)
                {
                    ModelState.AddModelError("CourseID", "ردیف انتخابی شما در پایگاه داده یافت نشد");
                    return View(model2);
                }

                exist.Description = model.Description;
              //  exist.CourseID = model.CourseID;
                exist.EndDate = model.EndDate;
                exist.StartDate = model.StartDate;
                exist.Title = model.Title;
                exist.RegUserID = curUser.ID;

                if (await _es.UpdateExamWithQuestionsAsync(exist,examQuestion,cancellationToken))
                {
                    TempData["success"] = "با موفقیت به روز رسانی شد";
                    return View();
                }
                else
                {
                    ModelState.AddModelError("CourseID", "در هنگام به روز رسانی ، خطایی رخ داده است");
                    return View(model2);
                }
            }
        }
        [UserAccess(Common.Enums.eAccessControl.ExamQuestionFillAnswer, Common.Enums.eAccessType.None, 1)]
        private string FillAnswer(DomainClass.EDU.Exam.Questions model, int optionAnswer)
        {
            //if (model.MultiOption == false) // تشریحی
            //{
            //    return model.Answer;
            //}
            //else
            //{
            //    switch (optionAnswer)
            //    {
            //        case 1:
            //            return model.Option1;
            //        case 2:
            //            return model.Option2;
            //        case 3:
            //            return model.Option3;
            //        case 4:
            //            return model.Option4;
            //    }
            //}
            return "";
        }
        [HttpPost]
        [Route("/Edu/RemoveExam")]
        [UserAccess(Common.Enums.eAccessControl.ExamQuestionRemove, Common.Enums.eAccessType.None, 1)]
        public async Task<string> Remove(int entityId, CancellationToken cancellationToken)
        {
            var curEntity = await _qs.GetByIDAsync(cancellationToken,entityId);
            if (curEntity == null)
                return "این موجودی حذف شده است";

            var curUser = await _um.GetCurrentUserAsync(cancellationToken);
            if (curEntity.RegUserID != curUser.ID)
                return "این موجودی توسط شما ساخته نشده است";

            curEntity.IsActive = false;
            if (await _qs.UpdateAsync(curEntity,cancellationToken) != null)
                return "done";
            else
                return "حذف با مشکل مواجه شد";
        }
        [HttpPost]
        [Route("/Edu/getAllQuestionsByCourceID")]
        [UserAccess(Common.Enums.eAccessControl.ExamQuestionGetAllQuestionByCourseId, Common.Enums.eAccessType.None, 1)]
        public string GetAllQuestionsByCourceId(int entityId,List<int> added)
        {
            var result = "";
            var allEntity = _qs.TableNoTracking();
            var filter = allEntity.Where(q => q.LessonID == entityId && q.IsActive == true).ToList();
            if (added != null && added.Count > 0)
                filter.RemoveAll(q => added.Contains((int) q.ID));

            foreach (var item in filter)
            {
                if (result.Length > 0)
                    result += "_$_";

               // result += item.ID + "|" + item.Title + "|" + item.Answer + "|" + (item.MultiOption == true? "چند گزینه ای" : "تشریحی") + "|" + (item.QuestionLevel == 1 ? "آسون" : item.QuestionLevel == 2 ? "متوسط" : item.QuestionLevel == 3 ? "سخت" : "خیلی سخت");
            }

            return result;
        }
        [HttpPost]
        [Route("/Edu/getQuestionsByID")]
        [UserAccess(Common.Enums.eAccessControl.ExamQuestionGetQuestionById, Common.Enums.eAccessType.None, 1)]
        public async Task<string> GetQuestionsById(List<int> entityIDs, CancellationToken cancellationToken)
        {
            var result = "";
            var curUser = await _um.GetCurrentUserAsync(cancellationToken);
            foreach (var item in entityIDs)
            {
                var curEntity = await _qs.GetByIDAsync(cancellationToken,item);

                if (curEntity == null)
                    continue;

                if (curEntity.RegUserID != curUser.ID)
                    continue;

                if (result.Length > 0)
                    result += "_$_";

                result += curEntity.ID + "|" +
                    curEntity.Title + "|" +
                   // curEntity.Answer + "|" +
                    (curEntity.MultiOption == true ? "چند گزینه ای" : "تشریحی") + "|" +
                    (curEntity.QuestionLevel == 1 ? "آسون" : curEntity.QuestionLevel == 2 ? "متوسط" : curEntity.QuestionLevel == 3 ? "سخت" : "خیلی سخت");

            }

            return result;
        }
        [Route("/Edu/getQuestionsByID2")]
        [UserAccess(Common.Enums.eAccessControl.ExamQuestionGetQuestionById2, Common.Enums.eAccessType.None, 1)]
        public static async Task<string> GetQuestionsById2(int id, CancellationToken cancellationToken)
        {
            var result = "";
            var curEntity = await _qs.GetByIDAsync(cancellationToken,id);

            if (curEntity == null)
                return "موجودی یافت نشد";

            result += curEntity.ID + "|" +
                curEntity.Title + "|" +
              //  curEntity.Answer + "|" +
                (curEntity.MultiOption == true ? "چند گزینه ای" : "تشریحی") + "|" +
                (curEntity.QuestionLevel == 1 ? "آسون" : curEntity.QuestionLevel == 2 ? "متوسط" : curEntity.QuestionLevel == 3 ? "سخت" : "خیلی سخت");
            return result;
        }
    }
}
