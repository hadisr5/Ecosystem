using Seventy.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.UserContent
{
    public interface IUserContentService : BaseService.IBaseService<DomainClass.EDU.TrainingContent.UserContent>
    {
        Task<List<DomainClass.EDU.TrainingContent.UserContent>> GetByUserIDAsync(int userID);
    }
}
