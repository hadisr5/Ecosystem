using Kendo.Mvc.UI;
using Seventy.Data;
using Seventy.ViewModel.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Core.UserPermissionGroup
{
    public interface IUserPermissionGroupService : BaseService.IBaseService<DomainClass.Core.UserPermissionGroup>
    {
        Task<DataSourceResult> GetPermissionGroupForUserAsync_DataSourceResult(int userID, DataSourceRequest request);
        Task<int> ChangeUserGroupAccessAsync(SavePermissionViewModel viewModel, CancellationToken cancellationToken);
    }
}
