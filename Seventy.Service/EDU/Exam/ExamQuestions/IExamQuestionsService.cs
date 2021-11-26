using Seventy.Data;
using Seventy.Service.BaseService;
using Seventy.ViewModel.EDU;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.ExamQuestions
{
    public interface IExamQuestionsService : IBaseService<DomainClass.EDU.Exam.ExamQuestions>
    {
        Task<bool> DeleteQuestionsByExamAsync(CancellationToken cancellationToken, int? examID);
        Task<IEnumerable<QuestionsViewModel>> GetQuestionsByExamAsync(int examID);
        Task<Seventy.DomainClass.EDU.Exam.ExamQuestions> GetQuestionByExamAsync(int id, int examID);
    }
}
