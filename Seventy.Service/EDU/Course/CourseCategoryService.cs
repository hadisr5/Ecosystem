using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.CourseCategory
{
    public class CourseCategoryService : BaseService.BaseService<DomainClass.EDU.Course.CourseCategory>, ICourseCategoryService
    {
        public CourseCategoryService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.Course.CourseCategory> Table() => _uow.CourseCategory.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.Course.CourseCategory> TableNoTracking() => _uow.CourseCategory.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.Course.CourseCategory> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.CourseCategory.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Course.CourseCategory entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.CourseCategory.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Course.CourseCategory> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.CourseCategory.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Course.CourseCategory> InsertAsync(DomainClass.EDU.Course.CourseCategory entity, CancellationToken cancellationToken)
        {
            var result = await _uow.CourseCategory.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Course.CourseCategory> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.CourseCategory.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Course.CourseCategory> UpdateAsync(DomainClass.EDU.Course.CourseCategory entity, CancellationToken cancellationToken)
        {
            var result = await _uow.CourseCategory.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Course.CourseCategory> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.CourseCategory.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.EDU.Course.CourseCategory>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.Course.CourseCategory, bool>> filter = null, Func<IQueryable<DomainClass.EDU.Course.CourseCategory>, IOrderedQueryable<DomainClass.EDU.Course.CourseCategory>> orderBy = null)
        {
            return await _uow.CourseCategory.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
