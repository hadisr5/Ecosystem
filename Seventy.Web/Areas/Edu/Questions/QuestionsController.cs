using AutoMapper;
using Seventy.Data;
using System.Threading;
using Seventy.Service.Users;
using Seventy.ViewModel.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Seventy.Service.Core.Files;
using Seventy.Service.EDU.Lesson;
using Seventy.ViewModel.EDU.Exam;
using Seventy.Service.EDU.Questions;
using System.Linq;
using Seventy.Service.EDU.QuestionOptions;
using Seventy.ViewModel.EDU;
using System;
using Seventy.WebFramework.Filters;
using Seventy.Common.Enums;

namespace Seventy.Web.Areas.Edu.Questions
{
    [Area("Edu")]
    public class QuestionsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;
        private static IFilesService _filesService;
        private static ILessonService _lessonService;
        private readonly IQuestionsService _questionsService;
        private readonly IQuestionOptionsService _questionOptionsService;

        private static int? _userId;

        public QuestionsController(IUserManager userManager, IMapper mapper, ILessonService lessonService,
            IQuestionsService questionsService, IFilesService filesService, IQuestionOptionsService questionOptionsService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _lessonService = lessonService;
            _questionsService = questionsService;
            _filesService = filesService;
            _questionOptionsService = questionOptionsService;
            _userId = _userManager.GetCurrentUserID();
        }


