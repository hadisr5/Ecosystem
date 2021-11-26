using Kendo.Mvc.UI;
using Seventy.ViewModel.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Core.Roles.DefaultRoleAccess
{
    public interface IDefaultRoleAccessService : BaseService.IBaseService<DomainClass.Core.DefaultRoleAccess>
    {
        Task<int> ChangeDefaultRoleAccessesAsync(SaveDefaultRoleAccessesViewModel viewModel, CancellationToken cancellationToken);
        Task<DataSourceResult> GetAccessesForRoleAsync_DataSourceResult(int roleID, DataSourceRequest request);
    }
}
