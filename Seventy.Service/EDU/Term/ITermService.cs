using Seventy.Data;
using Seventy.ViewModel.EDU;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.Term
{
    public interface ITermService : BaseService.IBaseService<DomainClass.EDU.Term.Term>
    {
        IQueryable<DomainClass.EDU.Term.Term> GetUserTerms(DomainClass.Core.Users user);
        Task<PagedList<TermViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters
            , Expression<Func<TermViewModel, bool>> filter = null,
            Func<IQueryable<TermViewModel>, IOrderedQueryable<TermViewModel>> orderBy = null);

        IQueryable<LessonViewModel> GetUserLessonsByTerm(DomainClass.Core.Users user, DomainClass.EDU.Term.Term term);
    }
}
