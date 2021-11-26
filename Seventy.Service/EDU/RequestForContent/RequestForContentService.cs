using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.RequestForContent
{
    public class RequestForContentService : BaseService.BaseService<DomainClass.EDU.TrainingContent.RequestForContent>, IRequestForContentService
    {
        public RequestForContentService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.TrainingContent.RequestForContent> Table() => _uow.RequestForContent.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.TrainingContent.RequestForContent> TableNoTracking() => _uow.RequestForContent.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.TrainingContent.RequestForContent> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.RequestForContent.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.TrainingContent.RequestForContent entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.RequestForContent.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.TrainingContent.RequestForContent> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.RequestForContent.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.TrainingContent.RequestForContent> InsertAsync(DomainClass.EDU.TrainingContent.RequestForContent entity, CancellationToken cancellationToken)
        {
            var result = await _uow.RequestForContent.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.TrainingContent.RequestForContent> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.RequestForContent.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.TrainingContent.RequestForContent> UpdateAsync(DomainClass.EDU.TrainingContent.RequestForContent entity, CancellationToken cancellationToken)
        {
            var result = await _uow.RequestForContent.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.TrainingContent.RequestForContent> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.RequestForContent.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.EDU.TrainingContent.RequestForContent>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.TrainingContent.RequestForContent, bool>> filter = null, Func<IQueryable<DomainClass.EDU.TrainingContent.RequestForContent>, IOrderedQueryable<DomainClass.EDU.TrainingContent.RequestForContent>> orderBy = null)
        {
            return await _uow.RequestForContent.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