        #region /Edu/Questions/Index
        [HttpGet]
        [Route("/Edu/Questions/Index")]
        [UserAccess(Common.Enums.eAccessControl.QuestionsIndex , Common.Enums.eAccessType.None, 1)]
        [Menu(eMenu.QuestionsBank, eModule.OnlineTraining, 8)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, int id = 0)
        {
            if (id == 0)
                return View();

            var model = await _questionsService.GetByIDAsync(cancellationToken, id);

            return View(_mapper.Map<QuestionsViewModel>(model));
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/Questions/Index")]
        [UserAccess(Common.Enums.eAccessControl.Questionsindex2 , Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, 
            DomainClass.EDU.Exam.Questions model, IFormFile? file)
        {
            var model2 = _mapper.Map<QuestionsViewModel>(model);
            if (!ModelState.IsValid)
            {
                TempData["err"] = "لطفا تمام فیلدها را تکمیل کنید";

                return View(model2);
            }

            model.RegUserID = _userManager.GetCurrentUserID();

            if (file != null)
            {
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
                model.IsActive = true;
                var insert = await _questionsService.InsertAsync(model, cancellationToken);

                if (insert != null)
                {
                    TempData["success"] = "با موفقیت افزوده شد";

                    return View(model2);
                }

                TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

                return View(model2);
            }

            var update = await _questionsService.UpdateAsync(model, cancellationToken);

            if (update != null)
            {
                TempData["success"] = "با موفقیت بروز رسانی شد";

                return View(model2);
            }

            TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

            return View(model2);
        }

        [Route("/Edu/Questions/List")]
        [UserAccess(Common.Enums.eAccessControl.QuestionsList, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> List(int page)
        {
            var model = await _questionsService
                .GetAllPaginatedAsync(new GenericPagingParameters
                {
                    PageNumber = page,
                    PageSize = 10
                }, null,
                    a => a.OrderByDescending(b => b.ID));

            return PartialView("List", model);
        }

        [Route("/Edu/Questions/Remove")]
        [UserAccess(Common.Enums.eAccessControl.QuestionsDelete, Common.Enums.eAccessType.None, 1)]
        public async Task<string> Delete(CancellationToken cancellationToken, int id)
        {
            var entity = await _questionsService.GetByIDAsync(cancellationToken, id);

            if (entity == null)
                return "آیتم مورد نظر یافت نشد";

            if (await _questionsService.DeleteAsync(entity, cancellationToken))
                return "done";

            return "خطا در زمان حذف داده";
        }
        [UserAccess(Common.Enums.eAccessControl.QuestionsGetAllLesson, Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<DomainClass.EDU.Lesson.Lesson> GetAllLesson()
        {
            return _lessonService.TableNoTracking();
        }
        [UserAccess(Common.Enums.eAccessControl.QuestionsCheckFile, Common.Enums.eAccessType.None, 1)]
        public static async Task<string?> CheckFile(CancellationToken cancellationToken, int? fileId)
        {
            if (fileId == null || _userId == null)
                return string.Empty;

            var file = await _filesService.CheckUserSignUpToContent((int)_userId, (int)fileId, cancellationToken);

            return file.File;
        }
        public static async Task<string?> GetQuestionFile(CancellationToken cancellationToken, int? fileId)
        {
            if (fileId == null || _userId == null)
                return string.Empty;

            return  await _filesService.GetFileUrlById((int)fileId, cancellationToken);

        }
        #endregion

        #region /Edu/Questions/MultiOptionQuestion

        [HttpGet]
        [Route("/Edu/Questions/MultiOptionQuestion")]
        [UserAccess(Common.Enums.eAccessControl.QuestionsMultipleOptionQuestions, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> MultiOptionQuestion(CancellationToken cancellationToken, int id = 0, int fordEdit = 0)
        {
            ViewBag.forEdit = fordEdit;
            if (id == 0 || fordEdit == 0)
                return View(new QuestionOptionsViewModel() { QuestionID = id });

            var model = await _questionOptionsService.GetByIDAsync(cancellationToken, id);

            return View(_mapper.Map<QuestionOptionsViewModel>(model));
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Route("/Edu/Questions/MultiOptionQuestion")]
        [UserAccess(Common.Enums.eAccessControl.QuestionsMultipleOptionQuestions2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> MultiOptionQuestion(CancellationToken cancellationToken,
            DomainClass.EDU.Exam.QuestionOptions model, IFormFile? file, int forEdit)
        {
            var model2 = _mapper.Map<QuestionOptionsViewModel>(model);
            try
            {
                ViewBag.forEdit = forEdit;
                if (!ModelState.IsValid)
                {
                    TempData["err"] = "لطفا تمام فیلدها را تکمیل کنید ";
                    return View(model2);
                }

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

                if (model.ID == 0 || model.ID == null || forEdit == 0)
                {
                    model.QuestionId = Convert.ToInt32(model.ID);
                    model.ID = null;


                    var insert = await _questionOptionsService.InsertAsync(model, cancellationToken);

                    if (insert != null)
                    {
                        TempData["success"] = "با موفقیت افزوده شد";

                        return View(model2);
                    }

                    TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

                    return View(model2);
                }

                var update = await _questionOptionsService.UpdateAsync(model, cancellationToken);

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
                return View(model2);
            }


            return View(model2);
        }


        [Route("/Edu/Questions/ListMultiOptionQuestion")]
        [UserAccess(Common.Enums.eAccessControl.QuestionsListMultiOptionQuestion, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> ListMultiOptionQuestion(int page, int questionId)
        {
            var model = await _questionOptionsService
                  .GetAllPaginatedAsync(new GenericPagingParameters
                  {
                      PageNumber = page,
                      PageSize = 10
                  }, q => q.IsActive && q.QuestionId == questionId,
                    a => a.OrderByDescending(b => b.ID));



            return PartialView("ListMultiOptionQuestion", model);
        }


        [Route("/Edu/Questions/RemoveMultiOptionQuestion")]
        [UserAccess(Common.Enums.eAccessControl.QuestionsDeleteMultiOptionQuestion, Common.Enums.eAccessType.None, 1)]
        public async Task<string> DeleteMultiOptionQuestion(CancellationToken cancellationToken, int id)
        {
            var entity = await _questionOptionsService.GetByIDAsync(cancellationToken, id);

            if (entity == null)
                return "آیتم مورد نظر یافت نشد";

            if (await _questionOptionsService.DeleteAsync(entity, cancellationToken))
                return "done";

            return "خطا در زمان حذف داده";
        }


        #endregion

    }
}