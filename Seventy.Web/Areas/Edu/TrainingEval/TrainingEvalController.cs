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
    public class TrainingEvalController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;
        private readonly ITrainingEvalIndexService _TrainingEvalIndexService;
        private readonly ICourseService _CourseService;
        private readonly ILessonService _LessonService;
        private readonly ITrainingContentService _TrainingContentService;
        private readonly ITeacherLessonService _TeacherLessonService;

        private static int? _userId;

        public TrainingEvalController(IMapper mapper, IUserManager userManager, ITrainingEvalIndexService TrainingEvalIndexService,
            ICourseService CourseService, ILessonService LessonService, ITrainingContentService TrainingContentService
            , ITeacherLessonService TeacherLessonService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _userId = _userManager.GetCurrentUserID();
            _TrainingEvalIndexService = TrainingEvalIndexService;
            _CourseService = CourseService;
            _LessonService = LessonService;
            _TrainingContentService = TrainingContentService;
            _TeacherLessonService = TeacherLessonService;
        }

        [HttpGet]
        [Route("/Edu/TrainingEval/Index")]
        [UserAccess(eAccessControl.TrainingEvalIndex, eAccessType.None, 1)]
        [Menu(eMenu.CreateTrainingEval, eModule.OnlineTraining, 12)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, int id = 0)
        {
            if (id == 0)
                return View();

            ViewBag.TargetIDs = _TrainingEvalIndexService.TableNoTracking();
            var model = await _TrainingEvalIndexService.GetByIDAsync(cancellationToken, id);
            return View(new Seventy.ViewModel.EDU.TrainingEval.TrainingEvalIndexEditModel()
            {
                ID = id,
                TargetType = model.TargetType,
                TargetID = model.TargetID,
                Title = model.Title
            });
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/TrainingEval/Index")]
        [UserAccess(Common.Enums.eAccessControl.TrainingEvalIndex2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, Seventy.ViewModel.EDU.TrainingEval.TrainingEvalIndexEditModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["err"] = "model is not valid";

                return View(model);
            }

            model.RegUserID = _userManager.GetCurrentUserID();

            string modelTargetType = "";

            switch(model.TargetType)
            {
                case "1": modelTargetType = "دوره";
                    break;
                case "2":
                    modelTargetType = "درس";
                    break;
                case "3":
                    modelTargetType = "محتوی";
                    break;
                case "4":
                    modelTargetType = "مدرس";
                    break;
            }

            var model2 = new DomainClass.EDU.TrainingEval.TrainingEvalIndex()
            {
                ID = model.ID,
                RegUserID = _userId,
                TargetType = modelTargetType,
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

        [Route("/Edu/TrainingEval/TargetIDList")]
        [UserAccess(Common.Enums.eAccessControl.TrainingEvalTargetIDList, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> TargetIDList(int TargetType)
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
                ViewBag.Data = _TeacherLessonService.TableNoTracking().ToList().Select(q => new { ID = q.ID, Title = q.Teacher }).ToList();
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

        [Route("/Edu/TrainingEval/List")]
        [UserAccess(Common.Enums.eAccessControl.TrainingEvalList, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> List(int page)
        {
            var model = await _TrainingEvalIndexService.GetAllPaginatedAsync(new Data.GenericPagingParameters()
            {
                PageNumber = page,
                PageSize = 10
            },q=>q.IsActive == true);
            return PartialView("List", model);
        }

        [Route("/Edu/TrainingEval/Remove")]
        [UserAccess(Common.Enums.eAccessControl.TrainingEvalDelete, Common.Enums.eAccessType.None, 1)]
        public async Task<string> Delete(CancellationToken cancellationToken, int id)
        {
            var entity = await _TrainingEvalIndexService.GetByIDAsync(cancellationToken, id);

            if (entity == null)
                return "آیتم مورد نظر یافت نشد";

            if (await _TrainingEvalIndexService.DeleteAsync(entity, cancellationToken))
                return "done";

            return "خطا در زمان حذف داده";
        }
    }
}