using Seventy.Data;
using Seventy.DomainClass.Core;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class PermissionsRepository : Repository<Permissions>, IPermissionsRepository
    {
        public PermissionsRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
