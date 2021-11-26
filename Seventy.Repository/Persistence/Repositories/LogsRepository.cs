using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class LogsRepository : Repository<DomainClass.Core.Logs>, ILogsRepository
    {
        public LogsRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
