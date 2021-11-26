using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Seventy.Data;
using Seventy.Service.BaseService;

namespace Seventy.Service.EDU.TrainingContent
{
    public interface ITrainingContentService : IBaseService<DomainClass.EDU.TrainingContent.TrainingContent>
    {
        List<DomainClass.EDU.TrainingContent.TrainingContent> GetByType(string type);
    }
}
