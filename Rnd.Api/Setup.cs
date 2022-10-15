using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Client.Models.Basic.Game;
using Rnd.Api.Data.Entities;
using Rnd.Api.Helpers;
using Rnd.Api.Localization;
using Rnd.Api.Client.Models.Basic.User;
using Rnd.Api.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Rnd.Api;

public static class Setup
{
    public static WebApplicationBuilder Builder
    {
        get => _builder ?? throw new NullReferenceException(Lang.Exceptions.NotInitialized);
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
        config.CreateMap<UserFormModel, User>()
            .ForMember(u => u.Email, c => c.Condition(u => u.Email != null))
            .ForMember(u => u.Login, c => c.Condition(u => u.Login != null))
            .ForMember(u => u.PasswordHash, c =>
            {
                c.Condition(u => u.Password != null);
                c.MapFrom(u => Hash.GenerateStringHash(u.Password ?? ""));
            });
        
        config.CreateMap<Game, GameModel>();
        config.CreateMap<GameFormModel, Game>()
            .ForMember(u => u.Name, c => c.Condition(u => u.Name != null))
            .ForMember(u => u.Title, c => c.Condition(u => u.Title != null))
            .ForMember(u => u.Description, c => c.Condition(u => u.Description != null));
    }

    private static WebApplicationBuilder? _builder;
}