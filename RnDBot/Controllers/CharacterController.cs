using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using RnDBot.Models.Character;
using RnDBot.Models.Common;
using RnDBot.Models.Glossaries;
using RnDBot.Models.Modals;
using RnDBot.Views;
using ValueType = RnDBot.Views.ValueType;

namespace RnDBot.Controllers;

[Group("сharacter", "Команды для управление персонажами")]
public class CharacterController : InteractionModuleBase<SocketInteractionContext>
{
    static CharacterController()
    {
        Characters = new List<AncorniaCharacter>
        {
            CharacterFactory.AncorniaCharacter("Имя персонажа 1"),
            CharacterFactory.AncorniaCharacter("Имя персонажа 2"),
            CharacterFactory.AncorniaCharacter("Имя персонажа 4"),
            CharacterFactory.AncorniaCharacter("Имя персонажа 5"),
        };
        
        Character = Characters.First();
    }

    /// <summary>
    /// Current character
    /// </summary>
    private static AncorniaCharacter Character { get; set; }
    public static List<AncorniaCharacter> Characters { get; }
    public List<string> CharacterNames => Characters.Select(c => c.Name).ToList();

    [SlashCommand("list", "Отображает список всех персонажей")]
    public async Task ListAsync()
    {
        var field = new ListField("Энкорния", CharacterNames);
        var panel = new CommonPanel("Мои персонажи", field);

        await RespondAsync(embed: EmbedView.Build(panel));
    }

    [AutocompleteCommand("имя", "chose")]
    public async Task ChoseNameAutocomplete()
    {
        var autocomplete = new Autocomplete<string>(Context, CharacterNames, s => s);
        
        await autocomplete.RespondAsync();
    }

    [SlashCommand("chose", "Выбор активного персонажа")]
    public async Task ChoseAsync([Autocomplete] [Summary("имя", "Имя выбираемого персонажа")] string name)
    {
        Character = Characters.First(c => c.Name == name);
        
        await RespondAsync($"Выбран персонаж **{Character.Name}**.");
    }
    
    [SlashCommand("create", "Создание нового персонажа")]
    public async Task CreateAsync(
        [Summary("имя", "Имя создаваемого персонажа")] string name, 
        [Summary("уровень", "Начальный уровень персонажа")] int level = 0)
    {
        var character = CharacterFactory.AncorniaCharacter(name, level);
        Characters.Add(character);
        Character = character;

        await RespondAsync($"Персонаж **{character.Name}** успешно создан и выбран как активный.");
    }

    // TODO Modal
    // [SlashCommand("create", "Создание нового персонажа")]
    // public async Task CreateAsync() => await RespondWithModalAsync<CharacterModal>("character_create");

    [Group("show", "Команды для отображения параметров текущего персонажа")]
    public class ShowController : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("all", "Отображение всех характеристик пероснажа")]
        public async Task AllAsync() => await RespondAsync(embeds: EmbedView.Build(Character));

        [SlashCommand("general", "Отображение основной информации о персонаже")]
        public async Task GeneralAsync() => await RespondAsync(embed: EmbedView.Build(Character.General));

        [SlashCommand("attributes", "Отображение атрибутов и развития персонажа")]
        public async Task AttributesAsync() => await RespondAsync(embed: EmbedView.Build(Character.Attributes));

        [SlashCommand("points", "Отображение состояний и очков персонажа")]
        public async Task PointsAsync() => await RespondAsync(embed: EmbedView.Build(Character.Pointers));

        [SlashCommand("skills", "Отображение навыков персонажа")]
        public async Task SkillsAsync() => await RespondAsync(embed: EmbedView.Build(Character.Domains));

        //TODO abilities, items, reputation, backstory
    }
    
    [Group("up", "Команды для повышения характеристик персонажа")]
    public class UpController : InteractionModuleBase<SocketInteractionContext>
    {
        //TODO Автокомплит должен возвращать нужное значение и при этом работать
        [AutocompleteCommand("навык", "skill")]
        public async Task SkillNameAutocomplete()
        {
            var autocomplete = new Autocomplete<string>(Context, 
                Glossary.AncorniaSkillNamesReversed.Keys, 
                s => s);
        
            await autocomplete.RespondAsync();
        }

        [SlashCommand("skill", "Увеличивает уровень выбранного навыка")]
        public async Task SkillAsync(
            [Summary("навык", "Название улучшаемого навыка")] [Autocomplete] string name,
            [Summary("уровень", "Прибавляемый к навыку уровень")] int level = 1)
        {
            var type = Glossary.AncorniaSkillNamesReversed[name];
            var skill = Character.Domains.AllCoreSkills.First(s => s.SkillType == type);
            skill.Value += level;

            var power = Character.Attributes.Power;
            
            await RespondAsync($"Навык **{name}** улучшен до уровня `{skill.Value}`.\n" + 
                           //  $"Максимальный уровень этого навыка `{10}`.\n" +
                               $"Осталось свободной мощи `{power.Max}`.");
        }

        [AutocompleteCommand("атрибут", "level")]
        public async Task LevelAttributeAutocomplete()
        {
            var autocomplete = new Autocomplete<string>(Context, 
                Glossary.AttributeNamesReversed.Keys, 
                s => s);
        
            await autocomplete.RespondAsync();
        }

        [SlashCommand("level", "Увеличивает уровень персонажа и повышает выбранный атрибут")]
        public async Task LevelAsync([Summary("атрибут", "Название атрибута для улучшения")] [Autocomplete] string name)
        {
            var type = Glossary.AttributeNamesReversed[name];
            var attribute = Character.Attributes.CoreAttributes.First(a => a.AttributeType == type);
            attribute.Modifier += 1;

            var power = Character.Attributes.Power;

            await RespondAsync($"Уровень **{Character.Name}** увеличен до `{Character.Attributes.Level}`" +
                               $"Атрибут **{name}** улучшен до уровня `{attribute.Modifier}`.\n" +
                            // $"Максимальный уровень атрибута `+1`.\n" +
                               $"Осталось свободной мощи `{power.Max}`.");
        }
    }
}