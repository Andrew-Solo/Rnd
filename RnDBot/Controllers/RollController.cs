using System.Globalization;
using Dice;
using Discord.Interactions;
using RnDBot.Controllers.Helpers;
using RnDBot.Models.Common;
using RnDBot.Models.Glossaries;
using RnDBot.Views;

namespace RnDBot.Controllers;

[Group("roll", "Команды для выполнения бросков и проверок")]
public class RollController : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("dice", "Выполняет бросок дайсов согласно нотации бросков")]
    public async Task DiceAsync([Summary("Нотация", "Нотация броска, по которой будет сгенерирован результат")] string notation)
    {
        var result = Roller.Roll(notation);

        var panel = new CommonPanel($"Бросок {notation}", 
            new TextField<decimal>("Результат", result.Value),
            new ListField("Кости", result.Values.Select(d => d.Value.ToString(CultureInfo.InvariantCulture))));

        await RespondAsync(embed: EmbedView.Build(panel));
    }

    [AutocompleteCommand("навык", "skill")]
    public async Task SkillNameAutocomplete()
    {
        var autocomplete = new Autocomplete<string>(Context, 
            Glossary.AncorniaSkillNamesReversed.Keys, 
            s => s);
        
        await autocomplete.RespondAsync();
    }

    [AutocompleteCommand("атрибут", "skill")]
    public async Task AttributeNameAutocomplete()
    {
        var autocomplete = new Autocomplete<string>(Context, 
            Glossary.AttributeNamesReversed.Keys, 
            s => s);
        
        await autocomplete.RespondAsync();
    }
    
    [SlashCommand("skill", "Выполняет проверку указанного навыка, используя модификатор выбранного аттрибута")]
    public Task SkillAsync(
        [Summary("навык", "Название проверяемого навыка")][Autocomplete] string skillName, 
        [Summary("атрибут", "Название атрибута для модификатора проверки")][Autocomplete] string? attributeName = null, 
        [Summary("преимущества", "Количество преимуществ или помех при отрицательном значении")] int? advantages = 0, 
        [Summary("модификатор", "Дополнительный модификатор броска")] int? modifier = 0) 
    { return Task.CompletedTask; }
}