using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.Forum
{
    public class ForumService : BaseService.BaseService<DomainClass.EDU.Forum>, IForumService
    {
        public ForumService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.Forum> Table() => _uow.Forum.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.Forum> TableNoTracking() => _uow.Forum.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.Forum> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.Forum.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Forum entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Forum.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Forum> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Forum.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Forum> InsertAsync(DomainClass.EDU.Forum entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Forum.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Forum> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Forum.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Forum> UpdateAsync(DomainClass.EDU.Forum entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Forum.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Forum> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Forum.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.EDU.Forum>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.Forum, bool>> filter = null, Func<IQueryable<DomainClass.EDU.Forum>, IOrderedQueryable<DomainClass.EDU.Forum>> orderBy = null)
        {
            return await _uow.Forum.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
