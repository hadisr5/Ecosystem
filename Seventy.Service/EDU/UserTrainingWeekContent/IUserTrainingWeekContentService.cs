using Seventy.ViewModel.EDU.TrainingWeek;
using Seventy.ViewModel.EDU.TrainingWeek.TrainingWeekSituationSummary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.UserTrainingWeekContent
{
    public interface IUserTrainingWeekContentService : BaseService.IBaseService<DomainClass.EDU.TrainingWeek.UserTrainingWeekContent>
    {
        Task<TrainingWeekSituationSummaryViewModel> GetUserTrainingWeekSummaryReport(int courseID, int userID);
        Task<UserCourseSummaryViewModel> GetUserTrainingWeekSummaryReportByLesson(int termID, int courseID, int lessonID, int userID);
        Task<List<DomainClass.EDU.TrainingWeek.UserTrainingWeekContent>> GetByUserIDAsync(int userID);
    }
}
