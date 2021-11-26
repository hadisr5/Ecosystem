using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Seventy.Data;
using Seventy.Repository.Core;
using Seventy.Service.Users;
using Seventy.ViewModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Core.UserPermissionGroup
{
    public class UserPermissionGroupService : BaseService.BaseService<DomainClass.Core.UserPermissionGroup>, IUserPermissionGroupService
    {
        private readonly IUserManager userManager;
        private readonly IMemoryCache memoryCache;

        public UserPermissionGroupService(IUnitOfWork uow,IUserManager userManager,IMemoryCache memoryCache) : base(uow)
        {
            this.userManager = userManager;
            this.memoryCache = memoryCache;
        }

        public override IEnumerable<DomainClass.Core.UserPermissionGroup> Table() => _uow.UserPermissionGroup.Table.AsEnumerable();
        public override IEnumerable<DomainClass.Core.UserPermissionGroup> TableNoTracking() => _uow.UserPermissionGroup.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.Core.UserPermissionGroup> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.UserPermissionGroup.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.Core.UserPermissionGroup entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.UserPermissionGroup.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.Core.UserPermissionGroup> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.UserPermissionGroup.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.UserPermissionGroup> InsertAsync(DomainClass.Core.UserPermissionGroup entity, CancellationToken cancellationToken)
        {
            var result = await _uow.UserPermissionGroup.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.Core.UserPermissionGroup> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.UserPermissionGroup.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.UserPermissionGroup> UpdateAsync(DomainClass.Core.UserPermissionGroup entity, CancellationToken cancellationToken)
        {
            var result = await _uow.UserPermissionGroup.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.Core.UserPermissionGroup> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.UserPermissionGroup.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.Core.UserPermissionGroup>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.Core.UserPermissionGroup, bool>> filter = null, Func<IQueryable<DomainClass.Core.UserPermissionGroup>, IOrderedQueryable<DomainClass.Core.UserPermissionGroup>> orderBy = null)
        {
            return await _uow.UserPermissionGroup.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }

        public async Task<DataSourceResult> GetPermissionGroupForUserAsync_DataSourceResult(int userID, DataSourceRequest request)
        {
            var result = await _uow.PermissionGroup.TableNoTracking.ToDataSourceResultAsync(request, p => new
            {
                ID = (int)p.ID,
                Permission = p.Title,
                Controller = "",
                Action = "",
                Route = "",
                HasPermission = _uow.UserPermissionGroup.TableNoTracking.Any(a => a.UserID == userID && a.PermissionGroupID == p.ID) ? true : false
            });
            return result;
        }
        public async Task<int> ChangeUserGroupAccessAsync(SavePermissionViewModel viewModel, CancellationToken cancellationToken)
        {
            var userPermissionGroup = await _uow.UserPermissionGroup.Table.Where(w => w.UserID == viewModel.UserID).ToListAsync();
            List<int> pendingDeleteAccess = new List<int>();

            foreach (var group in userPermissionGroup)
                if (!viewModel.Permissions.Any(a => a.Equals(group.PermissionGroupID)))
                {
                    pendingDeleteAccess.Add(group.PermissionGroupID);
                    await _uow.UserPermissionGroup.DeleteAsync(group, cancellationToken, true);
                }
            
            var accessPermissionGroup = _uow.AccessPermissionGroup.Table.Where(r => pendingDeleteAccess.Contains(r.PermissionGroupID)).Select(s => s.AccessID);
            var pendingForDelete = _uow.UserAccess.Table.Where(w => w.UserID == viewModel.UserID && accessPermissionGroup.Contains(w.AccessID));
            await _uow.UserAccess.DeleteRangeAsync(pendingForDelete, cancellationToken);

            foreach (var permissionGroupId in viewModel.Permissions)
                if (!userPermissionGroup.Any(a => a.PermissionGroupID == permissionGroupId))
                {
                    await _uow.UserPermissionGroup.InsertAsync(new DomainClass.Core.UserPermissionGroup
                    {
                        PermissionGroupID = permissionGroupId,
                        UserID = viewModel.UserID,
                        IsActive = true,
                        RegDate = DateTime.Now,
                        RegUserID = userManager.GetCurrentUserID(),
                    }, cancellationToken);
                    var accessPermissionGroups = await _uow.AccessPermissionGroup.Table.Where(w => w.PermissionGroupID == permissionGroupId).Select(s => new DomainClass.Core.UserAccess
                    {
                        AccessID = s.AccessID,
                        UserID = viewModel.UserID,
                        IsActive = true,
                        RegDate = DateTime.Now,
                        RegUserID = userManager.GetCurrentUserID(),
                    }).ToListAsync();
                    if (accessPermissionGroups.Count > 0)
                        await _uow.UserAccess.InsertRangeAsync(accessPermissionGroups, cancellationToken);
                }
            memoryCache.Remove("useraccess");
            return await _uow.CompleteAsync(cancellationToken);
        }
    }
}
