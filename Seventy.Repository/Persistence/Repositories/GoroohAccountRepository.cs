using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class GoroohAccountRepository : Repository<DomainClass.Accounting.GoroohAccount>, IGoroohAccountRepository
    {
        public GoroohAccountRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
