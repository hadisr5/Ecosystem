using Kendo.Mvc.UI;
using Seventy.Data;
using System.Threading.Tasks;

namespace Seventy.Service.Core.Roles
{
    public interface IRolesService : BaseService.IBaseService<DomainClass.Core.Roles>
    {
        Task<DataSourceResult> GetAllRoles_DataSourceResult(DataSourceRequest request);
    }
}
