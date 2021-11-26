using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class UserDocumentsRepository : Repository<DomainClass.Core.UserDocuments>, IUserDocumentsRepository
    {
        public UserDocumentsRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
