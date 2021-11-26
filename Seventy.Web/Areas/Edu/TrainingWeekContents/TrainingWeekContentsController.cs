using AutoMapper;
using Seventy.Data;
using System.Threading;
using Seventy.Service.Users;
using System.Threading.Tasks;
using Seventy.ViewModel.EDU;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Seventy.Service.EDU.TrainingWeek;
using Seventy.Service.EDU.TrainingContent;
using Seventy.DomainClass.EDU.TrainingWeek;
using Seventy.Service.EDU.TrainingWeekContent;
using Seventy.Service.EDU.UserTrainingWeekContent;

using System;
using System.Linq;
using Seventy.WebFramework.Filters;
using Seventy.Common.Enums;

// ReSharper disable Mvc.ViewNotResolvedSpeechLessonsList
// ReSharper disable Mvc.PartialViewNotResolved

namespace Seventy.Web.Areas.Edu.TrainingWeekContents
{
    [Area("Edu")]
    public class TrainingWeekContentsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;
        private static ITrainingWeekService _trainingWeekService;
        private static ITrainingContentService _trainingContentService;
        private readonly ITrainingWeekContentService _trainingWeekContentService;

        private static int? _userId;
        public TrainingWeekContentsController(IUserManager userManager, IMapper mapper,
            ITrainingWeekContentService trainingWeekContentService, ITrainingWeekService trainingWeekService,
            ITrainingContentService trainingContentService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _trainingWeekService = trainingWeekService;
            _trainingContentService = trainingContentService;
            _trainingWeekContentService = trainingWeekContentService;
            _userId = _userManager.GetCurrentUserID();
        }

