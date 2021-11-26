using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seventy.Data;
using Seventy.DomainClass.Core;
using Seventy.Service.BaseService;
using Seventy.Service.Core.Files;
using Seventy.Service.Core.UserProfiles;
using Seventy.Service.EDU.Course;
using Seventy.Service.EDU.TeacherLesson;
using Seventy.Service.Users;
using Seventy.ViewModel.EDU;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Areas.Edu.TeacherLesson
{
    [Area("Edu")]
    [Authorize(Policy = "user")]
    public class TeacherLessonController : Controller
    {
        #region Field
        private static IUserManager _userManager;
        private static ITeacherLessonService _teacherLessonService;
        private static IMapper _mapper;
        private static ICourseService _courseService;
        private static IUserProfilesService _ups;
        private static IFilesService _fs;
        private static int? _userId;
        #endregion

        #region CTOR
        public TeacherLessonController(ITeacherLessonService teacherLessonService, IMapper mapper,
            IUserManager userManager, ICourseService courseService, IUserProfilesService ups   ,
                     IFilesService fileService)

        {
            _userManager = userManager;
            _teacherLessonService = teacherLessonService;
            _mapper = mapper;
            _courseService = courseService;
            _ups = ups;
            _userId = _userManager.GetCurrentUserID();
            _fs = fileService;
        }

        #endregion
        [UserAccess(Common.Enums.eAccessControl.TeacherLessonGetAllCourse , Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<DomainClass.EDU.Course.Course> GetAllCourse()
        {
            var getAllCourse = _courseService.TableNoTracking();
            return getAllCourse;
        }
        [UserAccess(Common.Enums.eAccessControl.TeacherLessonGetAllTeacherLesson , Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<DomainClass.EDU.Teacher.TeacherLesson> GetAllTeacherLesson()
        {
            var getAllTeacher = _teacherLessonService.TableNoTracking();
            return getAllTeacher;
        }
        [UserAccess(Common.Enums.eAccessControl.TeacherLessonGetAllUserProfiles, Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<UserProfiles> GetAllUserProfiles()
        {
            var getAlluser = _ups.TableNoTracking();
            return getAlluser;
        }
        [UserAccess(Common.Enums.eAccessControl.TeacherLessonGetAllUser, Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<Users> GetAllUser()
        {
            var getAlluser = _userManager.TableNoTracking();
            return getAlluser;
        }


        #region AddTeacherLesson

        #region AddTeacherLesson = Get

        [HttpGet]
        [Route("/Edu/AddTeacherLesson")]
        [UserAccess(Common.Enums.eAccessControl.TeacherLessonAddTeacherLesson, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> 
            AddTeacherLesson(CancellationToken cancellationToken, int? id)
        {
            if (id == null)
                return View(new TeacherLessonEditModel());

            var model = await _teacherLessonService.GetByIDAsync(cancellationToken, id);

            var viewModel= _mapper.Map(model, new TeacherLessonEditModel());
            return View(viewModel);

        }
        #endregion

        #region AddTeacherLesson = Post
        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/AddTeacherLesson")]
        [UserAccess(Common.Enums.eAccessControl.TeacherLessonAdd, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> AddTeacherLesson(DomainClass.EDU.Teacher.TeacherLesson model, CancellationToken cancellationToken)
        {
            var model2 = _mapper.Map<TeacherLessonViewModel>(model);
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["err"]  = "لطفا تمام فیلدها را تکمیل کنید";
                    return View(model2);
                }

                //var curUser = await _um.GetCurrentUserAsync(cancellationToken);
                //if (model.ID == null)
                //{
                //    model.RegUserID = curUser.ID;
                //    model.RegDate = DateTime.Now;
                //    var ent = await _tls.InsertAsync(model, cancellationToken);
                //    if (ent != null)
                //    {
                //       TempData["success"] = "با موفقیت ذخیره شد";
                //        return View();
                //    }
                //    else
                //    {
                //        TempData["err"]  = "در هنگام ذخیره ، خطایی رخ داده است";
                //        return View(model2);
                //    }
                //}

                ////Edit Hastesh
                //else
                //{
                //    var exist = await _tls.GetByIDAsync(cancellationToken, model.ID);
                //    if (exist == null)
                //    {
                //        TempData["err"]  = "ردیف انتخابی شما در پایگاه داده یافت نشد";
                //        return View(model2);
                //    }

                //    exist.Description = model.Description;
                //    exist.RegUserID = curUser.ID;
                //    var result = await _tls.UpdateAsync(exist, cancellationToken);
                //    if (result != null)
                //    {
                //       TempData["success"] = "عملیات بروزرسانی با موفقیت انجام شد";
                //        return View();
                //    }
                //    else
                //    {
                //        TempData["err"]  = "در هنگام به روز رسانی ، خطایی رخ داده است";
                //        return View(model2);
                //    }
                //}

            }
            catch (Exception ex)
            {
                TempData["err"]  = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return View(model2);

        }
        #endregion

        #region GetAllTeacherLesson = GetAllPaginatedAsync
        [Route("/Edu/GetAllTeacherLesson/List")]
        [UserAccess(Common.Enums.eAccessControl.TeacherLessonList, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> List(int page,int? LessonID,
            int? TeacherID,CancellationToken cancellationToken)
        {

            // اگر پارامتر های جستجو پاس شده باشد ، به متد جستجو می رود
            if (LessonID.HasValue || TeacherID.HasValue)
            {
                return await SearchTeacherLesson(page, LessonID, TeacherID,cancellationToken);
            }
            
            //لیست معمولی 
            var model = await _teacherLessonService
                .GetAllPaginatedAsync(new GenericPagingParameters
                {
                    PageNumber = page,
                    PageSize = 10
                },lesson => lesson.IsActive==true,queryable => queryable.OrderByDescending(q=>q.ID));
            


            return PartialView("List", model);
        }
        #endregion

        #region RemoveTeacherLesson
        [HttpPost]
        [Route("/Edu/RemoveTeacherLesson")]
        [UserAccess(Common.Enums.eAccessControl.TeacherLessonRemoveTeacherLesson, Common.Enums.eAccessType.None, 1)]
        public async Task<string> RemoveTeacherLesson(int entityId, CancellationToken cancellationToken)
        {
            var curEntity = await _teacherLessonService.GetByIDAsync(cancellationToken, entityId);
            if (curEntity == null)
                return "این موجودی از قبل حذف شده است";
            var curUser = await _userManager.GetCurrentUserAsync(cancellationToken);
            if (curEntity.RegUserID != curUser.ID)
                return "این موجودی توسط شما ساخته نشده است";

            curEntity.IsActive = false;
            var result = await _teacherLessonService.UpdateAsync(curEntity, cancellationToken);
            if (result != null)
                return "done";
            else
                return "حذف با مشکل مواجه شد";
        }
        #endregion

        [UserAccess(Common.Enums.eAccessControl.TeacherLessonCheckFile, Common.Enums.eAccessType.None, 1)]
        public static async Task<string> CheckFile(CancellationToken cancellationToken, int? fileId)
        {
            try
            {
                if (fileId == null || _userId == null)
                    return string.Empty;

                var file = await _fs.CheckUserSignUpToContent((int)_userId, (int)fileId, cancellationToken);

                return file.File;
            }
            catch (Exception)
            {
                return null;
            }

        }

        #endregion



        //Jf
        #region Search

        [HttpPost]
        [Route("/Edu/SearchTeacherLesson")]
        [UserAccess(Common.Enums.eAccessControl.TeacherLessonSearchTeacherLesson, Common.Enums.eAccessType.None, 1)]
        public async Task<PartialViewResult> SearchTeacherLesson(int page,int? LessonID,
            int? TeacherID,
            CancellationToken cancellationToken)
        {


            if ( LessonID.HasValue && await _courseService.GetByIDAsync(cancellationToken, LessonID)==null)
            {
                throw new Exception("کد درس ارسالی صحیح نیست");
            }
            
            if (TeacherID.HasValue && await _userManager.GetByIDAsync(cancellationToken, TeacherID)==null)
            {
                throw new Exception("کد معلم ارسالی صحیح نیست");
            }

            
            // در جستجو هیچ فیلدی را انتخاب نکدره است پس همه را برگردان
            if (!LessonID.HasValue && !TeacherID.HasValue)
            {
                var model2 = await _teacherLessonService
                    .GetAllPaginatedAsync(new GenericPagingParameters
                    {
                        PageNumber = page,
                        PageSize = 10
                    },lesson => lesson.IsActive==true);

                return PartialView("List", model2);
            }
            
            
           
            //todo:Need Search Service
            var model = await _teacherLessonService
                .SearchAllPaginatedAsync(new GenericPagingParameters
                {
                    PageNumber = page,
                    PageSize = 10
                },LessonID,TeacherID);


            return PartialView("List", model);

        }
        
        
        
        
        

        #endregion

        #region Save

        [HttpPost]
        [Route("/Edu/TeacherLesson/SaveTeacherLesson")]
        [UserAccess(Common.Enums.eAccessControl.TeacherLessonSaveTeacherLesson, Common.Enums.eAccessType.None, 1)]
        public async Task<ActionResult> SaveTeacherLesson(TeacherLessonEditModel teacherLessonEditModel,
            CancellationToken cancellationToken)
        {
            
            var teacherLesson= _mapper.Map(teacherLessonEditModel, new DomainClass.EDU.Teacher.TeacherLesson());

            if ( await _courseService.GetByIDAsync(cancellationToken, teacherLessonEditModel.LessonID)==null)
            {
                throw new Exception("کد درس ارسالی صحیح نیست");
            }
            
            if ( await _userManager.GetByIDAsync(cancellationToken, teacherLessonEditModel.TeacherID)==null)
            {
                throw new Exception("کد معلم ارسالی صحیح نیست");
            }
            
            await ControllerUtilities.SaveAsync(teacherLesson, _teacherLessonService
                ,_userManager.GetCurrentUserID(),cancellationToken);

            TempData["Success"]="درس جدید با موفقیت ثبت گردید";

            return Ok();

        }

        #endregion
      

    }


    public class ControllerUtilities
    {
       
        
        public static async Task SaveAsync<T,TService>(T model, TService service,int? currentUserId,CancellationToken cancellationToken) 
        where TService: IBaseService<T> where T:  CoreBase
        {

            if (model.ID.HasValue==false || model.ID==0)
            {
                model.RegUserID = currentUserId;
                await service.InsertAsync(model, cancellationToken);
            }
            else
            {
                await service.UpdateAsync(model, cancellationToken);
            }
            
        }
    }
}
