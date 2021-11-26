using Seventy.Data;
using Seventy.DomainClass.EDU;
using Seventy.DomainClass.EDU.Course;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.CourseGroup
{
    public class CourseGroupsService : BaseService.BaseService<DomainClass.EDU.Course.CourseGroups>, ICourseGroupsService
    {
        public CourseGroupsService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.Course.CourseGroups> Table() => _uow.CourseGroups.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.Course.CourseGroups> TableNoTracking() => _uow.CourseGroups.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.Course.CourseGroups> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.CourseGroups.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Course.CourseGroups entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.CourseGroups.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Course.CourseGroups> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.CourseGroups.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Course.CourseGroups> InsertAsync(DomainClass.EDU.Course.CourseGroups entity, CancellationToken cancellationToken)
        {
            var result = await _uow.CourseGroups.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Course.CourseGroups> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.CourseGroups.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Course.CourseGroups> UpdateAsync(DomainClass.EDU.Course.CourseGroups entity, CancellationToken cancellationToken)
        {
            var result = await _uow.CourseGroups.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Course.CourseGroups> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.CourseGroups.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public async Task<PagedList<CourseGroupsViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters
                , Expression<Func<CourseGroupsViewModel, bool>> filter = null, Func<IQueryable<CourseGroupsViewModel>
                    , IOrderedQueryable<CourseGroupsViewModel>> orderBy = null)
        {
            try
            {
                var courseList = _uow.Course.TableNoTracking;
                var courseGroups = _uow.CourseGroups.TableNoTracking;

                var query = from c in courseList
                            from cg in courseGroups.Where(x => x.CourseID == c.ID)
                            select new CourseGroupsViewModel
                            {
                                ID = cg.ID,
                                CourseID = cg.CourseID,
                                CourseName = c.Title,
                                Title = cg.Title,
                                StartDate = cg.StartDate,
                                EndDate = cg.EndDate,
                                Capacity = cg.Capacity,
                                Description = cg.Description,
                                IsActive = cg.IsActive,
                                RegDate = cg.RegDate,
                                RegUserID = cg.RegUserID
                            };

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return await PagedList<CourseGroupsViewModel>.ToPagedList(orderBy(query),
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }
                else
                {
                    return await PagedList<CourseGroupsViewModel>.ToPagedList(query,
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }
            }
            catch
            {
                return null;
            }

        }

        public override async Task<PagedList<CourseGroups>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<CourseGroups, bool>> filter = null, Func<IQueryable<CourseGroups>, IOrderedQueryable<CourseGroups>> orderBy = null)
        {
            return await _uow.CourseGroups.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
