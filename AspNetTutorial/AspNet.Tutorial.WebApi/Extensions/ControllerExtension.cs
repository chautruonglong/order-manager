using System.Collections.Generic;
using System.Net;

using AspNet.Tutorial.Core.DataTransfers.Commons;
using AspNet.Tutorial.WebApi.Filters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace AspNet.Tutorial.WebApi.Extensions
{
    public static class ControllerExtension
    {
        public static void AddFilters(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            });
        }

        public static void OverrideModelFailedValidation(this IServiceCollection services)
        {
            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        List<string> messages = new();

                        foreach (ModelStateEntry modelStateValue in context.ModelState.Values)
                        {
                            foreach (ModelError modelError in modelStateValue.Errors)
                            {
                                messages.Add(modelError.ErrorMessage);
                            }
                        }

                        return new BadRequestObjectResult(new HttpResponseDto {Message = messages, Status = (int)HttpStatusCode.BadRequest, Type = context.ModelState.GetType().FullName});
                    };
                });
        }
    }
}
