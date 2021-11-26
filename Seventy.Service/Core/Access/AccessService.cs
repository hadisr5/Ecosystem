using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Seventy.Data;
using Seventy.DomainClass.Core;
using Seventy.Repository.Core;
using Seventy.Repository.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Core.UserAccess
{
    public class AccessService : BaseService.BaseService<DomainClass.Core.Access>, IAccessService
    {
        private readonly IAccessRepository accessRepository;

        public AccessService(IUnitOfWork uow, IAccessRepository accessRepository) : base(uow)
        {
            this.accessRepository = accessRepository;
        }
        public async Task<List<Access>> GetAll()
        {
            return await _uow.Access.TableNoTracking.ToListAsync();
        }
        public override IEnumerable<DomainClass.Core.Access> Table() => _uow.Access.Table.AsEnumerable();
        public override IEnumerable<DomainClass.Core.Access> TableNoTracking() => _uow.Access.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.Core.Access> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.Access.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.Core.Access entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Access.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.Core.Access> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Access.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.Access> InsertAsync(DomainClass.Core.Access entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Access.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.Core.Access> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Access.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.Access> UpdateAsync(DomainClass.Core.Access entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Access.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.Core.Access> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Access.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.Core.Access>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.Core.Access, bool>> filter = null, Func<IQueryable<DomainClass.Core.Access>, IOrderedQueryable<DomainClass.Core.Access>> orderBy = null)
        {
            return await _uow.Access.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }

        public async Task UpdateAccessTable(List<Access> accesses)
        {
            var createAccess = new List<Access>();
            var updateAccess = new List<Access>();
            var updateAccessHelper = new List<Access>();
            var deleteAccess = new List<Access>();

            var accessInDb = await _uow.Access.Table.ToListAsync();
            accesses.ForEach(f =>
            {
                if (f.Route != null && !f.Route.StartsWith("/"))
                    f.Route = $"/{f.Route}";
                if (accessInDb.Any(a => a.AccessControl == f.AccessControl))
                {
                    var model = accessInDb.FirstOrDefault(s => s.AccessControl == f.AccessControl);
                    if ((model.Index != f.Index) || (model.Controller != f.Controller) || (model.Action != f.Action) || (model.Route != f.Route) || (model.AccessType != f.AccessType) || (model.AllowAnonymous != f.AllowAnonymous))
                    {
                        model.Index = f.Index;
                        model.Controller = f.Controller;
                        model.Action = f.Action;
                        model.Route = f.Route;
                        model.AccessType = f.AccessType;
                        model.AllowAnonymous = f.AllowAnonymous;
                        updateAccessHelper.Add(model);
                    }
                    accessInDb.Remove(model);
                }
                else
                {
                    if (!accessInDb.Any(a => a.AccessControl == f.AccessControl))
                        createAccess.Add(f);
                }
            });

            if (createAccess.Count > 0)
                accessRepository.InsertRange(createAccess);
            if (updateAccessHelper.Count > 0)
                accessRepository.UpdateRange(updateAccess);
        }
    }
}
