using Microsoft.AspNetCore.Mvc;
using Seventy.Service.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Seventy.WebFramework.Api;
using Seventy.ViewModel.Core;
using Seventy.Service.Core.Files;
using Seventy.Repository.Core;
using Seventy.DomainClass.Core;
using System.Linq.Expressions;
using System;
using DataTables.AspNet.Core;
using Seventy.ViewModel;
using Seventy.WebFramework.Filters;
using Extensions;
using Microsoft.EntityFrameworkCore;
using Seventy.Common.Enums;

namespace Seventy.Web.Areas.Edu.Home
{
    [Area("Edu")]
    //[Authorize(Policy = "user")]
    public class FileController : BaseController
    {
        private readonly IFilesService _filesService;

        public FileController(IUnitOfWork uow
            , IFilesService filesService
            , AutoMapper.IMapper mapper
            , IUserManager userManager) : base(uow, mapper, userManager)
        {
            _filesService = filesService;
        }

        [HttpGet]
        [Route("/Edu/file/Index")]
        [UserAccess(Common.Enums.eAccessControl.FileIndex, Common.Enums.eAccessType.None, 1)]
        [Menu(eMenu.FileManager, eModule.OnlineTraining, 13)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, int id = 0)
        {
            if (id == 0)
                return View();

            var file = await _filesService.GetByIDAsync(cancellationToken, id);

            var viewModel = _mapper.Map<FilesViewModel>(file);
            return View(viewModel);
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        [Route("/Edu/file/Index")]
        [UserAccess(Common.Enums.eAccessControl.FileIndex2, Common.Enums.eAccessType.None, 1)]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, FilesViewModel filesViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["err"] = "model is not valid";

                return View(filesViewModel);
            }

            if (filesViewModel != null
                && !string.IsNullOrWhiteSpace(filesViewModel.Title)
                && filesViewModel.UploadFile != null
                && filesViewModel.UploadFile.Length > 0
                )
            {
                _ = await _filesService.UploadFileAsync(filesViewModel, cancellationToken);
                TempData["success"] = $" {filesViewModel.UploadFile.FileName} با موفقیت آپلود شد.";
            }
            return RedirectToAction("Index");
        }

        [Route("/edu/file/LoadData")]
        [HttpPost]
        [UserAccess(Common.Enums.eAccessControl.FileLoadData, Common.Enums.eAccessType.None, 1)]
        public async Task<GridResponseModel> LoadData(IDataTablesRequest request,CancellationToken cancellationToken)
        {
            return await _filesService.LoadDataAsync(request, cancellationToken);
        }
        [HttpPost]
        [Route("/edu/file/filterdfile")]
        [UserAccess(Common.Enums.eAccessControl.FileFilteredFile, Common.Enums.eAccessType.None, 1)]
        public async Task<List<Files>> FilterdFile(int page, string term, string type)
        {
            Expression<Func<Files, bool>> filter;
            term = term ?? "";
            string[] extensions;
            if (!string.IsNullOrWhiteSpace(type))
            {
                type = type.ToLower();
                if (type.Contains(','))
                {
                    extensions = type.Split(',').AsQueryable().Prepend(".").Distinct().ToArray();
                }
                else
                {
                    extensions = new string[] { type.Insert(0, ".") };
                }
                filter = x => x.IsActive && x.Title.Contains(term) && extensions.Contains(x.FileExtension.ToLower());
            }
            else
            {
                filter = x => x.IsActive && x.Title.Contains(term);
            }
            var pegedFiles = await _filesService.GetAllPaginatedAsync(new Data.GenericPagingParameters
            {
                PageSize = 10,
                PageNumber = page
            }, filter);
            return pegedFiles.ToList();
        }

        [HttpPost]
        [Route("/edu/file/filebytype")]
        [UserAccess(Common.Enums.eAccessControl.FileByType, Common.Enums.eAccessType.None, 1)]
        public async Task<List<Files>> FileByType(int page, string type)
        {
                Expression<Func<Files, bool>> filter;
                int fileType = (int)FileTypeEnum.Others;
                if (type == "ویدیو" || type == "فیلم")
                    fileType = (int)FileTypeEnum.Video;
                else
                    fileType = (int)FileTypeEnum.HTML;
                filter = x => x.IsActive && x.Type == fileType;
                var pegedFiles = await _filesService.GetAllPaginatedAsync(new Data.GenericPagingParameters
                {
                    PageSize = 50,
                    PageNumber = page
                }, filter);
                return pegedFiles.ToList();
        }


        [Route("edu/file/delete")]
        [HttpPost]
        [UserAccess(Common.Enums.eAccessControl.FileDelete, Common.Enums.eAccessType.None, 1)]
        public async Task<bool> Delete(CancellationToken cancellationToken, int id)
        {
            var result = new ApiResult(false, Common.Enums.eApiResultStatusCode.ServerError);
            try
            {
                var userId = _userManager.GetCurrentUserID();
                if (userId.HasValue)
                {
                    await _filesService.DeleteFileAsync(userId.Value, id, cancellationToken);
                    return true;
                }
                return false;
            }
            catch { return false; }
        }
    }

}
