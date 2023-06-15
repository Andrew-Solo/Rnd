using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Exceptions;
using Rnd.Api.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Rnd.Api;
 
public static class Setup
{
    public static WebApplicationBuilder Builder
    {
        get => _builder ?? throw new NotInitializedException(nameof(WebApplicationBuilder));
        set => _builder = value;
    }
    
    public static void Swagger(SwaggerGenOptions options)
    {
        options.OperationFilter<OptionalPathParameterFilter>();
        options.SchemaFilter<EnumSchemaFilter>();
        //TODO
        // options.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}\bin\SwaggerDemoApi.XML");
    }

    public static void DataContext(DbContextOptionsBuilder options)
    {
        options
            .UseNpgsql(Builder.Configuration.GetConnectionString("Default"))
            .UseLazyLoadingProxies();
    }

    public const string CorsPolicy = nameof(CorsPolicy);
    
    public static void Cors(CorsOptions options)
    {
        // Client address to config
        options.AddPolicy(name: CorsPolicy, policy => policy.WithOrigins("http://localhost:3000"));
    }

    private static WebApplicationBuilder? _builder;
}