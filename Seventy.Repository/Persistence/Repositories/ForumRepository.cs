using Seventy.Data;
using Seventy.DomainClass.EDU;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class ForumRepository : Repository<Forum>, IForumRepository
    {
        public ForumRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
