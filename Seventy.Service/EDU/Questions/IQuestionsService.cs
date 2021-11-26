using Seventy.Data;
using Seventy.ViewModel.EDU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Seventy.DomainClass.Core;
using Seventy.ViewModel;
using DataTables.AspNet.Core;

namespace Seventy.Service.EDU.Questions
{
    public interface IQuestionsService : BaseService.IBaseService<DomainClass.EDU.Exam.Questions>
    {
        Task<IEnumerable<QuestionsViewModel>> GetAllQuestionsByLessonID(CancellationToken cancellationToken, int? CourseID, int? QuestionLevel, List<int> excludeList
            , bool MultiOption = true, bool RegisteredByCurrentUser = true);

        Task<PagedList<QuestionsViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters
            , Expression<Func<QuestionsViewModel, bool>> filter = null,
             Func<IQueryable<QuestionsViewModel>, IOrderedQueryable<QuestionsViewModel>> orderBy = null);

        Task<PagedList<QuestionsViewModel>> GetAllPaginatedByLessonIdAsync(int lessonId,
            GenericPagingParameters genericPagingParameters
            , Expression<Func<QuestionsViewModel, bool>> filter = null,
            Func<IQueryable<QuestionsViewModel>, IOrderedQueryable<QuestionsViewModel>> orderBy = null);

        Task<PagedList<QuestionsViewModel>> GetAllPaginatedBySumBaromAsync(int examId,
            GenericPagingParameters genericPagingParameters
            , Expression<Func<QuestionsViewModel, bool>> filter = null,
            Func<IQueryable<QuestionsViewModel>, IOrderedQueryable<QuestionsViewModel>> orderBy = null);

        Task<GridResponseModel> GetExamQuestionsBySumBaromAsync(int ExamID, IDataTablesRequest request, CancellationToken cancellationToken = default);
        Task<GridResponseModel> GetExamQuestionsAsync(int ExamID, IDataTablesRequest request, CancellationToken cancellationToken = default);

        Task<PagedList<QuestionsViewModel>> GetAllPaginatedCustomAsync(GenericPagingParameters genericPagingParameters
            , Expression<Func<QuestionsViewModel, bool>> filter = null,
            Func<IQueryable<QuestionsViewModel>,
                IOrderedQueryable<QuestionsViewModel>> orderBy = null,
            bool? multiOption = null, int? userId = null, int? barom = null, int? level = null, int? lessonId = null);

        Task<List<UserProfiles>> GetUsersInQuestion(CancellationToken cancellationToken);
    }
}
