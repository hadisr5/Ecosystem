using DataTables.AspNet.Core;
using Seventy.Data;
using Seventy.Service.BaseService;
using Seventy.ViewModel;
using Seventy.ViewModel.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Core.Files
{
    public interface IFilesService : BaseService.IBaseService<DomainClass.Core.Files>
  {
        Task<CRUDResult> UploadFileAsync(FilesViewModel fileViewModel, CancellationToken cancellationToken);
        Task<CRUDResult> DeleteFileAsync(int userID, int fileID, CancellationToken cancellationToken);
        Task<string> GetFilePathByID(int fileID, CancellationToken cancellationToken);
        Task<string> GetFileUrlById(int fileID, CancellationToken cancellationToken);
        Task<DownloadFileViewModel> Download(int userID, int fileID, CancellationToken cancellationToken);
        Task<FileViewModel> CheckUserSignUpToContent(int userID, int fileID, CancellationToken cancellationToken);
        public Task<GridResponseModel> LoadDataAsync(IDataTablesRequest request, CancellationToken cancellationToken = default);
    }
}
