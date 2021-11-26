using Seventy.Data;
using Seventy.DomainClass.EDU.TrainingContent;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class ContentObservationRepository : Repository<ContentObservation>, IContentObservationRepository
    {
        public ContentObservationRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
