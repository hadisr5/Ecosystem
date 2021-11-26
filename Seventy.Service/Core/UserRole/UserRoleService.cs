using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Seventy.Data;
using Seventy.DomainClass.Core;
using Seventy.Repository.Core;
using Seventy.Service.Users;
using Seventy.ViewModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Core.UserRole
{
    public class UserRoleService : BaseService.BaseService<DomainClass.Core.UserRole>, IUserRoleService
    {
        private readonly IUserManager userManager;
        private readonly IMemoryCache memoryCache;

        public UserRoleService(IUnitOfWork uow, IUserManager userManager, IMemoryCache memoryCache) : base(uow)
        {
            this.userManager = userManager;
            this.memoryCache = memoryCache;
        }

        public override IEnumerable<DomainClass.Core.UserRole> Table() => _uow.UserRole.Table.AsEnumerable();
        public override IEnumerable<DomainClass.Core.UserRole> TableNoTracking() => _uow.UserRole.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.Core.UserRole> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.UserRole.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.Core.UserRole entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.UserRole.DeleteAsync(entity, cancellationToken, hardDelete);
            if (await RemoveAccessForRole(entity, cancellationToken))
            {
                await _uow.CompleteAsync(cancellationToken);
                return result;
            }
            return false;

        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.Core.UserRole> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.UserRole.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            foreach (var entity in entities)
                await RemoveAccessForRole(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.UserRole> InsertAsync(DomainClass.Core.UserRole entity, CancellationToken cancellationToken)
        {
            var result = await _uow.UserRole.InsertAsync(entity, cancellationToken);
            if (await AddAccessForRole(entity, cancellationToken))
            {
                await _uow.CompleteAsync(cancellationToken);
                return result;
            }
            return null;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.Core.UserRole> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.UserRole.InsertRangeAsync(entities, cancellationToken);
            foreach (var entity in entities)
                await AddAccessForRole(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.UserRole> UpdateAsync(DomainClass.Core.UserRole entity, CancellationToken cancellationToken)
        {
            var result = await _uow.UserRole.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.Core.UserRole> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.UserRole.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.Core.UserRole>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.Core.UserRole, bool>> filter = null, Func<IQueryable<DomainClass.Core.UserRole>, IOrderedQueryable<DomainClass.Core.UserRole>> orderBy = null)
        {
            return await _uow.UserRole.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
        public async Task<int> ChangeUserRolesAsync(SavePermissionViewModel viewModel, CancellationToken cancellationToken)
        {
            var userRoles = await _uow.UserRole.Table.Where(w => w.UserID == viewModel.UserID).ToListAsync();

            foreach (var role in userRoles)
                if (!viewModel.Permissions.Any(a => a.Equals(role.RoleID)))
                {
                    var defaultAccess = await _uow.DefaultRoleAccess.Table.Where(w => w.RoleID == role.RoleID).Select(s => s.AccessID).ToListAsync();
                    var useraccesses = await _uow.UserAccess.Table.Where(w => w.UserID == viewModel.UserID).ToListAsync();
                    foreach (var item in useraccesses)
                    {
                        if (defaultAccess.Contains(item.AccessID))
                            await _uow.UserAccess.DeleteAsync(item, cancellationToken);
                    }
                    await _uow.UserRole.DeleteAsync(role, cancellationToken, true);
                }

            foreach (var roleId in viewModel.Permissions)
                if (!userRoles.Any(a => a.RoleID == roleId))
                {
                    await _uow.UserRole.InsertAsync(new DomainClass.Core.UserRole
                    {
                        RoleID = roleId,
                        UserID = viewModel.UserID,
                        IsActive = true,
                        RegDate = DateTime.Now,
                        RegUserID = userManager.GetCurrentUserID(),
                    }, cancellationToken);

                    var defaultRoleAccesses = await _uow.DefaultRoleAccess.TableNoTracking.Where(w => w.RoleID == roleId).Select(s => new DomainClass.Core.UserAccess
                    {
                        AccessID = s.AccessID,
                        UserID = viewModel.UserID,
                        IsActive = true,
                        RegDate = DateTime.Now,
                        RegUserID = userManager.GetCurrentUserID(),
                    }).ToListAsync();
                    if (defaultRoleAccesses.Count > 0)
                    {
                        foreach (var defaultRole in defaultRoleAccesses)
                            if (!await _uow.UserAccess.TableNoTracking.AnyAsync(a => a.UserID == defaultRole.UserID && a.AccessID == defaultRole.AccessID, cancellationToken))
                                await _uow.UserAccess.InsertRangeAsync(defaultRoleAccesses, cancellationToken);
                    }
                }
            memoryCache.Remove("useraccess");
            return await _uow.CompleteAsync(cancellationToken);
        }

        public async Task<DataSourceResult> GetRolesForUserAsync_DataSourceResult(int userID, DataSourceRequest request)
        {
            var result = await _uow.Roles.TableNoTracking.ToDataSourceResultAsync(request, p => new
            {
                ID = (int)p.ID,
                Permission = p.Title,
                Controller = "",
                Action = "",
                Route = "",
                HasPermission = _uow.UserRole.TableNoTracking.Any(a => a.UserID == userID && a.RoleID == p.ID) ? true : false
            });
            return result;
        }

        private async Task<bool> AddAccessForRole(DomainClass.Core.UserRole userRole, CancellationToken cancellationToken)
        {
            var accesses = _uow.DefaultRoleAccess.Table.Where(w => w.RoleID == userRole.RoleID)
                .Select(s => new DomainClass.Core.UserAccess
                {
                    AccessID = s.AccessID,
                    UserID = userRole.UserID,
                });
            await _uow.UserAccess.InsertRangeAsync(accesses, cancellationToken);
            return true;
        }
        private async Task<bool> RemoveAccessForRole(DomainClass.Core.UserRole userRole, CancellationToken cancellationToken)
        {
            var query = from defaultRoleAccess in _uow.DefaultRoleAccess.Table
                        join userAccess in _uow.UserAccess.Table
                        on defaultRoleAccess.AccessID equals userAccess.AccessID
                        where userAccess.UserID == userRole.UserID
                        select userAccess;

            await _uow.UserAccess.DeleteRangeAsync(query, cancellationToken);
            return true;
        }
    }
}
