using Seventy.Data;
using Seventy.DomainClass.EDU;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.TrainingEval
{
    public interface ITrainingEvalIndexService : BaseService.IBaseService<DomainClass.EDU.TrainingEval.TrainingEvalIndex>
    {
        Task<PagedList<TrainingEvalIndexViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<TrainingEvalIndexViewModel, bool>> filter = null, Func<IQueryable<TrainingEvalIndexViewModel>, IOrderedQueryable<TrainingEvalIndexViewModel>> orderBy = null);
    }
}
