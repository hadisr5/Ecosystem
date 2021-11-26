using System;
using System.Linq;
using Seventy.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Seventy.Repository.Core;
using System.Collections.Generic;

namespace Seventy.Service.Core.Message
{
    public class MessageService : BaseService.BaseService<DomainClass.Core.Messages>, IMessageService
    {
        public MessageService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.Core.Messages> Table() => _uow.Messages.Table.AsEnumerable();
        public override IEnumerable<DomainClass.Core.Messages> TableNoTracking() => _uow.Messages.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.Core.Messages> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.Messages.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.Core.Messages entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Messages.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.Core.Messages> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Messages.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.Messages> InsertAsync(DomainClass.Core.Messages entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Messages.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.Core.Messages> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Messages.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;

        }

        public override async Task<DomainClass.Core.Messages> UpdateAsync(DomainClass.Core.Messages entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Messages.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.Core.Messages> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Messages.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.Core.Messages>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.Core.Messages, bool>> filter = null, Func<IQueryable<DomainClass.Core.Messages>, IOrderedQueryable<DomainClass.Core.Messages>> orderBy = null)
        {
            return await _uow.Messages.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
