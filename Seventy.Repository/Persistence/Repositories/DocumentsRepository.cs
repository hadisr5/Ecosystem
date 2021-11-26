using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class DocumentsRepository : Repository<DomainClass.Core.Documents>, IDocumentsRepository
    {
        public DocumentsRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
