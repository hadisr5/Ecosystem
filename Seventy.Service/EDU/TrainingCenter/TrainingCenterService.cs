using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.TrainingCenter
{
    public class TrainingCenterService : BaseService.BaseService<DomainClass.EDU.TrainingCenter>, ITrainingCenterService
    {
        public TrainingCenterService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.TrainingCenter> Table() => _uow.TrainingCenter.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.TrainingCenter> TableNoTracking() => _uow.TrainingCenter.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.TrainingCenter> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.TrainingCenter.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.TrainingCenter entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.TrainingCenter.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.TrainingCenter> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.TrainingCenter.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.TrainingCenter> InsertAsync(DomainClass.EDU.TrainingCenter entity, CancellationToken cancellationToken)
        {
            var result = await _uow.TrainingCenter.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.TrainingCenter> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.TrainingCenter.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.TrainingCenter> UpdateAsync(DomainClass.EDU.TrainingCenter entity, CancellationToken cancellationToken)
        {
            var result = await _uow.TrainingCenter.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.TrainingCenter> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.TrainingCenter.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.EDU.TrainingCenter>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.TrainingCenter, bool>> filter = null, Func<IQueryable<DomainClass.EDU.TrainingCenter>, IOrderedQueryable<DomainClass.EDU.TrainingCenter>> orderBy = null)
        {
            return await _uow.TrainingCenter.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
