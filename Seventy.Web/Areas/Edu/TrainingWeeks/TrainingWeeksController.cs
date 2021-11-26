using AutoMapper;
using Seventy.Data;
using System.Threading;
using Seventy.ViewModel.EDU;
using Seventy.Service.Users;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Seventy.DomainClass.EDU.Term;
using Seventy.Service.EDU.Lesson;
using Seventy.Service.EDU.TrainingWeek;
using Seventy.DomainClass.EDU.TrainingWeek;
using Seventy.Service.EDU.Term;
using Seventy.WebFramework.Filters;
using Seventy.Common.Enums;

// ReSharper disable Mvc.ViewNotResolved
// ReSharper disable Mvc.PartialViewNotResolved

namespace Seventy.Web.Areas.Edu.TrainingWeeks
{
    [Area("Edu")]
    public class TrainingWeeksController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;
        private static ITermService _termService;
        private static ILessonService _lessonService;
        private static ITrainingWeekService _trainingWeekService;

        public TrainingWeeksController(IUserManager userManager, IMapper mapper,
            ITrainingWeekService trainingWeekService,
            ILessonService lessonService, ITermService termService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _termService = termService;
            _lessonService = lessonService;
            _trainingWeekService = trainingWeekService;
        }

        [HttpGet]
        [Route("/Edu/TrainingWeeks/Index")]
        [UserAccess(eAccessControl.TrainingWeeksIndex, Common.Enums.eAccessType.None, 1)]
        [Menu(eMenu.TrainingWeeksManagement, eModule.OnlineTraining, 6)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, int id = 0)
        {
            if (id == 0)
                return View();

            var model = await _trainingWeekService.GetByIDAsync(cancellationToken, id);

            return View(_mapper.Map<TrainingWeekEditModel>(model));
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/TrainingWeeks/Index")]
        [UserAccess(Common.Enums.eAccessControl.TrainingWeeksIndex2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, TrainingWeek model)
        {
            var model2 = _mapper.Map<TrainingWeekEditModel>(model);
            try
            {

                if (!ModelState.IsValid || model.LessonID == 0)
                {
                    TempData["err"] = "model is not valid";

                    return View(model2);
                }

                model.RegUserID = _userManager.GetCurrentUserID();

                if (model.ID == 0 || model.ID == null)
                {
                    model.ID = null;

                    var insert = await _trainingWeekService.InsertAsync(model, cancellationToken);

                    if (insert != null)
                    {
                        TempData["success"] = "با موفقیت افزوده شد";

                        return RedirectToAction("Index");
                    }

                    TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

                    return View(model2);
                }

                var update = await _trainingWeekService.UpdateAsync(model, cancellationToken);

                if (update != null)
                {
                    TempData["success"] = "با موفقیت بروز رسانی شد";

                    return RedirectToAction("Index");
                }

                TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";
            }
            catch (System.Exception ex)
            {
                TempData["err"] = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return View(model2);
        }

        [Route("/Edu/TrainingWeeks/List")]
        [UserAccess(Common.Enums.eAccessControl.TrainingWeeksList, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> List(int page)
        {
            var model = await _trainingWeekService
                .GetAllPaginatedAsync(new GenericPagingParameters
                {
                    PageNumber = page,
                    PageSize = 10
                }, q => q.IsActive);

            return PartialView("List", model);
        }

        [Route("/Edu/TrainingWeeks/Remove")]
        [UserAccess(Common.Enums.eAccessControl.TrainingWeeksDelete, Common.Enums.eAccessType.None, 1)]
        public async Task<string> Delete(CancellationToken cancellationToken, int id)
        {
            var entity = await _trainingWeekService.GetByIDAsync(cancellationToken, id);

            if (entity == null)
                return "آیتم مورد نظر یافت نشد";

            if (await _trainingWeekService.DeleteAsync(entity, cancellationToken))
                return "done";

            return "خطا در زمان حذف داده";
        }
        [UserAccess(Common.Enums.eAccessControl.TrainingWeeksGetAllLesson, Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<DomainClass.EDU.Lesson.Lesson> GetAllLesson()
        {
            return _lessonService.TableNoTracking();
        }
        [UserAccess(Common.Enums.eAccessControl.TrainingWeeksGetAllTerm, Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<Term> GetAllTerm()
        {
            return _termService.TableNoTracking();
        }
    }
}