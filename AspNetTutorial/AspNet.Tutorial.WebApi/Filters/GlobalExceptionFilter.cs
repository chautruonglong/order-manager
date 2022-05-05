using System;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;

using AspNet.Tutorial.Core.DataTransfers.Commons;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNet.Tutorial.WebApi.Filters
{
    public class GlobalExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            Exception exception = context.Exception;

            if (exception is AuthenticationException)
            {
                context.Result = new UnauthorizedObjectResult(new HttpResponseDto {Status = (int)HttpStatusCode.Unauthorized, Message = exception.Message, Type = exception.GetType().FullName});
            }
            else
            {
                context.Result = new BadRequestObjectResult(new HttpResponseDto {Status = (int)HttpStatusCode.BadRequest, Message = exception.Message, Type = exception.GetType().FullName});
            }

            context.ExceptionHandled = true;

            return Task.CompletedTask;
        }
    }
}
