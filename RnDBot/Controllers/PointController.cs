using Discord.Interactions;

namespace RnDBot.Controllers;

[Group("point", "Команды для управления состояниями и очками персонажа")]
public class PointController : InteractionModuleBase<SocketInteractionContext>
{
    [AutocompleteCommand("name", "modify")]
    public Task PointModifyAutocomplete() { return Task.CompletedTask; }
    
    [SlashCommand("modify", "Изменение выбранного состояния или очков на указанное значение")]
    public Task ModifyAsync(
        [Summary("Состояние", "Название состояния/очков для изменения")][Autocomplete] string name,
        [Summary("Значение", "Значение на которое изменится состояние")] int number) 
    { return Task.CompletedTask; }
    
    [AutocompleteCommand("name", "refresh")]
    public Task PointRefreshAutocomplete() { return Task.CompletedTask; }
    
    [SlashCommand("refresh", "Устанавливает знаечние по умолчанию всех очков и состояний")]
    public Task RefreshAsync([Summary("Состояние", "Название состояния/очков для изменения")][Autocomplete] string name) 
    { return Task.CompletedTask; }
    
    [SlashCommand("rest", "Активирует длительный отдых, очки способностей тратяться на худшее состояние")]
    public Task RestAsync() { return Task.CompletedTask; }
}