        [HttpGet]
        [Route("/Edu/TrainingWeekContents/Index")]
        [UserAccess(Common.Enums.eAccessControl.TrainingWeekContentsIndex, Common.Enums.eAccessType.None, 1)]
        [Menu(eMenu.AllocateContentToTraining, eModule.OnlineTraining, 4)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, int id = 0)
        {
            if (id == 0)
                return View();

            var model = await _trainingWeekContentService.GetByIDAsync(cancellationToken, id);

            return View(_mapper.Map<TrainingWeekContentEditModel>(model));
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/TrainingWeekContents/Index")]
        [UserAccess(Common.Enums.eAccessControl.TrainingWeekContentsIndex2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, TrainingWeekContent model)
        {
            if (!ModelState.IsValid || model.ContentID == 0 || model.TrainingWeekID == 0 || model.ContentType == null)
            {
                TempData["err"] = "model is not valid";

                return View(model);
            }

            model.RegUserID = _userManager.GetCurrentUserID();

            if (model.ID == 0 || model.ID == null)
            {
                model.ID = null;

                var insert = await _trainingWeekContentService.InsertAsync(model, cancellationToken);

                if (insert != null)
                {
                    TempData["success"] = "با موفقیت افزوده شد";

                    return RedirectToAction("Index");
                }

                TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

                return View(model);
            }

            var update = await _trainingWeekContentService.UpdateAsync(model, cancellationToken);

            if (update != null)
            {
                TempData["success"] = "با موفقیت بروز رسانی شد";

                return RedirectToAction("Index");
            }

            TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

            return View(model);
        }

        [Route("/Edu/TrainingWeekContents/List")]
        [UserAccess(Common.Enums.eAccessControl.TrainingWeekContentsList, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> List(int page)
        {
            var model = await _trainingWeekContentService
                .GetAllPaginatedAsync(new GenericPagingParameters
                {
                    PageNumber = page,
                    PageSize = 10
                }, q => q.IsActive);

            return PartialView("List", model);
        }

        [Route("/Edu/TrainingWeekContents/SpeechLessonsList")]
        [UserAccess(Common.Enums.eAccessControl.TrainingWeekContentsSpeechLessonList, Common.Enums.eAccessType.None, 1)]
        public async Task<string> SpeechLessonsList(int PageNo)
        {
            var userId= _userManager.GetCurrentUserID();
            var model = await _trainingWeekContentService
                 .GetAllPaginatedAsync(new GenericPagingParameters
                 {
                     PageNumber = PageNo,
                     PageSize = 10
                 }, q => q.IsActive && q.RegUserID!=null && q.RegUserID==userId);

            if (model == null) return "";

            var res = "";
            res += model.TotalPages + "_%_";
            foreach (var item in model)
            {
                if (res.Length > 0)
                    res += "_$_";
                res += item.TrainingWeekID + "_|_";
                res += item.TrainingWeekTitle + "_|_";
                res += item.ContentTitle + "_|_";
                res += item.ContentType + "_|_";
                res += item.Description;

            }
            return res;
        }

        [Route("/Edu/TrainingWeekContents/Remove")]
        [UserAccess(Common.Enums.eAccessControl.TrainingWeekContentsDelete, Common.Enums.eAccessType.None, 1)]
        public async Task<string> Delete(CancellationToken cancellationToken, int id)
        {
            var entity = await _trainingWeekContentService.GetByIDAsync(cancellationToken, id);

            if (entity == null)
                return "آیتم مورد نظر یافت نشد";

            if (await _trainingWeekContentService.DeleteAsync(entity, cancellationToken))
                return "done";

            return "خطا در زمان حذف داده";
        }
        [UserAccess(Common.Enums.eAccessControl.TrainingWeekContentsGetAllTrainingContent, Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<DomainClass.EDU.TrainingContent.TrainingContent> GetAllTrainingContent()
        {
            return _trainingContentService.TableNoTracking();
        }
        [UserAccess(Common.Enums.eAccessControl.TrainingWeekContentsGetAllTrainingWeek, Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<TrainingWeek> GetAllTrainingWeek()
        {
            return _trainingWeekService.TableNoTracking();
        }

        [HttpGet]
        [Route("/Edu/TrainingWeekContents/GetAllTrainingContentByType")]
        [UserAccess(Common.Enums.eAccessControl.TrainingWeekContentsGetAllTrainingWeekContentByType, Common.Enums.eAccessType.None, 1)]
        public List<DomainClass.EDU.TrainingContent.TrainingContent> GetAllTrainingContentByType(string type)
        {
            return _trainingContentService.GetByType(type);
        }

        #region /Edu/TrainingWeekContents/SpeechLessons == لیست کاربران
        [Route("/Edu/TrainingWeekContents/SpeechLessons")]
        [UserAccess(Common.Enums.eAccessControl.TrainingWeekContentsSpeechLesson, Common.Enums.eAccessType.None, 1)]
        [Menu(eMenu.SpeechLessons, eModule.OnlineTraining, 3)]
        public async Task<IActionResult> SpeechLessons()
        {
            return View();
        }
        #endregion

        #region /Edu/TrainingWeekContents/ListOfClassmates ==همکلاسی ها
        [Route("/Edu/TrainingWeekContents/ListOfClassmates")]
        [UserAccess(Common.Enums.eAccessControl.TrainingWeekContentsListOfClassMates, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> ListOfClassmates(CancellationToken cancellation, int page = 1, int ListType = 0, bool isPartial = false, int weekId = 1)
        {
            var model = await
                _trainingWeekContentService.GetUserByContentWeekIdAsync(cancellation, weekId, _userId);

            return PartialView("ListOfClassmates", model);
        }
        #endregion

        #region /Edu/TrainingWeekContents/ListOfOtherInfoAsync ==همکلاسی ها
        [Route("/Edu/TrainingWeekContents/ListOfOtherInfoAsync")]
        [UserAccess(Common.Enums.eAccessControl.TrainingWeekContentsListOfOtherInfoAsync, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> ListOfOtherInfoAsync(CancellationToken cancellation, int weekId = 0)
        {
            var model = await
                _trainingWeekContentService.GetOtherInfoAsync(cancellation, weekId);

            return PartialView("ListOfOtherInfoAsync", model);
        }
        #endregion
    }
}