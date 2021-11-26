using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Seventy.Service.Users;
using Seventy.Service.EDU.UserTrainingWeekContent;
using Seventy.WebFramework.Filters;

namespace Seventy.Service.EDU.GetUserStates
{
    [Area("Edu")]
    public class GetUserStatesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;
        private readonly IUserTrainingWeekContentService _UserTrainingWeekContentService;
        private static int? _userId;

        public GetUserStatesController(IMapper mapper, IUserManager userManager,
            IUserTrainingWeekContentService UserTrainingWeekContentService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _userId = _userManager.GetCurrentUserID();
            _UserTrainingWeekContentService = UserTrainingWeekContentService;
        }

        [Route("/Edu/GetUserStates/Index")]
        [UserAccess(Common.Enums.eAccessControl.GetUserStatesIndex, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken,int TermID, int CourseID, int LessonID, int UserID,bool isPartial = false)
        {
            //var model  = await _UserTrainingWeekContentService.GetUserTrainingWeekSummaryReport(CourseID, UserID);
            var model  = await _UserTrainingWeekContentService.GetUserTrainingWeekSummaryReportByLesson(TermID, CourseID, LessonID, UserID);
            ViewBag.isPartial = isPartial;
            if (!isPartial)
                return View(model);
            else
                return PartialView(model);
        }
    }
}