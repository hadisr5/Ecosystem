using Microsoft.EntityFrameworkCore;
using Seventy.Data;
using Seventy.Repository.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Repository.Persistence.Repositories
{
    public class UserAccessRepository : Repository<DomainClass.Core.UserAccess>, IUserAccessRepository
    {
        public UserAccessRepository(DataContext dbContext) : base(dbContext)
        {
        }
       
    }
}
