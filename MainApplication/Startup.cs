using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Email;
using Common.Identity;
using Data;
using Data.Core;
using Data.Persistence;
using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using reCAPTCHA.AspNetCore;

namespace UltimateAspCoreProject
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            //Configuration = configuration;

            var builder = new ConfigurationBuilder();
            builder.SetBasePath(env.ContentRootPath);
            //builder.AddJsonFile("identitysettings.json", optional: false, reloadOnChange: false);
            //builder.AddJsonFile("emailsettings.json", optional: false, reloadOnChange: false);
            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            //builder.AddUserSecrets<Program>();
            Configuration = builder.Build();

            //static configs
            //builder = new ConfigurationBuilder();
            //builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); 
            //StaticConfiguration = builder.Build();
        }

        public IConfiguration Configuration { get; }
        //public static IConfiguration StaticConfiguration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.Configure<IdentityDefaultOptions>(Configuration.GetSection("IdentityProperties"));
            services.Configure<EmailSenderOptions>(Configuration.GetSection("EmailProperties"));
            services.AddRecaptcha(Configuration.GetSection("RecaptchaSettings"));

            #region Identity configurations 
            //(can be separated in IdentityHostingStartup)
            services.AddDbContext<ApplicationContext>(options =>
                  options.UseSqlServer(
                      Configuration.GetConnectionString("ApplicationContextConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;

                // Password configurations note: must change DataValidations for ViewModels
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 0;

                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = true;

                //Lockout settings.
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 5;

            })
                .AddRoles<IdentityRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddUserManager<CustomUserManager>(); //CustomUserManager class Inherited From UserManger<ApplicationUser> remove if not necessary 

            //external auth
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    IConfigurationSection googleAuthNSection =
                        Configuration.GetSection("Authentication:Google");

                    options.ClientId = googleAuthNSection["ClientId"];
                    options.ClientSecret = googleAuthNSection["ClientSecret"];
                });

            services.AddAuthorization(x =>
            {
                x.AddPolicy("AdministratorPolicy", y => y.RequireRole("Administrators"));
                x.AddPolicy("ContentManagerPolicy", y => y.RequireRole("ContentManagers"));
            });

            #endregion

            #region Cookie
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";

                //options.Cookie.HttpOnly = true;
                //options.ExpireTimeSpan = TimeSpan.FromDays(500);
                //options.SlidingExpiration = true;
            });
            #endregion

            services.AddTransient<IEmailSender, EmailSenderService>(); // requires using Microsoft.AspNetCore.Identity.UI.Services; using WebPWrecover.Services;

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider,
            IOptions<IdentityDefaultOptions> identityPropertiesOptions)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage(); //added
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); //enabling Identity
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages(); //required for identity
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //SeedIdentity.Initialize(serviceProvider, identityPropertiesOptions.Value).Wait(); //creating roles and admin acc //already used in dashboard
        }
    }
}
