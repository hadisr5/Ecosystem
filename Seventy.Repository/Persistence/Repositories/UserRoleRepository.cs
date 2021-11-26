using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class UserRoleRepository : Repository<DomainClass.Core.UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
