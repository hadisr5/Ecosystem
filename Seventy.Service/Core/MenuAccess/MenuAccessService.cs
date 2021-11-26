using Extensions;
using Kendo.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Seventy.Common.Enums;
using Seventy.Common.Utilities;
using Seventy.Data;
using Seventy.DomainClass.Core;
using Seventy.Repository.Core;
using Seventy.Repository.Core.Repositories;
using Seventy.Service.Core.UserRole;
using Seventy.Service.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Core.MenuAccess
{
    public class MenuAccessService : BaseService.BaseService<DomainClass.Core.MenuAccess>, IMenuAccessService
    {
        private readonly IMenuAccessRepository menuAccessRepository;
        private readonly IUserManager userManager;
        private readonly IUserRoleService userRoleService;

        public MenuAccessService(IUnitOfWork uow, IMenuAccessRepository menuAccessRepository, IUserManager userManager, IUserRoleService userRoleService) : base(uow)
        {
            this.menuAccessRepository = menuAccessRepository;
            this.userManager = userManager;
            this.userRoleService = userRoleService;
        }

        public override IEnumerable<DomainClass.Core.MenuAccess> Table() => _uow.MenuAccess.Table.AsEnumerable();
        public override IEnumerable<DomainClass.Core.MenuAccess> TableNoTracking() => _uow.MenuAccess.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.Core.MenuAccess> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.MenuAccess.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.Core.MenuAccess entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.MenuAccess.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.Core.MenuAccess> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.MenuAccess.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.MenuAccess> InsertAsync(DomainClass.Core.MenuAccess entity, CancellationToken cancellationToken)
        {
            var result = await _uow.MenuAccess.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.Core.MenuAccess> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.MenuAccess.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.MenuAccess> UpdateAsync(DomainClass.Core.MenuAccess entity, CancellationToken cancellationToken)
        {
            var result = await _uow.MenuAccess.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.Core.MenuAccess> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.MenuAccess.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.Core.MenuAccess>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.Core.MenuAccess, bool>> filter = null, Func<IQueryable<DomainClass.Core.MenuAccess>, IOrderedQueryable<DomainClass.Core.MenuAccess>> orderBy = null)
        {
            return await _uow.MenuAccess.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }

        public async Task UpdateMenuAccessTable(List<DomainClass.Core.MenuAccess> menues)
        {
            var createMenu = new List<DomainClass.Core.MenuAccess>();
            var updateMenu = new List<DomainClass.Core.MenuAccess>();
            var updateMenuHelper = new List<DomainClass.Core.MenuAccess>();
            var deleteMenu = new List<DomainClass.Core.MenuAccess>();

            var menuesInDb = await _uow.MenuAccess.Table.ToListAsync();
            menues.ForEach(f =>
            {
                if (f.Route != null && !f.Route.StartsWith("/"))
                    f.Route = $"/{f.Route}";
                if (menuesInDb.Any(a => a.MenuCode == f.MenuCode))
                {
                    var model = menuesInDb.FirstOrDefault(s => s.MenuCode == f.MenuCode);
                    if ((model.AccessCode != f.AccessCode) || (model.eModule != f.eModule) || (model.Order != f.Order) || (model.Route != f.Route))
                    {
                        model.AccessCode = f.AccessCode;
                        model.eModule = f.eModule;
                        model.Order = f.Order;
                        model.Route = f.Route;
                        updateMenuHelper.Add(model);
                    }
                    menuesInDb.Remove(model);
                }
                else
                {
                    if (!menuesInDb.Any(a => a.MenuCode == f.MenuCode))
                        createMenu.Add(f);
                }
            });

            if (createMenu.Count > 0)
                menuAccessRepository.InsertRange(createMenu);
            if (updateMenuHelper.Count > 0)
                menuAccessRepository.UpdateRange(updateMenu);
        }

        public async Task<List<IGrouping<string, MenuGroupHelper>>> GetMenues()
        {
            var currentUser = userManager.GetCurrentUserID();
            var roles = await _uow.UserRole.TableNoTracking.Include(i => i.Role).Where(w => w.UserID == currentUser).Select(s => s.Role.Description).ToListAsync();
            var menuGroup = new List<string>();
            var menuGroupLink = new List<MenuGroupHelper>();

            foreach (string role in roles)
                role.Split('،').ToList().ForEach(f => menuGroup.Add(f));
            menuGroup = menuGroup.Distinct().ToList();

            var userAccesses = await _uow.UserAccess.TableNoTracking.Include(i => i.Access).Where(w => w.UserID == currentUser)
                .Select(s => s.Access.AccessControl).ToListAsync();
            var userMenues = await _uow.MenuAccess.TableNoTracking.Where(w => userAccesses.Contains(w.AccessCode)).ToListAsync();


            var data = await _uow.DefaultRoleAccess.TableNoTracking.Include(i => i.Role).Include(i => i.Access).Where(w => userAccesses.Contains(w.Access.AccessControl)).ToListAsync();
            foreach (var f in data)
            {
                if (userMenues.Any(a => a.AccessCode == f.Access.AccessControl))
                {
                    var descriptions = f.Role.Description.Split('،').ToList();
                    var descriptionsTemp = data.Where(w => w.Access.AccessControl == f.Access.AccessControl && w.RoleID != f.Role.ID).Select(s => s.Role.Description).ToList();

                    if (descriptionsTemp.Count > 0)
                    {
                        var tempMenuGroup = new List<string>();
                        foreach (string tempRole in descriptionsTemp)
                            tempRole.Split('،').ToList().ForEach(f => tempMenuGroup.Add(f));
                        tempMenuGroup = tempMenuGroup.Distinct().ToList();

                        descriptions = descriptions.Intersect(tempMenuGroup).ToList();
                    }
                    else
                    {
                        var defaultMenu = descriptions[0];
                        descriptions.Clear();
                        descriptions.Add(defaultMenu);
                    }


                    foreach (string item in descriptions)
                    {
                        if (menuGroup.Contains(item))
                        {
                            var tempMenu = userMenues.FirstOrDefault(p => p.AccessCode == f.Access.AccessControl);
                            if (tempMenu != null && (!menuGroupLink.Any(a => a.Name == ((eMenu)tempMenu.MenuCode).ToDisplay() && a.Parent == item)))
                            {
                                menuGroupLink.Add(new MenuGroupHelper
                                {
                                    Parent = item,
                                    Name = ((eMenu)(tempMenu.MenuCode)).ToDisplay(),
                                    Route = tempMenu.Route
                                });
                            }
                        }
                    }
                }

            }
            return menuGroupLink.GroupBy(g => g.Parent).ToList();
        }

    }
    public class MenuGroupHelper
    {
        public string Name { get; set; }
        public string Route { get; set; }
        public string Parent { get; set; }
    }
}
