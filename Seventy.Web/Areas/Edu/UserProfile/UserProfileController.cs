using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Seventy.Data;
using Seventy.DomainClass.Core;
using Seventy.Service.BaseService;
using Seventy.Service.Core.Files;
using Seventy.Service.Core.UserProfiles;
using Seventy.Service.EDU.Course;
using Seventy.Service.Users;
using Seventy.ViewModel.Core;
using Seventy.ViewModel.EDU;
using Seventy.Web.Areas.Edu.TeacherLesson;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Areas.Edu.UserProfile
{
    [Area("Edu")]
    [Authorize(Policy = "user")]
    public class UserProfileController : Controller
    {
        #region Field
        private static IUserManager _userManager;
        private static IMapper _mapper;
        private static ICourseService _courseService;
        private static IFilesService _filesService;
        private readonly IUserProfilesService _userProfilesService;
        private static int? _userId;

        #endregion

        #region CTOR

        public UserProfileController(IMapper mapper,
            IUserManager userManager, ICourseService courseService,
            IFilesService fileService,
            IUserProfilesService userProfilesService)

        {
            _userManager = userManager;
            _mapper = mapper;
            _courseService = courseService;
            _userId = _userManager.GetCurrentUserID();
            _filesService = fileService;
            _userProfilesService = userProfilesService;
        }

        #endregion


        #region UserProfile

        #region UserProfile = Get

        [HttpGet]
        [Route("/Edu/UserProfileIndex")]
        [UserAccess(Common.Enums.eAccessControl.UserProfileIndex , Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult>
            UserProfileIndex(CancellationToken ct, int? id)
        {

            ViewBag.CurrentCancellationToken = ct;
            
            if (_userId.HasValue==false)
            {
                throw new Exception("کاربر وارد نشده است");
            }
            
      
            
            var userProfiles = await _userProfilesService.GetAllPaginatedAsync(new GenericPagingParameters
                {
                    PageSize = 10
                },
                profiles => profiles.UserID == _userId);


            // پروفایل کاربر را بر میگردانیم
            var currentUserProfile = userProfiles.Where(s=>s.UserID==_userId).SingleOrDefault();
            if (currentUserProfile == null)
            {
                currentUserProfile = new UserProfiles
                {
                    RegUserID = _userId,
                    UserID = _userId.Value
                };
                currentUserProfile=  await _userProfilesService.InsertAsync(currentUserProfile, ct);
            }


            var viewModel = _mapper.Map(currentUserProfile, new UserProfilesViewModel());
            return View("ProfileIndex",viewModel);
        }

        #endregion

        #region UserProfileIndex = Post

        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/UserProfileIndex")]
        [UserAccess(Common.Enums.eAccessControl.UserProfileSaveUserProfile, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> 
            SaveUserProfile(UserProfilesViewModel viewModel,
                 IFormFile? file,
            CancellationToken ct)
        {
            ViewBag.CurrentCancellationToken = ct;

            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.error = "لطفا تمام فیلدها را تکمیل کنید";
                    return View("ProfileIndex",viewModel);
                }

                var profile = await _userProfilesService.GetByIDAsync(ct, viewModel.ID);
                if (viewModel.ID.HasValue==false 
                    || viewModel.ID==0 
                   )
                {
                    ViewBag.error = "پروفایل یافت نشد";
                    return View("ProfileIndex",viewModel);
                }

                if (profile != null)
                {
                    if (profile.UserID!=_userId)
                    {
                        ViewBag.error = "شما صاحب این پروفایل نیستید";
                        return View("ProfileIndex",viewModel);
                    }
                }
                else
                {
                    profile = new UserProfiles
                    {
                        RegUserID = _userId,
                        UserID = _userId.Value,
                    };
                }

               

                viewModel.ID = profile.ID;
                int? RegUserId = profile.RegUserID;
                int userId = (int)profile.UserID;
                int? PhotoFileId = profile.PhotoFileId;
                
                var model = _mapper.Map(viewModel, profile);

                model.RegUserID = RegUserId;
                model.UserID = userId;
                model.PhotoFileId = PhotoFileId;
                viewModel.PhotoFileID = PhotoFileId;
                
                if (file != null)
                {
                    var fileResult = await _filesService.UploadFileAsync(new FilesViewModel
                    {
                        UploadFile = file,
                        Title = file.FileName,
                        RegUserID = model.RegUserID
                    }, ct);

                    model.PhotoFileId = fileResult.ResultID;
                    viewModel.PhotoFileID = fileResult.ResultID;
                }
                
                await _userProfilesService.UpdateAsync(model, ct);


                ViewBag.success = "تغییرات با موفقیت ذخیره شد";
                return View("ProfileIndex",viewModel);

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return View("ProfileIndex",viewModel);
            }

        }

        #endregion


        #region Form Select Options
        [UserAccess(Common.Enums.eAccessControl.UserProfileGetCountries, Common.Enums.eAccessType.None, 1)]
        public static async Task<SelectList> GetCountries()
        {
            List<dynamic> list = new List<dynamic>
            {
                new {Name = "ایران"  , Id="ایران"}
            };
           return new SelectList(list,"Id","Name");
        }

        [UserAccess(Common.Enums.eAccessControl.UserProfileGetStates, Common.Enums.eAccessType.None, 1)]
        public static async Task<IEnumerable<SelectListItem>> GetStates()
        {
            List<dynamic> list = new List<dynamic>
            {
                new {Name = "آذربایجان شرقی"  , Id="آذربایجان شرقی"},
                new {Name = "آذربایجان غربی"  , Id="آذربایجان غربی"},
                new {Name = "تهران"  , Id="تهران"},
                new {Name = "شیراز"  , Id="شیراز"},
            };
            return new SelectList(list,"Id","Name");
        }
        [UserAccess(Common.Enums.eAccessControl.UserProfileGetMadraks, Common.Enums.eAccessType.None, 1)]
        public static async Task<IEnumerable<SelectListItem>> GetMadraks()
        {
            List<dynamic> list = new List<dynamic>
            {
                new {Name = "ابتدایی"  , Id="ابتدایی"},
                new {Name = "سیکل"  , Id="سیکل"},
                new {Name = "دیپلم"  , Id="دیپلم"},
                new {Name = "فوق دیپلم"  , Id="فوق دیپلم"},
                new {Name = "لیسانس"  , Id="لیسانس"},
                new {Name = "فوق لیسانس"  , Id="فوق لیسانس"},
                new {Name = "دکترا"  , Id="دکترا"},
            };
            return new SelectList(list,"Id","Name");
        }
        [UserAccess(Common.Enums.eAccessControl.UserProfileGetFields, Common.Enums.eAccessType.None, 1)]
        public static async Task<IEnumerable<SelectListItem>> GetFields()
        {
            List<dynamic> list = new List<dynamic>
            {
                new {Name = "مهندسی کامپیوتر"  , Id="مهندسی کامپیوتر"},
                new {Name = "مهندسی فناوری اطلاعات"  , Id="مهندسی فناوری اطلاعات"},
                new {Name = "مهندسی برق"  , Id="مهندسی برق"},
            };
            return new SelectList(list,"Id","Name");
        }
        [UserAccess(Common.Enums.eAccessControl.UserProfileGetUnis, Common.Enums.eAccessType.None, 1)]
        public static async Task<IEnumerable<SelectListItem>> GetUniversities()
        {
            List<dynamic> list = new List<dynamic>
            {
                new {Name = "دانشگاه تهران"  , Id="مهندسی کامپیوتر"},
                new {Name = "مهندسی فناوری اطلاعات"  , Id="مهندسی فناوری اطلاعات"},
                new {Name = "مهندسی برق"  , Id="مهندسی برق"},
            };
            return new SelectList(list,"Id","Name");
        }
        #endregion
        #endregion



        #region Called by Ajax

        [HttpGet]
        [Route("/Edu/GetOstanCities")]
        [UserAccess(Common.Enums.eAccessControl.UserProfileGetOstan, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult>
            GetOstanCities(CancellationToken ct, int ostanId)
        {
            List<dynamic> list = new List<dynamic>
            {
                new {Name = "تبریز"  , Id="تبریز"},
                new {Name = "تهران"  , Id="تهران"},
                new {Name = "مشهد"  , Id="مشهد"},
            };
            return  Json(list);
        }


        #endregion
    }
}