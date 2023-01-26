using Discord.Interactions;
using Discord.WebSocket;
using Rnd.Bot.Discord.Sessions;
using Rnd.Bot.Discord.Views.Fields;
using Rnd.Bot.Discord.Views.Panels;
using Rnd.Result;

namespace Rnd.Bot.Discord.Controllers;

public static class Extensions
{
    public static Guid? AsGuidOrNull(this string? s)
    {
        return Guid.TryParse(s, out var guid)
            ? guid
            : null;
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
        throw new Exception(result.Message.Header);
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
    
    public static async Task EmbedResponseAsync<T>(this InteractionModuleBase<SocketInteractionContext> controller, 
        Result<T> result, string? header = null, bool ephemeral = true)
    {
        await CheckResultAsync(controller, result);
        await EmbedResponseAsync(controller, PanelBuilder.WithTitle(header ?? result.Message.Header ?? "Успешно").ByObject(result.Value));
    }
}