using Seventy.Data;
using Seventy.ViewModel.EDU;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.UserCourseGroup
{
    public interface ICourseRegistrationService : BaseService.IBaseService<DomainClass.EDU.Course.CourseRegistration>
    {
        Task<IEnumerable<CourseEnrollmentViewModel>> GetCourseListForEnrollment(string courseType = "بلند مدت");
        Task<int> CheckForRegistration(DomainClass.EDU.Course.CourseRegistration courseRegistration);
    }
}
