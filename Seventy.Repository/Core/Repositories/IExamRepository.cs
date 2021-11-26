using DataTables.AspNet.Core;
using Seventy.DomainClass.EDU.Exam;
using Seventy.ViewModel;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Repository.Core.Repositories
{
    public interface IExamRepository : IRepository<Exam>
    {
        public Task<GridResponseModel> LoadDataAsync(IDataTablesRequest request, CancellationToken cancellationToken = default);
    }
}
