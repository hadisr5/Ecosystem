using Microsoft.AspNetCore.Mvc;
using Seventy.DomainClass.EDU;
using Seventy.Service.EDU.Course;
using Seventy.Service.EDU.Questions;
using Seventy.Service.Users;
using Seventy.WebFramework.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Web.Areas.Edu.Question
{
    [Area("Edu")]
    public class QuestionController : Controller
    {
        private static IUserManager _um;
        private static IQuestionsService _qs;
        private static ICourseService _cs;
        public QuestionController(ICourseService cs, IQuestionsService qs, IUserManager um)
        {
            _cs = cs;
            _qs = qs;
            _um = um;
        }
        [Route("/Edu/getAllQuestion")]
        [HttpPost]
        [UserAccess(Common.Enums.eAccessControl.QuestionGetAllQuestions , Common.Enums.eAccessType.None, 1)]
        public async Task<string> GetAllQuestion(int pageNo = 1, int pageRowCo = 20)
        {
            var find = await _qs.GetAllPaginatedAsync(new Data.GenericPagingParameters()
            {
                PageNumber = pageNo,
                PageSize = pageRowCo
            });

            //find = find.Where(q => q.RegUserID == CurUser.ID && q.IsActive == true);
            //return find;
            if (find == null) return "";
            var res = "";
            res += find.TotalPages + "_%_";
            foreach (var item in find)
            {
                if (res.Length > 0)
                    res += "_$_";
                res += item.ID + "_|_";
                //  res += item.CourseID + "_|_";
                //res += (await GetCourceNamebyId(item.CourseID)) + "_|_";
                res += item.QuestionLevel + "_|_";
                res += (item.QuestionLevel == 1 ? "آسون" : item.QuestionLevel == 2 ? "متوسط" : item.QuestionLevel == 3 ? "سخت" : "خیلی سخت") + "_|_";
                res += item.Title + "_|_";
                res += item.MultiOption + "_|_";
                res += (item.MultiOption == true ? "چند گزینه ای" : "تشریحی") + "_|_";
                //res += item.Option1 + "_|_";
                //res += item.Option2 + "_|_";
                //res += item.Option3 + "_|_";
                //res += item.Option4 + "_|_";
                //res += item.Answer + "_|_";
            }
            return res;
        }
        [UserAccess(Common.Enums.eAccessControl.QuestionGetAllCourse, Common.Enums.eAccessType.None, 1)]
        public static async Task<IEnumerable<DomainClass.EDU.Course.Course>> GetAllCourse(CancellationToken cancellationToken)
        {
            var curUser = await _um.GetCurrentUserAsync(cancellationToken);
            var find = _cs.TableNoTracking();
            var filter = find.Where(q => q.RegUserID == curUser.ID && q.IsActive == true);
            return filter;
        }
        [UserAccess(Common.Enums.eAccessControl.QuestionGetCourseNameById, Common.Enums.eAccessType.None, 1)]
        public static async Task<string> GetCourceNamebyId(int id, CancellationToken cancellationToken)
        {
            var find = await _cs.GetByIDAsync(cancellationToken, id);
            return find != null ? find.Title : "یافت نشد";
        }
        [HttpGet]
        [Route("/Edu/Question")]
        [UserAccess(Common.Enums.eAccessControl.QuestionQuestion, Common.Enums.eAccessType.None, 1)]
        public IActionResult Question()
        {
            return View();
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/Question")]
        [UserAccess(Common.Enums.eAccessControl.QuestionQuestion2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Question(CancellationToken cancellationToken, DomainClass.EDU.Exam.Questions model, int optionAnswer = 0)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("PrimaryCat", "Model is not Valid");
                return View(model);
            }
            var curUser = await _um.GetCurrentUserAsync(cancellationToken);
            if (model.ID == 0)
            {
                model.RegUserID = curUser.ID;
                // model.Answer = FillAnswer(model, optionAnswer);
                var result = await _qs.InsertAsync(model, cancellationToken);
                if (result != null)
                {
                    ViewBag.success = "با موفقیت ذخیره شد";
                    return View();
                }
                else
                {
                    ModelState.AddModelError("PrimaryCat", "در هنگام ذخیره ، خطایی رخ داده است");
                    return View(model);
                }
            }
            else
            {
                var exist = await _qs.GetByIDAsync(cancellationToken, model.ID);
                if (exist == null)
                {
                    ModelState.AddModelError("PrimaryCat", "ردیف انتخابی شما در پایگاه داده یافت نشد");
                    return View(model);
                }

                exist.Description = model.Description;
                // exist.CourseID = model.CourseID;
                exist.MultiOption = model.MultiOption;
                //exist.Option1 = model.Option1;
                //exist.Option2 = model.Option2;
                //exist.Option3 = model.Option3;
                //exist.Option4 = model.Option4;
                //exist.Answer = FillAnswer(model,optionAnswer);
                exist.QuestionLevel = model.QuestionLevel;
                exist.Title = model.Title;
                exist.RegUserID = curUser.ID;
                var result = await _qs.UpdateAsync(exist, cancellationToken);
                if (result != null)
                {
                    ViewBag.success = "با موفقیت به روز رسانی شد";
                    return View();
                }
                else
                {
                    ModelState.AddModelError("PrimaryCat", "در هنگام به روز رسانی ، خطایی رخ داده است");
                    return View(model);
                }
            }
        }
        [UserAccess(Common.Enums.eAccessControl.QuestionFillAnswer, Common.Enums.eAccessType.None, 1)]
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
        [Route("/Edu/RemoveQuestion")]
        [UserAccess(Common.Enums.eAccessControl.QuestionRemoveLesson, Common.Enums.eAccessType.None, 1)]
        public async Task<string> RemoveLesson(int entityId, CancellationToken cancellationToken)
        {
            var curEntity = await _qs.GetByIDAsync(cancellationToken,entityId);
            if (curEntity == null)
                return "این موجودی حذف شده است";

            var curUser = await _um.GetCurrentUserAsync(cancellationToken);
            if (curEntity.RegUserID != curUser.ID)
                return "این موجودی توسط شما ساخته نشده است";

            curEntity.IsActive = false;
            var result = await _qs.UpdateAsync(curEntity, cancellationToken);
            if (result != null)
                return "done";
            else
                return "حذف با مشکل مواجه شد";
        }
    }
}
