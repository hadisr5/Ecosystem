using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class RolesRepository : Repository<DomainClass.Core.Roles>, IRolesRepository
    {
        public RolesRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
