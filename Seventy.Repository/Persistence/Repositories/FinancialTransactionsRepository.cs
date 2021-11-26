using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class FinancialTransactionsRepository : Repository<DomainClass.Accounting.FinancialTransactions>, IFinancialTransactionsRepository
    {
        public FinancialTransactionsRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
