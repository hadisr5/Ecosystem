using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Core.UserProfiles
{
    public class UserProfilesService : BaseService.BaseService<DomainClass.Core.UserProfiles>, IUserProfilesService
    {
        public UserProfilesService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.Core.UserProfiles> Table() => _uow.UserProfiles.Table.AsEnumerable();
        public override IEnumerable<DomainClass.Core.UserProfiles> TableNoTracking() => _uow.UserProfiles.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.Core.UserProfiles> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.UserProfiles.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.Core.UserProfiles entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.UserProfiles.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.Core.UserProfiles> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.UserProfiles.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.UserProfiles> InsertAsync(DomainClass.Core.UserProfiles entity, CancellationToken cancellationToken)
        {
            var result = await _uow.UserProfiles.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.Core.UserProfiles> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.UserProfiles.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;

        }

        public override async Task<DomainClass.Core.UserProfiles> UpdateAsync(DomainClass.Core.UserProfiles entity, CancellationToken cancellationToken)
        {
            var result = await _uow.UserProfiles.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.Core.UserProfiles> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.UserProfiles.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.Core.UserProfiles>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.Core.UserProfiles, bool>> filter = null, Func<IQueryable<DomainClass.Core.UserProfiles>, IOrderedQueryable<DomainClass.Core.UserProfiles>> orderBy = null)
        {
            return await _uow.UserProfiles.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
