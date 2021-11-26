using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Core.KMcategory
{
    public class KMcategoryService : BaseService.BaseService<DomainClass.Core.KMcategory>, IKMcategoryService
    {
        public KMcategoryService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.Core.KMcategory> Table() => _uow.KMcategory.Table.AsEnumerable();
        public override IEnumerable<DomainClass.Core.KMcategory> TableNoTracking() => _uow.KMcategory.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.Core.KMcategory> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.KMcategory.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.Core.KMcategory entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.KMcategory.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.Core.KMcategory> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.KMcategory.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.KMcategory> InsertAsync(DomainClass.Core.KMcategory entity, CancellationToken cancellationToken)
        {
            var result = await _uow.KMcategory.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.Core.KMcategory> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.KMcategory.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;

        }

        public override async Task<DomainClass.Core.KMcategory> UpdateAsync(DomainClass.Core.KMcategory entity, CancellationToken cancellationToken)
        {
            var result = await _uow.KMcategory.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.Core.KMcategory> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.KMcategory.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.Core.KMcategory>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.Core.KMcategory, bool>> filter = null, Func<IQueryable<DomainClass.Core.KMcategory>, IOrderedQueryable<DomainClass.Core.KMcategory>> orderBy = null)
        {
            return await _uow.KMcategory.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
