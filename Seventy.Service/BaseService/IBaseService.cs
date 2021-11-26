using Seventy.Data;
using Seventy.DomainClass.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.BaseService
{
  public interface IBaseService<TEntity> where TEntity : class, ICoreBase
  {
    public abstract IEnumerable<TEntity> Table(); // GET DATA FOR INSERT, UPDATE AND DELETE OPERATION
    public abstract IEnumerable<TEntity> TableNoTracking(); // GET DATA ONLY FOR READ OPERATION 

    public abstract Task<TEntity> GetByIDAsync(CancellationToken cancellationToken, params object[] ids);

    public abstract Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken);
    public abstract Task<bool> InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

    public abstract Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    public abstract Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

    public abstract Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool hardDelete = false);
    public abstract Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool hardDelete = false);

    public abstract Task<PagedList<TEntity>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters
            ,Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
  }
}
