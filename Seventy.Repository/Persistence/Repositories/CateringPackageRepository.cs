using Seventy.Data;
using Seventy.DomainClass.EDU;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class CateringPackageRepository : Repository<CateringPackage>, ICateringPackageRepository
    {
        public CateringPackageRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
