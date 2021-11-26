using Microsoft.EntityFrameworkCore;
using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.UserContent
{
    public class UserContentService : BaseService.BaseService<DomainClass.EDU.TrainingContent.UserContent>, IUserContentService
    {
        public UserContentService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.TrainingContent.UserContent> Table() => _uow.UserContent.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.TrainingContent.UserContent> TableNoTracking() => _uow.UserContent.TableNoTracking.AsEnumerable();

        public async Task<List<DomainClass.EDU.TrainingContent.UserContent>> GetByUserIDAsync(int userID)
        {
            return await _uow.UserContent.TableNoTracking.Where(x => x.UserID == userID).ToListAsync();
        }

        public override async Task<DomainClass.EDU.TrainingContent.UserContent> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.UserContent.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.TrainingContent.UserContent entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.UserContent.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.TrainingContent.UserContent> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.UserContent.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.TrainingContent.UserContent> InsertAsync(DomainClass.EDU.TrainingContent.UserContent entity, CancellationToken cancellationToken)
        {
            var result = await _uow.UserContent.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.TrainingContent.UserContent> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.UserContent.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.TrainingContent.UserContent> UpdateAsync(DomainClass.EDU.TrainingContent.UserContent entity, CancellationToken cancellationToken)
        {
            var result = await _uow.UserContent.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.TrainingContent.UserContent> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.UserContent.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.EDU.TrainingContent.UserContent>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.TrainingContent.UserContent, bool>> filter = null, Func<IQueryable<DomainClass.EDU.TrainingContent.UserContent>, IOrderedQueryable<DomainClass.EDU.TrainingContent.UserContent>> orderBy = null)
        {
            return await _uow.UserContent.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
