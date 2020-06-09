using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AM.BLL.Mapper;
using AM.BLL.User.Core;
using AM.BLL.User.Infrastructure;
using AM.BLL.Users.Core;
using AM.BLL.Users.Infrastructure;
using AM.DAL.User.Core;
using AM.DAL.User.Infrastructure;
using AM.DAL.Users.Core;
using AM.DAL.Users.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


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
            //AutoMapper configuration
            services.AddAutoMapper(typeof(AllMapper));

            services.AddControllersWithViews().AddRazorRuntimeCompilation();


            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IUserRepository, UserRepository>();

            services.AddSingleton<IProfessionService, ProfessionService>();
            services.AddSingleton<IProfessionRepository, ProfessionRepository>();

            services.AddSingleton<IOrganizationService, OrganizationService>();
            services.AddSingleton<IOrganizationRepository, OrganizationRepository>();
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
