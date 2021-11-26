using Seventy.Data;
using Seventy.DomainClass.Core;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Core.KmExperience
{
    public class KmExperienceService : BaseService.BaseService<DomainClass.Core.KMExperience>, IKmExperienceService
    {
        public KmExperienceService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.Core.KMExperience> Table() => _uow.KMExperience.Table.AsEnumerable();
        public override IEnumerable<DomainClass.Core.KMExperience> TableNoTracking() => _uow.KMExperience.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.Core.KMExperience> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.KMExperience.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.Core.KMExperience entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.KMExperience.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.Core.KMExperience> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.KMExperience.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.KMExperience> InsertAsync(DomainClass.Core.KMExperience entity, CancellationToken cancellationToken)
        {
            var result = await _uow.KMExperience.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.Core.KMExperience> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.KMExperience.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.KMExperience> UpdateAsync(DomainClass.Core.KMExperience entity, CancellationToken cancellationToken)
        {
            var result = await _uow.KMExperience.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.Core.KMExperience> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.KMExperience.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<KMExperience>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<KMExperience, bool>> filter = null, Func<IQueryable<KMExperience>, IOrderedQueryable<KMExperience>> orderBy = null)
        {
            return await _uow.KMExperience.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
