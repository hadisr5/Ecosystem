using Seventy.Data;
using Seventy.DomainClass.EDU.Course;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class FavoriteCoursesRepository : Repository<FavoriteCourses>, IFavoriteCoursesRepository
    {
        public FavoriteCoursesRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
