using Seventy.Data;
using Seventy.DomainClass.EDU.Exam;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class QuestionOptionsRepository : Repository<QuestionOptions>, IQuestionOptionsRepository
    {
        public QuestionOptionsRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
