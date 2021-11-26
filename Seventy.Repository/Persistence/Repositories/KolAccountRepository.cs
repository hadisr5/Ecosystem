using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class KolAccountRepository : Repository<DomainClass.Accounting.KolAccount>, IKolAccountRepository
    {
        public KolAccountRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
