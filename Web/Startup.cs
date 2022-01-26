using Diplom.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Diplom.Models.EF;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Data.Common;

namespace Diplom
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
            services.AddControllersWithViews();
            services.AddDistributedMemoryCache();
            services.AddRazorPages();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(1000);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
            opt =>
                {
                    //configure your other properties
                    opt.LoginPath = "/shop/main";
                });
            
            services.ConfigureApplicationCookie(options => options.LoginPath = "/shop/Main");
            services.AddDbContext<ShopContext>(p => p.UseSqlServer("server=LAPTOP-09UR5JLB;Database=Shop;integrated security=true;"));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();
            services.AddDbContext<IdentityContext>(opt => opt.UseSqlServer("server=LAPTOP-09UR5JLB;Database=ShopIdentity;integrated security=true;"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "Search", pattern: "shop/search/{type}/{Page?}");
                endpoints.MapControllerRoute(name: "Catalog", pattern: "shop/catalog/{type}/{Page?}");
                endpoints.MapControllerRoute(name: "item", pattern: "/{controller}/{action}/{Table}/{Page?}", constraints: new {Controller="Simple", action= "ItemList" });
                endpoints.MapControllerRoute(name: "List", pattern: "{controller}/{action}/{Page}");
                endpoints.MapControllerRoute(name: "Default", pattern: "/{controller=Shop}/{action=main}");
            });
        }
    }
}