using Seventy.Data;
using Seventy.DomainClass.Core;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class TicketsRepository : Repository<Tickets>, ITicketsRepository
    {
        public TicketsRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
