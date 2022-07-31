using System.Globalization;
using Dice;
using Discord;
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
    public async Task DiceAsync(
        [Summary("нотация", "Нотация броска, по которой будет сгенерирован результат")] string notation,
        [Summary("кости", "Показать все выпавшие кости отдельно")] bool showAll = false,
        [Summary("статистика", "Показать статистику для этой нотации")] bool showStats = false,
        [Summary("скрыть", "Скрывает результат броска")] bool hide = false)
    {
        try
        {
            //TODO использование характеристик персонажа
            var result = Roller.Roll(notation);
        
            var fields = new List<IField>
            {
                new TextField<string>("Сумма", result.Value + (result.Value == Roller.Max(notation).Value ? "!" : ""), false)
            };
            
            if (showStats)
            {
                fields.Add(new TextField<decimal>("Минимальное", Roller.Min(notation).Value));
                fields.Add(new TextField<decimal>("Среднее", Roller.Average(notation).Value));
                fields.Add(new TextField<decimal>("Максимальное", Roller.Max(notation).Value));
            }

            if (showAll)
            {
                var bySideGrouping = result.Values.GroupBy(d => d.NumSides).Where(d => d.Key != 0);

                fields.AddRange(bySideGrouping.Select(d => 
                    new ListField(
                        $"Кости d{d.Key}", 
                        d.Select(die => die.Value.ToString(CultureInfo.InvariantCulture) + (die.Value == d.Key ? "!" : "")), 
                        true)));
            }

            var panel = new CommonPanel($"Бросок {notation}", fields.ToArray()) { Color = Color.Blue };

            await RespondAsync(embed: EmbedView.Build(panel), ephemeral: hide);
        }
        catch (Exception e)
        {
            await RespondAsync(embed: EmbedView.Error("Ошибка парсинка нотации броска", e.Message), ephemeral: true);
        }
    }

    [SlashCommand("notation", "Информация о нотации бросков")]
    public async Task NotationAsync()
    {
        await RespondAsync($"Узнайте больше о записи нотации бросков\n" +
                     $"https://skizzerz.net/DiceRoller/Dice_Reference#Comparisons", ephemeral: true);
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