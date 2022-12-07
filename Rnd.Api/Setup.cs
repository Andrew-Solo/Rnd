using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Rnd.Api.Client.Models.Basic.Game;
using Rnd.Api.Client.Models.Basic.Member;
using Rnd.Api.Client.Models.Basic.User;
using Rnd.Api.Data.Entities;
using Rnd.Api.Exceptions;
using Rnd.Api.Helpers;
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

    public static void Automapper(IMapperConfigurationExpression config)
    {
        config.CreateMap<User, UserModel>();
        config.CreateMap<Game, GameModel>();
        config.CreateMap<Member, MemberModel>();
    }

    private static WebApplicationBuilder? _builder;
}