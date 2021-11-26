using Microsoft.EntityFrameworkCore;
using Seventy.Data;
using Seventy.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Seventy.Common.Enums;
using Seventy.Service.Users;
using Seventy.ViewModel.EDU;
using Seventy.ViewModel.EDU.Exam;

namespace Seventy.Service.EDU.ExamUser
{
    public class ExamUserService : BaseService.BaseService<DomainClass.EDU.Exam.ExamUser>, IExamUserService
    {
        private readonly IUserManager _userManager;

        public ExamUserService(IUnitOfWork uow, IUserManager userManager) : base(uow)
        {
            _userManager = userManager;
        }

        public override IEnumerable<DomainClass.EDU.Exam.ExamUser> Table() => _uow.ExamUser.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.Exam.ExamUser> TableNoTracking() => _uow.ExamUser.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.Exam.ExamUser> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.ExamUser.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Exam.ExamUser entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var checkDateOfExam = await _uow.Exam.TableNoTracking
                .SingleAsync(a => a.ID.Equals(entity.ExamID), cancellationToken);

            if (DateTime.Now > checkDateOfExam.StartDate)
                return false;

            var result = await _uow.ExamUser.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Exam.ExamUser> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.ExamUser.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Exam.ExamUser> InsertAsync(DomainClass.EDU.Exam.ExamUser entity, CancellationToken cancellationToken)
        {
            var result = await _uow.ExamUser.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Exam.ExamUser> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.ExamUser.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }
        public bool AssignedBefore(int examID, int userID)
        {
            return TableNoTracking().Count(q => q.ExamID == examID && q.UserID == userID) != 0;
        }
        public async Task<bool> AssignExamToUsers(List<int> userIds, int examId, CancellationToken cancellationToken)
        {
            List<DomainClass.EDU.Exam.ExamUser> userExams = new List<DomainClass.EDU.Exam.ExamUser>();
            foreach (int userId in userIds)
            {
                var assinedBefore = AssignedBefore(examId, userId);
                if (!assinedBefore)
                {
                    userExams.Add(new DomainClass.EDU.Exam.ExamUser
                    {
                        IsActive = true,
                        UserID = userId,
                        ExamID = examId,
                        RegUserID = _userManager?.GetCurrentUserID()
                    });
                }
            }
            return await InsertRangeAsync(userExams, cancellationToken);
        }
        public async Task<bool> AssignExamToUserGroups(List<int> userGroupIds, int examId, CancellationToken cancellationToken)
        {
            List<int> users = new List<int>();
            foreach (int groupId in userGroupIds)
            {
                var members = _uow.UserGroupMembers.Table.Where(w => w.UserGroupID == groupId).Select(s => s.UserID);
                users.AddRange(members);
            }
            return await AssignExamToUsers(users.Distinct().ToList(), examId, cancellationToken);
        }

        public override async Task<DomainClass.EDU.Exam.ExamUser> UpdateAsync(DomainClass.EDU.Exam.ExamUser entity, CancellationToken cancellationToken)
        {
            var result = await _uow.ExamUser.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Exam.ExamUser> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.ExamUser.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.EDU.Exam.ExamUser>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.Exam.ExamUser, bool>> filter = null, Func<IQueryable<DomainClass.EDU.Exam.ExamUser>, IOrderedQueryable<DomainClass.EDU.Exam.ExamUser>> orderBy = null)
        {
            return await _uow.ExamUser.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }

        public async Task<List<DomainClass.EDU.Exam.ExamUser>> GetByUserAndExamIdAsync(CancellationToken cancellationToken, int examId, int userId)
        {
            return await _uow.ExamUser.TableNoTracking
                .Include(a => a.Exam)
                .Include(a => a.Exam.Lesson)
                .Where(a => a.ExamID.Equals(examId) &&
                            a.UserID.Equals(userId)).ToListAsync(cancellationToken);
        }

