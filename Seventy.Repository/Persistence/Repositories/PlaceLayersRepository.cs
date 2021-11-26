using Seventy.Data;
using Seventy.DomainClass.Core;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class PlaceLayersRepository : Repository<PlaceLayers>, IPlaceLayersRepository
    {
        public PlaceLayersRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
