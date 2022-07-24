using Discord.Interactions;

namespace RnDBot.Controllers;

[Group("сharacter", "Команды для управление персонажами")]
public class CharacterController : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("list", "Отображает список всех персонажей")]
    public Task ListAsync() { return Task.CompletedTask; }
    
    [AutocompleteCommand("name", "chose")]
    public Task ChoseNameAutocomplete() { return Task.CompletedTask; }
    
    [SlashCommand("chose", "Выбор активного персонажа")]
    public Task ChoseAsync([Autocomplete][Summary("Имя", "Имя выбираемого персонажа")] string name) 
    { return Task.CompletedTask; }
    
    [AutocompleteCommand("setting", "create")]
    public Task CreateSettingAutocomplete() { return Task.CompletedTask; }
    
    [SlashCommand("create", "Создание нового персонажа")]
    public Task CreateAsync(
        [Summary("Имя", "Уникальное имя персонажа")] string name, 
        [Summary("Сеттинг", "Сеттинг, в котором создается персонаж")][Autocomplete] string setting) 
    { return Task.CompletedTask; }
    
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
        
        // public Task EquipAsync() { return Task.CompletedTask; }
        // public Task AbilitiesAsync() { return Task.CompletedTask; }
        // public Task ReputationAsync() { return Task.CompletedTask; }
        
        [SlashCommand("backstory", "Отображение предыстории персонажа")]
        public Task BackstoryAsync() { return Task.CompletedTask; }
    }
    
    [Group("up", "Команды для повышения характеристик персонажа")]
    public class UpController : InteractionModuleBase<SocketInteractionContext>
    {
        [AutocompleteCommand("name", "skill")]
        public Task SkillNameAutocomplete() { return Task.CompletedTask; }
        
        [SlashCommand("skill", "Увеличивает уровень выбранного навыка")]
        public Task SkillAsync(
            [Summary("Навык", "Название улучшаемого навыка")][Autocomplete] string name, 
            [Summary("Уровень", "Прибавляемый к навыку уровень")] int? level = 1) 
        { return Task.CompletedTask; }
        
        [AutocompleteCommand("attribute", "level")]
        public Task LevelAttributeAutocomplete() { return Task.CompletedTask; }
        
        [SlashCommand("level", "Увеличивает уровень персонажа и повышает выбранный атрибут")]
        public Task LevelAsync([Summary("Атрибут", "Название атрибута для улучшения")][Autocomplete] string attribute) 
        { return Task.CompletedTask; }
    }
    
    // //Управление способностями
    // public Task AbilityAsync() { return Task.CompletedTask; }
    //
    // public class AbilityController
    // {
    //     public Task AddAsync() { return Task.CompletedTask; }
    //     public Task RemoveAsync() { return Task.CompletedTask; }
    //     public Task EditAsync() { return Task.CompletedTask; }
    //     public Task CopyAsync() { return Task.CompletedTask; }
    // }
}