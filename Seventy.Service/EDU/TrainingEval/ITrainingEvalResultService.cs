using Seventy.Data;
using Seventy.DomainClass.EDU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.TrainingEval
{
    public interface ITrainingEvalResultService : BaseService.IBaseService<DomainClass.EDU.TrainingEval.TrainingEvalResult>
    {
        Task<PagedList<TrainingEvalResultViewModel>> GetAllPaginatedAsync(
            GenericPagingParameters genericPagingParameters,
            Expression<Func<TrainingEvalResultViewModel, bool>> filter = null,
            Func<IQueryable<TrainingEvalResultViewModel>,
            IOrderedQueryable<TrainingEvalResultViewModel>> orderBy = null);

        Task<List<TrainingEvalIndexViewModel>> GetByType(int targetId, string targetType,
            CancellationToken cancellationToken);
    }
}
