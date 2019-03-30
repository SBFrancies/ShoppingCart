using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ShoppingCartApi.Api.Filters
{
    public class GeneralExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            try
            {
                context.ExceptionHandled = true;
                context.Result =
                    new ObjectResult(context.Exception) {StatusCode = (int) HttpStatusCode.InternalServerError};
            }

            catch
            {

            }
        }
    }
}
