using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Caching.Memory;
using Seventy.Common;
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
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Seventy.Common.Utilities;
using Seventy.Common.Enums;

namespace Seventy.Service.Core.UserAccess
{
    public class UserAccessService : BaseService.BaseService<DomainClass.Core.UserAccess>, IUserAccessService
    {
        private readonly IUserManager userManager;
        private readonly IMemoryCache memoryCache;

        public UserAccessService(IUnitOfWork uow, IUserManager userManager, IMemoryCache memoryCache) : base(uow)
        {
            this.userManager = userManager;
            this.memoryCache = memoryCache;
        }

        public override IEnumerable<DomainClass.Core.UserAccess> Table() => _uow.UserAccess
            .Table
            .Include(i => i.Users)
            .Include(i => i.Access)
            .AsEnumerable();
        public override IEnumerable<DomainClass.Core.UserAccess> TableNoTracking() => _uow.UserAccess
            .TableNoTracking
            .Include(i => i.Users)
            .Include(i => i.Access)
            .AsEnumerable();

        public override async Task<DomainClass.Core.UserAccess> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.UserAccess.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.Core.UserAccess entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.UserAccess.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.Core.UserAccess> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.UserAccess.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.UserAccess> InsertAsync(DomainClass.Core.UserAccess entity, CancellationToken cancellationToken)
        {
            var result = await _uow.UserAccess.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.Core.UserAccess> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.UserAccess.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.UserAccess> UpdateAsync(DomainClass.Core.UserAccess entity, CancellationToken cancellationToken)
        {
            var result = await _uow.UserAccess.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.Core.UserAccess> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.UserAccess.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.Core.UserAccess>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.Core.UserAccess, bool>> filter = null, Func<IQueryable<DomainClass.Core.UserAccess>, IOrderedQueryable<DomainClass.Core.UserAccess>> orderBy = null)
        {
            return await _uow.UserAccess.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
        public async Task<bool> CheckForAccess(string path, CancellationToken cancellationToken = default)
        {
            var key = "useraccess";
            var user = await userManager.GetCurrentUserAsync(cancellationToken);
            if (!memoryCache.TryGetValue(key, out List<AccessCache> data))
            {
                data = await _uow.UserAccess.TableNoTracking.Include(i => i.Access).Select(s => new AccessCache
                {
                    allowanonymous = s.Access.AllowAnonymous,
                    route = s.Access.Route,
                    userid = s.UserID
                }).ToListAsync();

                var expirationOptions = new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromDays(1),
                    Priority = CacheItemPriority.High,
                    AbsoluteExpiration = DateTime.Now.AddHours(3)
                };
                memoryCache.Set(key, data, expirationOptions);
            }
            var access = memoryCache.Get<List<AccessCache>>("useraccess").Where(a => a.route == path.ToLower()).ToList();
            if (access.Count == 0 || access.Any(any => any.allowanonymous))
                return true;
            return access.Any(a => a.userid.Equals(user.ID));
        }

        public async Task<int> ChangeUserAccessesAsync(SavePermissionViewModel viewModel, CancellationToken cancellationToken)
        {
            var userAccesses = await _uow.UserAccess.Table.Where(w => w.UserID == viewModel.UserID).ToListAsync();

            foreach (var access in userAccesses)
                if (!viewModel.Permissions.Any(a => a.Equals(access.AccessID)))
                    await _uow.UserAccess.DeleteAsync(access, cancellationToken,true);

            foreach (var accessId in viewModel.Permissions)
                if (!userAccesses.Any(a => a.AccessID == accessId))
                    await _uow.UserAccess.InsertAsync(new DomainClass.Core.UserAccess
                    { 
                        AccessID = accessId,
                        UserID = viewModel.UserID,
                        IsActive = true,
                        RegDate = DateTime.Now,
                        RegUserID = userManager.GetCurrentUserID(),
                    }, cancellationToken);
            memoryCache.Remove("useraccess");
            return await _uow.CompleteAsync(cancellationToken);
        }
        public async Task<DataSourceResult> GetAccessesForUserAsync_DataSourceResult(int userID, DataSourceRequest request)
        {
            var result = await _uow.Access.TableNoTracking.ToDataSourceResultAsync(request, p => new
            {
                ID = (int)p.ID,
                Permission = ((eAccessControl)p.AccessControl).ToDisplay(),
                Controller = p.Controller,
                Action = p.Action,
                Route = p.Route,
                HasPermission = _uow.UserAccess.TableNoTracking.Any(a => a.UserID == userID && a.AccessID == p.ID) ? true : false
            });
            return result;
        }
    }
}
