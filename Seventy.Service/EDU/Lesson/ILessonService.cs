using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Seventy.Data;
using Seventy.ViewModel.EDU;

namespace Seventy.Service.EDU.Lesson
{
    public interface ILessonService : BaseService.IBaseService<DomainClass.EDU.Lesson.Lesson>
    {
        Task<PagedList<LessonViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters
            , Expression<Func<LessonViewModel, bool>> filter = null,
            Func<IQueryable<LessonViewModel>
                , IOrderedQueryable<LessonViewModel>> orderBy = null);
    }
}
