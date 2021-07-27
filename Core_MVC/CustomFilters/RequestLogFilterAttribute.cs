using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Core_MVC.CustomFilters
{
    public class RequestLogFilterAttribute : ActionFilterAttribute
    {
        private void LogHelper(RouteData route, string state)
        {
            string controllerName = route.Values["controller"].ToString();
            string actionName = route.Values["action"].ToString();
            string message = $"Current Execution State is {state} in controller {controllerName} in action {actionName}";

            Debug.WriteLine(message);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            LogHelper(context.RouteData, "On Action Executing");
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            LogHelper(context.RouteData, "On Action Executed");
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            LogHelper(context.RouteData, "On Result Executing");
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            LogHelper(context.RouteData, "On Result Executed");
        }
    }
}
