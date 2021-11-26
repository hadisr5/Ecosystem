using Microsoft.AspNetCore.Mvc;
using Seventy.DomainClass.EDU;
using Seventy.DomainClass.EDU.Term;
using Seventy.Service.EDU.Course;
using Seventy.Service.EDU.Lesson;
using Seventy.Service.EDU.Term;
using Seventy.Service.Users;
using Seventy.WebFramework.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Web.Areas.Edu.Lesson
{
    [Area("Edu")]
    public class LessonController : Controller
    {
        private static IUserManager _um;
        private static ILessonService _ls;
        private static ICourseService _cs;
        private static ITermService _ts;
        public LessonController(ILessonService ls, ICourseService cs, ITermService ts, IUserManager um)
        {
            _cs = cs;
            _ts = ts;
            _ls = ls;
            _um = um;
        }
        [Route("/Edu/getAllLesson")]
        [UserAccess(Common.Enums.eAccessControl.LessonGetAllLesson , Common.Enums.eAccessType.None, 1)]
        public async Task<string> GetAllLesson(CancellationToken cancellationToken, int pageNo, int pageSize = 20)
        {
            var curUser = await _um.GetCurrentUserAsync(cancellationToken);
            var all = await _ls.GetAllPaginatedAsync(new Data.GenericPagingParameters()
            {
                PageNumber = pageNo,
                PageSize = pageSize,
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
                // res += item.CourseID + "_|_";
                //  res += (await GetCourceNamebyId(item.CourseID)) + "_|_";
                //  res += item.TermID + "_|_";
                // res += (await GetTermNamebyId(item.TermID)) + "_|_";
                res += item.Title;
            }
            return res;
        }

        [UserAccess(Common.Enums.eAccessControl.LessonGetAllCourse, Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<DomainClass.EDU.Course.Course> GetAllCourse()
        {
            return _cs.TableNoTracking();
        }
        [UserAccess(Common.Enums.eAccessControl.LessonGetCourseNameById, Common.Enums.eAccessType.None, 1)]
        public static async Task<string> GetCourceNamebyId(int id, CancellationToken cancellationToken)
        {
            var find = await _cs.GetByIDAsync(cancellationToken, id);
            return find != null ? find.Title : "یافت نشد";
        }
        [UserAccess(Common.Enums.eAccessControl.LessonGetAllTerm, Common.Enums.eAccessType.None, 1)]
        public static async Task<IEnumerable<Term>> GetAllTerm(CancellationToken cancellationToken)
        {
            var curUser = await _um.GetCurrentUserAsync(cancellationToken);
            var all = _ts.TableNoTracking();
            var filter = all.Where(q => q.RegUserID == curUser.ID && q.IsActive == true);
            return filter;
        }
        [UserAccess(Common.Enums.eAccessControl.LessonGetTermNameById, Common.Enums.eAccessType.None, 1)]
        public static async Task<string> GetTermNamebyId(int id, CancellationToken cancellationToken)
        {
            var find = await _ts.GetByIDAsync(cancellationToken, id);
            return find != null ? find.Title : "یافت نشد";
        }
        [HttpGet]
        [Route("/Edu/Lesson")]
        [UserAccess(Common.Enums.eAccessControl.LessonLesson, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Lesson()
        {
            return View();
        }
        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/Lesson")]
        [UserAccess(Common.Enums.eAccessControl.LessonLesson2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Lesson(DomainClass.EDU.Lesson.Lesson model, CancellationToken cancellationToken)
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
                var result = await _ls.InsertAsync(model, cancellationToken);
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
                var exist = await _ls.GetByIDAsync(cancellationToken, model.ID);
                if (exist == null)
                {
                    ModelState.AddModelError("PrimaryCat", "ردیف انتخابی شما در پایگاه داده یافت نشد");
                    return View(model);
                }

                exist.Description = model.Description;
                //exist.CourseID = model.CourseID;
                //exist.TermID = model.TermID;
                exist.Title = model.Title;
                exist.RegUserID = curUser.ID;
                var result = await _ls.UpdateAsync(exist, cancellationToken);
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
        [HttpPost]
        [Route("/Edu/RemoveLesson")]
        [UserAccess(Common.Enums.eAccessControl.LessonRemoveLesson, Common.Enums.eAccessType.None, 1)]
        public async Task<string> RemoveLesson(int entityId, CancellationToken cancellationToken)
        {
            var curEntity = await _ls.GetByIDAsync(cancellationToken, entityId);
            if (curEntity == null)
                return "این دسته بندی حذف شده است";

            var curUser = await _um.GetCurrentUserAsync(cancellationToken);
            if (curEntity.RegUserID != curUser.ID)
                return "این موجودی توسط شما ساخته نشده است";

            curEntity.IsActive = false;
            var result = await _ls.UpdateAsync(curEntity, cancellationToken);
            if (result != null)
                return "done";
            else
                return "حذف با مشکل مواجه شد";
        }
    }
}
