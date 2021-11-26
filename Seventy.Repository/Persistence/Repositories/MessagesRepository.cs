using Seventy.Data;
using Seventy.DomainClass.Core;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class MessagesRepository : Repository<Messages>, IMessagesRepository
    {
        public MessagesRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
