using System;
using System.IO;
using Seventy.Service.Users;
using Seventy.ViewModel.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Seventy.Repository.Core;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Seventy.Data;
using System.Linq.Expressions;
using Seventy.ViewModel;
using DataTables.AspNet.Core;

namespace Seventy.Service.Core.Files
{
    public class FilesService : BaseService.BaseService<DomainClass.Core.Files>, IFilesService
    {
        #region Fields
        private readonly IUserManager _userManagerService;
        private readonly IHostingEnvironment _webHostEnvironment;
        #endregion

        #region Props
        public string BaseUploadFolder { get; set; }
        #endregion

        public FilesService(IUnitOfWork uow, IHostingEnvironment hostEnvironment, IUserManager userManager)
          : base(uow)
        {
            _webHostEnvironment = hostEnvironment;
            _userManagerService = userManager;
            BaseUploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "UploadFiles");
        }
        public override IEnumerable<DomainClass.Core.Files> Table() => _uow.File.Table.AsEnumerable();
        public override IEnumerable<DomainClass.Core.Files> TableNoTracking() => _uow.File.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.Core.Files> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.File.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.Core.Files entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.File.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.Core.Files> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.File.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.Files> InsertAsync(DomainClass.Core.Files entity, CancellationToken cancellationToken)
        {
            var result = await _uow.File.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.Core.Files> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.File.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.Files> UpdateAsync(DomainClass.Core.Files entity, CancellationToken cancellationToken)
        {
            var result = await _uow.File.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.Core.Files> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.File.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public async Task<string> GetFilePathByID(int fileID, CancellationToken cancellationToken)
        {
            var f = await _uow.File.GetByIDAsync(cancellationToken, fileID);
            return Path.Combine(BaseUploadFolder, f.ID + f.FileExtension);
        }

        public async Task<string> GetFileUrlById(int fileID, CancellationToken cancellationToken)
        {
            var f = await _uow.File.GetByIDAsync(cancellationToken, fileID);
            return Path.Combine("/UploadFiles/", f?.ID + f?.FileExtension);
        }

        public async Task<CRUDResult> UploadFileAsync(FilesViewModel fileViewModel, CancellationToken cancellationToken)
        {
            if (fileViewModel == null)
            {
                return new CRUDResult { Successful = false, Message = "Null file" };
            }
            try
            {
                if (fileViewModel.ID != null)
                {
                    return await UpdateFile(fileViewModel, cancellationToken);
                }
                var currentUserID = _userManagerService.GetCurrentUserID();

                var fileExtension = Path.GetExtension(fileViewModel.UploadFile.FileName);

                var fileModel = new DomainClass.Core.Files
                {
                    Title = fileViewModel.Title,
                    FileExtension = fileExtension,
                    Description = fileViewModel.Description,
                    IsActive = true,
                    RegDate = DateTime.Now,
                    UserID = currentUserID.Value,
                    RegUserID = currentUserID
                };
                var insertedFile = await _uow.File.UpdateAsync(fileModel, cancellationToken);
                await _uow.CompleteAsync(cancellationToken);

                var uniqueFileName = insertedFile.ID.ToString() + fileExtension;
                string filePath = Path.Combine(BaseUploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await fileViewModel.UploadFile.CopyToAsync(fileStream);
                }
                return new CRUDResult()
                {
                    Successful = true,
                    Message = "فایل بارگذاری شد",
                    ResultID = insertedFile.ID,
                    ResultEntity = insertedFile
                };

            }
            catch (Exception ex)
            {
                return new CRUDResult { Successful = false, Message = ex.Message };
            }
        }

        private async Task<CRUDResult> UpdateFile(FilesViewModel fileViewModel, CancellationToken cancellationToken)
        {
            if (fileViewModel == null)
            {
                return new CRUDResult { Successful = false, Message = "Null file" };
            }
            try
            {
                var currentUserID = _userManagerService.GetCurrentUserID();

                #region Delete old file
                var oldFile = await _uow.File.GetByIDAsync(cancellationToken, fileViewModel.ID);
                var oldFilePath = Path.Combine(BaseUploadFolder, oldFile.ID + oldFile.FileExtension);
                System.IO.File.Delete(oldFilePath);
                #endregion

                #region Insert new file
                var newFileExtension = Path.GetExtension(fileViewModel.UploadFile.FileName);

                var fileModel = new DomainClass.Core.Files
                {
                    Title = fileViewModel.Title,
                    FileExtension = newFileExtension,
                    Description = fileViewModel.Description,
                    IsActive = true,
                    RegDate = DateTime.Now,
                    UserID = currentUserID.Value,
                    RegUserID = currentUserID
                };

                var insertedFile = await _uow.File.UpdateAsync(fileModel, cancellationToken);
                await _uow.CompleteAsync(cancellationToken);

                var uniqueFileName = insertedFile.ID.ToString() + newFileExtension;
                string newFilePath = Path.Combine(BaseUploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(newFilePath, FileMode.Create))
                {
                    await fileViewModel.UploadFile.CopyToAsync(fileStream);
                }
                return new CRUDResult()
                {
                    Successful = true,
                    Message = "فایل بارگذاری شد",
                    ResultID = insertedFile.ID,
                    ResultEntity = insertedFile
                };
                #endregion

            }
            catch (Exception ex)
            {
                return new CRUDResult { Successful = false, Message = ex.Message };
            }
        }

        public async Task<CRUDResult> DeleteFileAsync(int userID, int fileID, CancellationToken cancellationToken)
        {
            if (fileID == 0)
            {
                return new CRUDResult { Successful = false, Message = "Null file" };
            }

            //TODO: if user is allowed to delete file

            try
            {
                var fileToDelete = await _uow.File.GetByIDAsync(cancellationToken, fileID);

                if (fileToDelete == null)
                    return new CRUDResult { Successful = false, Message = "Null file" };

                var filePath = Path.Combine(BaseUploadFolder, fileToDelete.ID + fileToDelete.FileExtension);
                File.Delete(filePath);

                await _uow.File.DeleteAsync(fileToDelete, cancellationToken);
                await _uow.CompleteAsync(cancellationToken);

                return new CRUDResult()
                {
                    Successful = true,
                    Message = "فایل حذف شد",
                    ResultID = fileID
                };

            }
            catch (Exception ex)
            {
                return new CRUDResult { Successful = false, Message = ex.Message };
            }

        }

        public async Task<DownloadFileViewModel> Download(int userID, int fileID, CancellationToken cancellationToken)
        {
            var model = new DownloadFileViewModel();
            try
            {
                var file = await _uow.File.GetByIDAsync(cancellationToken, fileID);
                if (file == null)
                    return model;
                return model;
            }
            catch { return model; }
        }

        public async Task<FileViewModel> CheckUserSignUpToContent(int userID, int fileID, CancellationToken cancellationToken)
        {
            try
            {
                var trainingContent = await _uow.TrainingContent.TableNoTracking
                    .SingleOrDefaultAsync(a =>
                        a.FileID.Equals(fileID));

                var file = await GetFileUrlById(fileID, cancellationToken);

                if (trainingContent == null || !trainingContent.DemoState.Equals("پولی"))
                    return new FileViewModel
                    {
                        File = file
                    };

                var courseRegisteredUsers = from tr in _uow.TrainingWeekContent.TableNoTracking.Where(x => x.ContentID == trainingContent.ID)
                        join tw in _uow.TrainingWeek.TableNoTracking on tr.TrainingWeekID equals tw.ID
                        join termLesson in _uow.TermLesson.TableNoTracking on tw.LessonID equals termLesson.LessonID
                        join courseRegistration in _uow.CourseRegistration.TableNoTracking on termLesson.CourseID equals courseRegistration.CourseID
                        select new { userID = courseRegistration.UserID };

                bool isCurrentUserRegistered =  courseRegisteredUsers.Any(a => a.userID.Equals(userID));

                if (isCurrentUserRegistered)
                    return new FileViewModel
                    {
                        File = file
                    };

                return new FileViewModel
                {
                    File = null
                };
            }
            catch (Exception ex)
            {
                return new FileViewModel
                {
                    File = null
                };
            }
        }

        public override async Task<PagedList<DomainClass.Core.Files>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.Core.Files, bool>> filter = null, Func<IQueryable<DomainClass.Core.Files>, IOrderedQueryable<DomainClass.Core.Files>> orderBy = null)
        {
            return await _uow.File.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }

        public async Task<GridResponseModel> LoadDataAsync(IDataTablesRequest request, CancellationToken cancellationToken=default)
        {
            return await _uow.File.LoadDataAsync(request,cancellationToken);
        }
    }
}
