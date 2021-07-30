using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core_API.CustomMiddlewares
{
    public class ErrorDescription
    {
        public int StatucCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// Class will be injected with RequestDelegate
    /// </summary>
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Write the logic for the middleware
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // if there is no exception, continue the execution with next niddleware
                await _next(context);
            }
            catch (Exception ex)
            {
                // set the status code
                context.Response.StatusCode = 500;
                // read the exception message
                string error = ex.Message;
                var errorData = new ErrorDescription()
                {
                    StatucCode = context.Response.StatusCode,
                    ErrorMessage = error
                };

                // write the error message by writing it in Http Response
                string errorResponse = JsonSerializer.Serialize(errorData);
                await context.Response.WriteAsync(errorResponse);
            }
        }
    }

    // create a class that will be anm extension  class to regiter the Custom Middleare in Pipeline
    /// <summary>
    /// UseMiddleware<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicMethods)] TMiddleware>), the T is a type (class), that is constructor  injected with RequestDelegate delegate
    /// DynamicallyAccessedMembers, will load the class using Reflectiona and read its public ctors and expose its
    /// public method, so that they can be used in HTTP Pipeline
    /// </summary>
    public static class CustomMiddlewaresInfra
    {
        public static void UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
