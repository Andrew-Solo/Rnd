using Discord.Interactions;
using Discord.WebSocket;
using Rnd.Api.Client.Clients;
using Rnd.Api.Client.Responses;
using Rnd.Bot.Discord.Views.Fields;
using Rnd.Bot.Discord.Views.Panels;

namespace Rnd.Bot.Discord.Controllers;

public static class Extensions
{
    public static SocketAutocompleteInteraction AsAutocomplete(this SocketInteraction interaction)
    {
        return interaction as SocketAutocompleteInteraction ?? throw new InvalidOperationException();
    } 
    
    public static async Task CheckNotAuthorized(this InteractionModuleBase<SocketInteractionContext> controller, 
        BasicClient client)
    {
        if (client.Status == ClientStatus.Ready)
        {
            await controller.EmbedResponseAsync(PanelBuilder.WithTitle("Вы уже авторизованы").AsError());
            throw new Exception("Already authorized");
        }

        await controller.CheckAuthorizationErrors(client);
    }
    
    public static async Task CheckAuthorized(this InteractionModuleBase<SocketInteractionContext> controller, 
        BasicClient client)
    {
        if (client.Status == ClientStatus.NotAuthorized)
        {
            await controller.EmbedResponseAsync(PanelBuilder.WithTitle("Вы не авторизованы").AsError());
            throw new Exception("Not authorized");
        }

        await controller.CheckAuthorizationErrors(client);
    }
    
    public static async Task CheckAuthorizationErrors(this InteractionModuleBase<SocketInteractionContext> controller, 
        BasicClient client)
    {
        if (client.Status == ClientStatus.AuthorizationError)
        {
            await controller.EmbedResponseAsync(PanelBuilder.WithTitle("Ошибка авторизации").ByErrors(client.Authorization.Errors));
            throw new Exception("Not authorized");
        }
    }
    
    public static async Task ApiResponseAsync<T>(this InteractionModuleBase<SocketInteractionContext> controller, 
        string title, Response<T> response, bool ephemeral = true) where T : class
    {
        await controller.CheckApiResponseAsync(response);
        await controller.EmbedResponseAsync(PanelBuilder.WithTitle(title).ByClass(response.Value), ephemeral);
    }
    
    public static async Task CheckApiResponseAsync<T>(this InteractionModuleBase<SocketInteractionContext> controller, 
        Response<T> response) where T : class
    {
        if (!response.IsSuccess)
        {
            await controller.EmbedResponseAsync(PanelBuilder.WithTitle("Ошибка валидации").ByErrors(response.Errors));
            throw new Exception("Validation error");
        }
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
}