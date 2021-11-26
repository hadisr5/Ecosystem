//using System;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.HttpOverrides;
//using Microsoft.AspNetCore.Mvc.Razor;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Seventy.Web.StartupCustomizations;
//using Seventy.Web.StartupCustomizations.CookieValidat;
//using Seventy.Web.Configurations;
//using Seventy.WebFramework.Configuration;
//using Microsoft.Extensions.Hosting;
//using Seventy.DomainClass;

//namespace Seventy.Web
//{

//    public class Startup
//    {
//        public IConfigurationRoot Configuration { get; }

//        public Startup(IConfiguration configuration, IWebHostEnvironment env)
//        {

//            var builder = new ConfigurationBuilder()
//               .SetBasePath(env.ContentRootPath)
//               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
//               .AddEnvironmentVariables();
//            Configuration = builder.Build();
//            //Configuration = configuration;
//        }

//        public void ConfigureServices(IServiceCollection services)
//        {
//            services.Configure<CookiePolicyOptions>(options =>
//            {
//                options.CheckConsentNeeded = context => true;
//                options.MinimumSameSitePolicy = SameSiteMode.None;
//            });
//            services.Configure<PublicConfiguration>(options => Configuration.GetSection("PublicConfiguration").Bind(options));

//            //services.AddAutoMapper(typeof(ExamProfile).Assembly);

//            services.AddSingleton(provider => { return Configuration; });

//            services.AddScoped<ICookieValidatorService, CookieValidatorService>();

//            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//            services.AddRazorPages().AddRazorRuntimeCompilation();

//            services.AddSession(options =>
//            {
//                options.IdleTimeout = TimeSpan.FromMinutes(20);
//                options.Cookie.HttpOnly = true;
//                options.Cookie.IsEssential = true;
//            });

//            services.Configure<RazorViewEngineOptions>(options =>
//            {
//                options.ViewLocationExpanders.Add(new FeatureLocationExpander());
//            });

//            services.AddAuthorization(options =>
//            {
//                options.AddPolicy("user", policy => policy.RequireRole("user"));
//            });

//            services.AddAuthentication(options =>
//            {
//                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//            })
//           .AddCookie(options =>
//           {
//               options.LoginPath = "/login";
//               options.LogoutPath = "/logout";
//               options.ExpireTimeSpan = TimeSpan.FromDays(6);
//               options.SlidingExpiration = false;
//               options.AccessDeniedPath = "/logout";
//               options.Cookie.Name = "video.cookie";
//               options.Cookie.HttpOnly = true;
//               options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
//               options.Cookie.SameSite = SameSiteMode.Lax;
//               options.Events = new CookieAuthenticationEvents
//               {
//                   OnValidatePrincipal = context =>
//                   {
//                       var cookieValidatorService = context.HttpContext.RequestServices.GetRequiredService<ICookieValidatorService>();
//                       return cookieValidatorService.ValidateAsync(context);
//                   }
//               };
//           });

//            services.AddMvc();

//            services.AddProgressiveWebApp();

//            services.AddControllersWithViews();

//            services.AddMemoryCache();

//            services.AddRepositoryDependencies();
            
//            services.AddServiceDependencies();
            
//            services.AddAutoMapperMap();
            
//            services.AddDbContext(Configuration);

//            services.AddMinimalMvc();

//            services.AddAutoAccessTypes();
//        }

//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }

//            app.UseStaticFiles(new StaticFileOptions
//            {
//                OnPrepareResponse = ctx =>
//                {
//                    const int durationInSeconds = 60 * 60 * 168;
//                    ctx.Context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.CacheControl] =
//                        "public,max-age=" + durationInSeconds;
//                }
//            });
            
//            app.UseSession();
            
//            app.UseAuthentication();
            
//            app.UseCors(options => options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

//            app.UseForwardedHeaders(new ForwardedHeadersOptions
//            {
//                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
//            });

//            app.UseRouting();

//            app.UseAuthorization();

//            //app.UseUserAccessControlHandler();

//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapControllerRoute(name: "SiteMap_route",
//                            pattern: "sitemap.xml",
//                            defaults: new { controller = "Home", action = "Sitemap" });
//                endpoints.MapControllerRoute(name: "default",
//                            pattern: "{controller=Home}/{action=Index}/{id?}");
//                endpoints.MapAreaControllerRoute(name: "areas", "areas", pattern: "{area:Core}/{controller=Default}/{action=Index}/{id?}");
//                endpoints.MapAreaControllerRoute(name: "areas", "areas", pattern: "{area:Admin}/{controller=Default}/{action=Index}/{id?}");
//                endpoints.MapAreaControllerRoute(name: "areas", "areas", pattern: "{area:Edu}/{controller=Default}/{action=Index}/{id?}");
//            });

//        }
//    }
//}

