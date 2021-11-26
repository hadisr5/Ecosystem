using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.ContentObservation
{
    public class ContentObservationService : BaseService.BaseService<DomainClass.EDU.TrainingContent.ContentObservation>, IContentObservationService
    {
        public ContentObservationService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.TrainingContent.ContentObservation> Table() => _uow.ContentObservation.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.TrainingContent.ContentObservation> TableNoTracking() => _uow.ContentObservation.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.TrainingContent.ContentObservation> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.ContentObservation.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.TrainingContent.ContentObservation entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.ContentObservation.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.TrainingContent.ContentObservation> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.ContentObservation.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.TrainingContent.ContentObservation> InsertAsync(DomainClass.EDU.TrainingContent.ContentObservation entity, CancellationToken cancellationToken)
        {
            var result = await _uow.ContentObservation.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.TrainingContent.ContentObservation> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.ContentObservation.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.TrainingContent.ContentObservation> UpdateAsync(DomainClass.EDU.TrainingContent.ContentObservation entity, CancellationToken cancellationToken)
        {
            var result = await _uow.ContentObservation.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.TrainingContent.ContentObservation> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.ContentObservation.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.EDU.TrainingContent.ContentObservation>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.TrainingContent.ContentObservation, bool>> filter = null, Func<IQueryable<DomainClass.EDU.TrainingContent.ContentObservation>, IOrderedQueryable<DomainClass.EDU.TrainingContent.ContentObservation>> orderBy = null)
        {
            return await _uow.ContentObservation.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
