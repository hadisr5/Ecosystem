using System;
using System.Linq;
using Seventy.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Seventy.Repository.Core;
using Seventy.DomainClass.Core;
using System.Collections.Generic;
using Seventy.ViewModel.Core.Users;

namespace Seventy.Service.Core.UserGroupMember
{
    public class UserGroupMemberService : BaseService.BaseService<UserGroupMembers>, IUserGroupMemberService
    {
        public UserGroupMemberService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<UserGroupMembers> Table() => _uow.UserGroupMembers.Table.AsEnumerable();
        public override IEnumerable<UserGroupMembers> TableNoTracking() => _uow.UserGroupMembers.TableNoTracking.AsEnumerable();

        public override async Task<UserGroupMembers> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.UserGroupMembers.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(UserGroupMembers entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.UserGroupMembers.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<UserGroupMembers> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.UserGroupMembers.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<UserGroupMembers> InsertAsync(UserGroupMembers entity, CancellationToken cancellationToken)
        {
            var result = await _uow.UserGroupMembers.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<UserGroupMembers> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.UserGroupMembers.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<UserGroupMembers> UpdateAsync(UserGroupMembers entity, CancellationToken cancellationToken)
        {
            var result = await _uow.UserGroupMembers.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<UserGroupMembers> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.UserGroupMembers.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public async Task<PagedList<UserGroupMemberViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters
                , Expression<Func<UserGroupMemberViewModel, bool>> filter = null,
                Func<IQueryable<UserGroupMemberViewModel>
                , IOrderedQueryable<UserGroupMemberViewModel>> orderBy = null)
        {
            try
            {
                var items = _uow.UserGroupMembers.TableNoTracking;
                var users = _uow.UserProfiles.TableNoTracking;
                var groups = _uow.UserGroups.TableNoTracking;

                var query =
                    from item in items
                    from user in users.Where(x => x.UserID.Equals(item.UserID))
                    from groupItem in groups.Where(x => x.ID.Equals(item.UserGroupID))
                    select new UserGroupMemberViewModel
                    {
                        ID = item.ID,
                        Description = item.Description,
                        IsActive = item.IsActive,
                        RegDate = item.RegDate,
                        RegUserID = item.RegUserID,
                        FullName = user.FirstName + " " + user.LastName,
                        UserGroupID = item.UserGroupID,
                        UserID = item.UserID,
                        ImageId = user.PhotoFileId,
                        UserGroupName = groupItem.Title
                    };

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return await PagedList<UserGroupMemberViewModel>.ToPagedList(orderBy(query),
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }
                else
                {
                    return await PagedList<UserGroupMemberViewModel>.ToPagedList(query,
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }
            }
            catch
            {
                return null;
            }
        }

        public override async Task<PagedList<UserGroupMembers>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<UserGroupMembers, bool>> filter = null, Func<IQueryable<UserGroupMembers>, IOrderedQueryable<UserGroupMembers>> orderBy = null)
        {
            return await _uow.UserGroupMembers.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
