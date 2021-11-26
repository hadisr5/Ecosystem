using System.Threading;
using AutoMapper;
using Seventy.Data;
using Seventy.Service.Users;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Seventy.ViewModel.EDU.Lesson;
using Seventy.Service.EDU.RequestedCourses;
using Seventy.WebFramework.Filters;
using Seventy.Common.Enums;

// ReSharper disable Mvc.ViewNotResolved
// ReSharper disable Mvc.PartialViewNotResolved

namespace Seventy.Web.Areas.Edu.RequestedCourses
{
    [Area("Edu")]
    public class RequestedCoursesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;
        private readonly IRequestedCoursesService _requestedCoursesService;

        public RequestedCoursesController(IMapper mapper, IUserManager userManager,
             IRequestedCoursesService requestedCoursesService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _requestedCoursesService = requestedCoursesService;
        }

        [HttpGet]
        [Route("/Edu/RequestedCourses/Index")]
        [UserAccess(Common.Enums.eAccessControl.RequestedCoursesIndex , Common.Enums.eAccessType.None, 1)]
        [Menu(eMenu.RequestedCourses, eModule.OnlineTraining, 12)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, int id = 0)
        {
            if (id == 0)
                return View();

            var model = await _requestedCoursesService.GetByIDAsync(cancellationToken, id);

            return View(_mapper.Map<LessonEditViewModel>(model));
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/RequestedCourses/Index")]
        [UserAccess(Common.Enums.eAccessControl.RequestedCoursesIndex2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, DomainClass.EDU.Course.RequestedCourses model)
        {
            if (!ModelState.IsValid)
            {
                TempData["err"] = "model is not valid";

                return View(model);
            }

            model.RegUserID = _userManager.GetCurrentUserID();

            if (model.ID == 0 || model.ID == null)
            {
                model.ID = null;

                var insert = await _requestedCoursesService.InsertAsync(model, cancellationToken);

                if (insert != null)
                {
                    TempData["success"] = "با موفقیت افزوده شد";

                    return RedirectToAction("Index");
                }

                TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

                return View(model);
            }

            var update = await _requestedCoursesService.UpdateAsync(model, cancellationToken);

            if (update != null)
            {
                TempData["success"] = "با موفقیت بروز رسانی شد";

                return RedirectToAction("Index");
            }

            TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

            return View(model);
        }

        [Route("/Edu/RequestedCourses/List")]
        [UserAccess(Common.Enums.eAccessControl.RequestedCoursesList, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> List(int page)
        {
            var model = await _requestedCoursesService
                .GetAllPaginatedAsync(new GenericPagingParameters
                {
                    PageNumber = page,
                    PageSize = 10
                }, q => q.IsActive);

            return PartialView("List", model);
        }

        [Route("/Edu/RequestedCourses/Remove")]
        [UserAccess(Common.Enums.eAccessControl.RequestedCoursesDelete, Common.Enums.eAccessType.None, 1)]
        public async Task<string> Delete(CancellationToken cancellationToken, int id)
        {
            var entity = await _requestedCoursesService.GetByIDAsync(cancellationToken, id);

            if (entity == null)
                return "آیتم مورد نظر یافت نشد";

            if (await _requestedCoursesService.DeleteAsync(entity, cancellationToken))
                return "done";

            return "خطا در زمان حذف داده";
        }
    }
}