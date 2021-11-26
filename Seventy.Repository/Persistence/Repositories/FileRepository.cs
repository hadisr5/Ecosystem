using DataTables.AspNet.Core;
using Extensions;
using Seventy.Data;
using Seventy.Repository.Core.Repositories;
using Seventy.ViewModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Repository.Persistence.Repositories
{
    public class FileRepository : Repository<DomainClass.Core.Files>, IFileRepository
    {
        public FileRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public async Task<GridResponseModel> LoadDataAsync(IDataTablesRequest request, CancellationToken cancellationToken = default)
        {
          return await TableNoTracking.Where(x => x.IsActive).LoadDataAsync(request, f => f.ID,cancellationToken);
        }
    }
}
