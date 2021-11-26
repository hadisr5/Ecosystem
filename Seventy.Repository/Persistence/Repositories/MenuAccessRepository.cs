using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class MenuAccessRepository : Repository<DomainClass.Core.MenuAccess>, IMenuAccessRepository
    {
        public MenuAccessRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
