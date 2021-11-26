using System;
using Seventy.Service.Users;
using System.Threading.Tasks;
using Seventy.DomainClass.EDU;
using Microsoft.AspNetCore.Mvc;
using Seventy.Service.EDU.Term;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Areas.Edu.UserTermCourses
{
    [Area("Edu")]
    public class UserTermCoursesController : Controller
    {
        private static IUserManager _userManager;
        private static ITermService _termService;
       // private static IUserCourseService _userCourseService;


        public UserTermCoursesController( ITermService termService, IUserManager userManager)
        {
            //_userCourseService = userCourseService;
            _userManager = userManager;
            _termService = termService;
        }

        [HttpGet]
        [Route("/Edu/UserTermCourses")]
        [UserAccess(Common.Enums.eAccessControl.UserTermCoursesIndex, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Index(int id)
        {
            // var user = await _userManager.GetCurrentUserAsync();
            //
            // var term = await _termService.GetByIDAsync(id);
            //
            // //var data = await _userCourseService
            // //    .GetUserCoursesByTerm(user, term).ToListAsync();

            return View();
        }
    }
}