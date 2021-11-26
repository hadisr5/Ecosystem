using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.ExerciseUser
{
    public class ExerciseUserService : BaseService.BaseService<DomainClass.EDU.Exercise.ExerciseUser>, IExerciseUserService
    {
        public ExerciseUserService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.Exercise.ExerciseUser> Table() => _uow.ExerciseUser.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.Exercise.ExerciseUser> TableNoTracking() => _uow.ExerciseUser.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.Exercise.ExerciseUser> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.ExerciseUser.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Exercise.ExerciseUser entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.ExerciseUser.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Exercise.ExerciseUser> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.ExerciseUser.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Exercise.ExerciseUser> InsertAsync(DomainClass.EDU.Exercise.ExerciseUser entity, CancellationToken cancellationToken)
        {
            var result = await _uow.ExerciseUser.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Exercise.ExerciseUser> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.ExerciseUser.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Exercise.ExerciseUser> UpdateAsync(DomainClass.EDU.Exercise.ExerciseUser entity, CancellationToken cancellationToken)
        {
            var result = await _uow.ExerciseUser.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Exercise.ExerciseUser> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.ExerciseUser.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.EDU.Exercise.ExerciseUser>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.Exercise.ExerciseUser, bool>> filter = null, Func<IQueryable<DomainClass.EDU.Exercise.ExerciseUser>, IOrderedQueryable<DomainClass.EDU.Exercise.ExerciseUser>> orderBy = null)
        {
            return await _uow.ExerciseUser.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
