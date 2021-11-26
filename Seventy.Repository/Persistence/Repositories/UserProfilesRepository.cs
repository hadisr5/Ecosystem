using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class UserProfilesRepository : Repository<DomainClass.Core.UserProfiles>, IUserProfilesRepository
    {
        public UserProfilesRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
