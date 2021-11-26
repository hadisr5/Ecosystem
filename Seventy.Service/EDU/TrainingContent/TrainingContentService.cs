using Seventy.Data;
using Seventy.Repository.Core;
using Seventy.Service.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Seventy.Common.Enums;

namespace Seventy.Service.EDU.TrainingContent
{
    public class TrainingContentService : BaseService<DomainClass.EDU.TrainingContent.TrainingContent>, ITrainingContentService
    {
        public TrainingContentService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.TrainingContent.TrainingContent> Table() => _uow.TrainingContent.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.TrainingContent.TrainingContent> TableNoTracking() => _uow.TrainingContent.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.TrainingContent.TrainingContent> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.TrainingContent.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.TrainingContent.TrainingContent entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.TrainingContent.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.TrainingContent.TrainingContent> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.TrainingContent.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.TrainingContent.TrainingContent> InsertAsync(DomainClass.EDU.TrainingContent.TrainingContent entity, CancellationToken cancellationToken)
        {
            var result = await _uow.TrainingContent.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.TrainingContent.TrainingContent> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.TrainingContent.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.TrainingContent.TrainingContent> UpdateAsync(DomainClass.EDU.TrainingContent.TrainingContent entity, CancellationToken cancellationToken)
        {
            var result = await _uow.TrainingContent.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.TrainingContent.TrainingContent> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.TrainingContent.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.EDU.TrainingContent.TrainingContent>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.TrainingContent.TrainingContent, bool>> filter = null, Func<IQueryable<DomainClass.EDU.TrainingContent.TrainingContent>, IOrderedQueryable<DomainClass.EDU.TrainingContent.TrainingContent>> orderBy = null)
        {
            return await _uow.TrainingContent.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }

        public List<DomainClass.EDU.TrainingContent.TrainingContent> GetByType(string type)
        {
                return _uow.TrainingContent.TableNoTracking
                      .Where(a => a.ContentType.Equals(type))
                      .ToList();
        }
    }
}
