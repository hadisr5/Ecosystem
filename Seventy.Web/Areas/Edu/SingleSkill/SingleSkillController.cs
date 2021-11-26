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
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Areas.Edu.SingleSkill
{
    [Area("Edu")]
    public class SingleSkillController : BaseController
    {
        private readonly ICourseService _courseService;

        public SingleSkillController(ICourseService courseService
            , IUnitOfWork uow
            , AutoMapper.IMapper mapper
            , IUserManager userManager) : base(uow, mapper, userManager)
        {
            _courseService = courseService;
        }
        [HttpGet]
        [Route("/Edu/SingleSkill")]
        [UserAccess(Common.Enums.eAccessControl.SingleSkillIndex, Common.Enums.eAccessType.None, 1)]
        [Menu(eMenu.SingleSkillCourse, eModule.OnlineTraining, 1)]
        public async Task<IActionResult> Index()
        {
            //var result = await _userCourseService
            //    .GetCourseListForEnrollment("تک مهارتی");

            return View();
        }

        #region GetAllCourse
        [Route("/edu/SingleSkill/LoadData")]
        [HttpPost]
        [UserAccess(Common.Enums.eAccessControl.SingleSkillList, Common.Enums.eAccessType.None, 1)]
        public async Task<GridResponseModel> LoadData(IDataTablesRequest request)
        {
            return await _courseService.GetCustomCourseAsync(CourseEnum.Single, request);
        }
        #endregion
    }
}