using Kendo.Mvc.UI;
using Seventy.ViewModel.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Core.UserRole
{
    public interface IUserRoleService : BaseService.IBaseService<DomainClass.Core.UserRole>
    {
        Task<DataSourceResult> GetRolesForUserAsync_DataSourceResult(int userID, DataSourceRequest request);
        Task<int> ChangeUserRolesAsync(SavePermissionViewModel viewModel, CancellationToken cancellationToken);
    }
}
