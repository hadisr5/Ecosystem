using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Seventy.DomainClass;
using Seventy.DomainClass.Core;
using Seventy.Data;

namespace Seventy.Repository.Core.Repositories
{
  public interface IRepository<TEntity> where TEntity : class, ICoreBase
  {
    DbSet<TEntity> Entities { get; }
    IQueryable<TEntity> Table { get; }
    IQueryable<TEntity> TableNoTracking { get; }
    TEntity Insert(TEntity entity);
    Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken);
    bool InsertRange(IEnumerable<TEntity> entities);
    Task<bool> InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
    void Attach(TEntity entity);
    void Delete(TEntity entity, bool hardDelete = false);
    Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool hardDelete = false);
    bool DeleteRange(IEnumerable<TEntity> entities, bool hardDelete = false);
    Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool hardDelete = false);
    void Detach(TEntity entity);
    TEntity GetByID(params object[] ids);
    Task<TEntity> GetByIDAsync(CancellationToken cancellationToken, params object[] ids);
    void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty) where TProperty : class;
    Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken) where TProperty : class;
    void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty) where TProperty : class;
    Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty, CancellationToken cancellationToken) where TProperty : class;
    void Update(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    bool UpdateRange(IEnumerable<TEntity> entities);
    Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
    Task<PagedList<TEntity>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
  }
}