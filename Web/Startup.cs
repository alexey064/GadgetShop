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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Web;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Logging;

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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                            
                            // указывает, будет ли валидироваться издатель при валидации токена
                            ValidateIssuer = true,
                            // строка, представляющая издателя
                            ValidIssuer = AuthOptions.ISSUER,
                            // будет ли валидироваться потребитель токена
                            ValidateAudience = true,
                            // установка потребителя токена
                            ValidAudience = AuthOptions.AUDIENCE,
                            // будет ли валидироваться время существования
                            ValidateLifetime = true,
                            // установка ключа безопасности
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            // валидация ключа безопасности
                            ValidateIssuerSigningKey = true,
                        };
                    });
            services.AddAuthorization();
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
                    //opt.LoginPath = "/shop/main";
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
            IdentityModelEventSource.ShowPII = true;
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "Api", pattern: "{controller}/{action}", constraints: new { Controller = "api" });
                endpoints.MapControllerRoute(name: "Search", pattern: "shop/search/{type}/{Page?}");
                endpoints.MapControllerRoute(name: "Catalog", pattern: "shop/catalog/{type}/{Page?}");
                endpoints.MapControllerRoute(name: "item", pattern: "/{controller}/{action}/{Table}/{Page?}", constraints: new {Controller="Simple", action= "ItemList" });
                endpoints.MapControllerRoute(name: "List", pattern: "{controller}/{action}/{Page}");
                endpoints.MapControllerRoute(name: "Default", pattern: "/{controller=Shop}/{action=main}");
            });
        }
    }
}