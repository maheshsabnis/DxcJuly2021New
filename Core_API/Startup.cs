using Core_API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
 
using Core_API.Services;
using System.Threading.Tasks;
using Core_API.CustomMiddlewares;

namespace Core_API
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
            services.AddDbContext<CompanyContext>(options=> {
                options.UseSqlServer(Configuration.GetConnectionString("AppConnection"));
            });

            // Adding the CORS Service Policy
            services.AddCors(options=> {
                options.AddPolicy("cors", policy => policy.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });


            services.AddScoped<IService<Department, int>, DepartmentService>();
            services.AddScoped<IService<Employee, int>, EmployeeService>();


            // The Request will be accepted for API Controllers
            // Adding the JSON Serialization by supressing the default Serialization Type
            services.AddControllers()
                    .AddJsonOptions(options=> 
                    {
                        options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    });
            // The Service added for current API Project to generate OpenAPI Spcification 3.0 Documentation
            services.AddSwaggerGen(c =>
            {
                // generate the JSOn Documentation
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Core_API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // The HttpContext knows that the JSON Documnetation must be resonded to the client for Swagger request
                app.UseSwagger();
                // Render the Default UI for Testing API with all of its methods 
                // with Request type Model and Response Types
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core_API v1"));
            }

            //Adding the CORS Middleware
            app.UseCors("cors");

            app.UseRouting();

            app.UseAuthorization();

            // Register all Custom Middlewares
            app.UseCustomExceptionMiddleware();


            app.UseEndpoints(endpoints =>
            {
                // Map the incomming Http Request to API Controllers
                endpoints.MapControllers();
            });
        }
    }
}
