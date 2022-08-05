using Discord;
using Discord.Interactions;
using RnDBot.Controllers.Helpers;
using RnDBot.Data;
using RnDBot.Models.Glossaries;
using RnDBot.Views;

namespace RnDBot.Controllers;

[Group("trauma", "Команды для управления травмами персонажа")]
public class TraumaController : InteractionModuleBase<SocketInteractionContext>
{
    //Dependency Injections
    public DataContext Db { get; set; } = null!;

    [AutocompleteCommand("имя", "remove")]
    public async Task TraumaNameAutocomplete()
    {
        var depot = new CharacterDepot(Db, Context);
        
        //TODO баг, если у гм нет перса, этьо работать не будет
        var traumas = (await depot.GetCharacterAsync()).Traumas.TraumaEffects.Select(e => e.Name)
            .ToDictionary(k => k, e => e);

        if (depot.IsUserValidGuide())
        {
            var dataCharacters = await depot.GetGuidedDataCharactersAsync();

            foreach (var dataCharacter in dataCharacters)
            {
                var character = dataCharacter.Character;

                foreach (var trauma in character.Traumas.TraumaEffects)
                {
                    var user = Context.Guild.Users.FirstOrDefault(u => u.Id == dataCharacter.PlayerId);
                
                    if (user == null) continue;
                    
                    traumas[$"{user.Nickname ?? user.Username} – {dataCharacter.Name}: {trauma.Name}"] = trauma.Name;
                }
            }
        }
        
        var autocomplete = new Autocomplete<string>(Context, traumas);
        
        await autocomplete.RespondAsync();
    }
    
    [SlashCommand("remove", "Удалить травму")]
    public async Task RemoveAsync(
        [Summary("имя", "Название травмы")] [Autocomplete] string name,
        [Summary("игрок", "Пользователь для выполнения команды")] IUser? player = null)
    {
        var depot = new CharacterDepot(Db, Context, player);
            
        var character = await depot.GetCharacterAsync();

        var trauma = character.Traumas.TraumaEffects.First(e => e.Name == name);

        var finalPointers = character.Pointers.FinalPointers;

        character.Traumas.TraumaEffects.Remove(trauma);
        
        character.Pointers.UpdateCurrentPoints(finalPointers);
        
        if (!character.IsValid)
        {
            await RespondAsync(embed: EmbedView.Error(character.Errors), ephemeral: true);
            return;
        }

        await depot.UpdateCharacterAsync(character);

        await RespondAsync($"{character.Name} теряет травму \n{trauma.View}");
    }

    [AutocompleteCommand("имя", "state")]
    public async Task StateTraumaNameAutocomplete() => await TraumaNameAutocomplete();

    [AutocompleteCommand("состояние", "state")]
    public async Task TraumaStateAutocomplete()
    {
        var autocomplete = new Autocomplete<string>(Context, 
            Glossary.TraumaStateNameReversed.Keys, 
            s => s);
        
        await autocomplete.RespondAsync();
    }

    [SlashCommand("state", "Изменить состояние травмы")]
    public async Task StateAsync(
        [Summary("имя", "Название эффекта")] [Autocomplete] string name,
        [Summary("состояние", "Новое состояние травмы")] [Autocomplete] string state,
        [Summary("игрок", "Пользователь для выполнения команды")] IUser? player = null)
    {
        var depot = new CharacterDepot(Db, Context, player);
            
        var character = await depot.GetCharacterAsync();

        var trauma = character.Traumas.TraumaEffects.First(e => e.Name == name);

        var originState = trauma.TraumaState;

        var stateType = Glossary.TraumaStateNameReversed[state];

        var finalPointers = character.Pointers.FinalPointers;

        trauma.TraumaState = stateType;
        
        character.Pointers.UpdateCurrentPoints(finalPointers);
        
        if (!character.IsValid)
        {
            await RespondAsync(embed: EmbedView.Error(character.Errors), ephemeral: true);
            return;
        }

        await depot.UpdateCharacterAsync(character, true);

        await RespondAsync($"{character.Name} изменяет состояние травмы " +
                           $"{Glossary.TraumaStateName[originState]} -> {Glossary.TraumaStateName[stateType]}" +
                           $"\n{trauma.View}");
    }
}