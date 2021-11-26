using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seventy.Common.Enums;
using Seventy.Service.Core.Documents;
using Seventy.Service.Users;
using Seventy.ViewModel.Core;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Areas.Edu.Documents
{
    [Area("Edu")]
    [Authorize(Policy = "user")]
    public class DocumentsController : Controller
    {
        #region Field
        private static IUserManager _um;
        private static IDocumentsTypeService _ds;
        private static IMapper _mapper;
        #endregion

        #region CTOR
        public DocumentsController(IDocumentsTypeService ds, IMapper mapper,
            IUserManager um)
        {
            _um = um;
            _ds = ds;
            _mapper = mapper;
        }
        #endregion

        #region AddDocuments

        #region AddDocuments = Get

        [HttpGet]
        [Route("/Edu/AddDocuments")]
        [UserAccess(Common.Enums.eAccessControl.DocumentsAddDocuments , Common.Enums.eAccessType.None, 1)]
        [Menu(eMenu.AddDocument, eModule.OnlineTraining, 10)]
        public async Task<IActionResult> AddDocuments(CancellationToken cancellationToken, int id=0)
        {
            if (id == 0)
                return View();

            //For Edit
            var model = await _ds.GetByIDAsync(cancellationToken, id);

            return View(_mapper.Map<DocumentTypeViewModel>(model));

        }
        #endregion

        #region AddDocuments = Post
        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/AddDocuments")]
        [UserAccess(Common.Enums.eAccessControl.DocumentsAddDocuments2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> AddDocuments(CancellationToken cancellationToken, DomainClass.Core.DocumentType model)
        {
            var model2 = _mapper.Map<DocumentTypeViewModel>(model);
            try
            {
                if (!ModelState.IsValid)
                {
                   TempData["err"] = "لطفا تمام فیلدها را تکمیل کنید";
                    return View(model2);
                }

                var curUser = await _um.GetCurrentUserAsync(cancellationToken);
                if (model.ID == 0 || model.ID == null)
                {
                    model.RegUserID = curUser.ID;
                    model.RegDate = DateTime.Now;
                    model.IsActive = true;
                    var ent = await _ds.InsertAsync(model, cancellationToken);
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

                //Edit Hastesh
                else
                {
                    var exist = await _ds.GetByIDAsync(cancellationToken, model.ID);
                    if (exist == null)
                    {
                       TempData["err"] = "ردیف انتخابی شما در پایگاه داده یافت نشد";
                        return View(model2);
                    }

                    exist.Title = model.Title;
                    exist.Description = model.Description;
                    exist.RegUserID = curUser.ID;

                    if (await _ds.UpdateAsync(exist, cancellationToken) != null)
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

        #region GetAllDocuments  = GetAllPaginatedAsync
        [Route("/Edu/GetAllDocuments")]
        [UserAccess(Common.Enums.eAccessControl.DocumentsGetAllDocuments, Common.Enums.eAccessType.None, 1)]
        public async Task<string> GetAllDocuments(int pageNo, int pageSize = 5)
        {
            var all = await _ds.GetAllPaginatedAsync(new Data.GenericPagingParameters()
            {
                PageNumber = pageNo,
                PageSize = pageSize
            }, q => q.IsActive,
                    a => a.OrderByDescending(b => b.ID));

            if (all == null) return "";
            //var model = all.Where(q => q.IsActive == true).OrderByDescending(q => q.ID);

            var res = "";
            res += all.TotalPages + "_%_";
            foreach (var item in all)
            {
                if (res.Length > 0)
                    res += "_$_";
                res += item.ID + "_|_";
                res += item.Title;

            }
            return res;
        }
        #endregion

        #region RemoveDocuments
        [HttpPost]
        [Route("/Edu/RemoveDocuments")]
        [UserAccess(Common.Enums.eAccessControl.DocumentsRemoveDocuments, Common.Enums.eAccessType.None, 1)]
        public async Task<string> RemoveDocuments(CancellationToken cancellationToken, int entityId)
        {
            var curEntity = await _ds.GetByIDAsync(cancellationToken, entityId);
            if (curEntity == null)
                return "این موجودی از قبل حذف شده است";
            var curUser = await _um.GetCurrentUserAsync(cancellationToken);
            if (curEntity.RegUserID != curUser.ID)
                return "این موجودی توسط شما ساخته نشده است";

            curEntity.IsActive = false;
            if (await _ds.UpdateAsync(curEntity, cancellationToken) != null)
                return "done";
            else
                return "حذف با مشکل مواجه شد";
        }
        #endregion

        #endregion
    }
}
