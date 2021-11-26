using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class TafsiliAccountRepository : Repository<DomainClass.Accounting.TafsiliAccount>, ITafsiliAccountRepository
    {
        public TafsiliAccountRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
