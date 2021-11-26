using Seventy.Data;
using Seventy.ViewModel.EDU;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.TrainingWeek
{
    public interface ITrainingWeekService : BaseService.IBaseService<DomainClass.EDU.TrainingWeek.TrainingWeek>
    {
        Task<PagedList<TrainingWeekListViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters
       , Expression<Func<TrainingWeekListViewModel, bool>> filter = null,
       Func<IQueryable<TrainingWeekListViewModel>, IOrderedQueryable<TrainingWeekListViewModel>> orderBy = null);
    }
}
