using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seventy.Common.Enums;
using Seventy.Data;
using Seventy.DomainClass.EDU.Course;
using Seventy.Repository.Core;
using Seventy.Service.Core.Files;
using Seventy.Service.EDU.Course;
using Seventy.Service.EDU.UserCourseGroup;
using Seventy.Service.Users;
using Seventy.ViewModel.EDU;
using Seventy.ViewModel.EDU.Course;
using Seventy.Web.Areas.Edu.AnswerQuestion;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Areas.Edu.CourseRegistration
{
    [Area("Edu")]
    [Authorize(Policy = "user")]
    public class CourseRegistrationController : Controller
    {
        #region Field
        private static IUserManager _userManager;
        private IUnitOfWork _uow;
        private ICourseRegistrationService _courseRegistrationService;    
        private ICourseService _courseService;
        private IMapper _mapper;

        private static IFilesService _FileService;
        #endregion

        public CourseRegistrationController(IUnitOfWork uow
            , ICourseRegistrationService courseRegistrationService
             , ICourseService courseService
            , IMapper mapper
            , IFilesService fileService
            , IUserManager userManager)
        {
            _userManager = userManager;
            _uow = uow;
            _courseRegistrationService = courseRegistrationService;
            _courseService = courseService;
            _mapper = mapper;
            _FileService = fileService;
        }
        [HttpGet]
        [Route("/Edu/CourseRegistration/Index")]
        [UserAccess(eAccessControl.ShowCourseView, eAccessType.View, 1)]
        [Menu(eMenu.RegisterInCourse, eModule.OnlineTraining, 1)]
        public async Task<IActionResult> Index(int page = 1)
        {
            return View(new CourseRegistrationEditModel());
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/CourseRegistration/Index")]
        [UserAccess(eAccessControl.CreateCourse, eAccessType.None, 2)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, CourseRegistrationEditModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["err"] = "model is not valid";

                return View(model);
            }
            try
            {
                model.RegUserID = _userManager.GetCurrentUserID();

                var courseRegistration = _mapper.Map<DomainClass.EDU.Course.CourseRegistration>(model);
                if (model.ID == 0 || model.ID == null)
                {
                    model.ID = null;

                    var insert = await _courseRegistrationService.InsertAsync(courseRegistration, cancellationToken);

                    if (insert != null)
                    {
                        TempData["success"] = "با موفقیت افزوده شد";
                        //Todo -> if mustPay>0 -> go to bank port 
                        var mustPay = await _courseRegistrationService.CheckForRegistration(insert);
                        return RedirectToAction("Index");
                    }

                    TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";
                    return View(model);
                }
            }
            catch
            {
                TempData["err"] = "model is not valid";
                return View(model);
            }

            TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";
            return View(model);
        }

        [Route("/edu/CourseRegistration/List")]
        [UserAccess(eAccessControl.GetCourseList, eAccessType.None, 3)]
        public async Task<IActionResult> List(int page = 1)
        {
            var model = await _courseService.GetCustomCourseAsync(CourseEnum.All,
              new GenericPagingParameters
              {
                  PageNumber = page,
                  PageSize = 10
              }, null,
              a => a.OrderByDescending(b => b.ID));
            return PartialView("List", model);
        }

        [HttpGet]
        [Route("Edu/CourseRegistration/PriceSum")]
        [UserAccess(eAccessControl.CoursePriceSum, eAccessType.None, 4)]
        public async Task<decimal> PriceSum(CancellationToken cancellationToken, CourseRegistrationEditModel model)
        {
            decimal price = 0;
            try
            {
                if (model.CourseID > 0)
                {
                    price += (await _uow.Course.GetByIDAsync(cancellationToken, model.CourseID))?.Price ?? 0;
                }
                if (model.CateringPackId > 0)
                {
                    price += (await _uow.CateringPackage.GetByIDAsync(cancellationToken, model.CateringPackId))?.Price ?? 0;
                }
                if (int.TryParse(model.CertificateType, out int certificateId) && certificateId > 0)
                {
                    price += (await _uow.Certificate.GetByIDAsync(cancellationToken, certificateId))?.Price ?? 0;
                }
            }
            catch { }

            return price;
        }

        [HttpGet]
        [Route("Edu/CourseRegistration/RequiredDocumentsList")]
        [UserAccess(eAccessControl.CourseRequiredDocumentsList, eAccessType.None, 5)]
        public async Task<string> RequiredDocumentsList(CancellationToken cancellationToken, CourseRegistrationEditModel model)
        {
            string RequiredDocuments = "";
            try
            {
                if (model.CourseID > 0)
                {       
                    var curCourse = await _courseService.GetByIDAsync(cancellationToken, model.CourseID);
                    RequiredDocuments = curCourse.RequiredDocuments;
                }
                else
                {
                    RequiredDocuments = "";
                }
            }
            catch { }

            return RequiredDocuments;
        }
    }
}