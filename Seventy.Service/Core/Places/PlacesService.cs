using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Core.Places
{
    public class PlacesService : BaseService.BaseService<DomainClass.Core.Places>, IPlacesService
    {
        public PlacesService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.Core.Places> Table() => _uow.Places.Table.AsEnumerable();
        public override IEnumerable<DomainClass.Core.Places> TableNoTracking() => _uow.Places.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.Core.Places> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.Places.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.Core.Places entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Places.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.Core.Places> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Places.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.Places> InsertAsync(DomainClass.Core.Places entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Places.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.Core.Places> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Places.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.Places> UpdateAsync(DomainClass.Core.Places entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Places.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.Core.Places> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Places.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.Core.Places>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.Core.Places, bool>> filter = null, Func<IQueryable<DomainClass.Core.Places>, IOrderedQueryable<DomainClass.Core.Places>> orderBy = null)
        {
            return await _uow.Places.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
