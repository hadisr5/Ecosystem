using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seventy.Common.Enums;
using Seventy.Service.EDU.CateringPackage;
using Seventy.Service.Users;
using Seventy.ViewModel.EDU;
using Seventy.Web.Areas.Edu.AnswerQuestion;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Areas.Edu.CateringPackage
{
    [Area("Edu")]
    [Authorize(Policy = "user")]
    public class CateringPackageController : Controller
    {
        #region Field
        private static IUserManager _um;
        private static ICateringPackageService _cps;
        private static IMapper _mapper;
        #endregion

        #region CTOR
        public CateringPackageController(ICateringPackageService cps, IMapper mapper,
            IUserManager um)
        {
            _um = um;
            _cps = cps;
            _mapper = mapper;
        }
        #endregion

        #region AddCateringPackage

        #region AddCateringPackage = Get

        [HttpGet]
        [Route("/Edu/AddCateringPackage")]
        [UserAccess(Common.Enums.eAccessControl.ShowCateringPackage,Common.Enums.eAccessType.View,1)]
        [Menu(eMenu.AddCateringPackage, eModule.OnlineTraining, 9)]
        public async Task<IActionResult> AddCateringPackage(CancellationToken cancellationToken, int? id)
        {
            //edit
            if (id != null)
            {
                ViewBag.ID = id;
                var curCourse = await _cps.GetByIDAsync(cancellationToken, id);
                return View(_mapper.Map<CateringPackageViewModel>(curCourse));
            }
            else
            {
                ViewBag.ID = null;
            }

            return View();

        }
        #endregion

        #region AddCateringPackage = Post
        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/AddCateringPackage")]
        [UserAccess(Common.Enums.eAccessControl.AddOrUpdateCateringPackage, Common.Enums.eAccessType.None, 2)]
        public async Task<IActionResult> AddCateringPackage(DomainClass.EDU.CateringPackage model, CancellationToken cancellationToken)
        {
            var model2 = _mapper.Map<CateringPackageViewModel>(model);
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.error = "لطفا تمام فیلدها را تکمیل کنید";
                    return View(model2);
                }

                var curUser = await _um.GetCurrentUserAsync(cancellationToken);
                if (model.ID == null)
                {
                    model.RegUserID = curUser.ID;
                    model.RegDate = DateTime.Now;

                    var ent = await _cps.InsertAsync(model, cancellationToken);
                    if (ent != null)
                    {
                        ViewBag.success = "با موفقیت ذخیره شد";
                        return View();
                    }
                    else
                    {
                        ViewBag.error = "در هنگام ذخیره ، خطایی رخ داده است";
                        return View(model2);
                    }
                }

                //Edit Hastesh
                else
                {
                    var exist = await _cps.GetByIDAsync(cancellationToken, model.ID);
                    if (exist == null)
                    {
                        ViewBag.error = "ردیف انتخابی شما در پایگاه داده یافت نشد";
                        return View(model2);
                    }

                    exist.Title = model.Title;
                    exist.Price = model.Price;
                    exist.Description = model.Description;
                    exist.RegUserID = curUser.ID;
                    var result = await _cps.UpdateAsync(exist, cancellationToken);
                    if (result != null)
                    {
                        ViewBag.success = "عملیات بروزرسانی با موفقیت انجام شد";
                        return View();
                    }
                    else
                    {
                        ViewBag.error = "در هنگام به روز رسانی ، خطایی رخ داده است";
                        //ModelState.AddModelError("PrimaryCat", "در هنگام به روز رسانی ، خطایی رخ داده است");
                        return View(model2);
                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return View(model2);

        }
        #endregion

        #region GetAllCateringPackage  = GetAllPaginatedAsync
        [Route("/Edu/GetAllCateringPackage")]
        [UserAccess(Common.Enums.eAccessControl.GetAllCateringPackage, Common.Enums.eAccessType.None, 3)]
        public async Task<string> GetAllCateringPackage(int pageNo, int pageSize = 10)
        {
            var all = await _cps.GetAllPaginatedAsync(new Data.GenericPagingParameters()
            {
                PageNumber = pageNo,
                PageSize = pageSize
            });
            if (all == null) return "";
            var CateringPackages = all.Where(q => q.IsActive == true).OrderByDescending(q => q.ID);

            var res = "";
            res += all.TotalPages + "_%_";
            foreach (var item in CateringPackages)
            {
                if (res.Length > 0)
                    res += "_$_";
                res += item.ID + "_|_";
                res += item.Title + "_|_";
                //res += item.Price + "_|_";
                res += item.Description + "_|_";

            }
            return res;
        }
        #endregion

        #region RemoveCateringPackage
        [HttpPost]
        [Route("/Edu/RemoveCateringPackage")]
        [UserAccess(Common.Enums.eAccessControl.RemoveCateringPackage, Common.Enums.eAccessType.None, 4)]
        public async Task<string> RemoveCateringPackage(int entityId, CancellationToken cancellationToken)
        {
            var curEntity = await _cps.GetByIDAsync(cancellationToken, entityId);
            if (curEntity == null)
                return "این موجودی از قبل حذف شده است";
            var curUser = await _um.GetCurrentUserAsync(cancellationToken);
            if (curEntity.RegUserID != curUser.ID)
                return "این موجودی توسط شما ساخته نشده است";

            curEntity.IsActive = false;
            var result = await _cps.UpdateAsync(curEntity, cancellationToken);
            if (result != null)
                return "done";
            else
                return "حذف با مشکل مواجه شد";
        }
        #endregion

        #endregion

    }
}
