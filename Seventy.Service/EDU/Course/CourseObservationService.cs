using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.CourseObservation
{
    public class CourseObservationService : BaseService.BaseService<DomainClass.EDU.Course.CourseObservation>, ICourseObservationService
    {
        public CourseObservationService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.Course.CourseObservation> Table() => _uow.CourseObservation.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.Course.CourseObservation> TableNoTracking() => _uow.CourseObservation.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.Course.CourseObservation> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.CourseObservation.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Course.CourseObservation entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.CourseObservation.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Course.CourseObservation> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.CourseObservation.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Course.CourseObservation> InsertAsync(DomainClass.EDU.Course.CourseObservation entity, CancellationToken cancellationToken)
        {
            var result = await _uow.CourseObservation.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Course.CourseObservation> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.CourseObservation.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Course.CourseObservation> UpdateAsync(DomainClass.EDU.Course.CourseObservation entity, CancellationToken cancellationToken)
        {
            var result = await _uow.CourseObservation.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Course.CourseObservation> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.CourseObservation.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.EDU.Course.CourseObservation>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.Course.CourseObservation, bool>> filter = null, Func<IQueryable<DomainClass.EDU.Course.CourseObservation>, IOrderedQueryable<DomainClass.EDU.Course.CourseObservation>> orderBy = null)
        {
            return await _uow.CourseObservation.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
