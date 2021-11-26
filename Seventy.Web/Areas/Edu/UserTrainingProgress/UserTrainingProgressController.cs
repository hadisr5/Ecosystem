using Microsoft.AspNetCore.Mvc;
using Seventy.Service.Users;
using Seventy.ViewModel.EDU;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Seventy.Data;
using Seventy.WebFramework.Api;
using Seventy.ViewModel.Core;
using Seventy.Service.Core.Files;
using Seventy.Repository.Core;
using Seventy.ViewModel.EDU.Lesson;
using Seventy.Service.EDU.UserTrainingWeekContent;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Areas.Edu.Home
{
    [Area("Edu")]
    //[Authorize(Policy = "user")]
    public class UserTrainingProgressController : Controller
    {
        private readonly IUserTrainingWeekContentService _userTrainingWeekContentService;

        public UserTrainingProgressController(IUserTrainingWeekContentService userTrainingWeekContentService)
        {
            _userTrainingWeekContentService = userTrainingWeekContentService;
        }

        [Route("/Edu/UserTrainingProgress/Index")]
        [UserAccess(Common.Enums.eAccessControl.UserTrainingProgressIndex , Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken
            , int termID, int courseID, int lessonID, int userID, bool isPartial = false)
        {
            ViewBag.isPartial = isPartial;
            if (courseID == 0 || userID == 0)
                return View();
            try
            {
                //var content = await _userTrainingWeekContentService.GetUserTrainingWeekSummaryReport(courseID, userID);
                var content = await _userTrainingWeekContentService.GetUserTrainingWeekSummaryReportByLesson(termID, courseID, lessonID, userID);
                if (content != null)
                {
                    if (!isPartial)
                        return View(content);
                    return PartialView(content);
                }
            }
            catch { }
            return View();
        }
    }
}