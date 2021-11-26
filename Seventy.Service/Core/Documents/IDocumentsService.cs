using System.Threading.Tasks;
using Seventy.Data;
using Seventy.ViewModel.Core;

namespace Seventy.Service.Core.Documents
{
    public interface IDocumentsService : BaseService.IBaseService<DomainClass.Core.Documents>
    {
        Task<PagedList<DocumentsViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters);
    }
}
