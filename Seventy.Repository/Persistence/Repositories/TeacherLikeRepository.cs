using Seventy.Data;
using Seventy.DomainClass.EDU.Teacher;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class TeacherLikeRepository : Repository<TeacherLike>, ITeacherLikeRepository
    {
        public TeacherLikeRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
