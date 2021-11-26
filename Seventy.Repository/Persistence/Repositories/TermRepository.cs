using Seventy.Data;
using Seventy.DomainClass.EDU.Term;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class TermRepository : Repository<Term>, ITermRepository
    {
        public TermRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
