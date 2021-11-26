using System.Threading;
using System.Threading.Tasks;
using Seventy.ViewModel.EDU.Main;

namespace Seventy.Service.EDU.Main
{
    public interface IMainService
    {
        Task<MainTeacherViewModel> GetTeacherHomeDataAsync(CancellationToken cancellationToken);

        Task<MainStudentViewModel> GetStudentHomeDataAsync(CancellationToken cancellationToken);
    }
}
