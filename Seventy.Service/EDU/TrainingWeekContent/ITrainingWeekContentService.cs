using Seventy.Data;
using Seventy.ViewModel.EDU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Seventy.Common.Enums;
using Seventy.ViewModel.Core;
using Seventy.ViewModel.EDU.Content;

namespace Seventy.Service.EDU.TrainingWeekContent
{
    public interface ITrainingWeekContentService : BaseService.IBaseService<DomainClass.EDU.TrainingWeek.TrainingWeekContent>
    {
        Task<PagedList<TrainingWeekContentViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters
            , Expression<Func<TrainingWeekContentViewModel, bool>> filter = null, Func<IQueryable<TrainingWeekContentViewModel>
                , IOrderedQueryable<TrainingWeekContentViewModel>> orderBy = null);

        Task<List<FilesSecondViewModel>> GetByLessonIdAndTypeAsync(ContentTypeEnum type, int lessonId,
            CancellationToken cancellationToken);

        Task<List<ContentUserViewModel>> GetUserByContentIdAsync(int contentId, CancellationToken cancellationToken);

        Task<List<ContentUserViewModel>> GetUserByContentWeekIdAsync(CancellationToken cancellationToken, int weekId,
            int? userId = null);

        Task<ContentOtherViewModel> GetOtherInfoAsync(CancellationToken cancellationToken, int weekId);
    }
}
