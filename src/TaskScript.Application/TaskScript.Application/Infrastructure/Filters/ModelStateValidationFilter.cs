namespace TaskScript.Application.Infrastructure.Filters
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    using TaskScript.Application.Constants;

    public class ModelStateValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid == false)
            {
                context.ActionDescriptor.RouteValues.TryGetValue("action", out string action);

                Controller executingController = context.Controller as Controller;

                foreach (var value in context.ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        executingController.TempData[NotificationsConstants.ErrorNotification] = error.ErrorMessage;
                    }
                }

                context.Result = executingController.RedirectToAction(action);
            }
        }
    }
}
