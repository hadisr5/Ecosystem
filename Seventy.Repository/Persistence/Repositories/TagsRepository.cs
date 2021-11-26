using Seventy.Data;
using Seventy.Repository.Core.Repositories;

namespace Seventy.Repository.Persistence.Repositories
{
    public class TagsRepository : Repository<DomainClass.Core.Tags>, ITagsRepository
    {
        public TagsRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
