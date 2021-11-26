using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.PollUser
{
    public class PollUserService : BaseService.BaseService<DomainClass.EDU.Poll.PollUser>, IPollUserService
    {
        public PollUserService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.Poll.PollUser> Table() => _uow.PollUser.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.Poll.PollUser> TableNoTracking() => _uow.PollUser.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.Poll.PollUser> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.PollUser.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Poll.PollUser entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.PollUser.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Poll.PollUser> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.PollUser.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Poll.PollUser> InsertAsync(DomainClass.EDU.Poll.PollUser entity, CancellationToken cancellationToken)
        {
            var result = await _uow.PollUser.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Poll.PollUser> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.PollUser.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Poll.PollUser> UpdateAsync(DomainClass.EDU.Poll.PollUser entity, CancellationToken cancellationToken)
        {
            var result = await _uow.PollUser.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Poll.PollUser> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.PollUser.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.EDU.Poll.PollUser>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.Poll.PollUser, bool>> filter = null, Func<IQueryable<DomainClass.EDU.Poll.PollUser>, IOrderedQueryable<DomainClass.EDU.Poll.PollUser>> orderBy = null)
        {
            return await _uow.PollUser.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
