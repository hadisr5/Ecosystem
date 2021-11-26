using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class SettlementRequestRepository : Repository<DomainClass.Accounting.SettlementRequest>, ISettlementRequestRepository
    {
        public SettlementRequestRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
