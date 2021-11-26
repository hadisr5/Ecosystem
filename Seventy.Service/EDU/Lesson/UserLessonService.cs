using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.UserLesson
{
    public class UserLessonService : BaseService.BaseService<DomainClass.EDU.Lesson.UserLesson>, IUserLessonService
    {
        public UserLessonService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.Lesson.UserLesson> Table() => _uow.UserLesson.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.Lesson.UserLesson> TableNoTracking() => _uow.UserLesson.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.Lesson.UserLesson> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.UserLesson.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Lesson.UserLesson entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.UserLesson.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Lesson.UserLesson> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.UserLesson.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Lesson.UserLesson> InsertAsync(DomainClass.EDU.Lesson.UserLesson entity, CancellationToken cancellationToken)
        {
            var result = await _uow.UserLesson.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Lesson.UserLesson> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.UserLesson.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Lesson.UserLesson> UpdateAsync(DomainClass.EDU.Lesson.UserLesson entity, CancellationToken cancellationToken)
        {
            var result = await _uow.UserLesson.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Lesson.UserLesson> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.UserLesson.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.EDU.Lesson.UserLesson>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.Lesson.UserLesson, bool>> filter = null, Func<IQueryable<DomainClass.EDU.Lesson.UserLesson>, IOrderedQueryable<DomainClass.EDU.Lesson.UserLesson>> orderBy = null)
        {
            return await _uow.UserLesson.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
