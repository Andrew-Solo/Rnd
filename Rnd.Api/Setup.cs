using System.Drawing;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Rnd.Api.Client.Models.Basic.Game;
using Rnd.Api.Client.Models.Basic.Member;
using Rnd.Api.Data.Entities;
using Rnd.Api.Helpers;
using Rnd.Api.Localization;
using Rnd.Api.Client.Models.Basic.User;
using Rnd.Api.Modules.Basic.Members;
using Rnd.Api.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Member = Rnd.Api.Data.Entities.Member;

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
        
        config.CreateMap<Member, MemberModel>();
        config.CreateMap<MemberFormModel, Modules.Basic.Members.Member>()
            .ForMember(u => u.Role, c =>
            {
                c.Condition(u => u.Role != null);
                c.MapFrom(u => JsonConvert.DeserializeObject<MemberRole>(u.Role ?? ""));
            })
            .ForMember(u => u.Nickname, c => c.Condition(u => u.Nickname != null))
            .ForMember(u => u.Color, c =>
            {
                c.Condition(u => u.ColorHex != null);
                c.MapFrom(u => ColorTranslator.FromHtml(u.ColorHex ?? ""));
            });
        config.CreateMap<MemberFormModel, Member>()
            .ForMember(u => u.Role, c =>
            {
                c.Condition(u => u.Role != null);
                c.MapFrom(u => JsonConvert.DeserializeObject<MemberRole>(u.Role ?? ""));
            })
            .ForMember(u => u.Nickname, c => c.Condition(u => u.Nickname != null))
            .ForMember(u => u.ColorHex, c => c.Condition(u => u.ColorHex != null));
    }

    private static WebApplicationBuilder? _builder;
}