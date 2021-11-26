using Seventy.Data;
using Seventy.Repository.Core;
using Seventy.ViewModel.EDU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.TrainingWeek
{
    public class TrainingWeekService : BaseService.BaseService<DomainClass.EDU.TrainingWeek.TrainingWeek>, ITrainingWeekService
    {
        public TrainingWeekService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.TrainingWeek.TrainingWeek> Table() => _uow.TrainingWeek.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.TrainingWeek.TrainingWeek> TableNoTracking() => _uow.TrainingWeek.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.TrainingWeek.TrainingWeek> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.TrainingWeek.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.TrainingWeek.TrainingWeek entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.TrainingWeek.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.TrainingWeek.TrainingWeek> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.TrainingWeek.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.TrainingWeek.TrainingWeek> InsertAsync(DomainClass.EDU.TrainingWeek.TrainingWeek entity, CancellationToken cancellationToken)
        {
            var result = await _uow.TrainingWeek.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.TrainingWeek.TrainingWeek> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.TrainingWeek.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.TrainingWeek.TrainingWeek> UpdateAsync(DomainClass.EDU.TrainingWeek.TrainingWeek entity, CancellationToken cancellationToken)
        {
            var result = await _uow.TrainingWeek.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.TrainingWeek.TrainingWeek> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.TrainingWeek.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public async Task<PagedList<TrainingWeekListViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters
                , Expression<Func<TrainingWeekListViewModel, bool>> filter = null, Func<IQueryable<TrainingWeekListViewModel>
                    , IOrderedQueryable<TrainingWeekListViewModel>> orderBy = null)
        {
            try
            {
                var lessons = _uow.Lesson.TableNoTracking;
                var terms = _uow.Term.TableNoTracking;
                var weeks = _uow.TrainingWeek.TableNoTracking;

                var query =
                    from w in weeks
                    from l in lessons.Where(x => x.ID == w.LessonID)
                    from t in terms.Where(x => x.ID == w.TermID)
                    select new TrainingWeekListViewModel
                    {
                        ID = w.ID,
                        Title = w.Title,
                        LessonName = l.Title,
                        Description = w.Description,
                        IsActive = w.IsActive,
                        RegDate = w.RegDate,
                        RegUserID = w.RegUserID,
                        TermName = t.Title
                    };

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return await PagedList<TrainingWeekListViewModel>.ToPagedList(orderBy(query),
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }
                else
                {
                    return await PagedList<TrainingWeekListViewModel>.ToPagedList(query,
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }
            }
            catch
            {
                return null;
            }
        }

        public override async Task<PagedList<DomainClass.EDU.TrainingWeek.TrainingWeek>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.TrainingWeek.TrainingWeek, bool>> filter = null, Func<IQueryable<DomainClass.EDU.TrainingWeek.TrainingWeek>, IOrderedQueryable<DomainClass.EDU.TrainingWeek.TrainingWeek>> orderBy = null)
        {
            return await _uow.TrainingWeek.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
