using Seventy.Data;
using Seventy.Repository.Core;
using Seventy.ViewModel.EDU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.Term
{
    public class TermService : BaseService.BaseService<DomainClass.EDU.Term.Term>, ITermService
    {
        public TermService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.Term.Term> Table() => _uow.Term.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.Term.Term> TableNoTracking() => _uow.Term.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.Term.Term> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.Term.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Term.Term entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Term.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Term.Term> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Term.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Term.Term> InsertAsync(DomainClass.EDU.Term.Term entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Term.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Term.Term> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Term.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Term.Term> UpdateAsync(DomainClass.EDU.Term.Term entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Term.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Term.Term> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Term.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }
        public async Task<PagedList<TermViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters
                , Expression<Func<TermViewModel, bool>> filter = null,
                Func<IQueryable<TermViewModel>, IOrderedQueryable<TermViewModel>> orderBy = null)
        {
            try
            {
                var courseList = _uow.Course.TableNoTracking;
                var courseGroups = _uow.CourseGroups.TableNoTracking;
                var terms = _uow.Term.TableNoTracking;

                var query =
                    from c in courseList
                    from cg in courseGroups.Where(x => x.CourseID == c.ID)
                    from t in terms.Where(x => x.CourseGroupID == cg.ID)
                    select new TermViewModel
                    {
                        ID = t.ID,
                        CourseTitle = c.Title,
                        GroupName = cg.Title,
                        Title = t.Title,
                        StartDate = t.StartDate,
                        EndDate = t.EndDate,
                        Duration = t.Duration,
                        Description = t.Description,
                        IsActive = t.IsActive,
                        RegDate = t.RegDate,
                        RegUserID = t.RegUserID
                    };

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return await PagedList<TermViewModel>.ToPagedList(orderBy(query),
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }
                else
                {
                    return await PagedList<TermViewModel>.ToPagedList(query,
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }
            }
            catch { return null; }
        }

        public IQueryable<DomainClass.EDU.Term.Term> GetUserTerms(DomainClass.Core.Users user)
        {
            if (user == null)
                return null;

            var userCourseGroups = _uow.CourseRegistration.TableNoTracking.Where(x => x.UserID == user.ID);
            var termsList = _uow.Term.TableNoTracking;

            var userTerms = from c in userCourseGroups
                            from t in termsList.Where(x => x.CourseGroupID == c.CourseGroupID)
                            select t;

            return userTerms;
        }

        public IQueryable<LessonViewModel> GetUserLessonsByTerm(DomainClass.Core.Users user, DomainClass.EDU.Term.Term term)
        {
            if (user == null || term == null)
                return null;

            var userCourseGroups = _uow.CourseRegistration.TableNoTracking.Where(x => x.UserID == user.ID);
            var lessonList = _uow.Lesson.TableNoTracking;
            var termLessons = _uow.TermLesson.TableNoTracking.Where(x => x.TermID == term.ID);
            var files = _uow.File.TableNoTracking;

            var userLessons = from l in lessonList
                              from tl in termLessons.Where(x => x.LessonID == l.ID)
                              from ucg in userCourseGroups.Where(x => x.CourseGroupID == tl.CourseGroupID)
                              from f in files.Where(x => x.ID == l.PicFileID).DefaultIfEmpty()
                              select new LessonViewModel
                              {
                                  ID = l.ID,
                                  CourseID = tl.CourseID,
                                  Description = l.Description,
                                  IsActive = l.IsActive,
                                  RegDate = l.RegDate,
                                  RegUserID = l.RegUserID,
                                  TermID = tl.TermID,
                                  Title = l.Title,
                                  PhotoPath = ""
                              };


            return userLessons;
        }

        public override async Task<PagedList<DomainClass.EDU.Term.Term>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.Term.Term, bool>> filter = null, Func<IQueryable<DomainClass.EDU.Term.Term>, IOrderedQueryable<DomainClass.EDU.Term.Term>> orderBy = null)
        {
            return await _uow.Term.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
