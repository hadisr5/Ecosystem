using Seventy.Data;
using Seventy.DomainClass.EDU;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.CourseGroup
{
    public interface ICourseGroupsService : BaseService.IBaseService<DomainClass.EDU.Course.CourseGroups>
    {
        Task<PagedList<CourseGroupsViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters
            , Expression<Func<CourseGroupsViewModel, bool>> filter = null, Func<IQueryable<CourseGroupsViewModel>
                , IOrderedQueryable<CourseGroupsViewModel>> orderBy = null);
    }
}
