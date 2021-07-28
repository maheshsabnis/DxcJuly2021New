using Core_MVC.Data;
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
using Core_MVC.Models;
using Core_MVC.Services;
using Core_MVC.CustomFilters;
namespace Core_MVC
{
	/// <summary>
	/// Invoked using IHostBuilder contract inside the Kestral Host
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// Invoked using UseStartup() method, and it will inject the IConfiguration 
		/// IConfiguration: Read appsettings.json for all application level settings
		/// These settings will be used by the Application at various places
		/// </summary>
		/// <param name="configuration"></param>
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// THis method will be immediately invoked after the Ctor and the IServiceCollection interface wll be passed to it 
		// IServiceCollection: USed to Discover application dependencies (?) and register it as services in current Host
		// with its Lifecycle (aka Scope) (?)
		// The 'ServiceDescriptor' class is used by this interface to discover and load the expernal dependencies. 
		// THis class uses 'ServiceLifetime' enum for defining the Scope of the application
		// Singleton: The object registered as Singleton will be having application wide scope
		// Scoped: The Statefull registration for object, the instance will be created once for every new session
		// Transient: The stateless registration, the object will be instantiated for each new request
		// IServiceCollection alsp manage the Resources in Request e.g. Using Request for Razor Pages, MVC Controllers and Views, API Controllers
		// Additional Services can be configured as
		// 1. Authentication and Authorization
		// 2. Sessions
		// 3. Caching
		// 4. Custom Business Services
		// 5. RAzor Pages Resources
		// 6. API Controller Resources
		// 7. DbContext for the EF Core that will be used for connecting to the Application to Database Server
		public void ConfigureServices(IServiceCollection services)
		{
			// the method for registering the DbContext object for Security for the current application
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));

			// Register the DbCOntext for Accessing the Database for the Application
			// DbContextOptions: Read the DbContext object (and hence the DbConnection) from
			// the DI Container and make it ready for Performing Transactions
			services.AddDbContext<CompanyContext>(options=> {
				options.UseSqlServer(Configuration.GetConnectionString("AppConnection"));
			  
			});

			 
			// ASP.NET Core 5
			// The defauilt Action Filter that will be execurted if the database connection failed or any ther
			// // database error occures. THis will render a default database error page
			services.AddDatabaseDeveloperPageExceptionFilter();

			// The default  Identity Support for rendering Identity Pages having the internal database 
			// using  EF Core with ApplicationDbContext class
			// This will use AspNetUser table for User BAsed identity Management 
			services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddEntityFrameworkStores<ApplicationDbContext>();

			// Register the Custom Services
			services.AddScoped<IService<Department,int>,DepartmentService>();
			services.AddScoped<IService<Employee, int>, EmployeeService>();

			// Initialize the Session State
			services.AddDistributedMemoryCache(); // Enable the Distributed Memory cachhe for the Session
			services.AddSession(options=> {
				options.IdleTimeout = TimeSpan.FromMinutes(20); // The Sesison Timeout
			});



			// The Resource Processing for MVC and API Controllers and MVC Views
			// Applying the Action Filter Globally
			services.AddControllersWithViews(options=> {
				options.Filters.Add(new RequestLogFilterAttribute());
				// Resolve the IModelMetadataProvider
				options.Filters.Add(typeof(CustomExceptionFilterAttribute));
			   
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		// The method to configure Middlewares replacement of HttpModules and HttpHandlers
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				// Standard Developer Exception Page with Stack Trace
				app.UseDeveloperExceptionPage();
				// Provide a Database Migration Page (typscally used in Identity)
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
			// Comnfoigure the HttpContext to use the Session Middleware so that
			// Read/Write operations on Cache for Session State will be carriedout
			app.UseSession();
			app.UseAuthentication();
			app.UseAuthorization();

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
