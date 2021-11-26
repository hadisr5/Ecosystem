using Seventy.Data;
using Seventy.DomainClass.EDU.TrainingContent;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class UserContentRepository : Repository<UserContent>, IUserContentRepository
    {
        public UserContentRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
