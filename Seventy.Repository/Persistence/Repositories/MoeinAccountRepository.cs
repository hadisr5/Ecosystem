using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class MoeinAccountRepository : Repository<DomainClass.Accounting.MoeinAccount>, IMoeinAccountRepository
    {
        public MoeinAccountRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
