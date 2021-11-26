using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Tickets
{
    public class TicketService : BaseService.BaseService<DomainClass.Core.Tickets>, ITicketService
    {
        public TicketService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.Core.Tickets> Table() => _uow.Tickets.Table.AsEnumerable();
        public override IEnumerable<DomainClass.Core.Tickets> TableNoTracking() => _uow.Tickets.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.Core.Tickets> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.Tickets.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.Core.Tickets entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Tickets.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.Core.Tickets> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Tickets.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.Tickets> InsertAsync(DomainClass.Core.Tickets entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Tickets.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.Core.Tickets> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Tickets.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.Tickets> UpdateAsync(DomainClass.Core.Tickets entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Tickets.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.Core.Tickets> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Tickets.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.Core.Tickets>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.Core.Tickets, bool>> filter = null, Func<IQueryable<DomainClass.Core.Tickets>, IOrderedQueryable<DomainClass.Core.Tickets>> orderBy = null)
        {
            return await _uow.Tickets.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
