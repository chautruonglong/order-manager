using AspNet.Tutorial.Core;
using AspNet.Tutorial.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

using Scrutor;

namespace AspNet.Tutorial.WebApi.Extensions
{
    public static class BeanScannerExtension
    {
        public static void ScanBeans(this IServiceCollection services)
        {
            services.Scan(scan =>
            {
                scan
                    .FromAssembliesOf(typeof(CoreAssembly), typeof(InfrastructureAssembly))
                    .AddClasses()
                    .AsImplementedInterfaces()
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsSelf()
                    .WithScopedLifetime();
            });
        }
    }
}
