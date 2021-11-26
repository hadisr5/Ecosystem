using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Seventy.DomainClass.EDU;
using Seventy.Service.Core.Files;
using Seventy.Service.EDU.Course;
using Seventy.Service.EDU.CourseCategory;
using Seventy.Service.Users;
using Seventy.ViewModel.Core;
using Seventy.ViewModel.EDU;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;
using Seventy.DomainClass.Core;
using Seventy.Service.EDU.CourseGroup;
using Seventy.Service.EDU.TrainingContent;
using System.Threading;
using Seventy.Service.EDU.UserCourseGroup;
using Seventy.Service.EDU.Lesson;
using Seventy.Data;
using Seventy.ViewModel.EDU.Course;
using Seventy.Web.Areas.Edu.AnswerQuestion;
using Seventy.WebFramework.Filters;
using Seventy.Common.Enums;

namespace Seventy.Web.Areas.Edu.Course
{
    [Area("Edu")]
    [Authorize(Policy = "user")]
    public class CourseController : Controller
    {

        #region Field
        private static IUserManager _um;
        private static ICourseService _cs;
        private static ICourseCategoryService _ccs;
        private static ICourseGroupsService _cgs;
        private static ITrainingContentService _tcs;
        private static IMapper _mapper;
        private static IFilesService _filesService;
        private static ICourseRegistrationService _courseRegistrationService;
        private readonly ILessonService _lessonService;

        private static int? _userId;
        #endregion

        #region CTOR
        public CourseController(ICourseService cs, ICourseCategoryService ccs, IUserManager um, IMapper mapper,
            IFilesService FileService, ITrainingContentService tcs, ICourseGroupsService cgs
            , ICourseRegistrationService courseRegistrationService, ILessonService lessonService)
        {
            _cs = cs;
            _ccs = ccs;
            _um = um;
            _mapper = mapper;
            _filesService = FileService;
            _tcs = tcs;
            _cgs = cgs;
            _courseRegistrationService = courseRegistrationService;
            _lessonService = lessonService;

            _userId = _um.GetCurrentUserID();
        }
        #endregion

        #region GetCourceNamebyId
        [UserAccess(Common.Enums.eAccessControl.CourseGetCourceNamebyId, Common.Enums.eAccessType.None, 1)]
        public static async Task<string> GetCourceNamebyId(int id, CancellationToken cancellationToken)
        {
            var find = await _cs.GetByIDAsync(cancellationToken, id);
            return find != null ? find.Title : "یافت نشد";
        }
        #endregion

        #region GetAllCource

        [UserAccess(Common.Enums.eAccessControl.CourseGetAllCourse, Common.Enums.eAccessType.None, 1)]
        public static async Task<IEnumerable<DomainClass.EDU.Course.Course>> GetAllCourse(CancellationToken cancellationToken)
        {
            var curUser = await _um.GetCurrentUserAsync(cancellationToken);
            var all = _cs.TableNoTracking();
            var filter = all.Where(q => q.RegUserID == curUser.ID && q.IsActive == true).OrderByDescending(ord => ord.ID).ToList();
            return filter;
        }
        #endregion

        #region GetAllCourceCategory
        [UserAccess(Common.Enums.eAccessControl.CourseGetAllCourseCategory, Common.Enums.eAccessType.None, 1)]
        public  async Task<IEnumerable<DomainClass.EDU.Course.CourseCategory>> GetAllCourceCategory(CancellationToken cancellationToken)
        {
            var curUser = await _um.GetCurrentUserAsync(cancellationToken);
            var all = _ccs.TableNoTracking();
            var filter = all.Where(q => q.IsActive).OrderByDescending(ord => ord.ID).ToList();
            return filter;
        }
        #endregion

        #region GetCourceCategoryNamebyId
        [UserAccess(Common.Enums.eAccessControl.CourseGetCourceCategoryNamebyId, Common.Enums.eAccessType.None, 1)]
        public static async Task<string> GetCourceCategoryNamebyId(int id, CancellationToken cancellationToken)
        {
            var find = await _ccs.GetByIDAsync(cancellationToken, id);
            return find != null ? find.PrimaryCat : "یافت نشد";
        }
        #endregion

