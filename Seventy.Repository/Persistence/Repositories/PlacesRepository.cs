using Seventy.Data;
using Seventy.DomainClass.Core;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class PlacesRepository : Repository<Places>, IPlacesRepository
    {
        public PlacesRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
