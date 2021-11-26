using Kendo.Mvc.UI;
using Seventy.Data;
using System.Threading.Tasks;

namespace Seventy.Service.Core.PermissionGroup
{
    public interface IPermissionGroupService : BaseService.IBaseService<DomainClass.Core.PermissionGroup>
    {
        Task<DataSourceResult> GetAllPermissionGroup_DataSourceResult(DataSourceRequest request);
    }
}
