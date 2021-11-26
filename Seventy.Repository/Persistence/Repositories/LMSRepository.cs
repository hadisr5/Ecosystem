using Seventy.Data;
using Seventy.DomainClass.EDU;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class LMSRepository : Repository<LMS>, ILMSRepository
    {
        public LMSRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
