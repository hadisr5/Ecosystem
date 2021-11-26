using Seventy.DomainClass.EDU.Course;
using System.Threading.Tasks;

namespace Seventy.Repository.Core.Repositories
{
    public interface ICourseRegistrationRepository : IRepository<CourseRegistration>
    {
        Task<int> RegisterationTotalPrice(CourseRegistration courseRegistration);
    }
}
