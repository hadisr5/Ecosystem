using System;
using AutoMapper;
using Seventy.Data;
using System.Threading;
using Seventy.Service.Users;
using Seventy.ViewModel.EDU;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Seventy.Service.EDU.Course;
using Seventy.Service.EDU.RelatedCourses;
using Seventy.WebFramework.Filters;
using Seventy.Common.Enums;

// ReSharper disable Mvc.ViewNotResolved
// ReSharper disable Mvc.PartialViewNotResolved

namespace Seventy.Web.Areas.Edu.RelatedCourses
{
    [Area("Edu")]
    public class RelatedCoursesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;
        private static ICourseService _courseService;
        private readonly IRelatedCoursesService _relatedCoursesService;

        public RelatedCoursesController(IUserManager userManager, IMapper mapper,
            IRelatedCoursesService relatedCoursesService, ICourseService courseService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _courseService = courseService;
            _relatedCoursesService = relatedCoursesService;
        }

        [HttpGet]
        [Route("/Edu/RelatedCourses/Index")]
        [UserAccess(Common.Enums.eAccessControl.RelatedCoursesIndex, Common.Enums.eAccessType.None, 1)]
        [Menu(eMenu.RelatedCourses, eModule.OnlineTraining, 11)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, int id = 0)
        {
            if (id == 0)
                return View();

            var model = await _relatedCoursesService.GetByIDAsync(cancellationToken, id);

            return View(_mapper.Map<RelatedCoursesEditModel>(model));
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/RelatedCourses/Index")]
        [UserAccess(Common.Enums.eAccessControl.RelatedCoursesIndex2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, DomainClass.EDU.Course.RelatedCourses model)
        {
            try
            {

                if (!ModelState.IsValid || model.FirstCourseID == 0 || model.SecondCourseID == 0)
                {
                    TempData["err"] = "لطفا در پر کردن فرم دقت نمایید ، فرم صحیح پر نشده است";

                    return View(_mapper.Map<RelatedCoursesEditModel>(model));
                }

                if (model.FirstCourseID == model.SecondCourseID)
                {
                    TempData["err"] = "دو دوره نمی توانند یکی باشند";

                    return View(_mapper.Map<RelatedCoursesEditModel>(model));
                }

                model.RegUserID = _userManager.GetCurrentUserID();

                if (model.ID == 0 || model.ID == null)
                {
                    model.ID = null;

                    var insert = await _relatedCoursesService.InsertAsync(model, cancellationToken);

                    if (insert != null)
                    {
                        TempData["success"] = "با موفقیت افزوده شد";

                        return RedirectToAction("Index");
                    }

                    TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

                    return View(_mapper.Map<RelatedCoursesEditModel>(model));
                }

                var update = await _relatedCoursesService.UpdateAsync(model, cancellationToken);

                if (update != null)
                {
                    TempData["success"] = "با موفقیت بروز رسانی شد";

                    return RedirectToAction("Index");
                }

                TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";
            }
            catch (Exception e)
            {

                TempData["err"] = e.Message;
            }

            return View(_mapper.Map<RelatedCoursesEditModel>(model));
        }

        [Route("/Edu/RelatedCourses/List")]
        [UserAccess(Common.Enums.eAccessControl.RelatedCoursesList, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> List(int page)
        {
            var model = await _relatedCoursesService
                .GetAllPaginatedAsync(new GenericPagingParameters
                {
                    PageNumber = page,
                    PageSize = 10
                }, q => q.IsActive);

            return PartialView("List", model);
        }

        [Route("/Edu/RelatedCourses/Remove")]
        [UserAccess(Common.Enums.eAccessControl.RelatedCoursesDelete, Common.Enums.eAccessType.None, 1)]
        public async Task<string> Delete(CancellationToken cancellationToken, int id)
        {
            var entity = await _relatedCoursesService.GetByIDAsync(cancellationToken, id);

            if (entity == null)
                return "آیتم مورد نظر یافت نشد";

            if (await _relatedCoursesService.DeleteAsync(entity, cancellationToken))
                return "done";

            return "خطا در زمان حذف داده";
        }
        [UserAccess(Common.Enums.eAccessControl.RelatedCoursesGetAllCourse, Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<DomainClass.EDU.Course.Course> GetAllCourse()
        {
            return _courseService.TableNoTracking();
        }
    }
}