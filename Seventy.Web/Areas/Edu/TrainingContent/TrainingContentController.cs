using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seventy.Common.Enums;
using Seventy.Service.Core.Files;
using Seventy.Service.EDU.Course;
using Seventy.Service.EDU.CourseCategory;
using Seventy.Service.EDU.CourseGroup;
using Seventy.Service.EDU.TrainingContent;
using Seventy.Service.Users;
using Seventy.ViewModel.EDU;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Areas.Edu.TrainingContent
{
    [Area("Edu")]
    [Authorize(Policy = "user")]
    public class TrainingContentController : Controller
    {
        #region Field
        private static IUserManager _um;
        private static ICourseService _cs;
        private static ICourseCategoryService _ccs;
        private static ICourseGroupsService _cgs;
        private static ITrainingContentService _tcs;
        private static IMapper _mapper;
        private static IFilesService _FileService;
        #endregion

        #region CTOR
        public TrainingContentController(ICourseService cs, ICourseCategoryService ccs, IUserManager um, IMapper mapper,
            IFilesService FileService, ITrainingContentService tcs, ICourseGroupsService cgs)
        {
            _cs = cs;
            _ccs = ccs;
            _um = um;
            _mapper = mapper;
            _FileService = FileService;
            _tcs = tcs;
            _cgs = cgs;
        }
        #endregion

        #region AddTrainingContent

        #region AddTrainingContent = Get

        [HttpGet]
        [Route("/Edu/AddTrainingContent")]
        [UserAccess(Common.Enums.eAccessControl.TrainingContentAddTrainingContent , Common.Enums.eAccessType.None, 1)]
        [Menu(eMenu.TrainingContentManagement, eModule.OnlineTraining, 7)]
        public async Task<IActionResult> AddTrainingContent(CancellationToken cancellationToken, int? id)
        {
            //edit
            if (id != null)
            {
                ViewBag.ID = id;
                var curCourse = await _tcs.GetByIDAsync(cancellationToken,id);
                return View(_mapper.Map<TrainingContentViewModel>(curCourse));
            }
            else
            {
                ViewBag.ID = null;
            }

            return View();

        }
        #endregion

        #region AddTrainingContent = Post
        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/AddTrainingContent")]
        [UserAccess(Common.Enums.eAccessControl.TrainingContentAddTrainingContent2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> AddTrainingContent(DomainClass.EDU.TrainingContent.TrainingContent model, CancellationToken cancellationToken)
        {
            var model2 = _mapper.Map<TrainingContentViewModel>(model);
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
                    model.IsActive = true;
                    var ent = await _tcs.InsertAsync(model,cancellationToken);
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
                    var exist = await _tcs.GetByIDAsync(cancellationToken,model.ID);
                    if (exist == null)
                    {
                        TempData["err"] = "ردیف انتخابی شما در پایگاه داده یافت نشد";
                        return View(model2);
                    }

                    exist.Title = model.Title;
                    exist.ContentType = model.ContentType;
                    exist.DemoState = model.DemoState;
                    exist.Achievement = model.Achievement;
                    exist.Description = model.Description;
                    exist.RegUserID = curUser.ID;

                    var result = await _tcs.UpdateAsync(exist, cancellationToken);
                    if (result != null)
                    {
                        TempData["success"] = "عملیات بروزرسانی با موفقیت انجام شد";
                        return View();
                    }
                    else
                    {
                        TempData["err"] = "در هنگام به روز رسانی ، خطایی رخ داده است";
                        //ModelState.AddModelError("PrimaryCat", "در هنگام به روز رسانی ، خطایی رخ داده است");
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

        #region GetAllTrainingContent  = GetAllPaginatedAsync
        [Route("/Edu/GetAllTrainingContent")]
        [UserAccess(Common.Enums.eAccessControl.TrainingContentGetAllTrainingContent, Common.Enums.eAccessType.None, 1)]
        public async Task<string> GetAllTrainingContent(int pageNo, int pageSize = 5)
        {
            var mdoel = await _tcs.GetAllPaginatedAsync(new Data.GenericPagingParameters()
            {
                PageNumber = pageNo,
                PageSize = pageSize
            }, q => q.IsActive,
                   a => a.OrderByDescending(b => b.ID));
            if (mdoel == null) return "";
            //var TrainingContents = all.Where(q => q.IsActive == true).OrderByDescending(q => q.ID);

            var res = "";
            res += mdoel.TotalPages + "_%_";
            foreach (var item in mdoel)
            {
                if (res.Length > 0)
                    res += "_$_";
                res += item.ID + "_|_";
                res += item.Title + "_|_";
                res += item.ContentType + "_|_";
                res += item.DemoState + "_|_";
                res += item.Achievement ;

                //res += item.EndDate.Year +"/"+ item.EndDate.Month+"/"+ item.EndDate.Day + "_|_";
                //res += item.StartDate.Year +"/"+ item.StartDate.Month+"/"+ item.StartDate.Day + "_|_";
            }
            return res;
        }
        #endregion

        #region RemoveTrainingContent
        [HttpPost]
        [Route("/Edu/RemoveTrainingContent")]
        [UserAccess(Common.Enums.eAccessControl.TrainingContentRemoveTrainingContent, Common.Enums.eAccessType.None, 1)]
        public async Task<string> RemoveTrainingContent(int entityId, CancellationToken cancellationToken)
        {
            var curEntity = await _tcs.GetByIDAsync(cancellationToken,entityId);
            if (curEntity == null)
                return "این موجودی از قبل حذف شده است";
            var curUser = await _um.GetCurrentUserAsync(cancellationToken);
            if (curEntity.RegUserID != curUser.ID)
                return "این موجودی توسط شما ساخته نشده است";

            curEntity.IsActive = false;
            var result = await _tcs.UpdateAsync(curEntity, cancellationToken);
            if (result != null)
                return "done";
            else
                return "حذف با مشکل مواجه شد";
        }
        #endregion

        #endregion

        
        
        #region Called by Ajax

        [HttpGet]
        [Route("/Edu/GetMortabetByContentType")]
        [UserAccess(Common.Enums.eAccessControl.TrainingContentGetMortabetByContentType, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult>
            GetMortabetByContentType(CancellationToken ct, string ContentType)
        {
            List<dynamic> list = new List<dynamic>
            {
                new {Name = "محتوای1"  , Id="محتوای1"},
                new {Name = "محتوای2"  , Id="محتوای2"},
                new {Name = "محتوای3"  , Id="محتوای3"},
            };
            return  Json(list);
        }


        #endregion
    }
}
