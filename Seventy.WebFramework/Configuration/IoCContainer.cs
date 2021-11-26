using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Seventy.Repository.Core;
using Seventy.Repository.Core.Repositories;
using Seventy.Repository.Persistence;
using Seventy.Repository.Persistence.Repositories;
using Seventy.Service.Core.AccessPermissionGroup;
using Seventy.Service.Core.Documents;
using Seventy.Service.Core.Files;
using Seventy.Service.Core.Message;
using Seventy.Service.Core.Messenger;
using Seventy.Service.Core.PermissionGroup;
using Seventy.Service.Core.Permissions;
using Seventy.Service.Core.RolePermissions;
using Seventy.Service.Core.Roles;
using Seventy.Service.Core.Roles.DefaultRoleAccess;
using Seventy.Service.Core.UserAccess;
using Seventy.Service.Core.UserDocument;
using Seventy.Service.Core.UserGroup;
using Seventy.Service.Core.UserGroupMember;
using Seventy.Service.Core.UserPermissionGroup;
using Seventy.Service.Core.UserProfiles;
using Seventy.Service.Core.UserRole;
using Seventy.Service.EDU.CateringPackage;
using Seventy.Service.EDU.Certificate;
using Seventy.Service.EDU.CertificateUser;
using Seventy.Service.EDU.ContentObservation;
using Seventy.Service.EDU.Course;
using Seventy.Service.EDU.CourseCategory;
using Seventy.Service.EDU.CourseGroup;
using Seventy.Service.EDU.CourseObservation;
using Seventy.Service.EDU.Exam;
using Seventy.Service.EDU.ExamAnswerSheet;
using Seventy.Service.EDU.ExamQuestions;
using Seventy.Service.EDU.ExamUser;
using Seventy.Service.EDU.Exercise;
using Seventy.Service.EDU.ExerciseUser;
using Seventy.Service.EDU.FavoriteCourses;
using Seventy.Service.EDU.Forum;
using Seventy.Service.EDU.Lesson;
using Seventy.Service.EDU.LessonObservation;
using Seventy.Service.EDU.LMS;
using Seventy.Service.EDU.Main;
using Seventy.Service.EDU.Poll;
using Seventy.Service.EDU.PollUser;
using Seventy.Service.EDU.QuestionOptions;
using Seventy.Service.EDU.Questions;
using Seventy.Service.EDU.RelatedCourses;
using Seventy.Service.EDU.RequestedCourses;
using Seventy.Service.EDU.RequestForContent;
using Seventy.Service.EDU.TeacherLesson;
using Seventy.Service.EDU.Term;
using Seventy.Service.EDU.TrainingCenter;
using Seventy.Service.EDU.TrainingContent;
using Seventy.Service.EDU.TrainingEval;
using Seventy.Service.EDU.TrainingWeek;
using Seventy.Service.EDU.TrainingWeekContent;
using Seventy.Service.EDU.UserContent;
using Seventy.Service.EDU.UserCourseGroup;
using Seventy.Service.EDU.UserLesson;
using Seventy.Service.EDU.UserTrainingWeekContent;
using Seventy.Service.Tickets;
using Seventy.Service.Users;
using Seventy.ViewModel.EDU;

