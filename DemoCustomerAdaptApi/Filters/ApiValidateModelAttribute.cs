using DemoCustomerAdaptApi.Extensions;
using DemoCustomerAdaptApi.Models;
using DemoCustomerAdaptApi.Results;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoCustomerAdaptApi.Filters
{
    public class ApiValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid) return;

            context.Result
                = ApiActionResult<ValidationResult>
                    .MakeFailed(new ValidationResult(context.ModelState), null, "Validation failed")
                    .ToActionResult();

            base.OnActionExecuting(context);
        }
    }
}