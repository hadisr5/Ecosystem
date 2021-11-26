using Seventy.Data;
using Seventy.Repository.Core;
using Seventy.ViewModel.EDU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Seventy.Common.Enums;
using Seventy.ViewModel.Core;
using Seventy.ViewModel.EDU.Content;
using Seventy.ViewModel.EDU.TrainingWeek;
using System.IO;

namespace Seventy.Service.EDU.TrainingWeekContent
{
    public class TrainingWeekContentService : BaseService.BaseService<DomainClass.EDU.TrainingWeek.TrainingWeekContent>, ITrainingWeekContentService
    {
        public TrainingWeekContentService(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<DomainClass.EDU.TrainingWeek.TrainingWeekContent> Table() => _uow.TrainingWeekContent.Table.AsEnumerable();
        public override IEnumerable<DomainClass.EDU.TrainingWeek.TrainingWeekContent> TableNoTracking() => _uow.TrainingWeekContent.TableNoTracking.AsEnumerable();

        public override async Task<DomainClass.EDU.TrainingWeek.TrainingWeekContent> GetByIDAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _uow.TrainingWeekContent.GetByIDAsync(cancellationToken, ids);
        }

        public override async Task<bool> DeleteAsync(DomainClass.EDU.TrainingWeek.TrainingWeekContent entity, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.TrainingWeekContent.DeleteAsync(entity, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> DeleteRangeAsync(IEnumerable<DomainClass.EDU.TrainingWeek.TrainingWeekContent> entities, CancellationToken cancellationToken, bool hardDelete = false)
        {
            var result = await _uow.TrainingWeekContent.DeleteRangeAsync(entities, cancellationToken, hardDelete);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.TrainingWeek.TrainingWeekContent> InsertAsync(DomainClass.EDU.TrainingWeek.TrainingWeekContent entity, CancellationToken cancellationToken)
        {
            var result = await _uow.TrainingWeekContent.InsertAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> InsertRangeAsync(IEnumerable<DomainClass.EDU.TrainingWeek.TrainingWeekContent> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.TrainingWeekContent.InsertRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<DomainClass.EDU.TrainingWeek.TrainingWeekContent> UpdateAsync(DomainClass.EDU.TrainingWeek.TrainingWeekContent entity, CancellationToken cancellationToken)
        {
            var result = await _uow.TrainingWeekContent.UpdateAsync(entity, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public override async Task<bool> UpdateRangeAsync(IEnumerable<DomainClass.EDU.TrainingWeek.TrainingWeekContent> entities, CancellationToken cancellationToken)
        {
            var result = await _uow.TrainingWeekContent.UpdateRangeAsync(entities, cancellationToken);
            await _uow.CompleteAsync(cancellationToken);
            return result;
        }

        public async Task<PagedList<TrainingWeekContentViewModel>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters
                , Expression<Func<TrainingWeekContentViewModel, bool>> filter = null, Func<IQueryable<TrainingWeekContentViewModel>
                    , IOrderedQueryable<TrainingWeekContentViewModel>> orderBy = null)
        {
            try
            {
                var trainingWeeks = _uow.TrainingWeek.TableNoTracking;
                var trainingContents = _uow.TrainingContent.TableNoTracking;
                var trainingWeekContents = _uow.TrainingWeekContent.TableNoTracking;

                var query = from twc in trainingWeekContents
                            from tw in trainingWeeks.Where(x => x.ID == twc.TrainingWeekID)
                            from tc in trainingContents.Where(x => x.ID == twc.ContentID)
                            select new TrainingWeekContentViewModel
                            {
                                ID = twc.ID,
                                TrainingWeekID = twc.TrainingWeekID,
                                TrainingWeekTitle = tw.Title,
                                ContentType = twc.ContentType,
                                ContentID = twc.ContentID,
                                ContentTitle = tc.Title,
                                Description = twc.Description,
                                IsActive = twc.IsActive,
                                RegDate = twc.RegDate,
                                RegUserID = twc.RegUserID
                            };

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return await PagedList<TrainingWeekContentViewModel>.ToPagedList(orderBy(query),
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }
                else
                {
                    return await PagedList<TrainingWeekContentViewModel>.ToPagedList(query,
                            genericPagingParameters.PageNumber,
                            genericPagingParameters.PageSize);
                }
            }
            catch
            {
                return null;
            }
        }

        public override async Task<PagedList<DomainClass.EDU.TrainingWeek.TrainingWeekContent>> GetAllPaginatedAsync(GenericPagingParameters genericPagingParameters, Expression<Func<DomainClass.EDU.TrainingWeek.TrainingWeekContent, bool>> filter = null, Func<IQueryable<DomainClass.EDU.TrainingWeek.TrainingWeekContent>, IOrderedQueryable<DomainClass.EDU.TrainingWeek.TrainingWeekContent>> orderBy = null)
        {
            return await _uow.TrainingWeekContent.GetAllPaginatedAsync(genericPagingParameters, filter, orderBy);
        }

        public async Task<List<FilesSecondViewModel>> GetByLessonIdAndTypeAsync(ContentTypeEnum type, int lessonId, CancellationToken cancellationToken)
        {
            try
            {
                if (lessonId == 0)
                    return null;

                IQueryable<FilesSecondViewModel> contents;

                switch (type)
                {
                    case ContentTypeEnum.Video:
                        contents = _uow.TrainingWeekContent.TableNoTracking
                            .Include(a => a.Content)
                            .Include(a => a.Content.File)
                            .Include(a => a.TrainingWeek)
                            .Where(a => a.ContentType.Equals("ویدیو") &&
                                        a.TrainingWeek.LessonID.Equals(lessonId))
                            .Select(a => new FilesSecondViewModel
                            {
                                FileId = a.Content.FileID,
                                FileExtension = a.Content.File.FileExtension,
                                Title = a.Content.File.Title,
                                ID = a.ContentID,
                                UserID = a.Content.File.UserID,
                                RegDate = a.Content.File.RegDate,
                                IsActive = a.Content.File.IsActive,
                                RegUserID = a.Content.File.RegUserID,
                                Description = a.Content.File.Description
                            });
                        break;

                    case ContentTypeEnum.Html:
                        contents = _uow.TrainingWeekContent.TableNoTracking
                            .Include(a => a.Content)
                            .Include(a => a.Content.File)
                            .Include(a => a.TrainingWeek)
                            .Where(a => a.ContentType.Equals("HTML") &&
                                        a.TrainingWeek.LessonID.Equals(lessonId))
                            .Select(a => new FilesSecondViewModel
                            {
                                FileId = a.Content.FileID,
                                FileExtension = a.Content.File.FileExtension,
                                Title = a.Content.File.Title,
                                ID = a.ContentID,
                                UserID = a.Content.File.UserID,
                                RegDate = a.Content.File.RegDate,
                                IsActive = a.Content.File.IsActive,
                                RegUserID = a.Content.File.RegUserID,
                                Description = a.Content.File.Description
                            });
                        break;

                    default:
                        contents = _uow.TrainingWeekContent.TableNoTracking
                            .Include(a => a.Content)
                            .Include(a => a.Content.File)
                            .Include(a => a.TrainingWeek)
                            .Where(a =>
                                   a.TrainingWeek.LessonID.Equals(lessonId))
                            .Select(a => new FilesSecondViewModel
                            {
                                FileId = a.Content.FileID,
                                FileExtension = a.Content.File.FileExtension,
                                Title = a.Content.File.Title,
                                ID = a.ContentID,
                                UserID = a.Content.File.UserID,
                                RegDate = a.Content.File.RegDate,
                                IsActive = a.Content.File.IsActive,
                                RegUserID = a.Content.File.RegUserID,
                                Description = a.Content.File.Description
                            });
                        break;
                }

                return await contents.ToListAsync(cancellationToken);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<ContentUserViewModel>> GetUserByContentIdAsync(int contentId, CancellationToken cancellationToken)
        {
            try
            {
                if (contentId == 0)
                    return null;

                var data = await _uow.TrainingWeekContent.TableNoTracking
                    .Where(a => a.ContentID.Equals(contentId))
                    .Join(_uow.UserProfiles.TableNoTracking,
                        content => content.RegUserID,
                        profile => profile.UserID,
                        (content, profile) => new { Content = content, Profile = profile })
                    .Select(a => new ContentUserViewModel
                    {
                        UserId = a.Profile.UserID,
                        UserName = a.Profile.FirstName + " " + a.Profile.LastName
                    })
                    .ToListAsync(cancellationToken);

                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<ContentUserViewModel>> GetUserByContentWeekIdAsync(CancellationToken cancellationToken, int weekId, int? userId = null)
        {
            try
            {
                if (weekId == 0)
                    return null;

                var trainingWeek = await _uow.TrainingWeek.TableNoTracking
                    .Where(a => a.ID.Equals(weekId))
                    .SingleAsync(cancellationToken);

                var termLesson = await _uow.TermLesson.TableNoTracking
                    .SingleAsync(a => a.LessonID.Equals(trainingWeek.LessonID) &&
                                      a.TermID.Equals(trainingWeek.TermID), cancellationToken);

                var courseGroup = await _uow.CourseGroups.TableNoTracking.Include(x => x.CourseRegistrations)
                    .SingleAsync(a => a.CourseID.Equals(termLesson.CourseID),
                        cancellationToken);

                List<ContentUserViewModel> result = new List<ContentUserViewModel>();

                foreach (var item in courseGroup.CourseRegistrations)
                {
                    if (item.UserID.Equals(userId))
                        continue;

                    var user = await _uow.UserProfiles.TableNoTracking
                        .SingleOrDefaultAsync(a => a.UserID.Equals(item.UserID),
                            cancellationToken);

                    result.Add(new ContentUserViewModel
                    {
                        UserId = user.UserID,
                        UserName = user.FirstName + " " + user.LastName
                    });
                }

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ContentOtherViewModel> GetOtherInfoAsync(CancellationToken cancellationToken, int weekId)
        {
            try
            {
                if (weekId == 0)
                    return null;

                var trainingWeek = await _uow.TrainingWeek.TableNoTracking
                    .Where(a => a.ID.Equals(weekId))
                    .SingleAsync(cancellationToken);

                var termLesson = await _uow.TermLesson.TableNoTracking
                    .SingleAsync(a => a.LessonID.Equals(trainingWeek.LessonID) &&
                                      a.TermID.Equals(trainingWeek.TermID), cancellationToken);

                var teacher = await _uow.UserProfiles.TableNoTracking
                    .SingleAsync(a => a.UserID.Equals(termLesson.TeacherID),
                        cancellationToken);

                var course = await _uow.Course.TableNoTracking
                    .SingleAsync(a => a.ID.Equals(termLesson.CourseID),
                        cancellationToken);

                return new ContentOtherViewModel
                {
                    TeacherId = (int)teacher.UserID,
                    TeacherName = teacher.FirstName + " " + teacher.LastName,
                    CourseId = course.ID,
                    CourseName = course.Title,
                    CourseDescription = course.Description
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
