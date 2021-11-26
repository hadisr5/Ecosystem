using Seventy.Data;
using Seventy.DomainClass.Core;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Seventy.Service.Core.Logs
{
    public interface ILogsService : BaseService.IBaseService<DomainClass.Core.Logs>
  {
        Task<PagedList<LogsViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters
            , Expression<Func<LogsViewModel, bool>> filter = null,
             Func<IQueryable<LogsViewModel>, IOrderedQueryable<LogsViewModel>> orderBy = null);
    }
}
