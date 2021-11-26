using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.LessonObservation
{
    public class LessonObservationService : BaseService.BaseService<DomainClass.EDU.Lesson.LessonObservation>, ILessonObservationService
    {
        public LessonObservationService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.Lesson.LessonObservation> Table() => _uow.LessonObservation.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.Lesson.LessonObservation> TableNoTracking() => _uow.LessonObservation.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.Lesson.LessonObservation> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.LessonObservation.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Lesson.LessonObservation entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.LessonObservation.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Lesson.LessonObservation> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.LessonObservation.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Lesson.LessonObservation> InsertAsync(DomainClass.EDU.Lesson.LessonObservation entity, CancellationToken cancellationToken)
        {
            var result = await _uow.LessonObservation.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Lesson.LessonObservation> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.LessonObservation.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Lesson.LessonObservation> UpdateAsync(DomainClass.EDU.Lesson.LessonObservation entity, CancellationToken cancellationToken)
        {
            var result = await _uow.LessonObservation.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Lesson.LessonObservation> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.LessonObservation.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.EDU.Lesson.LessonObservation>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.Lesson.LessonObservation, bool>> filter = null, Func<IQueryable<DomainClass.EDU.Lesson.LessonObservation>, IOrderedQueryable<DomainClass.EDU.Lesson.LessonObservation>> orderBy = null)
        {
            return await _uow.LessonObservation.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
