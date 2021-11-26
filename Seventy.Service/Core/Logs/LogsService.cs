using Seventy.Data;
using Seventy.DomainClass.Core;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.Core.Logs
{
    public class LogsService : BaseService.BaseService<DomainClass.Core.Logs>, ILogsService
    {
        public LogsService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.Core.Logs> Table() => _uow.Logs.Table.AsEnumerable();
        public override IEnumerable<DomainClass.Core.Logs> TableNoTracking() => _uow.Logs.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.Core.Logs> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.Logs.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.Core.Logs entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Logs.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.Core.Logs> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Logs.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.Logs> InsertAsync(DomainClass.Core.Logs entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Logs.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.Core.Logs> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Logs.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.Core.Logs> UpdateAsync(DomainClass.Core.Logs entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Logs.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.Core.Logs> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Logs.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public async Task<PagedList<LogsViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<LogsViewModel, bool>> filter = null, Func<IQueryable<LogsViewModel>, IOrderedQueryable<LogsViewModel>> orderBy = null)
        {
            try
            {
                var logs = _uow.Logs.TableNoTracking;
                var UserManagerService = _uow.Users.TableNoTracking;
                var query =
                    from p in logs
                    join a in UserManagerService on p.UserID equals a.ID
                    select new LogsViewModel
                    {
                        ID = p.ID,
                        Description = p.Description,
                        UserName = a.Mobile,
                        MAC = p.MAC,
                        IP = p.IP,
                        LogType = p.LogType,
                        RegDate = p.RegDate,
                        RegUserID = p.RegUserID,
                        Section = p.Section,
                        IsActive = p.IsActive
                    };

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return await PagedList<LogsViewModel>.ToPagedList(orderBy(query),
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }
                else
                {
                    return await PagedList<LogsViewModel>.ToPagedList(query,
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }
            }
            catch { return null; }
        }

        public override async Task<PagedList<DomainClass.Core.Logs>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.Core.Logs, bool>> filter = null, Func<IQueryable<DomainClass.Core.Logs>, IOrderedQueryable<DomainClass.Core.Logs>> orderBy = null)
        {
            return await _uow.Logs.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
