using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Seventy.ViewModel.EDU;

namespace Seventy.Service.EDU.Lesson
{
    public class LessonService : BaseService.BaseService<DomainClass.EDU.Lesson.Lesson>, ILessonService
    {
        public LessonService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.Lesson.Lesson> Table() => _uow.Lesson.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.Lesson.Lesson> TableNoTracking() => _uow.Lesson.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.Lesson.Lesson> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.Lesson.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Lesson.Lesson entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Lesson.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Lesson.Lesson> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Lesson.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Lesson.Lesson> InsertAsync(DomainClass.EDU.Lesson.Lesson entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Lesson.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Lesson.Lesson> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Lesson.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Lesson.Lesson> UpdateAsync(DomainClass.EDU.Lesson.Lesson entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Lesson.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Lesson.Lesson> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Lesson.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.EDU.Lesson.Lesson>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.Lesson.Lesson, bool>> filter = null, Func<IQueryable<DomainClass.EDU.Lesson.Lesson>, IOrderedQueryable<DomainClass.EDU.Lesson.Lesson>> orderBy = null)
        {
            return await _uow.Lesson.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }

        public async Task<PagedList<LessonViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters
           , Expression<Func<LessonViewModel, bool>> filter = null,
           Func<IQueryable<LessonViewModel>
               , IOrderedQueryable<LessonViewModel>> orderBy = null)
        {
            try
            {
                var lessons = _uow.Lesson.TableNoTracking;

                var query = from lesson in lessons
                    select new LessonViewModel
                            {
                                ID = lesson.ID,
                                Description = lesson.Description,
                                IsActive = lesson.IsActive,
                                RegDate = lesson.RegDate,
                                RegUserID = lesson.RegUserID,
                                Title = lesson.Title,
                                PicFileID = lesson.PicFileID
                            };

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return await PagedList<LessonViewModel>.ToPagedList(orderBy(query),
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }
                else
                {
                    return await PagedList<LessonViewModel>.ToPagedList(query,
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }
            }
            catch
            {
                return null;
            }
        }

    }
}
