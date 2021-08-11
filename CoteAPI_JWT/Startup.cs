using CoteAPI_JWT.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
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
using System.Threading.Tasks;
using CoteAPI_JWT.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CoteAPI_JWT
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
            services.AddDbContext<JwtSecurityDbContext>(options=> 
            {
               options.UseSqlServer(Configuration.GetConnectionString("SecurityDXCDbContext"));
            });

            // Adding Identity Service
            services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<JwtSecurityDbContext>();

            // Register CORS
            services.AddCors(options=>
            {
                options.AddPolicy("cors", policy=> 
                {
                    policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                });
            });

            // Service for Token Validation
            // Read the Key from appsettings.json for validation
            var key = Convert.FromBase64String(Configuration["JWTSettings:SecretKey"]);

            // Register JwtAuthService
            services.AddScoped<JwtAuthService>();
            // Add the Authentication configuration to inform the Host that the Token is expected
            // and must be validated

            services.AddAuthentication(options=> 
            {
                // The Htytp Request MUST contains the Hearer as
                // AUTHROIZATION: 'Bearer [Token-Value]'
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                // Read Authorization Value from HTTP Request Headers and Verify the token
                .AddJwtBearer(options=> {
                    options.SaveToken = true; // Token in Host Process
                    options.RequireHttpsMetadata = false; // Token from http scheme will also be read
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true, // use the key to validate the token
                        IssuerSigningKey = new SymmetricSecurityKey(key), // use the key to verify
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                    
                }); 

            services.AddControllers()
                .AddJsonOptions(options=> {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoteAPI_JWT", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoteAPI_JWT v1"));
            }
            app.UseCors("cors");

            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication(); // Mandartory for USer and Token Autrhentication
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
