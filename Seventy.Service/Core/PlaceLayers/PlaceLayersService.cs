using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Core.PlaceLayers
{
    public class PlaceLayersService : BaseService.BaseService<DomainClass.Core.PlaceLayers>, IPlaceLayersService
    {
        public PlaceLayersService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.Core.PlaceLayers> Table() => _uow.PlaceLayers.Table.AsEnumerable();
        public override IEnumerable<DomainClass.Core.PlaceLayers> TableNoTracking() => _uow.PlaceLayers.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.Core.PlaceLayers> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.PlaceLayers.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.Core.PlaceLayers entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.PlaceLayers.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.Core.PlaceLayers> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.PlaceLayers.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.PlaceLayers> InsertAsync(DomainClass.Core.PlaceLayers entity, CancellationToken cancellationToken)
        {
            var result = await _uow.PlaceLayers.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.Core.PlaceLayers> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.PlaceLayers.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.PlaceLayers> UpdateAsync(DomainClass.Core.PlaceLayers entity, CancellationToken cancellationToken)
        {
            var result = await _uow.PlaceLayers.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.Core.PlaceLayers> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.PlaceLayers.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.Core.PlaceLayers>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.Core.PlaceLayers, bool>> filter = null, Func<IQueryable<DomainClass.Core.PlaceLayers>, IOrderedQueryable<DomainClass.Core.PlaceLayers>> orderBy = null)
        {
            return await _uow.PlaceLayers.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
