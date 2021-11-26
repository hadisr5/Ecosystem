using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Seventy.Data;
using Seventy.Repository.Core;
using Seventy.Service.Users;
using Seventy.ViewModel.EDU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Seventy.DomainClass.Core;
using Seventy.ViewModel;
using DataTables.AspNet.Core;
using Extensions;
using System.Security.Cryptography.X509Certificates;

namespace Seventy.Service.EDU.Questions
{
    public class QuestionsService : BaseService.BaseService<DomainClass.EDU.Exam.Questions>, IQuestionsService
    {
        private readonly IMapper mapper;
        private readonly IUserManager userManager;
        public QuestionsService(IUnitOfWork uow, IMapper mapper, IUserManager userManager) : base(uow)
        {
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public override IEnumerable<DomainClass.EDU.Exam.Questions> Table() => _uow.Questions.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.Exam.Questions> TableNoTracking() => _uow.Questions.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.Exam.Questions> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.Questions.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.Exam.Questions entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Questions.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.Exam.Questions> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.Questions.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Exam.Questions> InsertAsync(DomainClass.EDU.Exam.Questions entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Questions.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.Exam.Questions> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Questions.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.Exam.Questions> UpdateAsync(DomainClass.EDU.Exam.Questions entity, CancellationToken cancellationToken)
        {
            var result = await _uow.Questions.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public async Task<List<UserProfiles>> GetUsersInQuestion(CancellationToken cancellationToken)
        {
            var result = await _uow.Questions.TableNoTracking
                .Include(a => a.RegUser)
                .Select(a => a.RegUser.ID)
                .ToListAsync(cancellationToken);

            var users = await _uow.UserProfiles.TableNoTracking
                .Where(a => result.Contains(a.UserID)).ToListAsync(cancellationToken);

            return users;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.Exam.Questions> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.Questions.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public async Task<PagedList<QuestionsViewModel>> GetAllPaginatedBySumBaromAsync(int examId, GenericPagingParameters genericPagingParameters
            , Expression<Func<QuestionsViewModel, bool>> filter = null,
            Func<IQueryable<QuestionsViewModel>, IOrderedQueryable<QuestionsViewModel>> orderBy = null)
        {
            try
            {
                var lessonList = _uow.Lesson.TableNoTracking;
                var questions = _uow.Questions.TableNoTracking;

                var exam = await _uow.Exam.TableNoTracking
                    .Include(a => a.ExamQuestions)
                    .SingleAsync(a => a.ID.Equals(examId));

                var question = exam.ExamQuestions
                    .Select(a => a.QuestionID).ToList();

                var query = from p in questions
                        .Where(a => question.Contains((int)a.ID))
                            join a in lessonList on p.LessonID equals a.ID
                            select new QuestionsViewModel
                            {
                                ID = p.ID,
                                LessonTitle = a.Title,
                                FileID = p.FileID,
                                QuestionLevel = p.QuestionLevel,
                                Title = p.Title,
                                MultiOption = p.MultiOption
                            };

                foreach (var item in query)
                {
                    var opt = _uow.ExamQuestions
                        .TableNoTracking
                        .Where(a => a.QuestionID.Equals(item.ID));

                    var barom = await opt.SumAsync(a => a.Barom);

                    item.Barom = barom;
                }

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return await PagedList<QuestionsViewModel>.ToPagedList(orderBy(query),
                        genericPagingParameters.PageNumber,
                        genericPagingParameters.PageSize);
                }
                else
                {
                    return await PagedList<QuestionsViewModel>.ToPagedList(query,
                        genericPagingParameters.PageNumber,
                        genericPagingParameters.PageSize);
                }
            }
            catch { return null; }
        }

        public async Task<GridResponseModel> GetExamQuestionsBySumBaromAsync(int ExamID, IDataTablesRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var lessonList = _uow.Lesson.TableNoTracking;
                var questions = _uow.Questions.TableNoTracking;

                var exam = await _uow.Exam.TableNoTracking
                    .Include(a => a.ExamQuestions)
                    .SingleAsync(a => a.ID.Equals(ExamID));

                var question = exam.ExamQuestions
                    .Select(a => a.QuestionID).ToList();

                var query = from p in questions
                        .Where(a => question.Contains((int)a.ID)).SPF(request, x => x.ID, out int total)
                            join a in lessonList on p.LessonID equals a.ID
                            select new QuestionsViewModel
                            {
                                ID = p.ID,
                                LessonTitle = a.Title,
                                FileID = p.FileID,
                                QuestionLevel = p.QuestionLevel,
                                Title = p.Title,
                                MultiOption = p.MultiOption
                            };

                foreach (var item in query)
                {
                    var opt = _uow.ExamQuestions
                        .TableNoTracking
                        .Where(a => a.QuestionID.Equals(item.ID));

                    var barom = await opt.SumAsync(a => a.Barom);

                    item.Barom = barom;
                }

                var data = await query.ToListAsync(cancellationToken);
                var model = new GridResponseModel()
                {
                    draw = request.Draw,
                    data = data,
                    recordsTotal = total,
                    recordsFiltered = total
                };
                return model;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public async Task<GridResponseModel> GetExamQuestionsAsync(int examID, IDataTablesRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var examQuestions = _uow.ExamQuestions
                        .TableNoTracking
                        .Where(a => a.ExamID==examID)
                        .Include(e=>e.Question)
                        .Include(e=>e.Exam).ThenInclude(e=>e.Lesson);

                var query = from p in examQuestions.SPF(request,x=>x.ExamID,out int total)
                            select new QuestionsViewModel
                            {
                                ID = p.ID,
                                LessonTitle = p.Exam.Lesson.Title,
                                FileID = p.Question.FileID,
                                QuestionLevel = p.Question.QuestionLevel,
                                Title = p.Question.Title,
                                MultiOption = p.Question.MultiOption,
                                Barom = p.Barom
                            };

                var data = await query.ToListAsync(cancellationToken);
                var model = new GridResponseModel()
                {
                    draw = request.Draw,
                    data = data,
                    recordsTotal = total,
                    recordsFiltered = total,
                    //AdditionalData = data.Sum(x => x.Barom)
                };
                return model;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PagedList<QuestionsViewModel>> GetAllPaginatedCustomAsync(GenericPagingParameters genericPagingParameters
            , Expression<Func<QuestionsViewModel, bool>> filter = null,
            Func<IQueryable<QuestionsViewModel>,
                IOrderedQueryable<QuestionsViewModel>> orderBy = null,
            bool? multiOption = null, int? userId = null, int? barom = null, int? level = null, int? lessonId = null)
        {
            try
            {
                var lessonList = _uow.Lesson.TableNoTracking;
                var questions = _uow.Questions.TableNoTracking;
                var query = from p in questions
                            join a in lessonList on p.LessonID equals a.ID
                            select new QuestionsViewModel
                            {
                                ID = p.ID,
                                LessonTitle = a.Title,
                                FileID = p.FileID,
                                QuestionLevel = p.QuestionLevel,
                                Title = p.Title,
                                MultiOption = p.MultiOption,
                                LessonID = p.LessonID
                            };

                if (lessonId != null)
                    query = query.Where(a => a.LessonID.Equals(lessonId));

                if (multiOption != null && multiOption == true)
                    query = query.Where(a => a.MultiOption);

                if (multiOption != null && multiOption == false)
                    query = query.Where(a => !a.MultiOption);

                if (userId != null)
                    query = query.Where(a => a.RegUserID.Equals(userId));

                if (barom != null)
                    query = query.Where(a => a.Barom > barom);

                if (level != null)
                    query = query.Where(a => a.QuestionLevel.Equals(level));

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return await PagedList<QuestionsViewModel>.ToPagedList(orderBy(query),
                        genericPagingParameters.PageNumber,
                        genericPagingParameters.PageSize);
                }
                else
                {
                    return await PagedList<QuestionsViewModel>.ToPagedList(query,
                        genericPagingParameters.PageNumber,
                        genericPagingParameters.PageSize);
                }
            }
            catch { return null; }
        }

        public async Task<PagedList<QuestionsViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters
         , Expression<Func<QuestionsViewModel, bool>> filter = null,
         Func<IQueryable<QuestionsViewModel>, IOrderedQueryable<QuestionsViewModel>> orderBy = null)
        {
            try
            {
                var lessonList = _uow.Lesson.TableNoTracking;
                var questions = _uow.Questions.TableNoTracking;
                var query = from p in questions
                            join a in lessonList on p.LessonID equals a.ID
                            select new QuestionsViewModel
                            {
                                ID = p.ID,
                                LessonTitle = a.Title,
                                FileID = p.FileID,
                                QuestionLevel = p.QuestionLevel,
                                Title = p.Title,
                                MultiOption = p.MultiOption
                            };

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return await PagedList<QuestionsViewModel>.ToPagedList(orderBy(query),
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }
                else
                {
                    return await PagedList<QuestionsViewModel>.ToPagedList(query,
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }
            }
            catch { return null; }
        }

        public async Task<PagedList<QuestionsViewModel>> GetAllPaginatedByLessonIdAsync(int lessonId, GenericPagingParameters genericPagingParameters
            , Expression<Func<QuestionsViewModel, bool>> filter = null,
            Func<IQueryable<QuestionsViewModel>, IOrderedQueryable<QuestionsViewModel>> orderBy = null)
        {
            try
            {
                var lessonList = _uow.Lesson.TableNoTracking;
                var questions = _uow.Questions.TableNoTracking;

                var query = from p in questions.Where(a => a.LessonID.Equals(lessonId))
                            join a in lessonList on p.LessonID equals a.ID
                            select new QuestionsViewModel
                            {
                                ID = p.ID,
                                LessonTitle = a.Title,
                                FileID = p.FileID,
                                QuestionLevel = p.QuestionLevel,
                                Title = p.Title,
                                MultiOption = p.MultiOption
                            };

                if (filter != null)
                {
                    query = query.Where(filter);
                }
                
                if (orderBy != null)
                {
                    return await PagedList<QuestionsViewModel>.ToPagedList(orderBy(query),
                        genericPagingParameters.PageNumber,
                        genericPagingParameters.PageSize);
                }
                else
                {
                    return await PagedList<QuestionsViewModel>.ToPagedList(query,
                        genericPagingParameters.PageNumber,
                        genericPagingParameters.PageSize);
                }
            }
            catch(Exception ex)
            { return null; }
        }

        /// <summary>
        /// لیست سوالات بر اساس دوره
        /// </summary>
        /// <param name = "lessonID" ></ param >
        /// < param name="QuestionLevel"></param>
        /// <param name = "excludeList" ></ param >
        /// < param name="MultiOption">سوالات تستی یا تشریحی</param>
        /// <param name = "RegisteredByCurrentUser" > سوالات ثبت شده توسط کاربر جاری</param>
        /// <returns></returns>
        public async Task<IEnumerable<QuestionsViewModel>> GetAllQuestionsByLessonID(CancellationToken cancellationToken,
            int? lessonID, int? QuestionLevel, List<int> excludeList
            , bool MultiOption = true, bool RegisteredByCurrentUser = true)
        {
            var currentUser = await userManager.GetCurrentUserAsync(cancellationToken);
            int currentUserID = (int)currentUser.ID;

            return mapper.Map<IEnumerable<QuestionsViewModel>>(
                    await _uow.Questions.TableNoTracking.Where(x => (lessonID == null || x.LessonID == lessonID)
                   && (QuestionLevel == null || x.QuestionLevel == QuestionLevel)
                   && (x.MultiOption == MultiOption)
                   && (RegisteredByCurrentUser == false || x.RegUserID == currentUserID)
                   && x.IsActive == true
                   && !excludeList.Any(y => y == x.ID)
                    ).ToListAsync()
                );
        }

        public override async Task<PagedList<DomainClass.EDU.Exam.Questions>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.Exam.Questions, bool>> filter = null, Func<IQueryable<DomainClass.EDU.Exam.Questions>, IOrderedQueryable<DomainClass.EDU.Exam.Questions>> orderBy = null)
        {
            return await _uow.Questions.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }
    }
}
