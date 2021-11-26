using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class DocumentTypeRepository : Repository<DomainClass.Core.DocumentType>, IDocumentTypeRepository
    {
        public DocumentTypeRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
