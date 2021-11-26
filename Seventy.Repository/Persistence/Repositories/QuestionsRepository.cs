using Seventy.Data;
using Seventy.DomainClass.EDU.Exam;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class QuestionsRepository : Repository<Questions>, IQuestionsRepository
    {
        public QuestionsRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
