using Seventy.Data;
using Seventy.ViewModel.EDU;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Seventy.ViewModel.EDU.TermLesson;
using System.Collections.Generic;

namespace Seventy.Service.EDU.Term
{
    public interface ITermLessonService : BaseService.IBaseService<DomainClass.EDU.Term.TermLesson>
    {
        Task<PagedList<TermLessonViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters
            , Expression<Func<TermLessonViewModel, bool>> filter = null,
            Func<IQueryable<TermLessonViewModel>, IOrderedQueryable<TermLessonViewModel>> orderBy = null);

        Task<List<DomainClass.EDU.Term.TermLesson>> GetByTermAndLesson(int TermID, int LessonID);
    }
}
