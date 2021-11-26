using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Seventy.DomainClass.EDU.Exercise;
using Seventy.Service.Core.Files;
using Seventy.Service.EDU.ExamAnswerSheet;
using Seventy.Service.EDU.Exercise;
using Seventy.Service.EDU.ExerciseUser;
using Seventy.Service.Users;
using Seventy.ViewModel.EDU;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Areas.Edu.AnswerQuestion
{
    [Area("Edu")]
    public class AnswerExerciseController : Controller
    {
        private static IUserManager _UserManager;
        private static IMapper _mapper;
        private static IExerciseUserService _ExerciseUserService;
        private static IExerciseService _ExerciseService;
        private static IFilesService _filesService;
        public AnswerExerciseController(IUserManager UserManager, IMapper mapper, IExerciseUserService ExerciseUserService
            , IExerciseService ExerciseService, IFilesService filesService)
        {
            _UserManager = UserManager;
            _mapper = mapper;
            _ExerciseUserService = ExerciseUserService;
            _filesService = filesService;
            _ExerciseService = ExerciseService;
        }
        [HttpGet]
        [Route("/Edu/AnswerExercise/Index")]
        [UserAccess(Common.Enums.eAccessControl.ShowAnswerExerciseView, Common.Enums.eAccessType.View, 1)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken,int ID)
        {
            var model = await _ExerciseUserService.GetByIDAsync(cancellationToken, ID);
            if (model == null || model.UserID != _UserManager.GetCurrentUserID())
            {
                TempData["Message"] = "سوال مورد نظر یافت نشد";
                return View("_MessageBox");
            }
            var nModel = await _ExerciseService.GetByIDAsync(cancellationToken, model.ExerciseId);
            var file = await _filesService.GetByIDAsync(cancellationToken,(int)nModel.FileID);
            model.Exercise = nModel;
            model.File = file;
            return View(model);
        }
        [HttpPost]
        [Route("/Edu/AnswerExercise/Index")]
        [UserAccess(Common.Enums.eAccessControl.CreateAnswerExercise, Common.Enums.eAccessType.None, 2)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken,ExerciseUser model, IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                TempData["err"] = "model is not valid";

                return View(model);
            }

            if (file != null)
            {
                var fileResult = await _filesService.UploadFileAsync(new ViewModel.Core.FilesViewModel
                {
                    UploadFile = file,
                    Title = file.FileName,
                    RegUserID = model.RegUserID
                }, cancellationToken);

                model.FileID = fileResult.ResultID.Value;
            }

            if (model.ID == 0 || model.ID == null)
            {
                model.ID = null;

                var insert = await _ExerciseUserService.InsertAsync(model, cancellationToken);

                if (insert != null)
                {
                    TempData["success"] = "با موفقیت افزوده شد";

                    return RedirectToAction("Index");
                }

                TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";

                return View(model);
            }
            var exist = await _ExerciseUserService.GetByIDAsync(cancellationToken, model.ID);
            exist.Answer = model.Answer;
            exist.FileID= model.FileID;
            var update = await _ExerciseUserService.UpdateAsync(exist, cancellationToken);

            if (update != null)
            {
                TempData["success"] = "با موفقیت بروز رسانی شد";

                return RedirectToAction("Index");
            }

            TempData["err"] = "خطا در زمان ذخیره در پایگاه داده";


            return View(model);
        }
    }
}
