using Seventy.Data;
using Seventy.DomainClass.EDU;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class CertificateUserRepository : Repository<CertificateUser>, ICertificateUserRepository
    {
        public CertificateUserRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
