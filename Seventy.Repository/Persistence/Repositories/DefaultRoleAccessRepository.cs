using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class DefaultRoleAccessRepository : Repository<DomainClass.Core.DefaultRoleAccess>, IDefaultRoleAccessRepository
    {
        public DefaultRoleAccessRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
