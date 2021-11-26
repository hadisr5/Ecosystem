using Seventy.Data;
using Seventy.DomainClass.EDU.Exam;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class ExamQuestionsRepository : Repository<ExamQuestions>, IExamQuestionsRepository
    {
        public ExamQuestionsRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
