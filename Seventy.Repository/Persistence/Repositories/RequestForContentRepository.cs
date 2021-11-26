using Seventy.Data;
using Seventy.DomainClass.EDU.TrainingContent;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class RequestForContentRepository : Repository<RequestForContent>, IRequestForContentRepository
    {
        public RequestForContentRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
