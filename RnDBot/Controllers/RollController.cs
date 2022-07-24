using Discord.Interactions;

namespace RnDBot.Controllers;

[Group("roll", "Команды для выполнения бросков и проверок")]
public class RollController : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("dice", "Выполняет бросок дайсов согласно нотации бросков")]
    public Task DiceAsync([Summary("Нотация", "Нотация броска, по которой будет сгенерирован результат")] string notation) 
    { return Task.CompletedTask; }

    [AutocompleteCommand("name", "skill")]
    public Task SkillNameAutocomplete() { return Task.CompletedTask; }
    
    [AutocompleteCommand("attribute", "skill")]
    public Task SkillAttributeAutocomplete() { return Task.CompletedTask; }
    
    [SlashCommand("skill", "Выполняет проверку указанного навыка, используя модификатор выбранного аттрибута")]
    public Task SkillAsync(
        [Summary("Навык", "Название проверяемого навыка")][Autocomplete] string name, 
        [Summary("Атрибут", "Название атрибута для модификатора проврки")][Autocomplete] string? attribute = null, 
        [Summary("Преимущества", "Количество преимуществ или помех при отрицательном значении")] int? advantages = 0, 
        [Summary("Состояние", "Дополнительный модификатор броска")] int? modifier = 0) 
    { return Task.CompletedTask; }
    
    // public Task AbilityAsync() { return Task.CompletedTask; }
}