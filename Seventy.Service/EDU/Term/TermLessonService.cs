using Seventy.Data;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Seventy.ViewModel.EDU.TermLesson;
using Seventy.Repository.Core;
using System.Collections.Generic;
using System.Threading;
using Seventy.DomainClass.EDU.Term;
using Microsoft.EntityFrameworkCore;

namespace Seventy.Service.EDU.Term
{
    public class TermLessonService : BaseService.BaseService<DomainClass.EDU.Term.TermLesson>, ITermLessonService
    {
        public TermLessonService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.Term.TermLesson> Table() => _uow.TermLesson.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.Term.TermLesson> TableNoTracking() => _uow.TermLesson.TableNoTracking.AsEnumerable();

        public async Task<List<DomainClass.EDU.Term.TermLesson>> GetByTermAndLesson(int TermID, int LessonID)
        {
            return await _uow.TermLesson.TableNoTracking.Where(a => a.TermID == TermID && a.LessonID == LessonID).ToListAsync();
        }

        public override async Task<DomainClass.EDU.Term.TermLesson> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.TermLesson.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Term.TermLesson entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.TermLesson.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Term.TermLesson> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.TermLesson.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Term.TermLesson> InsertAsync(DomainClass.EDU.Term.TermLesson entity, CancellationToken cancellationToken)
        {
            var result = await _uow.TermLesson.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Term.TermLesson> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.TermLesson.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Term.TermLesson> UpdateAsync(DomainClass.EDU.Term.TermLesson entity, CancellationToken cancellationToken)
        {
            var result = await _uow.TermLesson.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Term.TermLesson> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.TermLesson.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public async Task<PagedList<TermLessonViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters
                , Expression<Func<TermLessonViewModel, bool>> filter = null, Func<IQueryable<TermLessonViewModel>
                    , IOrderedQueryable<TermLessonViewModel>> orderBy = null)
        {
            try
            {

                var course = _uow.Course.TableNoTracking;
                var courseGroups = _uow.CourseGroups.TableNoTracking;
                var terms = _uow.Term.TableNoTracking;
                var termLesson = _uow.TermLesson.TableNoTracking;
                var lessons = _uow.Lesson.TableNoTracking;
                var teachers = _uow.TermLesson.TableNoTracking;
                var userProfiles = _uow.UserProfiles.TableNoTracking;

                var query = from tl in termLesson
                            from t in terms.Where(x => x.ID == tl.TermID)
                            from c in course.Where(x => x.ID == tl.CourseID)
                            from cg in courseGroups.Where(x => x.ID == tl.CourseGroupID)
                            from l in lessons.Where(x => x.ID == tl.LessonID)
                                //from tch in teachers.Where(x => x.ID == tl.TeacherID)
                            from prof in userProfiles.Where(x => x.UserID == tl.TeacherID).DefaultIfEmpty()
                            select new TermLessonViewModel
                            {
                                ID = tl.ID,
                                CourseName = c.Title,
                                GroupName = cg.Title,
                                LessonName = l.Title,
                                TeacherName = prof.FirstName + " " + prof.LastName,
                                Description = tl.Description,
                                IsActive = tl.IsActive,
                                RegDate = tl.RegDate,
                                RegUserID = tl.RegUserID,
                                TermName = t.Title
                            };

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return await PagedList<TermLessonViewModel>.ToPagedList(orderBy(query),
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }
                else
                {
                    return await PagedList<TermLessonViewModel>.ToPagedList(query,
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }

            }
            catch { return null; }
        }

        public override async Task<PagedList<TermLesson>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<TermLesson, bool>> filter = null, Func<IQueryable<TermLesson>, IOrderedQueryable<TermLesson>> orderBy = null)
        {
            return await _uow.TermLesson.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
