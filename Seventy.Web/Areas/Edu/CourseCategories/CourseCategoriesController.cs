using AutoMapper;
using Seventy.Data;
using System.Threading;
using Seventy.Service.Users;
using Seventy.ViewModel.EDU;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Seventy.DomainClass.EDU.Course;
using Seventy.Service.EDU.CourseCategory;
using Seventy.WebFramework.Filters;
using Seventy.Common.Enums;

// ReSharper disable Mvc.ViewNotResolved
// ReSharper disable Mvc.PartialViewNotResolved

namespace Seventy.Web.Areas.Edu.CourseCategories
{
    [Area("Edu")]
    public class CourseCategoriesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;
        private readonly ICourseCategoryService _courseCategoryService;

        public CourseCategoriesController(IUserManager userManager, IMapper mapper,
            ICourseCategoryService courseCategoryService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _courseCategoryService = courseCategoryService;
        }

        [HttpGet]
        [Route("/Edu/CourseCategories/Index")]
        [UserAccess(eAccessControl.CourseCategoriesIndex, eAccessType.None, 1)]
        [Menu(eMenu.CourseCategories, eModule.OnlineTraining, 2)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, int id = 0)
        {
            if (id == 0)
                return View();

            var model = await _courseCategoryService.GetByIDAsync(cancellationToken, id);

            return View(_mapper.Map<CourseCategoryEditModel>(model));
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/CourseCategories/Index")]
        [UserAccess(Common.Enums.eAccessControl.CourseCategoriesIndex2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, CourseCategory model)
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

                var insert = await _courseCategoryService.InsertAsync(model, cancellationToken);

                if (insert != null)
                {
                    TempData["success"] = "با موفقیت افزوده شد";

                    return RedirectToAction("Index");
                }

                TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

                return View(model);
            }

            var update = await _courseCategoryService.UpdateAsync(model, cancellationToken);

            if (update != null)
            {
                TempData["success"] = "با موفقیت بروز رسانی شد";

                return RedirectToAction("Index");
            }

            TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

            return View(model);
        }

        [Route("/Edu/CourseCategories/List")]
        [UserAccess(Common.Enums.eAccessControl.CourseCategoriesList, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> List(int page)
        {
            var model = await _courseCategoryService
                .GetAllPaginatedAsync(new GenericPagingParameters
                {
                    PageNumber = page,
                    PageSize = 10
                }, q => q.IsActive);

            return PartialView("List", model);
        }

        [Route("/Edu/CourseCategories/Remove")]
        [UserAccess(Common.Enums.eAccessControl.CourseCategoriesDelete, Common.Enums.eAccessType.None, 1)]
        public async Task<string> Delete(CancellationToken cancellationToken, int id)
        {
            var entity = await _courseCategoryService.GetByIDAsync(cancellationToken, id);

            if (entity == null)
                return "آیتم مورد نظر یافت نشد";

            if (await _courseCategoryService.DeleteAsync(entity, cancellationToken))
                return "done";

            return "خطا در زمان حذف داده";
        }
    }
}