namespace Seventy.WebFramework.Configuration
{
    public static class IoCContainer
    {
        public static void AddRepositoryDependencies(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAccessRepository, AccessRepository>();
        }
        public static void AddServiceDependencies(this IServiceCollection services)
        {

            services.AddTransient<IUserManager, UserManager>();
            services.AddScoped<INotif, NotifManager>();
            services.AddTransient<IUserDocumentService, UserDocumentService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<IMessageSender, SMSMessenger>();
            services.AddTransient<IUserRoleService, UserRoleService>();

            #region EDU Services
            services.AddTransient<IMainService, MainService>();
            services.AddTransient<IAccessService, AccessService>();
            services.AddTransient<IUserTrainingWeekContentService, UserTrainingWeekContentService>();
            services.AddTransient<IUserGroupService, UserGroupService>();
            services.AddTransient<IUserGroupMemberService, UserGroupMemberService>();
            services.AddTransient<Service.Core.Logs.ILogsService, Service.Core.Logs.LogsService>();
            services.AddTransient<Service.Core.Roles.IRolesService, RoleService>();
            services.AddTransient<ICateringPackageService, CateringPackageService>();
            services.AddTransient<ICertificateService, CertificateService>();
            services.AddTransient<ICertificateUserService, CertificateUserService>();
            services.AddTransient<IContentObservationService, ContentObservationService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IExamAnswerSheetService, ExamAnswerSheetService>();
            services.AddTransient<ICourseObservationService, CourseObservationService>();
            services.AddTransient<IExamService, ExamService>();
            services.AddTransient<IExamQuestionsService, ExamQuestionsService>();
            services.AddTransient<IExamUserService, ExamUserService>();
            services.AddTransient<IExerciseService, ExerciseService>();
            services.AddTransient<IExerciseUserService, ExerciseUserService>();
            services.AddTransient<IFavoriteCoursesService, FavoriteCoursesService>();
            services.AddTransient<IFilesService, FilesService>();
            services.AddTransient<IForumService, ForumService>();
            services.AddTransient<ILessonService, LessonService>();
            services.AddTransient<ITermLessonService, TermLessonService>();
            services.AddTransient<ICourseRegistrationService, CourseRegistrationService>();
            services.AddTransient<IDefaultRoleAccessService, DefaultRoleAccessService>();
            services.AddTransient<ILessonObservationService, LessonObservationService>();
            services.AddTransient<ILMSService, LMSService>();
            services.AddTransient<IPollService, PollService>();
            services.AddTransient<IPollUserService, PollUserService>();
            services.AddTransient<IQuestionsService, QuestionsService>();
            services.AddTransient<IQuestionOptionsService, QuestionOptionsService>();
            services.AddTransient<IRelatedCoursesService, RelatedCoursesService>();
            services.AddTransient<IRequestedCoursesService, RequestedCoursesService>();
            services.AddTransient<IRequestForContentService, RequestForContentService>();
            services.AddTransient<ITeacherLessonService, TeacherLessonService>();
            services.AddTransient<ICourseCategoryService, CourseCategoryService>();
            services.AddTransient<ITermService, TermService>();
            services.AddTransient<ITrainingCenterService, TrainingCenterService>();
            services.AddTransient<ITrainingContentService, TrainingContentService>();
            services.AddTransient<ITrainingEvalIndexService, TrainingEvalIndexService>();
            services.AddTransient<ITrainingEvalResultService, TrainingEvalResultService>();
            services.AddTransient<IUserTrainingWeekContentService, UserTrainingWeekContentService>();
            services.AddTransient<ITrainingWeekService, TrainingWeekService>();
            services.AddTransient<ITrainingWeekContentService, TrainingWeekContentService>();
            services.AddTransient<IUserContentService, UserContentService>();
            services.AddTransient<IUserLessonService, UserLessonService>();
            services.AddTransient<IUserProfilesService, UserProfilesService>();
            services.AddTransient<ICourseGroupsService, CourseGroupsService>();
            services.AddTransient<ICourseGroupsService, CourseGroupsService>();
            services.AddTransient<IDocumentsService, DocumentsService>();
            services.AddTransient<IDocumentsTypeService, DocumentsTypeService>();
            #endregion

            #region Admin Services
            services.AddTransient<IPermissionsService, PermissionsService>();
            services.AddTransient<IRolePermissionsService, RolePermissionsService>();
            services.AddTransient<IUserAccessService, UserAccessService>();

            services.AddTransient<IPermissionGroupService, PermissionGroupService>();
            services.AddTransient<IAccessPermissionGroupService, AccessPermissionGroupService>();
            services.AddTransient<IUserPermissionGroupService, UserPermissionGroupService>();

            #endregion

        }
        public static void AddAutoMapperMap(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ViewModel.Core.CoreProfiles).Assembly);
            services.AddAutoMapper(typeof(EDUProfiles).Assembly);
        }
    }
}
