using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Seventy.Common.Enums;
using Seventy.Data;
using Seventy.DomainClass.Core;
using Seventy.Service.Core.Documents;
using Seventy.Service.Core.Files;
using Seventy.Service.Core.UserDocument;
using Seventy.Service.Core.UserProfiles;
using Seventy.Service.Users;
using Seventy.ViewModel.Core;
using Seventy.WebFramework.Filters;

namespace Seventy.Web.Areas.Edu.EduDocument
{
    [Area("Edu")]
    public class EduDocumentsController : Controller
    {
        private IUserDocumentService _userDocumentService;
        private IUserManager _userManager;
        static IUserProfilesService _userProfiles;
        static IDocumentsTypeService _documentsTypeService;
        static IFilesService _filesService;
        static IMapper _mapper;

        public EduDocumentsController(IUserDocumentService userDocumentService, IUserManager userManager, IUserProfilesService profilesService
        , IDocumentsTypeService documentsService, IMapper mapper)
        {
            _userManager = userManager;
            _userDocumentService = userDocumentService;
            _userProfiles = profilesService;
            _documentsTypeService = documentsService;
            _mapper = mapper;
        }



        [HttpGet]
        [Route("/Edu/EduDocuments/Index")]
        [UserAccess(Common.Enums.eAccessControl.EduDocumentsIndex , Common.Enums.eAccessType.None, 1)]
        [Menu(eMenu.SubmitUserDocuments, eModule.OnlineTraining, 7)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var user = await _userManager.GetCurrentUserAsync(cancellationToken);
            if (_userManager.IsInRole((int)user.ID, 2))
                ViewBag.IsAdmin = true;
            else
                ViewBag.IsAdmin = false;

            return View();
        }

        [HttpPost]
        [Route("/Edu/EduDocuments/Index")]
        [UserAccess(Common.Enums.eAccessControl.EduDocumentsIndex2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, UserDocumentsViewModel model, IFormFile? file)
        {
            var user = await _userManager.GetCurrentUserAsync(cancellationToken);
            if (_userManager.IsInRole((int)user.ID, 2))
                ViewBag.IsAdmin = true;
            else
            {
                ViewBag.IsAdmin = false;

                model.UserID = (int)user.ID;
            }

            if (!ModelState.IsValid)
            {
                TempData["err"] = "model is not valid";
                return View(model);
            }
            if (file != null)
            {
                var fileResult = await _filesService.UploadFileAsync(new FilesViewModel
                {
                    UploadFile = file,
                    Title = file.FileName,
                    RegUserID = model.RegUserID
                }, cancellationToken);

                model.FileID = fileResult.ResultID ?? 0;
            }

            var saveOBJ = _mapper.Map<UserDocuments>(model);

            await _userDocumentService.InsertAsync(saveOBJ, cancellationToken);
            return RedirectToAction("Index");
        }

        [Route("/Edu/EduDocuments/List")]
        [UserAccess(Common.Enums.eAccessControl.EduDocumentsList, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> List(CancellationToken cancellationToken, int page)
        {
            var user = await _userManager.GetCurrentUserAsync(cancellationToken);

            var list = await _userDocumentService
                .GetDocumentsByUserIdAsync(user.ID ?? 0, cancellationToken);

            return PartialView("List", list);
        }

        [UserAccess(Common.Enums.eAccessControl.EduDocumentsGetAllUser, Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<UserProfiles> GetAllUser()
        {
            return _userProfiles.TableNoTracking();
        }

        [UserAccess(Common.Enums.eAccessControl.EduDocumentsGetAllDocType, Common.Enums.eAccessType.None, 1)]
        public static IEnumerable<DocumentType> GetAllDoctype()
        {
            return _documentsTypeService.TableNoTracking();
        }
    }
}