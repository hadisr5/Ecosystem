using System;
using System.Linq;
using System.Threading;
using Seventy.Service.Users;
using Seventy.ViewModel.EDU;
using System.Threading.Tasks;
using Seventy.Repository.Core;
using Seventy.ViewModel.EDU.Main;
using Microsoft.EntityFrameworkCore;
using Seventy.Service.EDU.UserTrainingWeekContent;
using Seventy.Service.EDU.Course;
using Seventy.Repository.Core.Repositories;
using System.Collections.Generic;
using Seventy.ViewModel.EDU.TrainingWeek.TrainingWeekSituationSummary;
using System.Diagnostics.CodeAnalysis;

namespace Seventy.Service.EDU.Main
{
    public class MainService : IMainService
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserManager _userManager;
        private readonly IUserTrainingWeekContentService _userTrainingWeekContentService;


        public MainService(IUnitOfWork uow, IUserManager userManager, IUserTrainingWeekContentService userTrainingWeekContentService)
        {
            _uow = uow;
            _userManager = userManager;
            _userTrainingWeekContentService = userTrainingWeekContentService;
        }

        public async Task<MainStudentViewModel> GetStudentHomeDataAsync(CancellationToken cancellationToken)
        {
            var user = await _userManager.GetCurrentUserAsync(cancellationToken);
            var userId = user.ID;

            var isValid = await _uow.UserRole.TableNoTracking
                .Include(a => a.Role)
                .AnyAsync(a => a.UserID.Equals(userId) && a.Role.Title.Equals("Student"), cancellationToken);

            if (!isValid)
                return null;

            MainStudentViewModel viewModel = new MainStudentViewModel();

            var query = from courseRegistration in _uow.CourseRegistration.TableNoTracking
                        join course in _uow.Course.TableNoTracking
                        on courseRegistration.CourseID equals course.ID
                        join termLesson in _uow.TermLesson.TableNoTracking
                        on course.ID equals termLesson.CourseID
                        join term in _uow.Term.TableNoTracking
                        on termLesson.TermID equals term.ID
                        join userProfile in _uow.UserProfiles.TableNoTracking
                        on termLesson.TeacherID equals userProfile.UserID
                        where courseRegistration.UserID == userId
                        select new UserCourseDashboardViewModel
                        {
                            CourseID = (int)course.ID,
                            CourseTeacher = userProfile.FullName,
                            Duration = term.Duration,
                            EndDate = term.EndDate,
                            StartDate = term.StartDate,
                            HozoriType = course.HozoriType,
                            PhotoFileID = course.PhotoFileID,
                            Price = course.Price,
                            Title = course.Title,
                            State =
                              DateTime.Now < term.StartDate && DateTime.Now < term.EndDate ? CourseState.Pending
                            : DateTime.Now > term.StartDate && DateTime.Now > term.EndDate ? CourseState.Done
                            : CourseState.InProgress
                        };
            viewModel.UserCourses = query.ToList();
            var summary = new List<TrainingWeekSituationSummaryViewModel>();
            foreach (var item in viewModel.UserCourses)
            {
                var re = await _userTrainingWeekContentService.GetUserTrainingWeekSummaryReport((int)item.CourseID,
                    (int)userId);

                if (re == null)
                    continue;

                summary.Add(re);
            }
            viewModel.SummaryViewModel = summary;
            int progess = 0;
            summary.ForEach(f => progess += f.Progress);
            viewModel.Progress = summary.Count == 0 ? 0:(progess / summary.Count);
            var allCourses = from termLesson in _uow.TermLesson.TableNoTracking
                             join teacher in _uow.UserProfiles.TableNoTracking
                             on termLesson.TeacherID equals teacher.ID
                             join course in _uow.Course.TableNoTracking
                             on termLesson.CourseID equals course.ID
                             join term in _uow.Term.TableNoTracking
                             on termLesson.TermID equals term.ID
                             where term.StartDate > DateTime.Now
                             select new TermViewModel
                             {
                                 IsActive = course.IsActive,
                                 CourseTitle = course.Title,
                                 Duration = course.Duration,
                                 StartDate = term.StartDate,
                                 EndDate = term.EndDate,
                                 TeacherName = $"{teacher.FirstName} {teacher.LastName}",
                                 Title = course.Title,
                                 ID = termLesson.ID
                             };
            viewModel.Courses = await allCourses.ToListAsync();
            return viewModel;
        }

        public async Task<MainTeacherViewModel> GetTeacherHomeDataAsync(CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager
                    .GetCurrentUserAsync(new CancellationToken());

                var userId = user.ID;

                var isValid = await _uow.UserRole.TableNoTracking
                    .Include(a => a.Role)
                    .AnyAsync(a => a.UserID.Equals(userId) &&
                                   a.Role.Title.Equals("Teacher"), cancellationToken);

                if (!isValid)
                    return null;

                var teacherLessons = await _uow.TeacherLesson.TableNoTracking
                    .Include(a => a.Lesson)
                    .Include(a => a.Lesson.Exam)
                    .Where(a => a.TeacherID.Equals(userId))
                    .Select(a => new MainTeacherLessonViewModel
                    {
                        ID = a.ID,
                        LessonName = a.Lesson.Title,
                        PicId = a.Lesson.PicFileID,
                        Exams = a.Lesson.Exam
                    })
                    .ToListAsync(cancellationToken);

                return new MainTeacherViewModel
                {
                    LessonsWithExams = teacherLessons
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
