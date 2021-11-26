using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Seventy.Service.Users;
using Seventy.Service.EDU.Course;
using Seventy.Service.EDU.Lesson;
using Seventy.Service.EDU.TrainingContent;
using Seventy.Service.EDU.TeacherLesson;
using System.Collections.Generic;
using Seventy.WebFramework.Filters;
using Seventy.Common.Enums;

namespace Seventy.Service.EDU.TrainingEval
{
    [Area("Edu")]
    public class TrainingEvalResultController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;
        private readonly ITrainingEvalIndexService _TrainingEvalIndexService;
        private readonly ITrainingEvalResultService _TrainingEvalResultService;
        private readonly ICourseService _CourseService;
        private readonly ILessonService _LessonService;
        private readonly ITrainingContentService _TrainingContentService;
        private readonly ITeacherLessonService _TeacherLessonService;
        private static int? _userId;

        public TrainingEvalResultController(IMapper mapper, IUserManager userManager, ITrainingEvalIndexService TrainingEvalIndexService,
            ICourseService CourseService, ILessonService LessonService, ITrainingContentService TrainingContentService
            , ITeacherLessonService TeacherLessonService, ITrainingEvalResultService TrainingEvalResultService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _userId = _userManager.GetCurrentUserID();
            _TrainingEvalIndexService = TrainingEvalIndexService;
            _CourseService = CourseService;
            _LessonService = LessonService;
            _TrainingContentService = TrainingContentService;
            _TeacherLessonService = TeacherLessonService;
            _TrainingEvalResultService = TrainingEvalResultService;
        }

        [HttpGet]
        [Route("/Edu/TrainingEvalResult/Index")]
        [Menu(eMenu.TheResultOfTheEvaluation, eModule.OnlineTraining, 14)]
        [UserAccess(Common.Enums.eAccessControl.TrainingEvalResultIndex, Common.Enums.eAccessType.None, 1)]
        public IActionResult Index(CancellationToken cancellationToken, int? TargetType, int? TargetID)
        {
            ViewData["TargetType"] = TargetType;
            ViewData["TargetID"] = TargetID;
            return View();
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/TrainingEvalResult/Index")]
        [UserAccess(Common.Enums.eAccessControl.TrainingEvalResultIndex2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, Seventy.ViewModel.EDU.TrainingEval.TrainingEvalIndexEditModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["err"] = "model is not valid";
                return View(model);
            }

            model.RegUserID = _userManager.GetCurrentUserID();
            var model2 = new DomainClass.EDU.TrainingEval.TrainingEvalIndex()
            {
                ID = model.ID,
                RegUserID = _userId,
                TargetType = model.TargetType,
                TargetID = model.TargetID,
                Title = model.Title,
            };

            if (model2.ID == 0 || model2.ID == null)
            {
                model2.ID = null;
                var insert = await _TrainingEvalIndexService.InsertAsync(model2, cancellationToken);

                if (insert != null)
                {
                    TempData["success"] = "با موفقیت افزوده شد";

                    return RedirectToAction("Index");
                }

                TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

                return View(model);
            }

            var update = await _TrainingEvalIndexService.UpdateAsync(model2, cancellationToken);

            if (update != null)
            {
                TempData["success"] = "با موفقیت بروز رسانی شد";

                return RedirectToAction("Index");
            }

            TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

            return View(model);
        }

        [Route("/Edu/TrainingEvalResult/TargetIDList")]
        [UserAccess(Common.Enums.eAccessControl.TrainingEvalResultTargetIDListAsync, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> TargetIDListAsync(int TargetType)
        {
            if (TargetType.Equals(1))
            {
                ViewBag.Data = _CourseService.TableNoTracking().ToList().Select(q => new { ID = q.ID, Title = q.Title }).ToList();
            }
            else if (TargetType.Equals(2))
            {
                ViewBag.Data = _LessonService.TableNoTracking().ToList().Select(q => new { ID = q.ID, Title = q.Title }).ToList();
            }
            else if (TargetType.Equals(3))
            {
                ViewBag.Data = _TrainingContentService.TableNoTracking().ToList().Select(q => new { ID = q.ID, Title = q.Title }).ToList();
            }
            else if (TargetType.Equals(4))
            {
                var teacherList = await _userManager.GetByRole(Core.Users.RoleSelect.Teacher);

                ViewBag.Data = teacherList.Select(q => new { ID = q.ID, Title = q.Name +" "+q.Family }).ToList();
            }
            List<DomainClass.EDU.Course.Course> allData = new List<DomainClass.EDU.Course.Course>();

            foreach (var item in ViewBag.Data)
            {
                allData.Add(new DomainClass.EDU.Course.Course()
                {
                    ID = item.ID,
                    Title = item.Title,
                });
            }
            return PartialView("TargetIDList", allData);
        }

        [Route("/Edu/TrainingEvalResult/List")]
        [UserAccess(Common.Enums.eAccessControl.TrainingEvalResultList, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> List(CancellationToken cancellationToken ,string TargetType,int TargetID)
        {
            var model = await _TrainingEvalResultService.GetByType(TargetID, TargetType, cancellationToken);
            return PartialView("List", model);
        }

        [Route("/Edu/TrainingEvalResult/Vote")]
        [UserAccess(Common.Enums.eAccessControl.TrainingEvalResultVote, Common.Enums.eAccessType.None, 1)]
        public async Task<string> Vote(CancellationToken cancellationToken, int id,int score)
        {
            var isInserted = await _TrainingEvalResultService.InsertAsync(new DomainClass.EDU.TrainingEval.TrainingEvalResult()
            {
                UserID = _userId.Value,
                Result = score,
                RegUserID = _userId,
                TrainingEvalIndexID = id,
            },cancellationToken);

            if(isInserted != null)
            {
                return "done";
            }
          
            return "شما نمیتونید دوباره به این سوال امتیاز دهید";
        }
    }
}