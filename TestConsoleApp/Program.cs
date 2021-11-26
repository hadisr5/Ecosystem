#region Usings
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Seventy.Data;
using Seventy.DomainClass;
using Seventy.Service.Core.Files;
using Seventy.Service.Core.Logs;
using Seventy.Service.Core.Messenger;
using Seventy.Service.Core.Permissions;
using Seventy.Service.Core.RolePermissions;
using Seventy.Service.Core.Roles;
using Seventy.Service.Core.UserProfiles;
using Seventy.Service.EDU.CateringPackage;
using Seventy.Service.EDU.Certificate;
using Seventy.Service.EDU.CertificateUser;
using Seventy.Service.EDU.ContentObservation;
using Seventy.Service.EDU.Course;
using Seventy.Service.EDU.CourseCategory;
using Seventy.Service.EDU.CourseObservation;
using Seventy.Service.EDU.Exam;
using Seventy.Service.EDU.ExamQuestions;
using Seventy.Service.EDU.ExamUser;
using Seventy.Service.EDU.Exercise;
using Seventy.Service.EDU.ExerciseUser;
using Seventy.Service.EDU.FavoriteCourses;
using Seventy.Service.EDU.Forum;
using Seventy.Service.EDU.Lesson;
using Seventy.Service.EDU.LessonObservation;
using Seventy.Service.EDU.LMS;
using Seventy.Service.EDU.Poll;
using Seventy.Service.EDU.PollUser;
using Seventy.Service.EDU.Questions;
using Seventy.Service.EDU.RelatedCourses;
using Seventy.Service.EDU.RequestedCourses;
using Seventy.Service.EDU.RequestForContent;
using Seventy.Service.EDU.Term;
using Seventy.Service.EDU.TrainingCenter;
using Seventy.Service.EDU.TrainingContent;
using Seventy.Service.EDU.TrainingWeek;
using Seventy.Service.EDU.TrainingWeekContent;
using Seventy.Service.EDU.UserContent;
using Seventy.Service.EDU.UserLesson;
using Seventy.Service.Users;
using System;
using System.IO;
using System.Threading.Tasks;
using Seventy.Repository.Core;
using Seventy.Repository.Persistence;
using Seventy.Service.EDU.TeacherLesson;
using Microsoft.Extensions.Hosting.Internal;

#endregion

namespace TestConsoleApp
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }
        public static IFilesService fileService;
        public static ITrainingWeekContentService  trainingWeekContentService;

        private static string HostingEnvironment => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        private static bool IsEnvironment(string environmentName) => HostingEnvironment?.ToLower() == environmentName?.ToLower() && null != environmentName;

        private static bool Development => IsEnvironment(EnvironmentName.Development);
        private static bool Production => IsEnvironment(EnvironmentName.Production);
        private static bool Staging => IsEnvironment(EnvironmentName.Staging);



        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            //setup our DI
            #region Services
            var serviceProvider = new ServiceCollection()
                    .AddSingleton<IConfigurationRoot>(provider => { return Configuration; })
                    .AddDbContext<DataContext>(options =>
                    {
                        options.UseSqlServer(Configuration.GetSection("PublicConfiguration").GetSection("ConnectionString").Value.ToString()
                            , o =>
                            {
                                o.EnableRetryOnFailure();
                            });
                    }, ServiceLifetime.Scoped)
                    .AddScoped<IUnitOfWork, UnitOfWork>()
                    .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                    .Configure<PublicConfiguration>(options => Configuration.GetSection("PublicConfiguration").Bind(options))
                    .AddScoped<ILogsService, LogsService>()
                    .AddTransient<ICateringPackageService, CateringPackageService>()
                    .AddTransient<ICertificateService, CertificateService>()
                    .AddTransient<ICertificateUserService, CertificateUserService>()
                    .AddTransient<IContentObservationService, ContentObservationService>()
                    .AddTransient<ICourseService, CourseService>()
                    .AddTransient<ICourseObservationService, CourseObservationService>()
                    .AddTransient<IExamService, ExamService>()
                    .AddTransient<IExamQuestionsService, ExamQuestionsService>()
                    .AddTransient<IExamUserService, ExamUserService>()
                    .AddTransient<IExerciseService, ExerciseService>()
                    .AddTransient<IExerciseUserService, ExerciseUserService>()
                    .AddTransient<IFavoriteCoursesService, FavoriteCoursesService>()

                    .AddTransient<IForumService, ForumService>()
                    .AddTransient<IFilesService, FilesService>()
                    .AddTransient<ILessonService, LessonService>()
                    .AddTransient<ILessonObservationService, LessonObservationService>()
                    .AddTransient<ILMSService, LMSService>()
                    .AddTransient<IPollService, PollService>()
                    .AddTransient<IPollUserService, PollUserService>()
                    .AddTransient<IQuestionsService, QuestionsService>()
                    .AddTransient<IRelatedCoursesService, RelatedCoursesService>()
                    .AddTransient<IRequestedCoursesService, RequestedCoursesService>()
                    .AddTransient<IRequestForContentService, RequestForContentService>()
                    //.AddTransient<ITeacherCourseService, TeacherCourseService>()
                    .AddTransient<ICourseCategoryService, CourseCategoryService>()

                    //.AddTransient<ITeacherUserService, TeacherUserService>()
                    .AddTransient<ITermService, TermService>()
                    .AddTransient<ITrainingCenterService, TrainingCenterService>()
                    .AddTransient<ITrainingContentService, TrainingContentService>()
                     .AddTransient<ITeacherLessonService, TeacherLessonService>()

                    .AddTransient<ITrainingWeekService, TrainingWeekService>()
                    .AddTransient<ITrainingWeekContentService, TrainingWeekContentService>()
                    .AddTransient<IPermissionsService, PermissionsService>()
                    .AddTransient<IUserContentService, UserContentService>()
                          .AddTransient<IRolePermissionsService, RolePermissionsService>()

                    .AddTransient<IUserLessonService, UserLessonService>()
                    .AddAutoMapper(typeof(Seventy.ViewModel.EDU.EDUProfiles).Assembly)
                    .AddTransient<IUserManager, UserManager>()
                    .AddScoped<IMessageSender, SMSMessenger>()
                    .AddSingleton<IUserProfilesService, UserProfilesService>()
                    .AddSingleton<IHostingEnvironment, HostingEnvironment>()
                    .BuildServiceProvider();
            #endregion

            //fileService = serviceProvider.GetService<IFilesService>();
            trainingWeekContentService = serviceProvider.GetService<ITrainingWeekContentService>();


            #region Test Code

            //var a = asyncTest();
            //return;
            //using (var stream = File.OpenRead(@"C:\Users\yousefi.DEV\Pictures\1428516.jpg"))
            //{
            //    var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
            //    {
            //        Headers = new HeaderDictionary(),
            //        ContentType = "image/jpeg"
            //    };

            //    fileService.UploadFileAsync(new Seventy.ViewModel.Core.FilesViewModel
            //    {
            //        Description = "test1",
            //        RegUserID = 4,
            //        Title = "تست 1",
            //        UserID = 4
            // ,
            //        UploadFile = file
            //    });
            //}


            #endregion

            Console.ReadLine();

        }

        async static Task<bool>  asyncTest()
        {
            var m = await trainingWeekContentService.GetAllPaginatedAsync(new GenericPagingParameters()
            { PageNumber = 1, PageSize = 10 });

            return true;
        }


    }
}
