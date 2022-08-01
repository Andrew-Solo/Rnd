using System.Globalization;
using Dice;
using Discord;
using Discord.Interactions;
using RnDBot.Controllers.Helpers;
using RnDBot.Data;
using RnDBot.Models.Character;
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
        [Summary("преимущества", "Количество преимуществ или помех при отрицательном значении")] int? advantages = 0,
        [Summary("модификатор", "Дополнительный модификатор броска")] int? modifier = 0,
        [Summary("игрок", "Пользователь для выполнения команды")] IUser? player = null)
    {
        var depot = new CharacterDepot(Db, Context, player);

        var character = await depot.GetCharacterAsync();
        
        var skillType = Glossary.AncorniaSkillNamesReversed[skillName];
        
        var attributeType = attributeName == null
            ? Glossary.GetSkillCoreAttribute(skillType)
            : Glossary.AttributeNamesReversed[attributeName];

        var skill = character.Domains.FinalSkills.First(s => s.SkillType == skillType);
        var attribute = character.Attributes.FinalAttributes.First(a => a.AttributeType == attributeType);

        var results = new List<int>();
        
        results.Add((int) Roller.Roll("1d6").Value);
        results.Add((int) Roller.Roll("1d6").Value);

        var crits = results.Count(x => x == 6);

        if (!character.Pointers.IsNearDeath)
        {
            var diceNumber = 1 + Math.Abs(advantages.GetValueOrDefault());

            int skillResult;

            if (skill.Value < 2)
            {
                skillResult = 0;
            }
            else
            {
                skillResult = (int) Roller.Roll($"{diceNumber}d{skill.Value}k{(advantages >= 0 ? "h" : "l")}1").Value;

                if (skill.Value < 4)
                {
                    skillResult--;
                    if (Roller.Roll("1d4").Value == 4) crits++;
                }
                else
                {
                    if (skillResult == skill.Value) crits++;
                }
            }
            
            results.Add(skillResult);
        }

        var misscrits = results.Count(x => x == 1); 

        if (skill.Value > 1 && crits >= 2)
        {
            results.Add((int) Roller.Roll($"1d{skill.Value}").Value);
            
            if (crits >= 3) results.Add((int) Roller.Roll($"1d{skill.Value}").Value);
        }
        else if (misscrits >= 2)
        {
            results.Add((int) Roller.Roll($"1d6").Value * -1);
            
            if (misscrits >= 3) results.Add((int) Roller.Roll($"1d6").Value * -1);
        }

        var result = results.Sum() + attribute.Modifier + modifier.GetValueOrDefault();
        
        var price = results.Count(x => x <= 1);

        var tricks = results.Count(x => x is >= 6 and < 12) + 
                     2 * results.Count(x => x is >= 12 and < 18) +
                     3 * results.Count(x => x is >= 18 and < 24) + 
                     4 * results.Count(x => x is >= 24 and < 100) +
                     5 * results.Count(x => x > 100);

        var fields = new IField[]
        {
            new TextField<int>("Результат", result),
            new TextField<int>("Трюки", tricks),
            new TextField<int>("Цена", price),
        };

        var title = $"Проверка **{skill.Name}** `{skill.Value}`";

        var description = $"{attribute.Name} {EmbedView.Build(attribute.Value, ValueType.InlineModifier)}";

        if (crits == 2) description += "\n**Критический!**";
        if (crits == 3) description += "\n**Суперкритический!**";
        
        if (misscrits == 2) description += "\n**Провальный!**";
        if (misscrits == 3) description += "\n**Суперпровальный!**";
        
        if (advantages > 0) description += $"\nПреимущества `{advantages}`";
        if (advantages < 0) description += $"\nПомехи `{Math.Abs(advantages.GetValueOrDefault())}`";
        
        if (modifier != 0) description += $"\nМодификатор {EmbedView.Build(modifier, ValueType.InlineModifier)}";
        
        if (character.Pointers.IsNearDeath) description += "\n*Персонаж присмерти!*";

        var panel = new CommonPanel(title, fields)
        {
            Description = description, 
            Footer = character.General.Character.GetFooter,
            Color = Color.Blue, 
        };

        await RespondAsync(embed: EmbedView.Build(panel));
    }
}