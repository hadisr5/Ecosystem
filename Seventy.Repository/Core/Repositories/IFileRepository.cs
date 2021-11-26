using DataTables.AspNet.Core;
using Seventy.ViewModel;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Repository.Core.Repositories
{
    public interface IFileRepository : IRepository<DomainClass.Core.Files>
    {
        public  Task<GridResponseModel> LoadDataAsync(IDataTablesRequest request, CancellationToken cancellationToken=default);
    }
}
