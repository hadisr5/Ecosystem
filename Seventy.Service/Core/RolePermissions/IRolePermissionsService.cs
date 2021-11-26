using Seventy.Data;
using Seventy.ViewModel.Core.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seventy.Service.Core.RolePermissions
{
  public interface IRolePermissionsService : BaseService.IBaseService<DomainClass.Core.RolePermissions>
  {
    Task<IEnumerable<RolePermissionsViewModel>> GetAllByRoleAsync(int RoleId);
  }
}
