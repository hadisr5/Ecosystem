using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class UserGroupsRepository : Repository<DomainClass.Core.UserGroups>, IUserGroupsRepository
    {
        public UserGroupsRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
