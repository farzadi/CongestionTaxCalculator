using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Framework.Extensions.Swagger;

public static class SwaggerConfigurationExtensions
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();

            _addDoc(options, 1);
            //set version "api/v{version}/[controller]" from current swagger doc verion
            options.DocumentFilter<SetVersionInPaths>();

            
        });
    }
    
    private static void _addDoc(SwaggerGenOptions options, int version)
    {
        var v = $"v{version}";
        var appTitle = $"WebApi {v}";
        options.SwaggerDoc(v, new() { Title = appTitle, Version = v });
    }
}