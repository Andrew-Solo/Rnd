using System.Text.Json;
using AirtableApiClient;
using Discord.Interactions;
using Discord.WebSocket;
using Rnd.Bot.Discord.Views;
using Rnd.Bot.Discord.Views.Fields;
using Rnd.Bot.Discord.Views.Panels;
using Rnd.Results;
using Fields = Rnd.Bot.Discord.Models.Fields;

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

    public static async Task<Result<List<AirtableRecord>>> ListAsync(this AirtableBase data, string table)
    {
        var response = await data.ListRecords(table);
        
        if (!response.Success)
        {
            return Result.Fail<List<AirtableRecord>>(
                new Message(response.AirtableApiError.ErrorName, 
                    response.AirtableApiError.ErrorMessage));
        }

        return Result.Success(response.Records.ToList(), "Результаты");
    }

    public static T? Get<T>(this AirtableRecord record, string field)
    {
        var value = record.GetField(field);
        return value is T t ? t : default;
    }
    
    public static SocketAutocompleteInteraction AsAutocomplete(this SocketInteraction interaction)
    {
        return interaction as SocketAutocompleteInteraction ?? throw new InvalidOperationException();
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