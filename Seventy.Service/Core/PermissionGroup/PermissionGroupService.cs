using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Core.PermissionGroup
{
    public class PermissionGroupService : BaseService.BaseService<DomainClass.Core.PermissionGroup>, IPermissionGroupService
    {
        public PermissionGroupService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.Core.PermissionGroup> Table() => _uow.PermissionGroup.Table.AsEnumerable();
        public override IEnumerable<DomainClass.Core.PermissionGroup> TableNoTracking() => _uow.PermissionGroup.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.Core.PermissionGroup> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.PermissionGroup.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.Core.PermissionGroup entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.PermissionGroup.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.Core.PermissionGroup> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.PermissionGroup.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.PermissionGroup> InsertAsync(DomainClass.Core.PermissionGroup entity, CancellationToken cancellationToken)
        {
            var result = await _uow.PermissionGroup.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.Core.PermissionGroup> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.PermissionGroup.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.PermissionGroup> UpdateAsync(DomainClass.Core.PermissionGroup entity, CancellationToken cancellationToken)
        {
            var result = await _uow.PermissionGroup.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.Core.PermissionGroup> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.PermissionGroup.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.Core.PermissionGroup>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.Core.PermissionGroup, bool>> filter = null, Func<IQueryable<DomainClass.Core.PermissionGroup>, IOrderedQueryable<DomainClass.Core.PermissionGroup>> orderBy = null)
        {
            return await _uow.PermissionGroup.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
        public async Task<DataSourceResult> GetAllPermissionGroup_DataSourceResult(DataSourceRequest request)
        {
            return await _uow.PermissionGroup.TableNoTracking.ToDataSourceResultAsync(request, s => new
            {
                ID = s.ID,
                Title = s.Title
            });
        }

    }
}
