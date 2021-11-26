using DataTables.AspNet.Core;
using Seventy.DomainClass.EDU.Course;
using Seventy.ViewModel;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Repository.Core.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        public Task<GridResponseModel> LoadDataAsync(IDataTablesRequest request, CancellationToken cancellationToken = default);
    }
}
