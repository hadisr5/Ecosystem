using Seventy.Data;
using Seventy.ViewModel.EDU;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.RelatedCourses
{
    public interface IRelatedCoursesService : BaseService.IBaseService<DomainClass.EDU.Course.RelatedCourses>
    {
        Task<PagedList<RelatedCoursesViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters
            , Expression<Func<RelatedCoursesViewModel, bool>> filter = null, Func<IQueryable<RelatedCoursesViewModel>
                , IOrderedQueryable<RelatedCoursesViewModel>> orderBy = null);
    }
}
