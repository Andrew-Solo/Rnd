using Discord.Interactions;
using Discord.WebSocket;
using Rnd.Bot.Discord.Sessions;
using Rnd.Bot.Discord.Views.Fields;
using Rnd.Bot.Discord.Views.Panels;
using Rnd.Constants;
using Rnd.Results;

namespace Rnd.Bot.Discord.Controllers;

public static class Extensions
{
    public static Guid? AsGuid(this string? s)
    {
        return Guid.TryParse(s, out var guid)
            ? guid
            : null;
    }
    
    public static TEnum? AsEnum<TEnum>(this string? s) where TEnum : Enum
    {
        return Enum.TryParse(typeof(TEnum), s, true, out var result) 
            ? (TEnum) result
            : default;
    }
    
    public static SocketAutocompleteInteraction AsAutocomplete(this SocketInteraction interaction)
    {
        return interaction as SocketAutocompleteInteraction ?? throw new InvalidOperationException();
    } 
    
    public static async Task CheckNotAuthorized(this InteractionModuleBase<SocketInteractionContext> controller, Session session)
    {
        if (!session.IsAuthorized) return;
        await controller.EmbedResponseAsync(PanelBuilder.WithTitle("Вы уже авторизованы").AsError());
        throw new Exception("Already authorized");
    }

    public static async Task CheckInRole(this InteractionModuleBase<SocketInteractionContext> controller, Session session, UserRole role)
    {
        await CheckAuthorized(controller, session);
        if ((int) session.Role >= (int) role) return;
        await controller.EmbedResponseAsync(PanelBuilder.WithTitle("Недостаточно прав").AsError());
        throw new Exception("No permissions");
    }
    
    public static async Task CheckAuthorized(this InteractionModuleBase<SocketInteractionContext> controller, Session session)
    {
        if (session.IsAuthorized) return;
        await controller.EmbedResponseAsync(PanelBuilder.WithTitle("Вы не авторизованы").AsError());
        throw new Exception("Not authorized");
    }
    
    public static async Task CheckResultAsync<T>(this InteractionModuleBase<SocketInteractionContext> controller, Result<T> result)
    {
        if (result.IsSuccess) return;
        await controller.EmbedResponseAsync(PanelBuilder.ByMessage(result.Message).AsError());
        throw new Exception(result.Message.Title);
    }
    
    public static async Task EmbedResponseAsync(this InteractionModuleBase<SocketInteractionContext> controller, 
        IPanel panel, bool ephemeral = true)
    {
        var interaction = controller.Context.Interaction;
        await interaction.RespondAsync(embed: panel.AsEmbed(), ephemeral: ephemeral);
    }
    
    public static async Task EmbedResponseAsync(this InteractionModuleBase<SocketInteractionContext> controller, 
        PanelBuilder panelBuilder, bool ephemeral = true)
    {
        await controller.EmbedResponseAsync(panelBuilder.Build(), ephemeral); 
    }

    public static async Task EmbedResponseAsync(this InteractionModuleBase<SocketInteractionContext> controller,
        FieldBuilder fieldBuilder, bool ephemeral = true)
    {
        await controller.EmbedResponseAsync(fieldBuilder.Build().AsPanel(), ephemeral);
    }
    
    //TODO Localization
    public static async Task EmbedResponseAsync<T>(this InteractionModuleBase<SocketInteractionContext> controller, 
        Result<T> result, string? title = null, Action? onSuccess = null, bool ephemeral = true)
    {
        await CheckResultAsync(controller, result);
        
        onSuccess?.Invoke();
        
        await EmbedResponseAsync(
            controller, 
            PanelBuilder.ByObject(result.Get(), title ?? result.Message.Title).AsSuccess(), 
            ephemeral
        );
    }
}