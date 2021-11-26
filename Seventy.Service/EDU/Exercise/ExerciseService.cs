using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.Exercise
{
    public class ExerciseService : BaseService.BaseService<DomainClass.EDU.Exercise.Exercise>, IExerciseService
    {
        public ExerciseService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.Exercise.Exercise> Table() => _uow.Exercise.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.Exercise.Exercise> TableNoTracking() => _uow.Exercise.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.Exercise.Exercise> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.Exercise.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Exercise.Exercise entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Exercise.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Exercise.Exercise> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Exercise.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Exercise.Exercise> InsertAsync(DomainClass.EDU.Exercise.Exercise entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Exercise.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Exercise.Exercise> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Exercise.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Exercise.Exercise> UpdateAsync(DomainClass.EDU.Exercise.Exercise entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Exercise.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Exercise.Exercise> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Exercise.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.EDU.Exercise.Exercise>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.Exercise.Exercise, bool>> filter = null, Func<IQueryable<DomainClass.EDU.Exercise.Exercise>, IOrderedQueryable<DomainClass.EDU.Exercise.Exercise>> orderBy = null)
        {
            return await _uow.Exercise.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
