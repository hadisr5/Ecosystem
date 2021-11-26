using Microsoft.EntityFrameworkCore;
using Seventy.Data;
using Seventy.Repository.Core;
using Seventy.ViewModel.EDU;
using Seventy.ViewModel.EDU.TrainingWeek;
using Seventy.ViewModel.EDU.TrainingWeek.TrainingWeekSituationSummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Seventy.Service.EDU.UserTrainingWeekContent
{
    public class UserTrainingWeekContentService : BaseService.BaseService<DomainClass.EDU.TrainingWeek.UserTrainingWeekContent>, IUserTrainingWeekContentService
    {
        public UserTrainingWeekContentService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.TrainingWeek.UserTrainingWeekContent> Table() => _uow.UserTrainingWeekContent.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.TrainingWeek.UserTrainingWeekContent> TableNoTracking() => _uow.UserTrainingWeekContent.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.TrainingWeek.UserTrainingWeekContent> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.UserTrainingWeekContent.GetByIDAsync(cancellationToken, ids);
        }

        public async Task<List<DomainClass.EDU.TrainingWeek.UserTrainingWeekContent>> GetByUserIDAsync(int userID)
        {
            return await _uow.UserTrainingWeekContent.TableNoTracking.Where(x => x.UserID == userID).ToListAsync();
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.TrainingWeek.UserTrainingWeekContent entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.UserTrainingWeekContent.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.TrainingWeek.UserTrainingWeekContent> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.UserTrainingWeekContent.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.TrainingWeek.UserTrainingWeekContent> InsertAsync(DomainClass.EDU.TrainingWeek.UserTrainingWeekContent entity, CancellationToken cancellationToken)
        {
            var result = await _uow.UserTrainingWeekContent.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.TrainingWeek.UserTrainingWeekContent> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.UserTrainingWeekContent.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.TrainingWeek.UserTrainingWeekContent> UpdateAsync(DomainClass.EDU.TrainingWeek.UserTrainingWeekContent entity, CancellationToken cancellationToken)
        {
            var result = await _uow.UserTrainingWeekContent.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.TrainingWeek.UserTrainingWeekContent> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.UserTrainingWeekContent.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<PagedList<DomainClass.EDU.TrainingWeek.UserTrainingWeekContent>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.TrainingWeek.UserTrainingWeekContent, bool>> filter = null, Func<IQueryable<DomainClass.EDU.TrainingWeek.UserTrainingWeekContent>, IOrderedQueryable<DomainClass.EDU.TrainingWeek.UserTrainingWeekContent>> orderBy = null)
        {
            return await _uow.UserTrainingWeekContent.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }

        public async Task<TrainingWeekSituationSummaryViewModel> GetUserTrainingWeekSummaryReport(int courseID, int userID)
        {
            var viewModel = new TrainingWeekSituationSummaryViewModel();
            var query = _uow.UserTrainingWeekContent.TableNoTracking
                .Include(i => i.Term)
                .Include(i => i.Course)
                .Include(i => i.CourseGroup)
                .Include(i => i.Content)
                .Include(i => i.Lesson)
                .Include(i => i.TrainingWeek)
                .Where(w => w.CourseID == courseID && w.UserID == userID);
            if (query.Count() == 0)
                return null;
            var value = await query.FirstOrDefaultAsync();
            viewModel.CourseTitle = value.Course.Title;
            viewModel.GroupTitle = value.CourseGroup.Title;
            viewModel.Progress = (query.Where(w => w.Result == true).Count() / query.Count()) * 100;
            viewModel.Achievements = query.Where(w => w.Result == true).Select(s => s.Content).Select(s => s.Achievement).ToList();
            viewModel.Terms = new List<TermSituationSummary>();
            viewModel.Terms.AddRange(query.Select(s => new TermSituationSummary()
            {
                IsActive = s.Term.IsActive,
                CourseGroupID = s.Term.CourseGroupID,
                CourseID = s.Term.CourseID,
                Description = s.Term.Description,
                Duration = s.Term.Duration,
                EndDate = s.Term.EndDate,
                ID = (int)s.Term.ID,
                RegDate = s.Term.RegDate,
                RegUserID = s.Term.RegUserID,
                StartDate = s.Term.StartDate,
                Title = s.Term.Title
            }));
            viewModel.Terms.ForEach(f =>
            {
                f.Lessons = new List<LessonSituationSummary>();
                f.Lessons.AddRange(query.Where(w => w.TermID == f.ID).Select(s => new LessonSituationSummary()
                {
                    IsActive = s.Lesson.IsActive,
                    Description = s.Lesson.Description,
                    ID = (int)s.Lesson.ID,
                    PicFileID = s.Lesson.ID,
                    RegDate = s.Lesson.RegDate,
                    RegUserID = s.Lesson.RegUserID,
                    Title = s.Lesson.Title,
                    TrainingWeeks = query.Where(w => w.LessonID == (int)s.Lesson.ID).Select(z => new WeekSituationSummary()
                    {
                        IsActive = z.TrainingWeek.IsActive,
                        Description = z.TrainingWeek.Description,
                        ID = (int)z.TrainingWeek.ID,
                        LessonID = z.TrainingWeek.LessonID,
                        RegDate = z.TrainingWeek.RegDate,
                        RegUserID = z.TrainingWeek.RegUserID,
                        Title = z.TrainingWeek.Title,
                        TrainingContents = query.Where(w => w.TrainingWeekID == (int)z.TrainingWeek.ID).Select(u => new TrainingContentSituationSummary()
                        {
                            Achievement = u.Content.Achievement,
                            IsActive = u.Content.IsActive,
                            ContentType = u.Content.ContentType,
                            ExternalContentID = u.Content.ExternalContentID,
                            DemoState = u.Content.DemoState,
                            Description = u.Content.Description,
                            FileID = u.Content.FileID,
                            ID = (int)u.Content.ID,
                            RegDate = u.Content.RegDate,
                            RegUserID = u.Content.RegUserID,
                            Title = u.Content.Title
                        }).ToList()
                    }).ToList()
                }));
            });
            return viewModel;
        }

        public async Task<UserCourseSummaryViewModel> GetUserTrainingWeekSummaryReportByLesson(int termID, int courseID, int lessonID, int userID)
        {
            try
            {
                UserCourseSummaryViewModel resultViewModel = new UserCourseSummaryViewModel();

                resultViewModel.Term = await _uow.Term.GetByIDAsync(new CancellationToken(), termID);

                resultViewModel.CourseTitle = (await _uow.Course.GetByIDAsync(new CancellationToken(), courseID))?.Title;

                resultViewModel.CourseGroupTitle = (from termLesson in _uow.TermLesson.TableNoTracking.Where(x => x.TermID == termID && x.LessonID == lessonID)
                                                    join courseGroup in _uow.CourseGroups.TableNoTracking on termLesson.CourseGroupID equals courseGroup.ID
                                                    select new { courseGroup.Title }
                                                    ).FirstOrDefault()?.Title;


                resultViewModel.Lessons = await (from termLesson in _uow.TermLesson.TableNoTracking.Where(x => x.TermID == termID && x.LessonID == lessonID)
                                                 join lesson in _uow.Lesson.TableNoTracking on termLesson.LessonID equals lesson.ID
                                                 select lesson).ToListAsync();

                resultViewModel.TrainingWeeks = await _uow.TrainingWeek.TableNoTracking.Where(x => x.TermID == termID && x.LessonID == lessonID).ToListAsync();

                resultViewModel.TrainingWeekContents = await (from trainingWeek in _uow.TrainingWeek.TableNoTracking.Where(x => x.TermID == termID && x.LessonID == lessonID)
                                                              join trainingWeekContent in _uow.TrainingWeekContent.TableNoTracking on trainingWeek.ID equals trainingWeekContent.TrainingWeekID
                                                              join trainingContent in _uow.TrainingContent.TableNoTracking on trainingWeekContent.ContentID equals trainingContent.ID
                                                              select new TrainingWeekContentViewModel
                                                              {
                                                                  ContentID = trainingContent.ID.Value,
                                                                  ContentTitle = trainingContent.Title,
                                                                  ContentType = trainingContent.ContentType,
                                                                  TrainingWeekID = trainingWeek.ID.Value,
                                                                  TrainingWeekTitle = trainingWeek.Title
                                                              }
                                                    ).ToListAsync();

                resultViewModel.UserTrainingWeekContents =
                    await (from userContent in _uow.UserTrainingWeekContent.TableNoTracking.Where(x => x.TermID == termID && x.LessonID == lessonID && x.UserID == userID)
                           join trainingContent in _uow.TrainingContent.TableNoTracking on userContent.ContentID equals trainingContent.ID
                           select new UserTrainingWeekContentViewModel
                           {
                               ContentID = trainingContent.ID.Value,
                               ContentTitle = trainingContent.Title,
                               ContentType = trainingContent.ContentType,
                               ID = userContent.ID
                            ,
                               CourseGroupID = userContent.CourseGroupID
                            ,
                               CourseID = userContent.CourseID
                            ,
                               Description = userContent.Description
                            ,
                               IsActive = userContent.IsActive
                            ,
                               LessonID = userContent.LessonID
                            ,
                               LikeRank = userContent.LikeRank
                            ,
                               Progress = userContent.Progress
                            ,
                               RegDate = userContent.RegDate
                            ,
                               RegUserID = userContent.RegUserID
                            ,
                               Result = userContent.Result
                            ,
                               TermID = userContent.TermID
                            ,
                               TrainingWeekID = userContent.TrainingWeekID
                            ,
                               UserID = userContent.UserID
                           }).ToListAsync();

                return resultViewModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
