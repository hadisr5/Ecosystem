using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class RolePermissionsRepository : Repository<DomainClass.Core.RolePermissions>, IRolePermissionsRepository
    {
        public RolePermissionsRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
