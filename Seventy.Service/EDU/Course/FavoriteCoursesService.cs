using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.FavoriteCourses
{
    public class FavoriteCoursesService : BaseService.BaseService<DomainClass.EDU.Course.FavoriteCourses>, IFavoriteCoursesService
    {
        public FavoriteCoursesService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.Course.FavoriteCourses> Table() => _uow.FavoriteCourses.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.Course.FavoriteCourses> TableNoTracking() => _uow.FavoriteCourses.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.Course.FavoriteCourses> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.FavoriteCourses.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Course.FavoriteCourses entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.FavoriteCourses.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Course.FavoriteCourses> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.FavoriteCourses.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Course.FavoriteCourses> InsertAsync(DomainClass.EDU.Course.FavoriteCourses entity, CancellationToken cancellationToken)
        {
            var result = await _uow.FavoriteCourses.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Course.FavoriteCourses> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.FavoriteCourses.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Course.FavoriteCourses> UpdateAsync(DomainClass.EDU.Course.FavoriteCourses entity, CancellationToken cancellationToken)
        {
            var result = await _uow.FavoriteCourses.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Course.FavoriteCourses> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.FavoriteCourses.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.EDU.Course.FavoriteCourses>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.Course.FavoriteCourses, bool>> filter = null, Func<IQueryable<DomainClass.EDU.Course.FavoriteCourses>, IOrderedQueryable<DomainClass.EDU.Course.FavoriteCourses>> orderBy = null)
        {
            return await _uow.FavoriteCourses.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
