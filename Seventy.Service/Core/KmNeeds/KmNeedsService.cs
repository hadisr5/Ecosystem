using Seventy.Data;
using Seventy.DomainClass.Core;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Core.KmNeeds
{
    public class KmNeedsService : BaseService.BaseService<DomainClass.Core.KMNeeds>, IKmNeedsService
    {
        public KmNeedsService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.Core.KMNeeds> Table() => _uow.KMNeeds.Table.AsEnumerable();
        public override IEnumerable<DomainClass.Core.KMNeeds> TableNoTracking() => _uow.KMNeeds.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.Core.KMNeeds> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.KMNeeds.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.Core.KMNeeds entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.KMNeeds.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.Core.KMNeeds> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.KMNeeds.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.KMNeeds> InsertAsync(DomainClass.Core.KMNeeds entity, CancellationToken cancellationToken)
        {
            var result = await _uow.KMNeeds.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.Core.KMNeeds> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.KMNeeds.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.KMNeeds> UpdateAsync(DomainClass.Core.KMNeeds entity, CancellationToken cancellationToken)
        {
            var result = await _uow.KMNeeds.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.Core.KMNeeds> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.KMNeeds.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<KMNeeds>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<KMNeeds, bool>> filter = null, Func<IQueryable<KMNeeds>, IOrderedQueryable<KMNeeds>> orderBy = null)
        {
            return await _uow.KMNeeds.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
