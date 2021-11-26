using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.Certificate
{
    public class CertificateService : BaseService.BaseService<DomainClass.EDU.Certificate>, ICertificateService
    {
        public CertificateService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.Certificate> Table() => _uow.Certificate.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.Certificate> TableNoTracking() => _uow.Certificate.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.Certificate> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.Certificate.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Certificate entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Certificate.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Certificate> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Certificate.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Certificate> InsertAsync(DomainClass.EDU.Certificate entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Certificate.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Certificate> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Certificate.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Certificate> UpdateAsync(DomainClass.EDU.Certificate entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Certificate.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Certificate> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Certificate.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.EDU.Certificate>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.Certificate, bool>> filter = null, Func<IQueryable<DomainClass.EDU.Certificate>, IOrderedQueryable<DomainClass.EDU.Certificate>> orderBy = null)
        {
            return await _uow.Certificate.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