        public async Task<PagedList<ExamUserViewModel>> GetAllPaginatedAsync(TypeEnum type, GenericPagingParameters genericPagingParameters
            , Expression<Func<ExamUserViewModel, bool>> filter = null,
            Func<IQueryable<ExamUserViewModel>
                , IOrderedQueryable<ExamUserViewModel>> orderBy = null)
        {
            try
            {
                var user = await _userManager
                    .GetCurrentUserAsync(new CancellationToken());

                IQueryable<DomainClass.EDU.Exam.ExamUser> exams;
                var userRoles = await _uow.UserRole.TableNoTracking.Include(i => i.Role).Where(w => w.UserID == user.ID).ToListAsync();

                //if (user.RoleID.Equals(2))
                if (userRoles.Where(w => w.RoleID.Equals(2)).FirstOrDefault() != null)
                {
                    switch (type)
                    {
                        case TypeEnum.Exam:
                            exams = _uow.ExamUser
                                .TableNoTracking
                                .Include(a => a.Exam)
                                .Where(a => a.Exam.Type.Equals("آزمون"));
                            break;

                        case TypeEnum.Exercise:
                            exams = _uow.ExamUser
                                .TableNoTracking
                                .Include(a => a.Exam)
                                .Where(a => a.Exam.Type.Equals("تمرین"));
                            break;

                        case TypeEnum.Quiz:
                            exams = _uow.ExamUser
                                .TableNoTracking
                                .Include(a => a.Exam)
                                .Where(a => a.Exam.Type.Equals("کوییز") || a.Exam.Type.Equals("کوئیز"));
                            break;

                        default:
                            exams = _uow.ExamUser
                                .TableNoTracking
                                .Include(a => a.Exam)
                                .Where(a => a.Exam.Type.Equals("آزمون"));
                            break;
                    }
                }
                else
                {
                    switch (type)
                    {
                        case TypeEnum.Exam:
                            exams = _uow.ExamUser
                                .TableNoTracking
                                .Include(a => a.Exam)
                                .Where(a => a.Exam.Type.Equals("آزمون") &&
                                          a.UserID.Equals(user.ID));
                            break;

                        case TypeEnum.Exercise:
                            exams = _uow.ExamUser
                                .TableNoTracking
                                .Include(a => a.Exam)
                                .Where(a => a.Exam.Type.Equals("تمرین") &&
                                          a.UserID.Equals(user.ID));
                            break;

                        case TypeEnum.Quiz:
                            exams = _uow.ExamUser
                                .TableNoTracking
                                .Include(a => a.Exam)
                                .Where(a => a.Exam.Type.Equals("کوییز") || a.Exam.Type.Equals("کوئیز") &&
                                            a.UserID.Equals(user.ID));
                            break;

                        default:
                            exams = _uow.ExamUser
                                .TableNoTracking
                                .Include(a => a.Exam)
                                .Where(a => a.Exam.Type.Equals("آزمون") &&
                                            a.UserID.Equals(user.ID));
                            break;
                    }
                }

                var lessons = _uow.Lesson.TableNoTracking;
                var users = _uow.UserProfiles.TableNoTracking;

                var query =
                    from e in exams
                    .Include(x=>x.Exam)
                    from lesson in lessons.Where(a => a.ID.Equals(e.Exam.LessonID))
                    from userP in users.Where(a => a.UserID.Equals(e.UserID))
                    select new ExamUserViewModel
                    {
                        ID = e.ID,
                        Description = e.Description,
                        IsActive = e.IsActive,
                        RegDate = e.RegDate,
                        RegUserID = e.RegUserID,
                        UserID = e.UserID,
                        Result = e.Result,
                        ExamTitle = e.Exam.Title,
                        UserNameAndFamily = userP.FirstName + " " + userP.LastName,
                        LessonTitle = lesson.Title,
                        StartDate = e.Exam.StartDate,
                        EndDate = e.Exam.EndDate,
                        ExamID = e.ExamID,
                        Exam=e.Exam
                    };

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return await PagedList<ExamUserViewModel>.ToPagedList(orderBy(query),
                        genericPagingParameters.PageNumber,
                        genericPagingParameters.PageSize);
                }
                else
                {
                    return await PagedList<ExamUserViewModel>.ToPagedList(query,
                        genericPagingParameters.PageNumber,
                        genericPagingParameters.PageSize);
                }
            }
            catch
            {
                return null;
            }
            //return null;

        }

