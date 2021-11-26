using Seventy.Data;
using Seventy.ViewModel.EDU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Seventy.Common.Enums;
using Seventy.ViewModel.EDU.Course;
using Seventy.ViewModel;
using DataTables.AspNet.Core;

namespace Seventy.Service.EDU.Course
{
    public interface ICourseService : BaseService.IBaseService<DomainClass.EDU.Course.Course>
    {
        IEnumerable<DomainClass.EDU.Course.Course> TableNoTracking(int reguserID);
        Task<PagedList<CourseViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters
            , Expression<Func<CourseViewModel, bool>> filter = null, Func<IQueryable<CourseViewModel>
                , IOrderedQueryable<CourseViewModel>> orderBy = null);

        Task<PagedList<CourseViewModel>> GetLongTermPaginatedAsync(GenericPagingParameters genericPagingParameters
         , Expression<Func<CourseViewModel, bool>> filter = null, Func<IQueryable<CourseViewModel>
            , IOrderedQueryable<CourseViewModel>> orderBy = null);
        
        Task<PagedList<CourseViewModel>> GetByTeacherAsync(CourseEnum type,
            GenericPagingParameters genericPagingParameters
            , Expression<Func<CourseViewModel, bool>> filter = null, Func<IQueryable<CourseViewModel>
                , IOrderedQueryable<CourseViewModel>> orderBy = null);

        Task<PagedList<CourseViewModel>> GetCustomCourseAsync(CourseEnum type,
            GenericPagingParameters genericPagingParameters
            , Expression<Func<CourseViewModel, bool>> filter = null, Func<IQueryable<CourseViewModel>
                , IOrderedQueryable<CourseViewModel>> orderBy = null);

        public Task<GridResponseModel> GetCustomCourseAsync(CourseEnum type, IDataTablesRequest request);

        Task<PagedList<CourseViewModel>> GetByTypeAsync(CourseEnum type, GenericPagingParameters genericPagingParameters
            , Expression<Func<CourseViewModel, bool>> filter = null, Func<IQueryable<CourseViewModel>
                , IOrderedQueryable<CourseViewModel>> orderBy = null);
    }
}
