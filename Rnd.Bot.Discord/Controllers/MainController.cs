using AirtableApiClient;
using Discord.Interactions;
using Rnd.Bot.Discord.Models;
using Rnd.Bot.Discord.Run;
using Rnd.Bot.Discord.Views;
using Rnd.Bot.Discord.Views.Panels;
using Rnd.Results;

namespace Rnd.Bot.Discord.Controllers;

public class MainController : InteractionModuleBase<SocketInteractionContext>
{
    public MainController()
    {
        var config = Setup.Configuration;
        Data = new AirtableBase(config.AirtableToken, config.Games[config.DefaultGame]);
    }
    
    ~MainController()
    {
        Data.Dispose();
    }

    public AirtableBase Data { get; private set; }
    
    public async Task GameAutocomplete()
    {
        var autocomplete = new Autocomplete<KeyValuePair<string, string>>(
            Setup.Configuration.Games, 
            s => s.Key,
            s => s.Value
        );
        
        await autocomplete.RespondAsync(Context);
    }
    
    public async Task CharacterAutocomplete()
    {
        var response = await Data.ListAsync(Table.Characters);
        
        if (response.IsFailed) return;

        var first = response.Value.First();

        var value = first.Get<string>(Character.Name);
        
        var values = response.Value
            .Select(r => r.Get<string>(Character.Name))
            .Where(s => s != null)
            .Cast<string>()
            .ToList();
        
        var autocomplete = new Autocomplete<string>(
            values, 
            s => s
        );
        
        await autocomplete.RespondAsync(Context);
    }
    
    [AutocompleteCommand("игра", "выбор_игры")]
    public async Task ChoseGameAutocomplete() => await GameAutocomplete();
    
    [SlashCommand("выбор_игры", "Выбрать активную игру")]
    public async Task ChoseGameAsync(
        [Summary("игра", "Имя игры"), Autocomplete] string game
    )
    {
        Data.Dispose();
        Data = new AirtableBase(Setup.Configuration.AirtableToken, game);
        await this.EmbedResponseAsync(PanelBuilder.WithTitle("Игра изменена").AsSuccess());
    }
    
    [AutocompleteCommand("персонаж", "бросок")]
    public async Task RollAutocomplete() => await CharacterAutocomplete();
    
    [SlashCommand("бросок", "Выполнить проверку навыка")]
    public async Task RollAsync(
        [Summary("персонаж", "Персонаж выполнящий проверку"), Autocomplete] string character
    )
    {
        await this.EmbedResponseAsync(PanelBuilder.WithTitle("Отлично").AsSuccess());
    }
}