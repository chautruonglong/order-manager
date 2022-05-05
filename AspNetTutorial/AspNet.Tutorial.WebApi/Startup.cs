using System;

using AspNet.Tutorial.Core;
using AspNet.Tutorial.Infrastructure.Contexts;
using AspNet.Tutorial.WebApi.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AspNet.Tutorial.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCors", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            services.AddFilters();

            services.OverrideModelFailedValidation();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "AspNet.Tutorial.WebApi", Version = "v1"});
            });

            services.AddAutoMapper(typeof(CoreAssembly));

            services.AddDbContext<AspNetTutorialContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AspNetTutorial"));
            }, ServiceLifetime.Singleton);

            services.ScanBeans();

            services.AddResponseCaching();

            services.AddJwtBearer(Configuration);

            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = Int32.MaxValue;
                options.MultipartBodyLengthLimit = Int32.MaxValue;
                options.MemoryBufferThreshold = Int32.MaxValue;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AspNet.Tutorial.WebApi v1"));
            }

            app.UseCors("EnableCors");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseResponseCaching();

            app.UseExternalStaticFiles(Configuration["StaticPath"]);
        }
    }
}
