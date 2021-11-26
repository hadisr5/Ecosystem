using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Core.Permissions
{
    public class PermissionsService : BaseService.BaseService<DomainClass.Core.Permissions>, IPermissionsService
    {
        public PermissionsService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.Core.Permissions> Table() => _uow.Permissions.Table.AsEnumerable();
        public override IEnumerable<DomainClass.Core.Permissions> TableNoTracking() => _uow.Permissions.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.Core.Permissions> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.Permissions.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.Core.Permissions entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Permissions.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.Core.Permissions> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Permissions.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.Permissions> InsertAsync(DomainClass.Core.Permissions entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Permissions.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.Core.Permissions> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Permissions.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.Permissions> UpdateAsync(DomainClass.Core.Permissions entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Permissions.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.Core.Permissions> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Permissions.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.Core.Permissions>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.Core.Permissions, bool>> filter = null, Func<IQueryable<DomainClass.Core.Permissions>, IOrderedQueryable<DomainClass.Core.Permissions>> orderBy = null)
        {
            return await _uow.Permissions.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
