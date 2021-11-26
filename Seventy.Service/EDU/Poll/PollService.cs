using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.Poll
{
    public class PollService : BaseService.BaseService<DomainClass.EDU.Poll.Poll>, IPollService
    {
        public PollService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.Poll.Poll> Table() => _uow.Poll.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.Poll.Poll> TableNoTracking() => _uow.Poll.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.Poll.Poll> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.Poll.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Poll.Poll entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Poll.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Poll.Poll> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Poll.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Poll.Poll> InsertAsync(DomainClass.EDU.Poll.Poll entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Poll.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Poll.Poll> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Poll.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Poll.Poll> UpdateAsync(DomainClass.EDU.Poll.Poll entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Poll.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Poll.Poll> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Poll.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.EDU.Poll.Poll>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.Poll.Poll, bool>> filter = null, Func<IQueryable<DomainClass.EDU.Poll.Poll>, IOrderedQueryable<DomainClass.EDU.Poll.Poll>> orderBy = null)
        {
            return await _uow.Poll.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
