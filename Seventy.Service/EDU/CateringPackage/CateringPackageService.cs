using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.CateringPackage
{
    public class CateringPackageService : BaseService.BaseService<DomainClass.EDU.CateringPackage>, ICateringPackageService
    {
        public CateringPackageService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.CateringPackage> Table() => _uow.CateringPackage.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.CateringPackage> TableNoTracking() => _uow.CateringPackage.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.CateringPackage> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.CateringPackage.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.CateringPackage entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.CateringPackage.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.CateringPackage> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.CateringPackage.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.CateringPackage> InsertAsync(DomainClass.EDU.CateringPackage entity, CancellationToken cancellationToken)
        {
            var result = await _uow.CateringPackage.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.CateringPackage> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.CateringPackage.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.CateringPackage> UpdateAsync(DomainClass.EDU.CateringPackage entity, CancellationToken cancellationToken)
        {
            var result = await _uow.CateringPackage.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.CateringPackage> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.CateringPackage.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.EDU.CateringPackage>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.CateringPackage, bool>> filter = null, Func<IQueryable<DomainClass.EDU.CateringPackage>, IOrderedQueryable<DomainClass.EDU.CateringPackage>> orderBy = null)
        {
            return await _uow.CateringPackage.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
