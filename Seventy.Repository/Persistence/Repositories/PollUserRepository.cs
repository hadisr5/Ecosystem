using Seventy.Data;
using Seventy.DomainClass.EDU.Poll;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class PollUserRepository : Repository<PollUser>, IPollUserRepository
    {
        public PollUserRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
