using Discord.Interactions;
using Rnd.Bot.Discord.Run;
using Rnd.Bot.Discord.Sessions;
using Rnd.Constants;
using Rnd.Data;
using Rnd.Models;

namespace Rnd.Bot.Discord.Controllers;

[Group("module", "Команды для управления модулями")]
public class ModuleController : InteractionModuleBase<SocketInteractionContext>
{
    //Dependency Injections
    public DataContext Data { get; set; } = null!;
    public SessionProvider Provider { get; set; } = null!;
    
    [AutocompleteCommand("module", "show")]
    public async Task ModuleNameAutocomplete()
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);

        var result = await Data.Modules.ListAsync();
        await this.CheckResultAsync(result);
        
        var autocomplete = new Autocomplete<Module>(result.Value, m => m.VersionedTitle, g => g.Id.ToString());
        await autocomplete.RespondAsync(Context);
    }
    
    [SlashCommand("show", "Показать данные модуля")]
    public async Task ShowAsync(
        [Summary("module", "Отображаемый модуль"), Autocomplete] string moduleId
        )
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);

        //TODO nullable warning
        var result = await Data.Modules.GetAsync(moduleId.AsGuid()!.Value);
        await this.EmbedResponseAsync(result, "Модуль");
    }
    
    [SlashCommand("list", "Показать все модули")]
    public async Task ListAsync()
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);
        
        var result = await Data.Modules.ListAsync();
        await this.EmbedResponseAsync(result.OnSuccess(gs => gs.Select(g => g.VersionedTitle)), "Модули");
    }
    
    [AutocompleteCommand("file", "update")]
    public async Task FileNameAutocomplete()
    {
        var directory = new DirectoryInfo(Setup.Configuration.ModulesPath);

        var files = directory
            .GetFiles()
            .Where(f => f.Extension == ".rnd");
        
        var autocomplete = new Autocomplete<FileInfo>(files, f => f.Name, f => f.FullName);
        
        await autocomplete.RespondAsync(Context);
    }
    
    [SlashCommand("update", "Обновить модули")]
    public async Task UpdateAsync(
        [Summary("file", "Обновляемый файл, оставьте пустым, чтобы обновить все"), Autocomplete] string? file = null
    )
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckInRole(session, UserRole.Admin);

        if (file != null)
        {
            var result = await Data.Modules.UpdateAsync(file);
            await this.EmbedResponseAsync(result, "Модуль обновлен");
        }
        else
        {
            var result = await Data.Modules.UpdateAllAsync(Setup.Configuration.ModulesPath);
            await this.EmbedResponseAsync(result.OnSuccess(gs => gs.Select(g => g.VersionedTitle)), "Модули обновлены");
        }
    }
}