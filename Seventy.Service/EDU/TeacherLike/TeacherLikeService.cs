using Seventy.Data;
using Seventy.DomainClass.EDU.Teacher;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.TeacherUser
{
    public class TeacherLikeService : BaseService.BaseService<DomainClass.EDU.Teacher.TeacherLike>, ITeacherLikeService
    {
        public TeacherLikeService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.Teacher.TeacherLike> Table() => _uow.TeacherLike.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.Teacher.TeacherLike> TableNoTracking() => _uow.TeacherLike.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.Teacher.TeacherLike> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.TeacherLike.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Teacher.TeacherLike entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.TeacherLike.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Teacher.TeacherLike> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.TeacherLike.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Teacher.TeacherLike> InsertAsync(DomainClass.EDU.Teacher.TeacherLike entity, CancellationToken cancellationToken)
        {
            var result = await _uow.TeacherLike.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Teacher.TeacherLike> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.TeacherLike.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Teacher.TeacherLike> UpdateAsync(DomainClass.EDU.Teacher.TeacherLike entity, CancellationToken cancellationToken)
        {
            var result = await _uow.TeacherLike.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Teacher.TeacherLike> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.TeacherLike.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<TeacherLike>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<TeacherLike, bool>> filter = null, Func<IQueryable<TeacherLike>, IOrderedQueryable<TeacherLike>> orderBy = null)
        {
            return await _uow.TeacherLike.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
