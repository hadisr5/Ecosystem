using Microsoft.EntityFrameworkCore;
using Seventy.Data;
using Seventy.DomainClass.EDU.Course;
using Seventy.Repository.Core.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Seventy.Repository.Persistence.Repositories
{
    public class CourseRegistrationRepository : Repository<CourseRegistration>, ICourseRegistrationRepository
    {
        private readonly ICourseRepository courseRepository;
        private readonly ICateringPackageRepository cateringPackageRepository;
        private readonly ITermRepository termRepository;

        public CourseRegistrationRepository(DataContext dbContext
            , ICourseRepository courseRepository
            , ICateringPackageRepository cateringPackageRepository
            , ITermRepository termRepository) : base(dbContext)
        {
            this.courseRepository = courseRepository;
            this.cateringPackageRepository = cateringPackageRepository;
            this.termRepository = termRepository;
        }
        public async Task<int> RegisterationTotalPrice(CourseRegistration courseRegistration)
        {
            int totalPrice = 0;
            var query = from term in termRepository.TableNoTracking
                        join course in courseRepository.TableNoTracking on term.CourseID equals course.ID
                        where term.ID == courseRegistration.TermID
                        select new { course.Price };
            var selectedcourse = await query.FirstOrDefaultAsync();
            totalPrice += selectedcourse.Price;

            if (courseRegistration.CateringPackId != null)
            {
                var selectedCatering = await cateringPackageRepository.TableNoTracking.FirstOrDefaultAsync(w => w.ID == courseRegistration.CateringPackId);
                totalPrice += selectedCatering.Price;
            }
            return totalPrice;
        }
    }
}
