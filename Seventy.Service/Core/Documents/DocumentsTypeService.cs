using Seventy.Data;
using Seventy.DomainClass.Core;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Core.Documents
{
    public class DocumentsTypeService : BaseService.BaseService<DocumentType>, IDocumentsTypeService
    {
        public DocumentsTypeService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DocumentType> Table() => _uow.DocumentType.Table.AsEnumerable();
        public override IEnumerable<DocumentType> TableNoTracking() => _uow.DocumentType.TableNoTracking.AsEnumerable();

        public override async Task<DocumentType> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.DocumentType.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DocumentType entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.DocumentType.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DocumentType> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.DocumentType.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DocumentType> InsertAsync(DocumentType entity, CancellationToken cancellationToken)
        {
            var result = await _uow.DocumentType.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DocumentType> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.DocumentType.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DocumentType> UpdateAsync(DocumentType entity, CancellationToken cancellationToken)
        {
            var result = await _uow.DocumentType.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DocumentType> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.DocumentType.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DocumentType>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DocumentType, bool>> filter = null, Func<IQueryable<DocumentType>, IOrderedQueryable<DocumentType>> orderBy = null)
        {
            return await _uow.DocumentType.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
