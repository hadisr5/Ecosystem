using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Seventy.ViewModel.Core.Users;
using System.Linq;
using Seventy.Repository.Core;
using Seventy.Data;
using System.Threading;
using System.Linq.Expressions;
using System;

namespace Seventy.Service.Core.RolePermissions
{
    public class RolePermissionsService : BaseService.BaseService<DomainClass.Core.RolePermissions>, IRolePermissionsService
    {
        public RolePermissionsService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.Core.RolePermissions> Table() => _uow.RolePermissions.Table.AsEnumerable();
        public override IEnumerable<DomainClass.Core.RolePermissions> TableNoTracking() => _uow.RolePermissions.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.Core.RolePermissions> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.RolePermissions.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.Core.RolePermissions entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.RolePermissions.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.Core.RolePermissions> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.RolePermissions.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.RolePermissions> InsertAsync(DomainClass.Core.RolePermissions entity, CancellationToken cancellationToken)
        {
            var result = await _uow.RolePermissions.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.Core.RolePermissions> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.RolePermissions.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;

        }

        public override async Task<DomainClass.Core.RolePermissions> UpdateAsync(DomainClass.Core.RolePermissions entity, CancellationToken cancellationToken)
        {
            var result = await _uow.RolePermissions.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.Core.RolePermissions> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.RolePermissions.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public async Task<IEnumerable<RolePermissionsViewModel>> GetAllByRoleAsync(int RoleId)
        {
            return await (from p in _uow.RolePermissions.TableNoTracking
                          join a in _uow.Roles.TableNoTracking on p.RoleID equals a.ID
                          join c in _uow.Permissions.TableNoTracking on p.PermissionID equals c.ID
                          where p.RoleID == RoleId
                          select new RolePermissionsViewModel
                          {
                              PermissionId = p.PermissionID,
                              PermissionTitle = c.Title,
                              RoleId = p.RoleID,
                              RoleName = a.Title,
                              Section = c.Section
                          })
                     .AsNoTracking().ToListAsync();
        }

        public override async Task<PagedList<DomainClass.Core.RolePermissions>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.Core.RolePermissions, bool>> filter = null, Func<IQueryable<DomainClass.Core.RolePermissions>, IOrderedQueryable<DomainClass.Core.RolePermissions>> orderBy = null)
        {
            return await _uow.RolePermissions.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
