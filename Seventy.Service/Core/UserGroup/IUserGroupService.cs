using Kendo.Mvc.UI;
using Seventy.Data;
using Seventy.ViewModel.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Core.UserGroup
{
    public interface IUserGroupService : BaseService.IBaseService<DomainClass.Core.UserGroups>
    {
        Task<int> ChangeUserGroupMemberAsync(SaveUserGroupViewModel viewModel, CancellationToken cancellationToken);
        Task<DataSourceResult> GetUserGroupMembersForUserAsync_DataSourceResult(int userID, DataSourceRequest request);
    }
}