        public async Task<PagedList<ExamWithUserViewModel>> GetAllPaginatedUsersInExamAsync(TypeEnum type, GenericPagingParameters genericPagingParameters
           , Expression<Func<ExamWithUserViewModel, bool>> filter = null,
           Func<IQueryable<ExamWithUserViewModel>
               , IOrderedQueryable<ExamWithUserViewModel>> orderBy = null)
        {
            try
            {
                IQueryable<DomainClass.EDU.Exam.ExamUser> exams;

                switch (type)
                {
                    case TypeEnum.Exam:
                        exams = _uow.ExamUser
                            .TableNoTracking
                            .Include(a => a.Exam)
                            .Where(a => a.Exam.Type.Equals("آزمون"));
                        break;

                    case TypeEnum.Exercise:
                        exams = _uow.ExamUser
                            .TableNoTracking
                            .Include(a => a.Exam)
                            .Where(a => a.Exam.Type.Equals("تمرین"));
                        break;

                    case TypeEnum.Quiz:
                        exams = _uow.ExamUser
                            .TableNoTracking
                            .Include(a => a.Exam)
                            .Where(a => a.Exam.Type.Equals("کوییز"));
                        break;

                    default:
                        exams = _uow.ExamUser
                            .TableNoTracking
                            .Include(a => a.Exam)
                            .Where(a => a.Exam.Type.Equals("آزمون"));
                        break;
                }

                var users = _uow.UserProfiles.TableNoTracking;

                var query =
                    from e in exams
                    from userP in users.Where(a => a.UserID.Equals(e.UserID))
                    select new ExamWithUserViewModel
                    {
                        ID = e.ID,
                        Description = e.Description,
                        IsActive = e.IsActive,
                        RegDate = e.RegDate,
                        RegUserID = e.RegUserID,
                        UserID = e.UserID,
                        ExamTitle = e.Exam.Title,
                        ExamID = e.ExamID,
                        UserNameAndFamily = userP.FirstName + " " + userP.LastName,
                    };

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return await PagedList<ExamWithUserViewModel>.ToPagedList(orderBy(query),
                        genericPagingParameters.PageNumber,
                        genericPagingParameters.PageSize);
                }
                else
                {
                    return await PagedList<ExamWithUserViewModel>.ToPagedList(query,
                        genericPagingParameters.PageNumber,
                        genericPagingParameters.PageSize);
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<PagedList<ExamUserViewModel>> GetAllPaginatedExerciseAsync(GenericPagingParameters genericPagingParameters
           , Expression<Func<ExamUserViewModel, bool>> filter = null,
           Func<IQueryable<ExamUserViewModel>
               , IOrderedQueryable<ExamUserViewModel>> orderBy = null)
        {
            try
            {
                var user = await _userManager
                    .GetCurrentUserAsync(new CancellationToken());

                IQueryable<DomainClass.EDU.Exam.ExamUser> exams;
                var userRoles = await _uow.UserRole.TableNoTracking.Include(i => i.Role).Where(w => w.UserID == user.ID).ToListAsync();
                //if (user.RoleID.Equals(5))
                if (userRoles.Where(w => w.RoleID.Equals(5)).FirstOrDefault() != null)
                {
                    exams = _uow.ExamUser
                        .TableNoTracking
                        .Include(a => a.Exam)
                        .Where(a => a.Exam.Type.Equals("تمرین"));
                }
                else
                {
                    exams = _uow.ExamUser
                       .TableNoTracking
                       .Include(a => a.Exam)
                       .Where(a => a.UserID.Equals(user.ID) && a.Exam.Type.Equals("تمرین"));
                }

                var lessons = _uow.Lesson.TableNoTracking;
                var users = _uow.UserProfiles.TableNoTracking;

                var query =
                    from e in exams
                    from lesson in lessons.Where(a => a.ID.Equals(e.Exam.LessonID))
                    from userP in users.Where(a => a.UserID.Equals(e.UserID))
                    select new ExamUserViewModel
                    {
                        ID = e.ID,
                        Description = e.Description,
                        IsActive = e.IsActive,
                        RegDate = e.RegDate,
                        RegUserID = e.RegUserID,
                        UserID = e.UserID,
                        Result = e.Result,
                        ExamTitle = e.Exam.Title,
                        UserNameAndFamily = userP.FirstName + " " + userP.LastName,
                        LessonTitle = lesson.Title,
                        ExamID = e.ExamID
                    };

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return await PagedList<ExamUserViewModel>.ToPagedList(orderBy(query),
                        genericPagingParameters.PageNumber,
                        genericPagingParameters.PageSize);
                }
                else
                {
                    return await PagedList<ExamUserViewModel>.ToPagedList(query,
                        genericPagingParameters.PageNumber,
                        genericPagingParameters.PageSize);
                }
            }
            catch
            {
                return null;
            }
            //return null;
        }
    }
}
