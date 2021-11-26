using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.QuestionOptions
{
    public class QuestionOptionsService : BaseService.BaseService<DomainClass.EDU.Exam.QuestionOptions>, IQuestionOptionsService
    {
        public QuestionOptionsService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.Exam.QuestionOptions> Table() => _uow.QuestionOptions.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.Exam.QuestionOptions> TableNoTracking() => _uow.QuestionOptions.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.Exam.QuestionOptions> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.QuestionOptions.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Exam.QuestionOptions entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.QuestionOptions.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Exam.QuestionOptions> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.QuestionOptions.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Exam.QuestionOptions> InsertAsync(DomainClass.EDU.Exam.QuestionOptions entity, CancellationToken cancellationToken)
        {
            var result = await _uow.QuestionOptions.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Exam.QuestionOptions> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.QuestionOptions.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Exam.QuestionOptions> UpdateAsync(DomainClass.EDU.Exam.QuestionOptions entity, CancellationToken cancellationToken)
        {
            var result = await _uow.QuestionOptions.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Exam.QuestionOptions> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.QuestionOptions.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.EDU.Exam.QuestionOptions>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.Exam.QuestionOptions, bool>> filter = null, Func<IQueryable<DomainClass.EDU.Exam.QuestionOptions>, IOrderedQueryable<DomainClass.EDU.Exam.QuestionOptions>> orderBy = null)
        {
            return await _uow.QuestionOptions.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
