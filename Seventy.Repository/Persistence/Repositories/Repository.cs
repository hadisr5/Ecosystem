using Seventy.Common.Utilities;
using Seventy.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Seventy.DomainClass;
using Seventy.DomainClass.Core;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, ICoreBase
    {
        protected readonly DataContext DbContext;
        public DbSet<TEntity> Entities { get; }
        public virtual IQueryable<TEntity> Table => Entities;
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        public Repository(DataContext dbContext)
        {
            DbContext = dbContext;
            Entities = DbContext.Set<TEntity>();
        }

        #region Async Method
        public virtual async Task<TEntity> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await Entities.FindAsync(ids, cancellationToken);
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken)
        {
            Assert.NotNull(entity, nameof(entity));
            await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            return entity;
        }

        public virtual async Task<bool> InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            Assert.NotNull(entities, nameof(entities));
            await Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
            return true;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Update(entity);
            if (false)
                await DbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public virtual async Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.UpdateRange(entities);
            if (false)
                await DbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public virtual async Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            Assert.NotNull(entity, nameof(entity));
            if (hardDelete)
            {
                Entities.Remove(entity);
            }
            else if (!hardDelete)
            {
                entity.IsActive = false;
                await UpdateAsync(entity, cancellationToken);
            }
            return true;
        }

        public virtual async Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            Assert.NotNull(entities, nameof(entities));
            if (hardDelete)
            {
                Entities.RemoveRange(entities);
            }
            else if (!hardDelete)
            {
                foreach (var entity in entities)
                    entity.IsActive = false;
                await UpdateRangeAsync(entities, cancellationToken);
            }
            return true;
        }

        public virtual async Task<PagedList<TEntity>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = TableNoTracking;
            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
            {
                return await PagedList<TEntity>.ToPagedList(orderBy(query),
                        genericPagingParameters.PageNumber,
                        genericPagingParameters.PageSize);
            }
            else
            {
                return await PagedList<TEntity>.ToPagedList(query,
                        genericPagingParameters.PageNumber,
                        genericPagingParameters.PageSize);
            }
        }
        #endregion

        #region Sync Methods
        public virtual TEntity GetByID(params object[] ids)
        {
            return Entities.Find(ids);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Add(entity);
            return entity;
        }

        public virtual bool InsertRange(IEnumerable<TEntity> entities)
        { 
            Assert.NotNull(entities, nameof(entities));
            Entities.AddRange(entities);
            DbContext.SaveChanges();
            return true;
        }

        public virtual void Update(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Update(entity);
            DbContext.SaveChanges();
        }

        public virtual bool UpdateRange(IEnumerable<TEntity> entities)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.UpdateRange(entities);
            return true;
        }

        public virtual void Delete(TEntity entity, bool hardDelete = false)
        {
            Assert.NotNull(entity, nameof(entity));
            if (hardDelete)
            {
                Entities.Remove(entity);
            }
            else if (!hardDelete)
            {
                entity.IsActive = false;
                Update(entity);
            }
        }

        public virtual bool DeleteRange(IEnumerable<TEntity> entities, bool hardDelete = false)
        {
            Assert.NotNull(entities, nameof(entities));
            if (hardDelete)
            {
                Entities.RemoveRange(entities);
            }
            else if (!hardDelete)
            {
                foreach (var entity in entities)
                    entity.IsActive = false;
                UpdateRange(entities);
            }
            return true;
        }
        #endregion

        #region Attach & Detach
        public virtual void Detach(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            var entry = DbContext.Entry(entity);
            if (entry != null)
            {
                entry.State = EntityState.Detached;
            }
        }

        public virtual void Attach(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            if (DbContext.Entry(entity).State == EntityState.Detached)
            {
                Entities.Attach(entity);
            }
        }
        #endregion

        #region Explicit Loading
        public virtual async Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken)
            where TProperty : class
        {
            Attach(entity);

            var collection = DbContext.Entry(entity).Collection(collectionProperty);
            if (!collection.IsLoaded)
            {
                await collection.LoadAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        public virtual void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty)
            where TProperty : class
        {
            Attach(entity);
            var collection = DbContext.Entry(entity).Collection(collectionProperty);
            if (!collection.IsLoaded)
            {
                collection.Load();
            }
        }

        public virtual async Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty, CancellationToken cancellationToken)
            where TProperty : class
        {
            Attach(entity);
            var reference = DbContext.Entry(entity).Reference(referenceProperty);
            if (!reference.IsLoaded)
            {
                await reference.LoadAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        public virtual void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty)
            where TProperty : class
        {
            Attach(entity);
            var reference = DbContext.Entry(entity).Reference(referenceProperty);
            if (!reference.IsLoaded)
            {
                reference.Load();
            }
        }
        #endregion
    }
}
