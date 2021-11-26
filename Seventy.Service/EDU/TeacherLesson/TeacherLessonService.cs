using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Seventy.Data;
using Seventy.DomainClass.Core;
using Seventy.Repository.Core;
using Seventy.Service.BaseService;
using Seventy.ViewModel.EDU;

namespace Seventy.Service.EDU.TeacherLesson
{
    public class TeacherLessonService : BaseService<DomainClass.EDU.Teacher.TeacherLesson>, ITeacherLessonService
    {
        public TeacherLessonService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.Teacher.TeacherLesson> Table() =>
            _uow.TeacherLesson.Table.AsEnumerable();

        public override IEnumerable<DomainClass.EDU.Teacher.TeacherLesson> TableNoTracking() =>
            _uow.TeacherLesson.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.Teacher.TeacherLesson> GetByIDAsync(
            CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.TeacherLesson.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Teacher.TeacherLesson entity,
            CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.TeacherLesson.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Teacher.TeacherLesson> entities,
            CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.TeacherLesson.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Teacher.TeacherLesson> InsertAsync(
            DomainClass.EDU.Teacher.TeacherLesson entity, CancellationToken cancellationToken)
        {
            var result = await _uow.TeacherLesson.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Teacher.TeacherLesson> entities,
            CancellationToken cancellationToken)
        {
            var result = await _uow.TeacherLesson.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Teacher.TeacherLesson> UpdateAsync(
            DomainClass.EDU.Teacher.TeacherLesson entity, CancellationToken cancellationToken)
        {
            var result = await _uow.TeacherLesson.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Teacher.TeacherLesson> entities,
            CancellationToken cancellationToken)
        {
            var result = await _uow.TeacherLesson.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public async Task<PagedList<TeacherLessonViewModel>> GetAllPaginatedAsync(
            GenericPagingParameters genericPagingParameters
            , Expression<Func<TeacherLessonViewModel, bool>> filter = null, Func<IQueryable<TeacherLessonViewModel>
                , IOrderedQueryable<TeacherLessonViewModel>> orderBy = null)
        {
            try
            {
                var query = GetAllHelperAsync();

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return await PagedList<TeacherLessonViewModel>.ToPagedList(orderBy(query),
                        genericPagingParameters.PageNumber,
                        genericPagingParameters.PageSize);
                }
                else
                {
                    return await PagedList<TeacherLessonViewModel>.ToPagedList(query,
                        genericPagingParameters.PageNumber,
                        genericPagingParameters.PageSize);
                }
            }
            catch
            {
                return null;
            }
        }

        private IQueryable<TeacherLessonViewModel> GetAllHelperAsync(int? teacherId=null,int? lessonId=null)
        {
            var userProfiles = _uow.UserProfiles.TableNoTracking;
            var lessons = _uow.Lesson.TableNoTracking;
            var teacherLessons= _uow.TeacherLesson.TableNoTracking;
            
            if (teacherId.HasValue)
            {
                teacherLessons=teacherLessons.Where(tl => tl.TeacherID == teacherId);
            }

            if (lessonId.HasValue)
            {
                lessons=lessons.Where(tl => tl.ID == lessonId);
            }
            
            
            var query = (from tl in teacherLessons
                    from u in userProfiles.Where(x => x.UserID == tl.TeacherID).DefaultIfEmpty()
                    from l in lessons.Where(x => x.ID == tl.LessonID)
                    select new {TeacherLesson= tl, Lesson= l,UserProfiles= u});

                
          var viewModels=  query.Select(res =>
                     new TeacherLessonViewModel
                    {
                        ID = res.TeacherLesson.ID,
                        LessonID = res.TeacherLesson.LessonID,
                        LessonName = res.Lesson.Title,
                        TeacherID = res.TeacherLesson.TeacherID,
                        TeacherName = res.UserProfiles.FirstName + " " + res.UserProfiles.LastName,
                        Description = res.TeacherLesson.Description,
                        IsActive = res.TeacherLesson.IsActive,
                        RegDate = res.TeacherLesson.RegDate,
                        RegUserID = res.TeacherLesson.RegUserID,
                        PicId = res.Lesson.PicFileID
                    }
                );

            return viewModels;
        }

        public async Task<PagedList<TeacherLessonViewModel>>
            SearchAllPaginatedAsync(GenericPagingParameters genericPagingParameters
                , int? LessonID, int? TeacherID, Func<IQueryable<TeacherLessonViewModel>
                    , IOrderedQueryable<TeacherLessonViewModel>> orderBy = null)
        {
            try
            {
               
                var query = GetAllHelperAsync(TeacherID,LessonID);

                if (orderBy != null)
                {
                    return await PagedList<TeacherLessonViewModel>.ToPagedList(orderBy(query),
                        genericPagingParameters.PageNumber,
                        genericPagingParameters.PageSize);
                }
                else
                {
                    return await PagedList<TeacherLessonViewModel>.ToPagedList(query,
                        genericPagingParameters.PageNumber,
                        genericPagingParameters.PageSize);
                }
            }
            catch
            {
                return null;
            }
        }

        public override async Task<PagedList<DomainClass.EDU.Teacher.TeacherLesson>> GetAllPaginatedAsync(
            GenericPagingParameters genericPagingParameters,
            Expression<Func<DomainClass.EDU.Teacher.TeacherLesson, bool>> filter = null,
            Func<IQueryable<DomainClass.EDU.Teacher.TeacherLesson>,
                IOrderedQueryable<DomainClass.EDU.Teacher.TeacherLesson>> orderBy = null)
        {
            return await _uow.TeacherLesson.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}