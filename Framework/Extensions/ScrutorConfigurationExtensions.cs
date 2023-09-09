using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SqlRepository;

namespace Framework.Extensions;

public static class ScrutorConfigurationExtensions
{
    public static void AddServices(this IServiceCollection service)
    {
        service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        
        service.Scan(
            scan => scan
                .FromAssembliesOf(typeof(ICore), typeof(IFramework))
                .AddClasses(classes => classes.AssignableTo<IScopedDependency>())
                .AsSelfWithInterfaces()
                .WithScopedLifetime()
        );

        service.Scan(
            scan => scan
                .FromAssembliesOf(typeof(ICore), typeof(IFramework))
                .AddClasses(classes => classes.AssignableTo<ITransientDependency>())
                .AsSelfWithInterfaces()
                .WithTransientLifetime()
        );

        service.Scan(
            scan => scan
                .FromAssembliesOf(typeof(ICore), typeof(IFramework))
                .AddClasses(classes => classes.AssignableTo<ISingletonDependency>())
                .AsSelfWithInterfaces()
                .WithSingletonLifetime()
        );
        
        service.Scan(
            scan => scan
                .FromAssemblyOf<ISqlRepository>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("SqlRepository")))
                .AsSelf()
                .WithScopedLifetime()
        );
    }
}