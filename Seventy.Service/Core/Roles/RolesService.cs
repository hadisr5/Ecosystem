using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.EntityFrameworkCore;
using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Core.Roles
{
    public class RoleService : BaseService.BaseService<DomainClass.Core.Roles>, IRolesService
    {
        public RoleService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.Core.Roles> Table() => _uow.Roles.Table.AsEnumerable();
        public override IEnumerable<DomainClass.Core.Roles> TableNoTracking() => _uow.Roles.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.Core.Roles> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.Roles.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.Core.Roles entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Roles.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.Core.Roles> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Roles.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.Roles> InsertAsync(DomainClass.Core.Roles entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Roles.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.Core.Roles> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Roles.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.Roles> UpdateAsync(DomainClass.Core.Roles entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Roles.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.Core.Roles> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Roles.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.Core.Roles>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.Core.Roles, bool>> filter = null, Func<IQueryable<DomainClass.Core.Roles>, IOrderedQueryable<DomainClass.Core.Roles>> orderBy = null)
        {
            return await _uow.Roles.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }

        public async Task<DataSourceResult> GetAllRoles_DataSourceResult(DataSourceRequest request)
        {
            return await _uow.Roles.TableNoTracking.ToDataSourceResultAsync(request, s => new
            {
                ID = s.ID,
                Title = s.Title
            });
        }
    }
}