        #region Old Code
        #region Course = Get
        [HttpGet]
        [Route("/Edu/Course")]
        [UserAccess(Common.Enums.eAccessControl.CourseCourse, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Course(CancellationToken cancellationToken, int id = 0)
        {


            //edit
            if (id != 0)
            {
                ViewBag.ID = id;
                var curCourse = await _cs.GetByIDAsync(cancellationToken, id);
                return View(_mapper.Map<CourseViewModel>(curCourse));
            }
            else
            {
                ViewBag.ID = 0;
            }
            return View();
        }
        #endregion

        #region Course = HttpPost
        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/Course")]
        [UserAccess(Common.Enums.eAccessControl.CourseCourse2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Course(DomainClass.EDU.Course.Course model, IFormFile photo, CancellationToken cancellationToken)
        {
            var model2 = _mapper.Map<CourseViewModel>(model);
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("PrimaryCat", "Model is not Valid");
                return View(model2);
            }
            var curUser = await _um.GetCurrentUserAsync(cancellationToken);

            if (photo != null)
            {
                FileInfo fi = new FileInfo(photo.FileName);
                var uploaded = await _filesService.UploadFileAsync(new FilesViewModel()
                {
                    UploadFile = photo,
                    UserID = curUser.ID.Value,
                    Title = fi.Name,
                    Description = fi.FullName,
                }, cancellationToken);
                //model.PhotoFileID = uploaded.ResultEntity.Entity.ID;
            }

            if (model.ID == 0)
            {
                model.RegUserID = curUser.ID;

                var ent = await _cs.InsertAsync(model, cancellationToken);
                if (ent != null)
                {
                    TempData["success"] = "با موفقیت ذخیره شد";
                    return View();
                }
                else
                {
                    ModelState.AddModelError("PrimaryCat", "در هنگام ذخیره ، خطایی رخ داده است");
                    return View(model2);
                }
            }
            else
            {
                var exist = await _cs.GetByIDAsync(cancellationToken, model.ID);
                if (exist == null)
                {
                    ModelState.AddModelError("PrimaryCat", "ردیف انتخابی شما در پایگاه داده یافت نشد");
                    return View(model2);
                }

                exist.Description = model.Description;
                exist.Title = model.Title;
                exist.RegUserID = curUser.ID;
                if (photo != null)
                    exist.PhotoFileID = model.PhotoFileID;
                exist.Price = model.Price;
                exist.PublishState = model.PublishState;
                exist.Achievements = model.Achievements;
                exist.CategoryID = model.CategoryID;
                exist.Description = model.Description;
                exist.Duration = model.Duration;
                //  exist.EndDate = model.EndDate;
                exist.HozoriType = model.HozoriType;
                exist.RequiredDocuments = model.RequiredDocuments;
                //exist.StartDate = model.StartDate;
                var result = await _cs.UpdateAsync(exist, cancellationToken);
                if (result != null)
                {
                    TempData["success"] = "با موفقیت به روز رسانی شد";
                    return View();
                }
                else
                {
                    ModelState.AddModelError("PrimaryCat", "در هنگام به روز رسانی ، خطایی رخ داده است");
                    return View(model2);
                }
            }
        }
        #endregion

        #endregion

        #region AddCourse

        #region AddCourse = Get
        //ثبت دوره آموزشی
        [HttpGet]
        [Route("/Edu/AddCourse")]
        [UserAccess(Common.Enums.eAccessControl.CourseAddCourse , Common.Enums.eAccessType.None, 1)]
        [Menu(eMenu.CourseManagement, eModule.OnlineTraining, 1)]
        public async Task<IActionResult> AddCourse(CancellationToken cancellationToken, int? id)
        {
            //edit
            if (id != null)
            {
                ViewBag.ID = id;
                var curCourse = await _cs.GetByIDAsync(cancellationToken, id);
                var RequiredDocumentsList = curCourse.RequiredDocuments;
                ViewBag.RequiredDocumentsList = RequiredDocumentsList;
                
                return View(_mapper.Map<CourseViewModel>(curCourse));
            }
            else
            {
                ViewBag.ID = null;
            }

            return View();

        }
        #endregion

        #region AddCourse = Post
        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/AddCourse")]
        [UserAccess(Common.Enums.eAccessControl.CourseAddCourse2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> AddCourse(DomainClass.EDU.Course.Course model, List<string> RequiredDocumentsList, CancellationToken cancellationToken)
        {
            var model2 = _mapper.Map<CourseViewModel>(model);

            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["success"] = "لطفا تمام فیلدها را تکمیل کنید";
                    return View(model2);
                }

                var curUser = await _um.GetCurrentUserAsync(cancellationToken);

               
                if (model.ID == null)
                {
                    model.RegUserID = curUser.ID;
                    model.HozoriType = "HozoriType";
                    RequiredDocumentsList.Remove(RequiredDocumentsList.Last());
                    model.RequiredDocuments = string.Join(",", RequiredDocumentsList);
                    var ent = await _cs.InsertAsync(model, cancellationToken);
                    if (ent != null)
                    {
                        TempData["success"] = "با موفقیت ذخیره شد";
                        return View(model2);
                    }
                    else
                    {
                        TempData["err"] = "در هنگام ذخیره ، خطایی رخ داده است";
                        return View(model2);
                    }
                }

                else
                {
                    var exist = await _cs.GetByIDAsync(cancellationToken, model.ID);
                    if (exist == null)
                    {
                        TempData["err"] = "ردیف انتخابی شما در پایگاه داده یافت نشد";
                        return View(model2);
                    }

                    exist.Description = model.Description;
                    exist.Title = model.Title;
                    exist.RegUserID = curUser.ID;

                    exist.Price = model.Price;
                    exist.PublishState = model.PublishState;
                    exist.Achievements = model.Achievements;
                    exist.CategoryID = model.CategoryID;
                    exist.Description = model.Description;
                    exist.Duration = model.Duration;
                    //  exist.EndDate = model.EndDate;
                    exist.HozoriType = "HozoriType ";
                    RequiredDocumentsList.Remove(RequiredDocumentsList.Last());
                    exist.RequiredDocuments = string.Join(",", RequiredDocumentsList);
                    //exist.StartDate = model.StartDate;
                    var result = await _cs.UpdateAsync(exist, cancellationToken);
                    if (result != null)
                    {
                        TempData["success"] = "عملیات بروزرسانی با موفقیت انجام شد";
                        return View(model2);
                    }
                    else
                    {
                        TempData["err"] = "در هنگام به روز رسانی ، خطایی رخ داده است";
                        return View(model2);
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["err"] = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return View(model2);
            }

        }
        #endregion

        #region GetAllCourse  = GetAllPaginatedAsync
        [Route("/Edu/getAllCourse")]
        [UserAccess(Common.Enums.eAccessControl.CourseGetAllCourse2, Common.Enums.eAccessType.None, 1)]
        public async Task<string> GetAllCourse(CancellationToken cancellationToken,int pageNo, int pageSize = 5)
        {

            var all = await _cs.GetAllPaginatedAsync(new Data.GenericPagingParameters()
            {
                PageNumber = pageNo,
                PageSize = pageSize
            }, q => q.IsActive, (IQueryable<DomainClass.EDU.Course.Course> a) => a.OrderByDescending(x => x.ID));

            if (all == null) return "";
            //var courses = all.Where(q => q.IsActive == true).OrderByDescending(x => x.ID).ToList();

            var res = "";
            res += all.TotalPages + "_%_";
            foreach (var item in all)
            {
                if (res.Length > 0)
                    res += "_$_";
                res += item.ID + "_|_";
                res += item.CourseType + "_|_";
                res += item.Title + "_|_";
                res += item.Duration + "_|_";
                res += item.PublishState + "_|_";
                res +=await GetCourceCategoryNamebyId(item.CategoryID, cancellationToken) + "_|_";
                res += string.Format("{0:#,0}", item.Price) + " تومان";
            }
            return res;
        }
        #endregion

        #region RemoveCourseController
        [HttpPost]
        [Route("/Edu/RemoveCourse")]
        [UserAccess(Common.Enums.eAccessControl.CourseRemoveCourseController, Common.Enums.eAccessType.None, 1)]
        public async Task<string> RemoveCourseController(int entityId, CancellationToken cancellationToken)
        {
            var curEntity = await _cs.GetByIDAsync(cancellationToken, entityId);
            if (curEntity == null)
                return "این موجودی از قبل حذف شده است";
            var curUser = await _um.GetCurrentUserAsync(cancellationToken);
            if (curEntity.RegUserID != curUser.ID)
                return "این موجودی توسط شما ساخته نشده است";

            curEntity.IsActive = false;
            var result = await _cs.UpdateAsync(curEntity, cancellationToken);
            if (result != null)
                return "done";
            else
                return "حذف با مشکل مواجه شد";
        }
        #endregion

        #endregion

        #region AddCourseGroups

        #region AddCourseGroups = HttpGet
        [HttpGet]
        [Route("/Edu/AddCourseGroups")]
        [UserAccess(Common.Enums.eAccessControl.CourseAddCourseGroups, Common.Enums.eAccessType.None, 1)]
        [Menu(eMenu.CourseGroupManagement, eModule.OnlineTraining, 3)]
        public async Task<IActionResult> AddCourseGroups(CancellationToken cancellationToken, int? id)
        {

            //edit
            if (id != null)
            {
                ViewBag.ID = id;
                var curCourse = await _cgs.GetByIDAsync(cancellationToken, id);
                return View(_mapper.Map<CourseGroupsViewModel>(curCourse));
            }
            else
            {
                ViewBag.ID = null;
            }

            return View();

        }

        #endregion

        #region AddCourseGroups = HttpPost
        [HttpPost, ValidateAntiForgeryToken]
        [Route("/Edu/AddCourseGroups")]
        [UserAccess(Common.Enums.eAccessControl.CourseAddCourseGroups2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> AddCourseGroups(DomainClass.EDU.Course.CourseGroups model, CancellationToken cancellationToken)
        {
            var model2 = _mapper.Map<CourseGroupsViewModel>(model);

            try
            {

                if (!ModelState.IsValid)
                {
                    TempData["err"] = "لطفا تمام فیلدها را تکمیل کنید";
                    return View(model2);
                }
                var curUser = await _um.GetCurrentUserAsync(cancellationToken);
                if (model.ID == null)
                {
                    model.RegUserID = curUser.ID;
                    model.RegDate = DateTime.Now;

                    var ent = await _cgs.InsertAsync(model, cancellationToken);
                    if (ent != null)
                    {
                        TempData["success"] = "با موفقیت ذخیره شد";
                        return View();
                    }
                    else
                    {
                        TempData["err"] = "در هنگام ذخیره ، خطایی رخ داده است";
                        return View(model2);
                    }
                }
                else
                {
                    var exist = await _cgs.GetByIDAsync(cancellationToken, model.ID);
                    if (exist == null)
                    {
                        TempData["err"] = "ردیف انتخابی شما در پایگاه داده یافت نشد";
                        return View(model2);
                    }

                    exist.Title = model.Title;
                    exist.StartDate = model.StartDate;
                    exist.EndDate = model.EndDate;
                    exist.Description = model.Description;
                    exist.Capacity = model.Capacity;
                    exist.CourseID = model.CourseID;
                    var result = await _cgs.UpdateAsync(exist, cancellationToken);
                    if (result != null)
                    {
                        TempData["success"] = "عملیات بروزرسانی با موفقیت انجام شد";
                        return View();
                    }
                    else
                    {
                        TempData["err"] = "در هنگام به روز رسانی ، خطایی رخ داده است";
                        return View(model2);
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["err"] = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return View(model2);
        }

        #endregion

        #region GetAllCourseGroups  = GetAllPaginatedAsync
        [HttpPost]
        [Route("/Edu/GetAllCourseGroups")]
        [UserAccess(Common.Enums.eAccessControl.CourseGetAllCourseGroups, Common.Enums.eAccessType.None, 1)]
        public async Task<string> GetAllCourseGroups(int pageNo, int pageSize = 5)
        {
            var all = await _cgs.GetAllPaginatedAsync(new Data.GenericPagingParameters()
            {
                PageNumber = pageNo,
                PageSize = pageSize
            });
            if (all == null) return "";
            var CourseGroups = all.Where(q => q.IsActive == true).OrderByDescending(q => q.ID);

            var res = "";
            res += all.TotalPages + "_%_";
            foreach (var item in CourseGroups)
            {
                if (res.Length > 0)
                    res += "_$_";
                res += item.ID + "_|_";
                res += item.Title + "_|_";
                res += item.StartDate.ToShortDateString() + "_|_";
                res += item.EndDate.ToShortDateString() + "_|_";
                res += item.Capacity + "_|_";
                res += item.CourseName + "_|_";


                //res += item.EndDate.Year +"/"+ item.EndDate.Month+"/"+ item.EndDate.Day + "_|_";
                //res += item.StartDate.Year +"/"+ item.StartDate.Month+"/"+ item.StartDate.Day + "_|_";
            }
            return res;
        }
        #endregion

        #region RemoveCourseGroups
        [HttpPost]
        [UserAccess(Common.Enums.eAccessControl.CourseRemoveCourseGroups, Common.Enums.eAccessType.None, 1)]
        public async Task<string> RemoveCourseGroups(int entityId, CancellationToken cancellationToken)
        {
            var curEntity = await _cgs.GetByIDAsync(cancellationToken,entityId);
            if (curEntity == null)
                return "این موجودی از قبل حذف شده است";
            var curUser = await _um.GetCurrentUserAsync(cancellationToken);
            if (curEntity.RegUserID != curUser.ID)
                return "این موجودی توسط شما ساخته نشده است";

            curEntity.IsActive = false;
            var result = await _cgs.UpdateAsync(curEntity, cancellationToken);
            if (result != null)
                return "done";
            else
                return "حذف با مشکل مواجه شد";
        }
        #endregion

        #endregion


    }
}
