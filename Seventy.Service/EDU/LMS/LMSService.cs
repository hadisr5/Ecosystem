using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.LMS
{
    public class LMSService : BaseService.BaseService<DomainClass.EDU.LMS>, ILMSService
    {
        public LMSService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.LMS> Table() => _uow.LMS.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.LMS> TableNoTracking() => _uow.LMS.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.LMS> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.LMS.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.LMS entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.LMS.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.LMS> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.LMS.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.LMS> InsertAsync(DomainClass.EDU.LMS entity, CancellationToken cancellationToken)
        {
            var result = await _uow.LMS.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.LMS> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.LMS.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.LMS> UpdateAsync(DomainClass.EDU.LMS entity, CancellationToken cancellationToken)
        {
            var result = await _uow.LMS.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.LMS> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.LMS.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.EDU.LMS>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.LMS, bool>> filter = null, Func<IQueryable<DomainClass.EDU.LMS>, IOrderedQueryable<DomainClass.EDU.LMS>> orderBy = null)
        {
            return await _uow.LMS.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
