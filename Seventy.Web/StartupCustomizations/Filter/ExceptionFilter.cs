using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Seventy.Web.StartupCustomizations.Filter
{
  
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class ExceptionFilter : ExceptionFilterAttribute
    {
       
        //private readonly IUserManager _UserManager;
        //private readonly ISentryService _SentryService;

        



        //public ExceptionFilter(IUserManager UserManager, ISentryService SentryService)
        //{
        //    _UserManager = UserManager;
        //    _SentryService = SentryService;
        //}

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            // _Logger.Insert(new ViewModel.Log.LogViewModel() { LogLevelId = (Byte)Services.Log.LogLevel.Error, ShortMessage = LogType.Exception.ToString(), FullMessage = context.Exception.Message });
            // _MessageService.SendMassageTelegram(context.Exception.Message.ToString(), Utility.General.ticketgroup());
            //int userId = 0;
            //var user = _UserManager.GetCurrentUser();
            //if (user != null) userId = user.Id;
            //string url = context.RouteData.Values["action"].ToString() + "/" + context.RouteData.Values["controller"].ToString();
            //string ip = context.HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();
            //_SentryService.InsertAsync(context.Exception);
            //_EventLogService.Insert(Service.Logging.Enums.LogType.error, "Exception", context.Exception.Message.ToString(), url, ip, userId);
            return base.OnExceptionAsync(context);
        }
    }
}
