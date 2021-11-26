using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Seventy.Common.Enums;
using Seventy.Common.Utilities;
using Seventy.Data;
using Seventy.DomainClass.EDU.Exam;
using Seventy.Repository.Core;
using Seventy.Service.Core.Files;
using Seventy.Service.Core.UserGroup;
using Seventy.Service.Core.UserProfiles;
using Seventy.Service.EDU.Exam;
using Seventy.Service.EDU.ExamQuestions;
using Seventy.Service.EDU.ExamUser;
using Seventy.Service.EDU.Exercise;
using Seventy.Service.EDU.ExerciseUser;
using Seventy.Service.EDU.Lesson;
using Seventy.Service.EDU.Questions;
using Seventy.Service.EDU.TrainingWeekContent;
using Seventy.Service.EDU.UserContent;
using Seventy.Service.Users;
using Seventy.ViewModel;
using Seventy.ViewModel.Core;
using Seventy.ViewModel.EDU;
using Seventy.WebFramework.Api;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Areas.Edu.Exam
{
    [Area("Edu")]
    [Authorize(Policy = "user")]
    public class ExamController : BaseController
    {

        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;
        private readonly IExerciseUserService _exerciseUserService;
        private readonly IExerciseService _exerciseService;
        private readonly IExamUserService _examUser;
        private readonly IExamService _examService;
        private static IQuestionsService _questionsService;
        private readonly IExamQuestionsService _examQuestionsService;
        private static IFilesService _filesService;
        private static ILessonService _lessonService;
        private static IExamUserService _ExamUserService;
        private static IUserGroupService _UserGroupService;
        private static IUserProfilesService _userProfileService;


        private static int? _userId;

        #region CTOR
        public ExamController(IUnitOfWork uow
            , AutoMapper.IMapper mapper
            , IUserManager userManager
            , IExerciseUserService exerciseUserService,
            IExerciseService exerciseService, IExamUserService examUser, IExamService examService,
            IQuestionsService questionsService, IExamQuestionsService examQuestionsService,
            IFilesService filesService, ILessonService lessonService,
            IExamUserService ExamUserService, IUserGroupService UserGroupService
            , IUserProfilesService userProfileService) : base(uow, mapper, userManager)
        {
            _exerciseUserService = exerciseUserService;
            _exerciseService = exerciseService;
            _examUser = examUser;
            _examService = examService;
            _questionsService = questionsService;
            _examQuestionsService = examQuestionsService;
            _filesService = filesService;
            _lessonService = lessonService;
            _ExamUserService = ExamUserService;
            _UserGroupService = UserGroupService;
            _mapper = mapper;
            _userManager = userManager;
            _userProfileService = userProfileService;
            _userId = userManager.GetCurrentUserID();
        }
        #endregion


        [Route("/edu/Exam/LoadGeneralExamList")]
        [HttpPost]
        [UserAccess(Common.Enums.eAccessControl.LoadGeneralExamList, Common.Enums.eAccessType.None, 1)]
        public async Task<GridResponseModel> LoadGeneralExamList(IDataTablesRequest request, CancellationToken cancellationToken)
        {
            return await _examService.LoadDataAsync(request, cancellationToken);
        }

        [Route("/edu/Exam/EditGeneralExam")]
        [HttpGet]
        [UserAccess(Common.Enums.eAccessControl.LoadGeneralExamQuestionList1, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> EditGeneralExam(int ExamID, CancellationToken cancellationToken)
        {
            if (ExamID == 0)
                return View();

            var model = await _examService.GetByIDAsync(cancellationToken, ExamID);
            return View(_mapper.Map<Seventy.ViewModel.EDU.ExamViewModel>(model));
        }

        [Route("/edu/Exam/LoadGeneralExamQuestionList")]
        [HttpPost]
        [UserAccess(Common.Enums.eAccessControl.LoadGeneralExamQuestionList2, Common.Enums.eAccessType.None, 1)]
        public async Task<GridResponseModel> LoadGeneralExamQuestionList(int ExamID, IDataTablesRequest request, CancellationToken cancellationToken)
        {
            return await _questionsService.GetExamQuestionsBySumBaromAsync(ExamID, request, cancellationToken);
        }

        [Route("edu/Exam/DeleteGeneralExam")]
        [HttpPost]
        [UserAccess(Common.Enums.eAccessControl.DeleteGeneralExam, Common.Enums.eAccessType.None, 1)]
        public async Task<bool> DeleteGeneralExam(CancellationToken cancellationToken, int id)
        {
            var result = new ApiResult(false, Common.Enums.eApiResultStatusCode.ServerError);
            try
            {
                var userId = _userManager.GetCurrentUserID();
                if (userId.HasValue)
                {
                    //await _filesService.DeleteFileAsync(userId.Value, id, cancellationToken);
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        [Route("edu/Exam/DeleteQuestionFromExam")]
        [HttpPost]
        [UserAccess(Common.Enums.eAccessControl.DeleteQuestionFromExam, Common.Enums.eAccessType.None, 1)]
        public async Task<bool> DeleteQuestionFromExam(CancellationToken cancellationToken, int id)
        {
            var examQuestion = await _examQuestionsService.GetByIDAsync(cancellationToken, id);
            return await _examQuestionsService.DeleteAsync(examQuestion, cancellationToken, true);
        }

        #region GeneralExam == آزمون ساز عمومی

        [HttpGet]
        [Route("/Edu/Exam/GeneralExam")]
        [UserAccess(Common.Enums.eAccessControl.ExamGeneralExam, Common.Enums.eAccessType.None, 1)]
        [Menu(eMenu.GeneralExamMaker, eModule.OnlineTraining, 1)]
        public async Task<IActionResult> GeneralExam(CancellationToken cancellationToken, int id = 0)
        {
            if (id == 0)
                return View();

            var model = await _examService.GetByIDAsync(cancellationToken, id);
            return View(_mapper.Map<Seventy.ViewModel.EDU.ExamViewModel>(model));
        }


        [HttpPost, ValidateAntiForgeryToken]
        [Route("/Edu/Exam/GeneralExam")]
        [UserAccess(Common.Enums.eAccessControl.ExamGeneralExam2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> GeneralExam(CancellationToken cancellationToken,
            DomainClass.EDU.Exam.Exam model, IFormFile? file, string examQuestion)
        {
            var model2 = _mapper.Map<Seventy.ViewModel.EDU.ExamViewModel>(model);

            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["err"] = "لطفا تمام فیلدها را تکمیل کنید";

                    return View(model2);
                }

                #region لیست سوالات انتخاب شده
                //examQuestion = 1_2_3  => 1==> QuestionId  2 ==> Examid 3==> Barom
                List<ExamQuestions> list = new List<ExamQuestions>();
                string values = examQuestion;
                string[] words = values.Split(",");

                foreach (var item in words)
                {
                    if (item != "")
                    {
                        var seperate = item.Split("_");
                        list.Add(new ExamQuestions()
                        {
                            QuestionID = Convert.ToInt32(seperate[0]),
                            ExamID = Convert.ToInt32(seperate[1]),
                            Barom = Convert.ToInt32(seperate[2]),
                        });
                    }
                }
                model.ExamQuestions = list;

                #endregion

                model.RegUserID = _userManager.GetCurrentUserID();

                if (file != null)
                {
                    if (file.FileName.Count() >= 40)
                    {
                        TempData["err"] = "طول نام فایل انتخابی طولانی است.";
                        return View(model2);
                    }

                    var fileResult = await _filesService.UploadFileAsync(new FilesViewModel
                    {
                        UploadFile = file,
                        Title = file.FileName,
                        RegUserID = model.RegUserID
                    }, cancellationToken);

                    model.FileID = fileResult.ResultID;
                }

                if (model.ID == 0 || model.ID == null)
                {
                    model.ID = null;

                    var insert = await _examService.InsertAsync(model, cancellationToken);

                    if (insert != null)
                    {
                        TempData["success"] = "با موفقیت افزوده شد";

                        return View(model2);
                    }

                    TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

                    return View(model2);
                }

                var update = await _examService.UpdateAsync(model, cancellationToken);

                if (update != null)
                {
                    TempData["success"] = "با موفقیت بروز رسانی شد";

                    return View(model2);
                }

                TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";
            }
            catch (Exception ex)
            {
                TempData["err"] = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }


            return View(model2);
        }



        #region GeneralExam  = GetAllQuestionExamAsync

        //لیست سوالات بر اساس درس
        [Route("/Edu/Exam/GetAllQuestionExamAsync")]
        [UserAccess(Common.Enums.eAccessControl.ExamGetAllQuestionExamAsync, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> GetAllQuestionExamAsync(int page, int lessonId)
        {
            var model = await _questionsService.GetAllPaginatedByLessonIdAsync(lessonId, new Data.GenericPagingParameters()
            {
                PageNumber = page,
                PageSize = 10,

            }, null, x => x.OrderByDescending(x => x.ID));

            return PartialView("List", model);
        }
        #endregion

        #region GeneralExam  = GetAllQuestionExamAsync

        //جستجو لیست سوالات بر اساس درس 
        [Route("/Edu/Exam/GetAllCustomAsync")]
        [UserAccess(Common.Enums.eAccessControl.ExamGetAllCustomAsync, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> GetAllCustomAsync(int page, bool? type, int? userId, int? level, int? lessonid)
        {

            var model = await _questionsService.GetAllPaginatedCustomAsync(new Data.GenericPagingParameters()
            {
                PageNumber = page,
                PageSize = 10,

            }, null, x => x.OrderByDescending(x => x.ID),
            type == null ? null : type, userId == null ? null : userId, null, level == null ? null : level, lessonid);

            return PartialView("List", model);
        }


        //سرویس لیست سوالات انتخاب شده آزمون

        [Route("/Edu/Exam/GetAllBySumBaromAsync")]
        [UserAccess(Common.Enums.eAccessControl.ExamGetAllBySumBaromAsyncs, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> GetAllBySumBaromAsync(int page, int exam = 0)
        {

            var model = await _questionsService.GetAllPaginatedBySumBaromAsync(exam, new Data.GenericPagingParameters()
            {
                PageNumber = page,
                PageSize = 10,

            }, null, x => x.OrderByDescending(x => x.ID));

            return PartialView("ListQuestion", model);
        }


        //حذف لیست سوالات انتخاب شده در آزمون
        [Route("/Edu/Exam/RemoveSumBaromAsync")]
        [UserAccess(Common.Enums.eAccessControl.ExamRemoveSumBaromAsync, Common.Enums.eAccessType.None, 1)]
        public async Task<string> RemoveSumBaromAsync(CancellationToken cancellationToken, int id)
        {
            var entity = await _questionsService.GetByIDAsync(cancellationToken, id);

            if (entity == null)
                return "آیتم مورد نظر یافت نشد";

            if (await _questionsService.DeleteAsync(entity, cancellationToken))
                return "done";

            return "خطا در زمان حذف داده";
        }
        [UserAccess(Common.Enums.eAccessControl.ExamGetUsersInQuestion, Common.Enums.eAccessType.None, 1)]
        public static Task<List<DomainClass.Core.UserProfiles>> GetUsersInQuestion(CancellationToken cancellationToken)
        {
            return _questionsService.GetUsersInQuestion(cancellationToken);
        }

        #endregion


        #endregion

        #region AllocationExam == تخصیص آزمون به فراگیر

        [HttpPost]
        [Route("/edu/exam/filterdexam")]
        [UserAccess(eAccessControl.ExamFilteredFile, eAccessType.None, 1)]
        public async Task<List<DomainClass.EDU.Exam.Exam>> filterdExam(int page, string term, string type)
        {
            term = term ?? "";
            var now = DateTime.Parse(DateTime.Now.ToPersianDate());
            var pegedExams = await _examService.GetAllPaginatedAsync(new Data.GenericPagingParameters
            {
                PageSize = 10,
                PageNumber = page
            }, x => x.IsActive && x.Title.Contains(term) && x.Type == type && x.EndDate < now);


            return pegedExams.ToList();
        }

        //تخصیص آزمون به فراگیر
        [HttpGet]
        [Route("/Edu/Exam/AllocationExam")]
        [UserAccess(Common.Enums.eAccessControl.ExamAllocationExam, Common.Enums.eAccessType.None, 1)]
        [Menu(eMenu.AllocateExamToStudent, eModule.OnlineTraining, 2)]
        public async Task<IActionResult> AllocationExam(CancellationToken cancellationToken)
        {
            return View();
        }
        [HttpPost]
        [Route("/Edu/Exam/AllocationExam")]
        [UserAccess(Common.Enums.eAccessControl.ExamAllocationExam2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> AllocationExam(CancellationToken cancellationToken, int ExamID, int ExamGroup, int TargetID)
        {
            if (!ModelState.IsValid || ExamID == 0)
            {
                TempData["Message"] = " همه مقادیر را وارد کنید ";
                return RedirectToAction("AllocationExam");
            }
            if (ExamGroup == 1)
            {
                var insert = await _ExamUserService.AssignExamToUsers(new List<int> { TargetID }, ExamID, cancellationToken);
                if (insert)
                {
                    TempData["success"] = "با موفقیت ثبت شد";
                    return RedirectToAction("AllocationExam");
                }
                TempData["Message"] = "خطایی در ثبت رخ داده است";
                return View("_MessageBox");
            }
            else
            {
                var insert = await _ExamUserService.AssignExamToUserGroups(new List<int> { TargetID }, ExamID, cancellationToken);
                if (insert != false)
                {
                    TempData["success"] = "با موفقیت ثبت شد";
                    return RedirectToAction("AllocationExam");
                }
                TempData["Message"] = "خطایی در ثبت رخ داده است";
                return View("_MessageBox");
            }
        }

        [Route("/Edu/Exam/AllocationExamList")]
        [UserAccess(Common.Enums.eAccessControl.ExamAllocationExamList, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> AllocationExamList(CancellationToken cancellation, int ExamGroup)
        {
            if (ExamGroup == 1)
            {
                return PartialView("Users", _userProfileService.TableNoTracking().ToList());
            }
            else
            {
                return PartialView("Groups", _UserGroupService.TableNoTracking().ToList());
            }
        }

        [Route("/Edu/Exam/AllocationExamAssignedList")]
        [UserAccess(Common.Enums.eAccessControl.ExamAllocationExamAssignedList, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> AllocationExamAssignedList(CancellationToken cancellation, int page)
        {
            var model = await _ExamUserService.GetAllPaginatedUsersInExamAsync(TypeEnum.Exam, new GenericPagingParameters()
            {
                PageNumber = page,
                PageSize = 10,
            }, q => q.IsActive == true);
            return PartialView("AllocationExamAssignedList", model);
        }
        [Route("/Edu/Exam/AllocationExamRemove")]
        [UserAccess(Common.Enums.eAccessControl.ExamDelete, Common.Enums.eAccessType.None, 1)]
        public async Task<string> Delete(CancellationToken cancellationToken, int id)
        {
            var entity = await _ExamUserService.GetByIDAsync(cancellationToken, id);

            if (entity == null)
                return "آیتم مورد نظر یافت نشد";

            if (await _ExamUserService.DeleteAsync(entity, cancellationToken))
                return "done";

            return "شما نمیتوانید افراد را از امتحانی که از تاریخ شروع اش گذشته حذف کنید";
        }
        #endregion

        #region MyExamList==لست آزمونها

        //[HttpGet]
        [Route("/Edu/Exam/MyExamList")]
        [UserAccess(Common.Enums.eAccessControl.ExamMyExamList, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> MyExamList(CancellationToken cancellation, int page = 1, bool isPartial = false)
        {
            var userId = _userManager.GetCurrentUserID();
            var Exams = await _examUser.GetAllPaginatedAsync(TypeEnum.Exam, new GenericPagingParameters
            {
                PageNumber = page,
                PageSize = 10
            }, q => q.IsActive && q.RegUserID != null && q.RegUserID == userId,
                a => a.OrderByDescending(b => b.ID));
            if (!isPartial)
                return View(Exams);
            else
                return PartialView("MyExamList", Exams);
        }
        //[HttpGet]
        [Route("/Edu/Exam/UserExamList")]
        [UserAccess(Common.Enums.eAccessControl.UserExamList, Common.Enums.eAccessType.None, 1)]
        [Menu(eMenu.ExamList, eModule.OnlineTraining, 4)]
        public async Task<IActionResult> UserExamList(CancellationToken cancellation, int page = 1, bool isPartial = false)
        {
            var userId = _userManager.GetCurrentUserID();
            var Exams = await _examUser.GetAllPaginatedAsync(TypeEnum.Exam, new GenericPagingParameters
            {
                PageNumber = page,
                PageSize = 10
            }, q => q.IsActive && q.UserID == userId,
                a => a.OrderByDescending(b => b.ID));
            if (!isPartial)
                return View(Exams);
            else
                return PartialView("UserExamList", Exams);
        }

        #endregion

        #region QuizOrExercixzeList==لیست تمرین / کوییز

        [Route("/Edu/Exam/QuizOrExercixzeList0")]
        [UserAccess(Common.Enums.eAccessControl.ExamQuizOrExercixzeList0, Common.Enums.eAccessType.None, 1)]
        [Menu(eMenu.ExerciseList, eModule.OnlineTraining, 5)]
        public async Task<IActionResult> QuizOrExercixzeList0(CancellationToken cancellation, int page = 1, int ListType = 0, bool isPartial = false)
        {
            var userId = _userManager.GetCurrentUserID();
            PagedList<ExamUserViewModel> Exams;
            if (ListType == 0)
            {
                Exams = await _examUser.GetAllPaginatedAsync(TypeEnum.Exercise, new GenericPagingParameters
                {
                    PageNumber = page,
                    PageSize = 10
                }, q => q.IsActive && q.RegUserID != null && q.RegUserID == userId,
                    a => a.OrderByDescending(b => b.ID));
                ViewBag.LType = "تمرین";

                if (!isPartial)
                    return View("ExercixzeList", Exams);
                else
                    return PartialView("ExercixzeList", Exams);
            }
            else
            {
                Exams = await _examUser.GetAllPaginatedAsync(TypeEnum.Quiz, new GenericPagingParameters
                {
                    PageNumber = page,
                    PageSize = 10
                }, q => q.IsActive,
                    a => a.OrderByDescending(b => b.ID));
                ViewBag.LType = "کوییز";

                if (!isPartial)
                    return View("QuizList", Exams);
                else
                    return PartialView("QuizList", Exams);
            }
        }



        [Route("/Edu/Exam/QuizOrExercixzeList1")]
        [UserAccess(Common.Enums.eAccessControl.ExamQuizOrExercixzeList1, Common.Enums.eAccessType.None, 1)]
        [Menu(eMenu.QuizList, eModule.OnlineTraining, 6)]
        public async Task<IActionResult> QuizOrExercixzeList1(CancellationToken cancellation, int page = 1, int ListType = 1, bool isPartial = false)
        {
            var userId = _userManager.GetCurrentUserID();
            PagedList<ExamUserViewModel> Exams;
            if (ListType == 0)
            {
                Exams = await _examUser.GetAllPaginatedAsync(TypeEnum.Exercise, new GenericPagingParameters
                {
                    PageNumber = page,
                    PageSize = 10
                }, q => q.IsActive && q.RegUserID != null && q.RegUserID == userId,
                    a => a.OrderByDescending(b => b.ID));
                ViewBag.LType = "تمرین";

                if (!isPartial)
                    return View("ExercixzeList", Exams);
                else
                    return PartialView("ExercixzeList", Exams);
            }
            else
            {
                Exams = await _examUser.GetAllPaginatedAsync(TypeEnum.Quiz, new GenericPagingParameters
                {
                    PageNumber = page,
                    PageSize = 10
                }, q => q.IsActive,
                    a => a.OrderByDescending(b => b.ID));
                ViewBag.LType = "کوییز";

                if (!isPartial)
                    return View("QuizList", Exams);
                else
                    return PartialView("QuizList", Exams);
            }
        }



        #endregion

        #region GetAllLesson
        [UserAccess(Common.Enums.eAccessControl.ExamGetAllLesson, Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<DomainClass.EDU.Lesson.Lesson> GetAllLesson()
        {
            return _lessonService.TableNoTracking();
        }
        #endregion

    }
}