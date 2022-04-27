using DemoCustomerAdaptApi.Extensions;
using DemoCustomerAdaptApi.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoCustomerAdaptApi.Filters
{
    public class ApiActionResultFilterAttribute<T> : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
                return;

            if (!(context.Result is ObjectResult objectResult))
                return;

            context.Result = objectResult.Value switch
            {
                null => ApiActionResult<T>.MakeSuccess(default, null).ToActionResult(),
                T data => ApiActionResult<T>.MakeSuccess(data, null).ToActionResult(),
                _ => context.Result
            };
        }
    }
}