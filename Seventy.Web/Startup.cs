using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Seventy.Data;
using Seventy.DomainClass;
using Seventy.Service.EDU.CateringPackage;
using Seventy.Service.EDU.Certificate;
using Seventy.Service.EDU.CertificateUser;
using Seventy.Service.EDU.ContentObservation;
using Seventy.Service.EDU.Course;
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
using Seventy.Service.EDU.CourseCategory;
using Seventy.Service.EDU.Term;
using Seventy.Service.EDU.TrainingCenter;
using Seventy.Service.EDU.TrainingContent;

using Seventy.Service.EDU.TrainingWeek;
using Seventy.Service.EDU.TrainingWeekContent;

using Seventy.Service.EDU.UserContent;
using Seventy.Service.EDU.UserLesson;
using Seventy.Service.Tickets;
using Seventy.Service.Users;
using AutoMapper;
using Seventy.ViewModel.EDU;
using Seventy.Service.Core.Messenger;
using Seventy.Web.StartupCustomizations;
using Seventy.Web.StartupCustomizations.CookieValidat;
using Seventy.Service.Core.UserProfiles;
using Seventy.Service.Core.Files;
using Seventy.Service.Core.Permissions;
using Seventy.Service.Core.RolePermissions;
using Seventy.Service.EDU.CourseGroup;
using Seventy.Service.EDU.UserCourseGroup;
using Seventy.Service.Core.Documents;
using Seventy.Service.Core.UserGroup;
using Seventy.Service.EDU.TrainingEval;
using Seventy.Repository.Core;
using Seventy.Repository.Persistence;
using Seventy.Service.Core.Message;
using Seventy.Service.Core.Roles;
using Seventy.Service.Core.Roles.DefaultRoleAccess;
using Seventy.Service.Core.UserDocument;
using Seventy.Service.Core.UserGroupMember;
using Seventy.Service.EDU.ExamAnswerSheet;
using Seventy.Service.EDU.QuestionOptions;
using Seventy.Service.EDU.TeacherLesson;
using Seventy.Service.EDU.UserTrainingWeekContent;
using Seventy.Web.Configurations;
using Seventy.Service.Core.UserAccess;
using Seventy.Service.Core.UserRole;
using Seventy.Service.EDU.Main;
using Newtonsoft.Json.Serialization;
using Seventy.Repository.Core.Repositories;
using Seventy.Repository.Persistence.Repositories;
using Seventy.Service.Core.PermissionGroup;
using Seventy.Service.Core.AccessPermissionGroup;
using Seventy.Service.Core.UserPermissionGroup;
using Seventy.WebFramework.Middlewares;
using DataTables.AspNet.AspNetCore;
using Seventy.Service.Core.MenuAccess;

