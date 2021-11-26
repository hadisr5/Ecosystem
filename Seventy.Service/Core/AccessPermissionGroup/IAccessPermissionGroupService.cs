using Kendo.Mvc.UI;
using Seventy.Data;
using Seventy.DomainClass.Core;
using Seventy.ViewModel.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Core.AccessPermissionGroup
{
    public interface IAccessPermissionGroupService : BaseService.IBaseService<DomainClass.Core.AccessPermissionGroup>
    {
        Task<DataSourceResult> GetAccessesForPermissionGroupAsync_DataSourceResult(int permissionGroupID, DataSourceRequest request);
        Task<int> ChangeAccessPermissionGroupAsync(SaveAccessPermissionGroupViewModel viewModel, CancellationToken cancellationToken);
    }
}
