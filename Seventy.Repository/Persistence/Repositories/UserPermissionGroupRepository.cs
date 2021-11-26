using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class UserPermissionGroupRepository : Repository<DomainClass.Core.UserPermissionGroup>, IUserPermissionGroupRepository
    {
        public UserPermissionGroupRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
