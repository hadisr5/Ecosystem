using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Seventy.DomainClass.Core;

namespace Seventy.Service.Core.UserDocument
{
    public interface IUserDocumentService : BaseService.IBaseService<UserDocuments>
    {
        Task<List<UserDocumentsViewModel>> GetDocumentsByUserIdAsync(int userId,
            CancellationToken cancellationToken);
    }
}
