using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_MVC.CustomFilters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IModelMetadataProvider modelMetadataProvider;

        /// <summary>
        /// Inject the IModelMetyadataProvider to ctor. This will be resolved using 
        /// AddControllersWithViews() method in ConfigureServices() method using MvcOptions
        /// </summary>
        public CustomExceptionFilterAttribute(IModelMetadataProvider modelMetadataProvider)
        {
            this.modelMetadataProvider = modelMetadataProvider;
        }
        /// <summary>
        /// Handle the excpetion
        /// Read the Exception Message from Exception Object
        /// Set the Result as the error response, e.g. View, JSON, etc.
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            // 1. Handle the exception, so that the request processing will stop
            context.ExceptionHandled = true;
            // 2. Read the error message
            string errorMessage = context.Exception.Message;
            // 3. set the result as ViewResult object
            ViewResult viewResult = new ViewResult();
            // 3a. Set the View Name
            viewResult.ViewName = "Error";

            // Since the 'Model' property of the ViewResult is ReadOnly we cannot pass the 
            // Model to it. So to pass the data of execption to view lets use ViewDataDictionary
            // The ViewDataDictionay accepts FIrst Parameter as IModelMetadataProvider and Second Parameter as
            // ModelStateDicyionary. The second parameter is provided using ExceptionContext.
            // For the first parametyer use the AddControllersWithViews() method in ConfigureServices()
            // Method for MvcOptions to resolve the IModelMetadataProvider
            ViewDataDictionary dictionary = new ViewDataDictionary(modelMetadataProvider,context.ModelState);
            dictionary["ControllerName"] = context.RouteData.Values["controller"].ToString();
            dictionary["ActionName"] = context.RouteData.Values["action"].ToString();
            dictionary["ErrorMessage"] = errorMessage;
            viewResult.ViewData = dictionary;

            context.Result = viewResult;

        }
    }
}
