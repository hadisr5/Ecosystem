using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Seventy.Data
{
    public interface IRepository<T> where T : class
    {
        EntityEntry<T> Insert(T entity);
        Task<EntityEntry<T>> InsertAsync(T entity);
        bool BulkInsert(IList<T> entity);
        Task<bool> BulkInsertAsync(IList<T> entity);
        bool Delete(T entity);
        Task<bool> DeleteAsync(T entity);
        bool BulkDelete(IList<T> entity);
        Task<bool> BulkDeleteAsync(IList<T> entity);
        bool Update(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<EntityEntry<T>> UpdateReturnModelAsync(T entity);
        bool BulkUpdate(IList<T> entity);
        Task<bool> BulkUpdateAsync(IList<T> entity);
        T GetByID(object ID);
        Task<T> GetByIDAsync(object ID);
        IEnumerable<T> GetAll();
        IQueryable<T> GetAllQueryable(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<PagedList<T>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters,
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        IQueryable<T> Table { get; }


    }
}
