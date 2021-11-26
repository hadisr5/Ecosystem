using Seventy.Data;
using Seventy.Repository.Core;
using Seventy.ViewModel.EDU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.RelatedCourses
{
    public class RelatedCoursesService : BaseService.BaseService<DomainClass.EDU.Course.RelatedCourses>, IRelatedCoursesService
    {
        public RelatedCoursesService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.Course.RelatedCourses> Table() => _uow.RelatedCourses.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.Course.RelatedCourses> TableNoTracking() => _uow.RelatedCourses.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.Course.RelatedCourses> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.RelatedCourses.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Course.RelatedCourses entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.RelatedCourses.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Course.RelatedCourses> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.RelatedCourses.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Course.RelatedCourses> InsertAsync(DomainClass.EDU.Course.RelatedCourses entity, CancellationToken cancellationToken)
        {
            var result = await _uow.RelatedCourses.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Course.RelatedCourses> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.RelatedCourses.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Course.RelatedCourses> UpdateAsync(DomainClass.EDU.Course.RelatedCourses entity, CancellationToken cancellationToken)
        {
            var result = await _uow.RelatedCourses.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Course.RelatedCourses> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.RelatedCourses.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public async Task<PagedList<RelatedCoursesViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters
            , Expression<Func<RelatedCoursesViewModel, bool>> filter = null, Func<IQueryable<RelatedCoursesViewModel>
                , IOrderedQueryable<RelatedCoursesViewModel>> orderBy = null)
        {
            try
            {
                var relatedCourses = _uow.RelatedCourses.TableNoTracking;
                var firstCourseList = _uow.Course.TableNoTracking;
                var secondCourseList = _uow.Course.TableNoTracking;


                var query = from rc in relatedCourses
                            from fc in firstCourseList.Where(x => x.ID == rc.FirstCourseID)
                            from sc in secondCourseList.Where(x => x.ID == rc.SecondCourseID)
                            select new RelatedCoursesViewModel
                            {
                                ID = rc.ID,
                                FirstCourseTitle = fc.Title,
                                SecondCourseTitle = sc.Title,
                                Description = rc.Description,
                                IsActive = rc.IsActive,
                                RegDate = rc.RegDate,
                                RegUserID = rc.RegUserID
                            };

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return await PagedList<RelatedCoursesViewModel>.ToPagedList(orderBy(query),
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }
                else
                {
                    return await PagedList<RelatedCoursesViewModel>.ToPagedList(query,
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }
            }
            catch
            {
                return null;
            }
        }

        public override async Task<PagedList<DomainClass.EDU.Course.RelatedCourses>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.Course.RelatedCourses, bool>> filter = null, Func<IQueryable<DomainClass.EDU.Course.RelatedCourses>, IOrderedQueryable<DomainClass.EDU.Course.RelatedCourses>> orderBy = null)
        {
            return await _uow.RelatedCourses.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
