using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using Seventy.Repository.Core;
using Seventy.Data;
using System.Linq.Expressions;
using System;

namespace Seventy.Service.EDU.RequestedCourses
{
    public class RequestedCoursesService : BaseService.BaseService<DomainClass.EDU.Course.RequestedCourses>, IRequestedCoursesService
    {
        public RequestedCoursesService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.Course.RequestedCourses> Table() => _uow.RequestedCourses.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.Course.RequestedCourses> TableNoTracking() => _uow.RequestedCourses.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.Course.RequestedCourses> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.RequestedCourses.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Course.RequestedCourses entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.RequestedCourses.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Course.RequestedCourses> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.RequestedCourses.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Course.RequestedCourses> InsertAsync(DomainClass.EDU.Course.RequestedCourses entity, CancellationToken cancellationToken)
        {
            var result = await _uow.RequestedCourses.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Course.RequestedCourses> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.RequestedCourses.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Course.RequestedCourses> UpdateAsync(DomainClass.EDU.Course.RequestedCourses entity, CancellationToken cancellationToken)
        {
            var result = await _uow.RequestedCourses.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Course.RequestedCourses> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.RequestedCourses.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.EDU.Course.RequestedCourses>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.Course.RequestedCourses, bool>> filter = null, Func<IQueryable<DomainClass.EDU.Course.RequestedCourses>, IOrderedQueryable<DomainClass.EDU.Course.RequestedCourses>> orderBy = null)
        {
            return await _uow.RequestedCourses.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }

        //#region Fields

        //private readonly IUnitOfWork _uow;
        //private readonly ILogsService _logsService;
        //private readonly IHttpContextAccessor _httpContext;
        //private readonly DbSet<DomainClass.EDU.RequestedCourses> _repository;

        //#endregion

        //public RequestedCoursesService(IUnitOfWork uow, IHttpContextAccessor httpContextAccessor,
        //    ILogsService logsService) : base(uow)
        //{
        //    _uow = uow;
        //    _logsService = logsService;
        //    _httpContext = httpContextAccessor;
        //    _repository = _uow.Set<DomainClass.EDU.RequestedCourses>();
        //}

        //public async Task<PagedList<RequestedCourseViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters
        //   , Expression<Func<RequestedCourseViewModel, bool>> filter = null, Func<IQueryable<RequestedCourseViewModel>
        //       , IOrderedQueryable<RequestedCourseViewModel>> orderBy = null)
        //{
        //    try
        //    {
        //        var data = GetAllQueryable();

        //        var query = from item in data
        //                    select new RequestedCourseViewModel
        //                    {
        //                        ID = item.ID,
        //                        Title = item.Title,
        //                        Status = item.Status,
        //                        CourseType = item.CourseType,
        //                        Description = item.Description,
        //                        IsActive = item.IsActive,
        //                        RegDate = item.RegDate,
        //                        RegUserID = item.RegUserID
        //                    };

        //        if (filter != null)
        //        {
        //            query = query.Where(filter);
        //        }

        //        if (orderBy != null)
        //        {
        //            return await PagedList<RequestedCourseViewModel>.ToPagedList(orderBy(query),
        //                    genericPagingParameters.PageNumber,
        //                    genericPagingParameters.PageSize);
        //        }

        //        return await PagedList<RequestedCourseViewModel>.ToPagedList(query,
        //            genericPagingParameters.PageNumber,
        //            genericPagingParameters.PageSize);
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

    }
}
