using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Seventy.Data;
using Seventy.Service.EDU.Exam;
//using Seventy.Service.EDU.ExamAnswerSheet;
using Seventy.Service.Users;
using Seventy.ViewModel.EDU;

namespace Seventy.Web.Areas.Edu.ExamCorrection
{
    [Area("Edu")]
    public class ExamCorrectionController : Controller
    {
        private static IUserManager _UserManager;
        private static IMapper _mapper;
        private static IExamService _ExamService;
        //private static IExamAnswerSheetService _ExamAnswerSheetService;
        //public ExamCorrectionController(IUserManager UserManager, IMapper mapper, IUserCourseService UserCourseService,
        //    IExamService ExamService, IExamAnswerSheetService ExamAnswerSheetService)
        //{
        //    _UserManager = UserManager;
        //    _mapper = mapper;
        //    _UserCourseService = UserCourseService;
        //    _ExamService = ExamService;
        //    _ExamAnswerSheetService = ExamAnswerSheetService;
        //}
        //public static async Task<IEnumerable<ExamUserViewModel>> getNeedReviewExams()
        //{
        //    var User = await _UserManager.GetCurrentUserAsync();
        //    var all = await _ExamAnswerSheetService.GetPendingAnswersheetsForTeacher(User.ID.Value);
        //    return all;
        //}

        //[HttpGet]
        //[Route("/Edu/NeedCorrectionExams")]
        //public async Task<IActionResult> NeedCorrectionExams()
        //{
        //    return View();
        //}
    }
}
