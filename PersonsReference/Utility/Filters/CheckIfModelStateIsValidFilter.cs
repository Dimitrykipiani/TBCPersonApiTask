using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsReference.Utility.Filters
{
    public class CheckIfModelStateIsValidFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if(!context.ModelState.IsValid)
            {
                context.Result = new ContentResult()
                {
                    Content = "Bad Request",
                    StatusCode = 400
                };
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
