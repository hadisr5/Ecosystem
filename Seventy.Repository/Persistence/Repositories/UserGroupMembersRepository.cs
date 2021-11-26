using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class UserGroupMembersRepository : Repository<DomainClass.Core.UserGroupMembers>, IUserGroupMembersRepository
    {
        public UserGroupMembersRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
