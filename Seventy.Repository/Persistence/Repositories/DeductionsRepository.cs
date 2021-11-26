using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class DeductionsRepository : Repository<DomainClass.Accounting.Deductions>, IDeductionsRepository
    {
        public DeductionsRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
