using Core_App_Security.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_App_Security
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
            // DbCOntext registration for Idnetity Database
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            // ASP.NET Core 5
            // DEfuilt Action FIlter for Database Exception
            // this will be used to render the page for Applying Database Migrations
            // if the database does not exists
            services.AddDatabaseDeveloperPageExceptionFilter();
            // the servive that will provide the Identity Check for the User
            // AddDefaultIdentity<IdentotyUser>(), used to Resolve the USerManager<IdentityUser> by default. The UserManager<IdentityUser> is injected in Register.cshmtl.cs
            // The Register.cshtml and Register.cshtml.cs is provided using Microsoft.AspNetCore.Identoty.UI Package
            //  services.AddDefaultIdentity<IdentityUser>(
            //options => options.SignIn.RequireConfirmedAccount = true
            //   )// the identity will be verfied using the EF Core with SQL Server
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            // AddIdentity, this will be used for User Management as well as Role Management 
            services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI();


            // Add the Authrization Service to Support Policies
            services.AddAuthorization(options=>
            {
                options.AddPolicy("AllRolePolicy", policy=> 
                {
                    policy.RequireRole("Manager", "Clerk", "Operator");
                   // policy.RequireRole(Configuration.GetSection("AllRolePOlicy").Value);
                
                });
                options.AddPolicy("ManagerClerkPolicy", policy =>
                {
                    policy.RequireRole("Manager", "Clerk");

                });
            });


            services.AddControllersWithViews();
            // The suport for RAzor View used for Identity Customization
            // THis is Mandotory for any Customization for Identity Management
            // ASP.NET Core 5 Feature
            services.AddRazorPages(); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // ASP.NET Core 5, a middleware that will implicitely run Migrations
                // to create Identity Database
                app.UseMigrationsEndPoint();
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
            // Middlewares for security
            app.UseAuthentication(); // FOrce the HttpContext to verifiy Identity using [Authorize] attribute so that USerName and PAssword can be verified
            app.UseAuthorization();// Force the HttpContext to make sure that the RoleBAsed Authorization is verified if [Authorize] attrbute has the Roles Property set

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
