using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Core.Tags
{
  public class TagsService : BaseService.BaseService<DomainClass.Core.Tags>, ITagsService
  {
    public TagsService(IUnitOfWork uow) : base(uow)
    {
    }

    public override IEnumerable<DomainClass.Core.Tags> Table() => _uow.Tags.Table.AsEnumerable();
    public override IEnumerable<DomainClass.Core.Tags> TableNoTracking() => _uow.Tags.TableNoTracking.AsEnumerable();

    public override async Task<DomainClass.Core.Tags> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
    {
      return await _uow.Tags.GetByIDAsync(cancellationToken, ids);
    }

    public override async Task<bool> DeleteAsync(DomainClass.Core.Tags entity, CancellationToken cancellationToken, bool hardDelete = false)
    {
      var result = await _uow.Tags.DeleteAsync(entity, cancellationToken, hardDelete);
      await _uow.CompleteAsync(cancellationToken);
      return result;
    }

    public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.Core.Tags> entities, CancellationToken cancellationToken, bool hardDelete = false)
    {
      var result = await _uow.Tags.DeleteRangeAsync(entities, cancellationToken, hardDelete);
      await _uow.CompleteAsync(cancellationToken);
      return result;
    }

    public override async Task<DomainClass.Core.Tags> InsertAsync(DomainClass.Core.Tags entity, CancellationToken cancellationToken)
    {
      var result = await _uow.Tags.InsertAsync(entity, cancellationToken);
      await _uow.CompleteAsync(cancellationToken);
      return result;
    }

    public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.Core.Tags> entities, CancellationToken cancellationToken)
    {
      var result = await _uow.Tags.InsertRangeAsync(entities, cancellationToken);
      await _uow.CompleteAsync(cancellationToken);
      return result;

    }

    public override async Task<DomainClass.Core.Tags> UpdateAsync(DomainClass.Core.Tags entity, CancellationToken cancellationToken)
    {
      var result = await _uow.Tags.UpdateAsync(entity, cancellationToken);
      await _uow.CompleteAsync(cancellationToken);
      return result;
    }

    public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.Core.Tags> entities, CancellationToken cancellationToken)
    {
      var result = await _uow.Tags.UpdateRangeAsync(entities, cancellationToken);
      await _uow.CompleteAsync(cancellationToken);
      return result;
    }

    public override async Task<PagedList<DomainClass.Core.Tags>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.Core.Tags, bool>> filter = null, Func<IQueryable<DomainClass.Core.Tags>, IOrderedQueryable<DomainClass.Core.Tags>> orderBy = null)
    {
      return await _uow.Tags.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
    }
  }
}
