using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.EntityFrameworkCore;
using Seventy.Common.Enums;
using Seventy.Common.Utilities;
using Seventy.Data;
using Seventy.DomainClass.Core;
using Seventy.Repository.Core;
using Seventy.Repository.Core.Repositories;
using Seventy.Service.Users;
using Seventy.ViewModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Core.AccessPermissionGroup
{
    public class AccessPermissionGroupService : BaseService.BaseService<DomainClass.Core.AccessPermissionGroup>, IAccessPermissionGroupService
    {
        private readonly IAccessRepository accessRepository;
        private readonly IUserManager userManager;

        public AccessPermissionGroupService(IUnitOfWork uow, IAccessRepository accessRepository,IUserManager userManager) : base(uow)
        {
            this.accessRepository = accessRepository;
            this.userManager = userManager;
        }
        public async Task<List<Access>> GetAll()
        {
            return await _uow.Access.TableNoTracking.ToListAsync();
        }
        public override IEnumerable<DomainClass.Core.AccessPermissionGroup> Table() => _uow.AccessPermissionGroup.Table.AsEnumerable();
        public override IEnumerable<DomainClass.Core.AccessPermissionGroup> TableNoTracking() => _uow.AccessPermissionGroup.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.Core.AccessPermissionGroup> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.AccessPermissionGroup.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.Core.AccessPermissionGroup entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.AccessPermissionGroup.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.Core.AccessPermissionGroup> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.AccessPermissionGroup.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.AccessPermissionGroup> InsertAsync(DomainClass.Core.AccessPermissionGroup entity, CancellationToken cancellationToken)
        {
            var result = await _uow.AccessPermissionGroup.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.Core.AccessPermissionGroup> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.AccessPermissionGroup.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.AccessPermissionGroup> UpdateAsync(DomainClass.Core.AccessPermissionGroup entity, CancellationToken cancellationToken)
        {
            var result = await _uow.AccessPermissionGroup.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.Core.AccessPermissionGroup> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.AccessPermissionGroup.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.Core.AccessPermissionGroup>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.Core.AccessPermissionGroup, bool>> filter = null, Func<IQueryable<DomainClass.Core.AccessPermissionGroup>, IOrderedQueryable<DomainClass.Core.AccessPermissionGroup>> orderBy = null)
        {
            return await _uow.AccessPermissionGroup.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
        public async Task<DataSourceResult> GetAccessesForPermissionGroupAsync_DataSourceResult(int permissionGroupID, DataSourceRequest request)
        {
            var result = await _uow.Access.TableNoTracking.ToDataSourceResultAsync(request, p => new
            {
                ID = (int)p.ID,
                Access = ((eAccessControl)p.AccessControl).ToDisplay(),
                Controller = p.Controller,
                Action = p.Action,
                Route = p.Route,
                HasPermission = _uow.AccessPermissionGroup.TableNoTracking.Any(a => a.PermissionGroupID == permissionGroupID && a.AccessID == p.ID) ? true : false
            });
            return result;
        }
        public async Task<int> ChangeAccessPermissionGroupAsync(SaveAccessPermissionGroupViewModel viewModel, CancellationToken cancellationToken)
        {
            var accessPermissionGroup = await _uow.AccessPermissionGroup.Table.Where(w => w.PermissionGroupID == viewModel.PermissionGroupID).ToListAsync();

            foreach (var access in accessPermissionGroup)
                if (!viewModel.Accesses.Any(a => a.Equals(access.AccessID)))
                    await _uow.AccessPermissionGroup.DeleteAsync(access, cancellationToken, true);

            foreach (var accessId in viewModel.Accesses)
                if (!accessPermissionGroup.Any(a => a.AccessID == accessId))
                    await _uow.AccessPermissionGroup.InsertAsync(new DomainClass.Core.AccessPermissionGroup
                    {
                        AccessID = accessId,
                        PermissionGroupID = viewModel.PermissionGroupID,
                        IsActive = true,
                        RegDate = DateTime.Now,
                        RegUserID = userManager.GetCurrentUserID(),
                    }, cancellationToken);

            return await _uow.CompleteAsync(cancellationToken);
        }
    }
}
