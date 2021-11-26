using Seventy.Data;
using Seventy.DomainClass.EDU.Poll;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class PollRepository : Repository<Poll>, IPollRepository
    {
        public PollRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
