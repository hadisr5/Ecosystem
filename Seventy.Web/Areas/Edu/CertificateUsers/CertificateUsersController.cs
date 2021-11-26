using System.Collections.Generic;
using AutoMapper;
using Seventy.Data;
using System.Threading;
using Seventy.Service.Users;
using System.Threading.Tasks;
using Seventy.DomainClass.EDU;
using Microsoft.AspNetCore.Mvc;
using Seventy.DomainClass.Core;
using Seventy.DomainClass.EDU.Course;
using Seventy.Service.Core.UserProfiles;
using Seventy.Service.EDU.Certificate;
using Seventy.Service.EDU.CertificateUser;
using Seventy.Service.EDU.Course;
using Seventy.Service.EDU.CourseGroup;
using Seventy.ViewModel.EDU.CertificateUser;
using Seventy.WebFramework.Filters;
using Microsoft.AspNetCore.Hosting;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using Seventy.Common.Enums;

namespace Seventy.Web.Areas.Edu.CertificateUsers
{
    [Area("Edu")]
    public class CertificateUsersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;
        private readonly ICertificateUserService _certificateUserService;
        private static ICourseService _courseService;
        private static ICourseGroupsService _courseGroupsService;
        private static ICertificateService _certificateService;
        private static IUserProfilesService _userProfilesService;
        private readonly IHostingEnvironment _hostEnvironment;
        public CertificateUsersController(IMapper mapper, IUserManager userManager,
            ICertificateUserService certificateUserService, IUserProfilesService userProfilesService,
            ICourseService courseService,
            ICourseGroupsService courseGroupsService, ICertificateService certificateService, IHostingEnvironment hostingEnvironment)
        {
            _mapper = mapper;
            _userManager = userManager;
            _certificateUserService = certificateUserService;
            _userProfilesService = userProfilesService;
            _certificateService = certificateService;
            _courseService = courseService;
            _courseGroupsService = courseGroupsService;
            _hostEnvironment = hostingEnvironment;
        }

        [HttpGet]
        [Route("/Edu/CertificateUsers/Index")]
        [UserAccess(Common.Enums.eAccessControl.CertificateUserIndex, Common.Enums.eAccessType.None, 1)]
        [Menu(eMenu.CertificateIssued, eModule.OnlineTraining, 10)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, int id = 0)
        {
            if (id == 0)
                return View();

            var model = await _certificateUserService.GetByIDAsync(cancellationToken, id);

            return View(_mapper.Map<CertificateUserViewModel>(model));
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/CertificateUsers/Index")]
        [UserAccess(Common.Enums.eAccessControl.CertificateUserIndex2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, CertificateUser model)
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

                var insert = await _certificateUserService.InsertAsync(model, cancellationToken);

                if (insert != null)
                {
                    TempData["success"] = "با موفقیت افزوده شد";

                    return RedirectToAction("Index");
                }

                TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

                return View(model);
            }

            var update = await _certificateUserService.UpdateAsync(model, cancellationToken);

            if (update != null)
            {
                TempData["success"] = "با موفقیت بروز رسانی شد";

                return RedirectToAction("Index");
            }

            TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

            return View(model);
        }

        [Route("/Edu/CertificateUsers/List")]
        [UserAccess(Common.Enums.eAccessControl.CertificateUserList, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> List(int page)
        {
            var model = await _certificateUserService
                .GetAllPaginatedAsync(new GenericPagingParameters
                {
                    PageNumber = page,
                    PageSize = 10
                }, q => q.IsActive);

            return PartialView("List", model);
        }

        [Route("/Edu/CertificateUsers/Remove")]
        [UserAccess(Common.Enums.eAccessControl.CertificateUserDelete, Common.Enums.eAccessType.None, 1)]
        public async Task<string> Delete(CancellationToken cancellationToken, int id)
        {
            var entity = await _certificateUserService.GetByIDAsync(cancellationToken, id);

            if (entity == null)
                return "آیتم مورد نظر یافت نشد";

            if (await _certificateUserService.DeleteAsync(entity, cancellationToken))
                return "done";

            return "خطا در زمان حذف داده";
        }

        [UserAccess(Common.Enums.eAccessControl.CertificateUserGetAllUser, Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<UserProfiles> GetAllUser()
        {
            return _userProfilesService.TableNoTracking();
        }

        [UserAccess(Common.Enums.eAccessControl.CertificateUserGetAllCourse, Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<DomainClass.EDU.Course.Course> GetAllCourse()
        {
            return _courseService.TableNoTracking();
        }

        [UserAccess(Common.Enums.eAccessControl.CertificateUserGetAllCourseGroup, Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<CourseGroups> GetAllCourseGroup()
        {
            return _courseGroupsService.TableNoTracking();
        }

        [UserAccess(Common.Enums.eAccessControl.CertificateUserGetAllCertificate, Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<Certificate> GetAllCertificate()
        {
            return _certificateService.TableNoTracking();
        }

        [Route("/Edu/CertificateUsers/Report")]
        [UserAccess(Common.Enums.eAccessControl.CertificateUsersReport, Common.Enums.eAccessType.None, 1)]
        public IActionResult Report()
        {
            return View();
        }

        [Route("/Edu/CertificateUsers/GetReport")]
        [UserAccess(Common.Enums.eAccessControl.CertificateUsersGetReport, Common.Enums.eAccessType.None, 1)]
        public IActionResult GetReport()
        {
            StiReport report = new StiReport();
            report.Load(_hostEnvironment.WebRootPath + "\\Reports\\Reportww.mrt");
            return StiNetCoreViewer.GetReportResult(this, report);
        }

        [Route("/Edu/CertificateUsers/ViewerEvent")]
        [UserAccess(Common.Enums.eAccessControl.CertificateUsersViewerEvent, Common.Enums.eAccessType.None, 1)]
        public IActionResult ViewerEvent()
        {
            return StiNetCoreViewer.ViewerEventResult(this);
        }
    }
}