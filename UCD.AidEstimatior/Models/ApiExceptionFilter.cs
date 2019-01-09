using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Net;

namespace Web.Models
{
    public class ApiExceptionFilter : ExceptionFilterAttribute, IExceptionFilter
    {
        public override void OnException(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType();
            // only wrap the excpetion in json if the request was an API (json) request
            if (context.HttpContext.Request.GetTypedHeaders().Accept.Any(header => header.MediaType == "application/json") || context.HttpContext.Request.Path.Value.IndexOf("/api/", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                HttpStatusCode status = HttpStatusCode.InternalServerError;
                String message = String.Empty;

                if (exceptionType == typeof(UnauthorizedAccessException))
                {
                    message = "Unauthorized Access";
                    status = HttpStatusCode.Unauthorized;
                }
                if (exceptionType == typeof(AccessViolationException))
                {
                    message = "Access Denied";
                    status = HttpStatusCode.Forbidden;
                }
                else if (exceptionType == typeof(NotImplementedException))
                {
                    message = "A server error occurred.";
                    status = HttpStatusCode.NotImplemented;
                }
                //else if (exceptionType == typeof(MyAppException))
                //{
                //	message = context.Exception.ToString();
                //	status = HttpStatusCode.InternalServerError;
                //}
                else
                {
                    message = context.Exception.Message;
                }

                context.HttpContext.Response.StatusCode = (int)status;
                context.Result = new JsonResult(new
                {
                    Code = status,
                    Message = message
#if DEBUG
                    ,
                    details = context.Exception.StackTrace
#endif
                });

            }
        }
    }
}