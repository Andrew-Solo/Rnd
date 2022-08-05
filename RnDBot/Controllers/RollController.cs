using System.Globalization;
using Dice;
using Discord;
using Discord.Interactions;
using RnDBot.Controllers.Helpers;
using RnDBot.Data;
using RnDBot.Models.Common;
using RnDBot.Models.Glossaries;
using RnDBot.Views;
using ValueType = RnDBot.Views.ValueType;

namespace RnDBot.Controllers;

[Group("roll", "Команды для выполнения бросков и проверок")]
public class RollController : InteractionModuleBase<SocketInteractionContext>
{
    //Dependency Injections
    public DataContext Db { get; set; } = null!;
    
    [SlashCommand("dice", "Выполняет бросок дайсов согласно нотации бросков")]
    public async Task DiceAsync(
        [Summary("нотация", "Нотация броска, по которой будет сгенерирован результат")] string notation,
        [Summary("кости", "Показать все выпавшие кости отдельно")] bool showAll = false,
        [Summary("статистика", "Показать статистику для этой нотации")] bool showStats = false,
        [Summary("скрыть", "Скрывает результат броска")] bool hide = false)
    {
        RollResult result;
        decimal min, max, average;

        try
        {
            //TODO использование характеристик персонажа
            result = Roller.Roll(notation);
            min = Roller.Min(notation).Value;
            max = Roller.Average(notation).Value;
            average = Roller.Max(notation).Value;
        }
        catch (Exception e)
        {
            await RespondAsync(embed: EmbedView.Error("Ошибка парсинка нотации броска", e.Message), ephemeral: true);
            return;
        }

        var fields = new List<IField>
        {
            new TextField<string>("Сумма", result.Value + (result.Value == Roller.Max(notation).Value ? "!" : ""), false)
        };

        if (showStats)
        {
            fields.Add(new TextField<decimal>("Минимальное", min));
            fields.Add(new TextField<decimal>("Среднее", average));
            fields.Add(new TextField<decimal>("Максимальное", max));
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

        var panel = new CommonPanel($"Бросок {notation}", fields.ToArray()) {Color = Color.Blue};

        await RespondAsync(embed: EmbedView.Build(panel), ephemeral: hide);
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
    public async Task SkillAsync(
        [Summary("навык", "Название проверяемого навыка")] [Autocomplete] string skillName,
        [Summary("атрибут", "Название атрибута для модификатора проверки")] [Autocomplete] string? attributeName = null,
        [Summary("преимущества", "Количество преимуществ или помех при отрицательном значении")] int advantages = 0,
        [Summary("модификатор", "Дополнительный модификатор броска")] int modifier = 0,
        [Summary("игрок", "Пользователь для выполнения команды")] IUser? player = null)
    {
        var depot = new CharacterDepot(Db, Context, player);

        var character = await depot.GetCharacterAsync();
        
        var skillType = Glossary.AncorniaSkillNamesReversed[skillName];
        
        var attributeType = attributeName == null
            ? Glossary.GetSkillCoreAttribute(skillType)
            : Glossary.AttributeNamesReversed[attributeName];

        var roll = character.Domains.GetRoll(skillType, attributeType, advantages, modifier);

        roll.Roll();

        await RespondAsync(embed: EmbedView.Build(roll));
    }
}