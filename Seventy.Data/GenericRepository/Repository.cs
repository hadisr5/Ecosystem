using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Seventy.Data
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected IUnitOfWork _uow;
        protected DbSet<T> _tEntities;
        public Repository(IUnitOfWork uow)
        {
            _uow = uow;
            _tEntities = _uow.Set<T>();
        }

        public virtual EntityEntry<T> Insert(T entity)
        {
            try
            {
                var m = _tEntities.Add(entity);
                _uow.SaveAllChanges();
                return m;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        public virtual bool BulkInsert(IList<T> entity)
        {
            try
            {
                _uow.AddRange<T>(entity);
                _uow.SaveAllChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public virtual bool Delete(T entity)
        {
            try
            {
                _tEntities.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public virtual bool Update(T entity)
        {
            try
            {
                if (entity == null)
                    return false;
                _uow.Entry<T>(entity).State = EntityState.Modified;
                _uow.SaveAllChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public virtual T GetByID(object ID)
        {
            try
            {
                return _tEntities.Find(ID);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual IEnumerable<T> GetAll()
        {
            try
            {
                return _tEntities.ToList();
            }
            catch (Exception ex) { return null; }
        }
        public void Dispose()
        {
            try
            {
                _uow.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        public virtual bool BulkDelete(IList<T> entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entities");
                foreach (var item in entity)
                    this._tEntities.Remove(item);
                this._uow.SaveAllChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual bool BulkUpdate(IList<T> entity)
        {
            try
            {
                if (entity == null)
                    return false;
                foreach (var item in entity)
                    _uow.Entry<T>(item).State = EntityState.Modified;
                _uow.SaveAllChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual async Task<EntityEntry<T>> InsertAsync(T entity)
        {
            try
            {
                var m = _tEntities.Add(entity);
                await _uow.SaveAllChangesAsync();
                return m;
            }
            catch (Exception ex) { throw; }
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                _tEntities.Remove(entity);
                await _uow.SaveAllChangesAsync();
                return true;
            }
            catch (Exception ex) { throw; }
        }

        public virtual async Task<bool> BulkDeleteAsync(IList<T> entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entities");
                foreach (var item in entity)
                    this._tEntities.Remove(item);
                await this._uow.SaveAllChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                if (entity == null)
                    return false;
                _uow.Entry<T>(entity).State = EntityState.Modified;
                await _uow.SaveAllChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual async Task<bool> BulkUpdateAsync(IList<T> entity)
        {
            try
            {
                if (entity == null)
                    return false;
                foreach (var item in entity)
                    _uow.Entry<T>(item).State = EntityState.Modified;
                await _uow.SaveAllChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual IQueryable<T> GetAllQueryable(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            try
            {
                IQueryable<T> query = _tEntities;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return orderBy(query).AsQueryable<T>();
                }
                else
                {
                    return query.AsQueryable<T>();
                }
            }
            catch (Exception ex) { return null; }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            try
            {
                IQueryable<T> query = _tEntities;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return await orderBy(query).ToListAsync();
                }
                else
                {
                    return await query.ToListAsync();
                }
            }
            catch (Exception ex) { return null; }
        }

        public virtual async Task<PagedList<T>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters
            , Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            try
            {
                IQueryable<T> query = _tEntities;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return await PagedList<T>.ToPagedList(orderBy(query),
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }
                else
                {
                    return await PagedList<T>.ToPagedList(query,
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }

            }
            catch  { return null; }
        }

        public virtual async Task<bool> BulkInsertAsync(IList<T> entity)
        {
            try
            {
                _uow.AddRange<T>(entity);
                await _uow.SaveAllChangesAsync();
                return true;
            }
            catch (Exception ex) { throw; }
        }

        public async Task<T> GetByIDAsync(object ID)
        {
            try
            {
                return await _tEntities.FindAsync(ID);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<EntityEntry<T>> UpdateReturnModelAsync(T entity)
        {
            try
            {
                if (entity == null)
                    return _uow.Entry<T>(entity);
                _uow.Entry<T>(entity).State = EntityState.Modified;
                await _uow.SaveAllChangesAsync();
                return _uow.Entry<T>(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return this._tEntities;
            }
        }

    }
}
