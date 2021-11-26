using Seventy.Data;
using Seventy.DomainClass.EDU.Exam;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class ExamAnswerSheetRepository : Repository<ExamAnswerSheet>, IExamAnswerSheetRepository
    {
        public ExamAnswerSheetRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
