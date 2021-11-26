using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Seventy.Data;
using Seventy.Service.BaseService;
using Seventy.ViewModel.EDU;

namespace Seventy.Service.EDU.TeacherLesson
{
    public interface ITeacherLessonService : IBaseService<DomainClass.EDU.Teacher.TeacherLesson>
    {
        /// <summary>
        /// lessonid=0 all lesson 
        /// teacherid=0 all teacher
        /// </summary>
        /// <param name="LessonId"></param>
        /// <param name="TeacherId"></param>
        /// <returns></returns>
        Task<PagedList<TeacherLessonViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters
            , Expression<Func<TeacherLessonViewModel, bool>> filter = null, Func<IQueryable<TeacherLessonViewModel>
                , IOrderedQueryable<TeacherLessonViewModel>> orderBy = null);


        /// <summary>
        /// Search For A Teacher lesson
        /// </summary>
        /// <returns></returns>
        Task<PagedList<TeacherLessonViewModel>>
            SearchAllPaginatedAsync(GenericPagingParameters genericPagingParameters
                , int? LessonID, int? TeacherID, Func<IQueryable<TeacherLessonViewModel>
                    , IOrderedQueryable<TeacherLessonViewModel>> orderBy = null);
    }
}
