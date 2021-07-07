using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalRChat.Hubs;
using SignalRChat.Initialization;
using VueCliMiddleware;

namespace SignalRChat
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllersWithViews();
            services.AddJwtAuthentication();
            services.AddSignalR();
            services.AddMvc(options =>{
                options.EnableEndpointRouting = false;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSignalR(options =>
            {
                options.MapHub<ChatHub>("/chatHub");
            });

            if (env.IsDevelopment())
            {
                app.UseMvc(routes =>
                {
                    routes.MapRoute("all", "api/{*url}", new { controller = "Home", action = "Index" });
                });
            }

            else
            {
                app.UseMvc(routes =>
                {
                    routes.MapRoute("all", "{*url}", new { controller = "Home", action = "Index" });
                });

            }
            if (env.IsDevelopment())
            {
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapToVueCliProxy(
                       new SpaOptions { SourcePath = "ClientApp" },
                       npmScript: "serve",
                       forceKill: true
                       );
                });
            }
            

        }
    }
}
