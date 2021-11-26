using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Mvc;
using Seventy.Common.Enums;
using Seventy.Repository.Core;
using Seventy.Service.EDU.Course;
using Seventy.Service.Users;
using Seventy.ViewModel;
using Seventy.Web.Areas.Edu.AnswerQuestion;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Areas.Edu.LongCourses
{
    [Area("Edu")]
    public class LongCoursesController : BaseController
    {
        private readonly ICourseService _courseService;

        public LongCoursesController(ICourseService courseService
            , IUnitOfWork uow
            , AutoMapper.IMapper mapper
            , IUserManager userManager) : base(uow, mapper, userManager)
        {
            _courseService = courseService;
        }
        [HttpGet]
        [Route("/Edu/LongCourses")]
        [UserAccess(Common.Enums.eAccessControl.LongCoursesIndex, eAccessType.None, 1)]
        [Menu(eMenu.LongCourses, eModule.OnlineTraining, 2)]
        public async Task<IActionResult> Index()
        {
            //var result = await _userCourseService
            //    .GetCourseListForEnrollment("تک مهارتی");

            return View();
        }

        #region GetAllCourse
        [Route("/edu/LongCourses/LoadData")]
        [HttpPost]
        [UserAccess(Common.Enums.eAccessControl.LongCoursesList, eAccessType.None, 1)]
        public async Task<GridResponseModel> LoadData(IDataTablesRequest request)
        {
            return await _courseService.GetCustomCourseAsync(CourseEnum.Long, request);
        }
        #endregion
    }
}