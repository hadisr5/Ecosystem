using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class UsersRepository : Repository<DomainClass.Core.Users>, IUsersRepository
    {
        public UsersRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
