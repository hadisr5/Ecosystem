using Seventy.Data;
using Seventy.DomainClass.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Core.UserAccess
{
  public interface IAccessService : BaseService.IBaseService<DomainClass.Core.Access>
  {
        Task UpdateAccessTable(List<DomainClass.Core.Access> accesses);
        Task<List<Access>> GetAll();
  }
}