namespace Seventy.Web
{

    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        [Obsolete]
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {

            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();
            Configuration = builder.Build();
            //Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.Configure<PublicConfiguration>(options => Configuration.GetSection("PublicConfiguration").Bind(options));

            //services.AddAutoMapper(typeof(ExamProfile).Assembly);

            services.AddSingleton(provider => { return Configuration; });
            services.AddDbContext<DataContext>(ServiceLifetime.Scoped);
            //services.AddTransient<IDapperContext, DapperContext>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IUserDocumentService, UserDocumentService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<IMessageSender, SMSMessenger>();
            services.AddTransient<IUserRoleService, UserRoleService>();
            services.AddTransient<IAccessRepository, AccessRepository>();
            services.AddTransient<IMenuAccessRepository, MenuAccessRepository>();


            services.AddAutoMapper(typeof(ViewModel.Core.CoreProfiles).Assembly);
            services.AddAutoMapper(typeof(EDUProfiles).Assembly);

            #region EDU Services
            services.AddTransient<IMainService, MainService>();
            services.AddTransient<IAccessService, AccessService>();
            services.AddTransient<IMenuAccessService, MenuAccessService>();
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
            //services.AddTransient<ICourseEvaluationIndexService, CourseEvaluationIndexService>();
            services.AddTransient<IExamAnswerSheetService, ExamAnswerSheetService>();
            //services.AddTransient<ICourseEvaluationResultService, CourseEvaluationResultService>();
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
            
            //services.AddTransient<ILessonEvalIndexService, LessonEvalIndexService>();
            //services.AddTransient<ILessonEvalResultService, LessonEvalResultService>();
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
            //services.AddTransient<ITeacherService, TeacherService>();
            //services.AddTransient<ITeacherCourseService, TeacherCourseService>();
            services.AddTransient<ICourseCategoryService, CourseCategoryService>();
            //services.AddTransient<ITeacherEvalIndexService, TeacherEvalIndexService>();
            //services.AddTransient<ITeacherEvalResultService, TeacherEvalResultService>();
            //services.AddTransient<ITeacherUserService, TeacherUserService>();
            services.AddTransient<ITermService, TermService>();
            services.AddTransient<ITrainingCenterService, TrainingCenterService>();
            services.AddTransient<ITrainingContentService, TrainingContentService>();
            services.AddTransient<ITrainingEvalIndexService, TrainingEvalIndexService>();
            services.AddTransient<ITrainingEvalResultService, TrainingEvalResultService>();
            services.AddTransient<IUserTrainingWeekContentService, UserTrainingWeekContentService>();

            //services.AddTransient<ITrainingContentEvalIndexService, TrainingContentEvalIndexService>();
            //services.AddTransient<ITrainingContentEvalResultService, TrainingContentEvalResultService>();
            services.AddTransient<ITrainingWeekService, TrainingWeekService>();
            services.AddTransient<ITrainingWeekContentService, TrainingWeekContentService>();
            //services.AddTransient<ITrainingWeekUserService, TrainingWeekUserService>();
            services.AddTransient<IUserContentService, UserContentService>();
            //services.AddTransient<IUserCourseService, UserCourseService>();
            services.AddTransient<IUserLessonService, UserLessonService>();
            services.AddTransient<IUserProfilesService, UserProfilesService>();
            services.AddTransient<ICourseGroupsService, CourseGroupsService>();
            services.AddTransient<ICourseGroupsService, CourseGroupsService>();
            services.AddTransient<IDocumentsService, DocumentsService>();
            services.AddTransient<IDocumentsTypeService, DocumentsTypeService>();
            #endregion

            #region Admin Services
            services.AddTransient<IRolePermissionsService, RolePermissionsService>();
            services.AddTransient<IUserAccessService, UserAccessService>();

            services.AddTransient<IPermissionGroupService, PermissionGroupService>();
            services.AddTransient<IAccessPermissionGroupService, AccessPermissionGroupService>();
            services.AddTransient<IUserPermissionGroupService, UserPermissionGroupService>();
            #endregion

            services.AddScoped<ICookieValidatorService, CookieValidatorService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INotif, NotifManager>();
            //services.AddMvc(options =>
            //{
            //    options.Filters.Add(typeof(ExceptionFilter));
            //});
            services.AddControllersWithViews()
                .AddControllersAsServices()
                .AddRazorRuntimeCompilation()
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddRazorPages();
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new FeatureLocationExpander());
            });
            services.AddAuthorization(options =>
            {
                //options.AddPolicy(CustomRoles.Admin, policy => policy.RequireRole(CustomRoles.Admin));
                options.AddPolicy("user", policy => policy.RequireRole("user"));
            });


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
           .AddCookie(options =>
           {
               options.LoginPath = "/login";
               options.LogoutPath = "/logout";
               options.ExpireTimeSpan = TimeSpan.FromDays(6);
               options.SlidingExpiration = false;
               options.AccessDeniedPath = "/logout";
               options.Cookie.Name = "video.cookie";
               options.Cookie.HttpOnly = true;
               options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
               options.Cookie.SameSite = SameSiteMode.Lax;
               options.Events = new CookieAuthenticationEvents
               {
                   OnValidatePrincipal = context =>
                   {
                       var cookieValidatorService = context.HttpContext.RequestServices.GetRequiredService<ICookieValidatorService>();
                       return cookieValidatorService.ValidateAsync(context);
                   }
               };
           });
            services.AddMvc();
            services.AddProgressiveWebApp();
            services.AddControllersWithViews();
            services.AddMemoryCache();
            services.AddAutoAccessTypes();
            services.RegisterDataTables();
        }

        [Obsolete]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    const int durationInSeconds = 60 * 60 * 168;
                    ctx.Context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.CacheControl] =
                        "public,max-age=" + durationInSeconds;
                }
            });
            app.UseSession();
            app.UseAuthentication();
            app.UseCors(
                   options => options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()
               );
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
           ForwardedHeaders.XForwardedProto
            });
            app.UseRouting();
            app.UseAuthorization();
            //app.UseUserAccessControlHandler();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "SiteMap_route",
                            pattern: "sitemap.xml",
                            defaults: new { controller = "Home", action = "Sitemap" });
                endpoints.MapControllerRoute(name: "default",
                            pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute(name: "areas", "areas", pattern: "{area:Core}/{controller=Default}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute(name: "areas", "areas", pattern: "{area:Admin}/{controller=Default}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute(name: "areas", "areas", pattern: "{area:Edu}/{controller=Default}/{action=Index}/{id?}");
            });

        }
    }
}

