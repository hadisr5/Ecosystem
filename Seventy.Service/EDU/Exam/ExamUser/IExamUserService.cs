using System;
using Seventy.Data;
using Seventy.Service.BaseService;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Seventy.Common.Enums;
using Seventy.ViewModel.EDU;
using Seventy.ViewModel.EDU.Exam;

namespace Seventy.Service.EDU.ExamUser
{
    public interface IExamUserService : IBaseService<DomainClass.EDU.Exam.ExamUser>
    {
        Task<bool> AssignExamToUserGroups(List<int> userGroupIds, int examId, CancellationToken cancellationToken);
        Task<bool> AssignExamToUsers(List<int> userIds, int examId, CancellationToken cancellationToken);
        bool AssignedBefore(int examID, int userID);

        Task<PagedList<ExamUserViewModel>> GetAllPaginatedAsync(TypeEnum type, GenericPagingParameters genericPagingParameters
            , Expression<Func<ExamUserViewModel, bool>> filter = null,
            Func<IQueryable<ExamUserViewModel>
                , IOrderedQueryable<ExamUserViewModel>> orderBy = null);

        Task<PagedList<ExamUserViewModel>> GetAllPaginatedExerciseAsync(GenericPagingParameters genericPagingParameters
            , Expression<Func<ExamUserViewModel, bool>> filter = null,
            Func<IQueryable<ExamUserViewModel>
                , IOrderedQueryable<ExamUserViewModel>> orderBy = null);

        Task<PagedList<ExamWithUserViewModel>> GetAllPaginatedUsersInExamAsync(TypeEnum type,
            GenericPagingParameters genericPagingParameters
            , Expression<Func<ExamWithUserViewModel, bool>> filter = null,
            Func<IQueryable<ExamWithUserViewModel>
                , IOrderedQueryable<ExamWithUserViewModel>> orderBy = null);

        Task<List<DomainClass.EDU.Exam.ExamUser>> GetByUserAndExamIdAsync(CancellationToken cancellationToken,
            int examId, int userId);
    }
}
