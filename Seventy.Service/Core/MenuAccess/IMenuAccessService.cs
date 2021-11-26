using Seventy.Data;
using Seventy.DomainClass.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Core.MenuAccess
{
  public interface IMenuAccessService : BaseService.IBaseService<DomainClass.Core.MenuAccess>
  {
        Task UpdateMenuAccessTable(List<DomainClass.Core.MenuAccess> accesses);
        Task<List<IGrouping<string, MenuGroupHelper>>> GetMenues();
  }
}
