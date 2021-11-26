using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.EntityFrameworkCore;
using Seventy.Common.Enums;
using Seventy.Common.Utilities;
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

namespace Seventy.Service.Core.Roles.DefaultRoleAccess
{
    public class DefaultRoleAccessService : BaseService.BaseService<DomainClass.Core.DefaultRoleAccess>, IDefaultRoleAccessService
    {
        private readonly IUserManager userManager;

        public DefaultRoleAccessService(IUnitOfWork uow,IUserManager userManager) : base(uow)
        {
            this.userManager = userManager;
        }

        public override IEnumerable<DomainClass.Core.DefaultRoleAccess> Table() => _uow.DefaultRoleAccess.Table.AsEnumerable();
        public override IEnumerable<DomainClass.Core.DefaultRoleAccess> TableNoTracking() => _uow.DefaultRoleAccess.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.Core.DefaultRoleAccess> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.DefaultRoleAccess.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.Core.DefaultRoleAccess entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.DefaultRoleAccess.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.Core.DefaultRoleAccess> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.DefaultRoleAccess.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.DefaultRoleAccess> InsertAsync(DomainClass.Core.DefaultRoleAccess entity, CancellationToken cancellationToken)
        {
            var result = await _uow.DefaultRoleAccess.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.Core.DefaultRoleAccess> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.DefaultRoleAccess.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.DefaultRoleAccess> UpdateAsync(DomainClass.Core.DefaultRoleAccess entity, CancellationToken cancellationToken)
        {
            var result = await _uow.DefaultRoleAccess.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.Core.DefaultRoleAccess> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.DefaultRoleAccess.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.Core.DefaultRoleAccess>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.Core.DefaultRoleAccess, bool>> filter = null, Func<IQueryable<DomainClass.Core.DefaultRoleAccess>, IOrderedQueryable<DomainClass.Core.DefaultRoleAccess>> orderBy = null)
        {
            return await _uow.DefaultRoleAccess.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
        public async Task<int> ChangeDefaultRoleAccessesAsync(SaveDefaultRoleAccessesViewModel viewModel, CancellationToken cancellationToken)
        {
            var defaultRoleAccesses = await _uow.DefaultRoleAccess.Table.Where(w => w.RoleID == viewModel.RoleID).ToListAsync();

            foreach (var access in defaultRoleAccesses)
                if (!viewModel.Accesses.Any(a => a.Equals(access.AccessID)))
                    await _uow.DefaultRoleAccess.DeleteAsync(access, cancellationToken, true);

            foreach (var accessId in viewModel.Accesses)
                if (!defaultRoleAccesses.Any(a => a.AccessID == accessId))
                    await _uow.DefaultRoleAccess.InsertAsync(new DomainClass.Core.DefaultRoleAccess
                    {
                        AccessID = accessId,
                        RoleID = viewModel.RoleID,
                        IsActive = true,
                        RegDate = DateTime.Now,
                        RegUserID = userManager.GetCurrentUserID(),
                    }, cancellationToken);

            return await _uow.CompleteAsync(cancellationToken);
        }
       
        public async Task<DataSourceResult> GetAccessesForRoleAsync_DataSourceResult(int roleID, DataSourceRequest request)
        {
            var result = await _uow.Access.TableNoTracking.ToDataSourceResultAsync(request, p => new
            {
                ID = (int)p.ID,
                Access = ((eAccessControl)p.AccessControl).ToDisplay(),
                Controller = p.Controller,
                Action = p.Action,
                Route = p.Route,
                HasPermission = _uow.DefaultRoleAccess.TableNoTracking.Any(a => a.RoleID == roleID && a.AccessID == p.ID) ? true : false
            });
            return result;
        }
        
    }
}
