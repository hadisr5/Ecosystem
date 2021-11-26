using Seventy.Data;
using Seventy.Repository.Core;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Seventy.DomainClass.Core;
using System.Linq.Expressions;
using System;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Seventy.Service.Users;
using Seventy.ViewModel.Core;
using Microsoft.EntityFrameworkCore;

namespace Seventy.Service.Core.UserGroup
{
    public class UserGroupService : BaseService.BaseService<DomainClass.Core.UserGroups>, IUserGroupService
    {
        private readonly IUserManager userManager;

        public UserGroupService(IUnitOfWork uow,IUserManager userManager) : base(uow)
        {
            this.userManager = userManager;
        }

        public override IEnumerable<DomainClass.Core.UserGroups> Table() => _uow.UserGroups.Table.AsEnumerable();
        public override IEnumerable<DomainClass.Core.UserGroups> TableNoTracking() => _uow.UserGroups.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.Core.UserGroups> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.UserGroups.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.Core.UserGroups entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.UserGroups.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.Core.UserGroups> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.UserGroups.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.UserGroups> InsertAsync(DomainClass.Core.UserGroups entity, CancellationToken cancellationToken)
        {
            var result = await _uow.UserGroups.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.Core.UserGroups> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.UserGroups.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.UserGroups> UpdateAsync(DomainClass.Core.UserGroups entity, CancellationToken cancellationToken)
        {
            var result = await _uow.UserGroups.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.Core.UserGroups> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.UserGroups.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<UserGroups>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<UserGroups, bool>> filter = null, Func<IQueryable<UserGroups>, IOrderedQueryable<UserGroups>> orderBy = null)
        {
            return await _uow.UserGroups.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
        public async Task<int> ChangeUserGroupMemberAsync(SaveUserGroupViewModel viewModel, CancellationToken cancellationToken)
        {
            var userGroupMember = await _uow.UserGroupMembers.Table.Where(w => w.UserID == viewModel.UserID).ToListAsync();

            foreach (var groupMember in userGroupMember)
                if (!viewModel.UserGroup.Any(a => a.Equals(groupMember.UserGroupID)))
                    await _uow.UserGroupMembers.DeleteAsync(groupMember, cancellationToken, true);

            foreach (var userGroup in viewModel.UserGroup)
                if (!userGroupMember.Any(a => a.UserGroupID == userGroup))
                    await _uow.UserGroupMembers.InsertAsync(new DomainClass.Core.UserGroupMembers
                    {
                        UserGroupID = userGroup,
                        UserID = viewModel.UserID,
                        IsActive = true,
                        RegDate = DateTime.Now,
                        RegUserID = userManager.GetCurrentUserID(),
                    }, cancellationToken);
            return await _uow.CompleteAsync(cancellationToken);
        }
        public async Task<DataSourceResult> GetUserGroupMembersForUserAsync_DataSourceResult(int userID, DataSourceRequest request)
        {
            var result = await _uow.UserGroups.TableNoTracking.ToDataSourceResultAsync(request, p => new
            {
                ID = (int)p.ID,
                UserGroup = p.Title,
                IsAvtive = _uow.UserGroupMembers.TableNoTracking.Any(a => a.UserID == userID && a.UserGroupID == p.ID) ? true : false
            });
            return result;
        }
    }
}
