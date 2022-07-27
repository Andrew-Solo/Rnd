using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using RnDBot.Models.Character;
using RnDBot.Models.Common;
using RnDBot.Models.Modals;
using RnDBot.View;

namespace RnDBot.Controllers;

[Group("сharacter", "Команды для управление персонажами")]
public class CharacterController : InteractionModuleBase<SocketInteractionContext>
{
    public CharacterController()
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
    private AncorniaCharacter Character { get; set; }
    public List<AncorniaCharacter> Characters { get; }
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
        var autocomplete = Context.Interaction as SocketAutocompleteInteraction ?? throw new InvalidOperationException();
        
        string userInput = autocomplete.Data.Current.Value.ToString() ?? String.Empty;

        var results = CharacterNames.Where(c => c.StartsWith(userInput))
            .Select(name => new AutocompleteResult(name, name)).ToList();
        
        await autocomplete.RespondAsync(results.Take(25));
    }

    [SlashCommand("chose", "Выбор активного персонажа")]
    public async Task ChoseAsync([Autocomplete] [Summary("имя", "Имя выбираемого персонажа")] string name)
    {
        Character = Characters.First(c => c.Name == name);
        
        await RespondAsync($"Выбран персонаж **{Character.Name}**");
    }

    [SlashCommand("create", "Создание нового персонажа")]
    public async Task CreateAsync()
    {
        await RespondWithModalAsync<CharacterModal>("character_create");
    }

    [Group("show", "Команды для отображения параметров текущего персонажа")]
    public class ShowController : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("all", "Отображение всех характеристик пероснажа")]
        public Task AllAsync() { return Task.CompletedTask; }
        
        [SlashCommand("general", "Отображение основной информации о персонаже")]
        public Task GeneralAsync() { return Task.CompletedTask; }
        
        [SlashCommand("attributes", "Отображение атрибутов и развития персонажа")]
        public Task AttributesAsync() { return Task.CompletedTask; }
        
        [SlashCommand("points", "Отображение состояний и очков персонажа")]
        public Task PointsAsync() { return Task.CompletedTask; }
        
        [SlashCommand("skills", "Отображение навыков персонажа")]
        public Task SkillsAsync() { return Task.CompletedTask; }
        
        [SlashCommand("backstory", "Отображение предыстории персонажа")]
        public Task BackstoryAsync() { return Task.CompletedTask; }
    }
    
    [Group("up", "Команды для повышения характеристик персонажа")]
    public class UpController : InteractionModuleBase<SocketInteractionContext>
    {
        [AutocompleteCommand("навык", "skill")]
        public Task SkillNameAutocomplete() { return Task.CompletedTask; }
        
        [SlashCommand("skill", "Увеличивает уровень выбранного навыка")]
        public Task SkillAsync(
            [Summary("навык", "Название улучшаемого навыка")][Autocomplete] string name, 
            [Summary("уровень", "Прибавляемый к навыку уровень")] int? level = 1) 
        { return Task.CompletedTask; }
        
        [AutocompleteCommand("атрибут", "level")]
        public Task LevelAttributeAutocomplete() { return Task.CompletedTask; }
        
        [SlashCommand("level", "Увеличивает уровень персонажа и повышает выбранный атрибут")]
        public Task LevelAsync([Summary("атрибут", "Название атрибута для улучшения")][Autocomplete] string attribute) 
        { return Task.CompletedTask; }
    }
}