using Kendo.Mvc.UI;
using Seventy.Data;
using Seventy.ViewModel.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Core.UserAccess
{
  public interface IUserAccessService : BaseService.IBaseService<DomainClass.Core.UserAccess>
  {
        Task<bool> CheckForAccess(string path, CancellationToken cancellationToken = default);
        Task<int> ChangeUserAccessesAsync(SavePermissionViewModel viewModel, CancellationToken cancellationToken);
        Task<DataSourceResult> GetAccessesForUserAsync_DataSourceResult(int userID, DataSourceRequest request);
  }
}
