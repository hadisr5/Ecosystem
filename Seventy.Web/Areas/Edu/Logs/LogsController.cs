using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Seventy.DomainClass.Core;
using Seventy.Service.Core.Logs;
using Seventy.Service.Users;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Areas.Edu.Logs
{
    [Area("Edu")]
    [Authorize(Policy = "user")]
    public class LogsController : Controller
    {
        #region Field
        private static IUserManager _um;
        private static IMapper _mapper;
        private static ILogsService _logsService;
        private IHttpContextAccessor _accessor;
        #endregion

        #region CTOR
        public LogsController(IUserManager um, IMapper mapper, ILogsService logsService, IHttpContextAccessor accessor)
        {
            _um = um;
            _mapper = mapper;
            _logsService = logsService;
            _accessor = accessor;

        }
        #endregion

        #region Logs


        #region Logs = Get
        [HttpGet]
        [Route("/Edu/AddLogs")]
        [UserAccess(Common.Enums.eAccessControl.LogsAddLogs, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> AddLogs(CancellationToken cancellationToken, int? id)
        {
            //edit
            if (id != null)
            {
                ViewBag.ID = id;
                var curCourse = await _logsService.GetByIDAsync(cancellationToken, id);
                return View(_mapper.Map<LogsViewModel>(curCourse));
            }
            else
            {
                ViewBag.ID = null;
            }

            return View();

        }
        #endregion

        #region Logs =Post
        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/AddLogs")]
        [UserAccess(Common.Enums.eAccessControl.LogsAddLogs2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> AddLogs(CancellationToken cancellationToken, DomainClass.Core.Logs model)
        {
            var model2 = _mapper.Map<LogsViewModel>(model);
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
                    model.IP = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    model.MAC = "Mac";
                    var ent = await _logsService.InsertAsync(model, cancellationToken);
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

                else
                {
                    var exist = await _logsService.GetByIDAsync(cancellationToken, model.ID);
                    if (exist == null)
                    {
                        ViewBag.error = "ردیف انتخابی شما در پایگاه داده یافت نشد";
                        return View(model2);
                    }


                    exist.Section = model.Section;
                    exist.LogType = model.LogType;


                    if (await _logsService.UpdateAsync(exist, cancellationToken) != null)
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

        #region GetAllLogs
        [Route("/Edu/GetAllLogs")]
        [UserAccess(Common.Enums.eAccessControl.LogsGetAllLogs, Common.Enums.eAccessType.None, 1)]
        public async Task<string> GetAllLogs(int pageNo, int pageSize = 5)
        {

            var all = await _logsService.GetAllPaginatedAsync(new Data.GenericPagingParameters()
            {
                PageNumber = pageNo,
                PageSize = pageSize
            });
            if (all == null) return "";
            var logs = all.Where(q => q.IsActive == true).OrderByDescending(q => q.ID);

            var res = "";
            res += all.TotalPages + "_%_";
            foreach (var item in logs)
            {
                if (res.Length > 0)
                    res += "_$_";
                res += item.ID + "_|_";
                res += item.UserName + "_|_";
                res += item.Section + "_|_";
                res += item.LogType + "_|_";
                res += item.IP + "_|_";

            }
            return res;
        }

        #endregion

        #region RemoveLogs
        [HttpPost]
        [Route("/Edu/RemoveLogs")]
        [UserAccess(Common.Enums.eAccessControl.LogsRemoveLogs, Common.Enums.eAccessType.None, 1)]
        public async Task<string> RemoveLogs(CancellationToken cancellationToken, int entityId)
        {
            var curEntity = await _logsService.GetByIDAsync(cancellationToken, entityId);
            if (curEntity == null)
                return "این موجودی از قبل حذف شده است";
            var curUser = await _um.GetCurrentUserAsync(cancellationToken);
            if (curEntity.RegUserID != curUser.ID)
                return "این موجودی توسط شما ساخته نشده است";

            curEntity.IsActive = false;
            if (await _logsService.UpdateAsync(curEntity, cancellationToken) != null)
                return "done";
            else
                return "حذف با مشکل مواجه شد";
        }
        #endregion


        #endregion
    }
}
