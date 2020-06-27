using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AM.BLL.Articles.Core;
using AM.BLL.Articles.Infrastructure;
using AM.BLL.Common.Core;
using AM.BLL.Common.Infrastructure;
using AM.BLL.Mapper;
using AM.BLL.User.Core;
using AM.BLL.User.Infrastructure;
using AM.BLL.Users.Core;
using AM.BLL.Users.Infrastructure;
using AM.DAL.Articles.Core;
using AM.DAL.Articles.Infrastructure;
using AM.DAL.User.Core;
using AM.DAL.User.Infrastructure;
using AM.DAL.Users.Core;
using AM.DAL.Users.Infrastructure;
using AM.DM.Common;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserAccessTokenClaim.Core;
using UserAccessTokenClaim.Infrastructure;

namespace ArticleManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //To access global configuration
            services.Configure<GlobalConfigModel>(Configuration.GetSection("CustomConfiguration"));

            //To Access HTTPContext from anywhere
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //AutoMapper configuration
            services.AddAutoMapper(typeof(AllMapper));

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie();

            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IUserRepository, UserRepository>();

            services.AddSingleton<IProfessionService, ProfessionService>();
            services.AddSingleton<IProfessionRepository, ProfessionRepository>();

            services.AddSingleton<IOrganizationService, OrganizationService>();
            services.AddSingleton<IOrganizationRepository, OrganizationRepository>();

            services.AddSingleton<IArticleService, ArticleService>();
            services.AddSingleton<IArticleRepository, ArticleRepository>();

            services.AddSingleton<IUserAccessTokenClaimsService, UserAccessTokenClaimsService>();

            services.AddSingleton<IEmailHandlerService, EmailHandlerService>();

            services.AddSingleton<IGlobalConfigurationService, GlobalConfigurationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();


            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
