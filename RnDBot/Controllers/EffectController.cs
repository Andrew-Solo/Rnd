using Discord.Interactions;
using RnDBot.Controllers.Helpers;
using RnDBot.Data;
using RnDBot.Models.Character.Panels.Effect;
using RnDBot.Models.Glossaries;
using RnDBot.Views;

namespace RnDBot.Controllers;

[Group("effect", "Команды для управления эффектами персонажа")]
public class EffectController : InteractionModuleBase<SocketInteractionContext>
{
    //Dependency Injections
    public DataContext Db { get; set; } = null!;

    [Group("add", "Команды для добавления эффектов")]
    public class AddController : InteractionModuleBase<SocketInteractionContext>
    {
        //Dependency Injections
        public DataContext Db { get; set; } = null!;

        [AutocompleteCommand("атрибут", "attribute")]
        public async Task AttributeNameAutocomplete()
        {
            var autocomplete = new Autocomplete<string>(Context, 
                Glossary.AttributeNamesReversed.Keys, 
                s => s);
        
            await autocomplete.RespondAsync();
        }
        
        [SlashCommand("attribute", "Эффект изменяющий атрибут")]
        public async Task AttributeAsync(
            [Summary("имя", "Название эффекта")] string name,
            [Summary("атрибут", "Модифицируемый атрибут")] [Autocomplete] string attribute,
            [Summary("модификатор", "Значение модификатора")] int modifier)
        {
            var depot = new CharacterDepot(Db, Context);
            
            var character = await depot.GetCharacterAsync();
            
            var effect = new AttributeEffect(name, Glossary.AttributeNamesReversed[attribute], modifier);
            
            character.Effects.AttributeEffects.Add(effect);
            
            if (!character.IsValid)
            {
                await RespondAsync(embed: EmbedView.Error(character.Errors), ephemeral: true);
                return;
            }

            await depot.UpdateCharacterAsync(character);

            await RespondAsync($"{character.Name} получает эффект {effect.View}");
        }
        
        [AutocompleteCommand("состояние", "pointer")]
        public async Task PointNameAutocomplete()
        {
            var autocomplete = new Autocomplete<string>(Context, 
                Glossary.PointerNamesReversed.Keys, 
                s => s);
        
            await autocomplete.RespondAsync();
        }

        [SlashCommand("pointer", "Эффект изменяющий состояние")]
        public async Task PointerAsync(
            [Summary("имя", "Название эффекта")] string name,
            [Summary("состояние", "Модифицируемое состояние")] [Autocomplete] string pointer,
            [Summary("модификатор", "Значение модификатора")] int modifier)
        {
            var depot = new CharacterDepot(Db, Context);
            
            var character = await depot.GetCharacterAsync();
            
            var effect = new PointEffect(name, Glossary.PointerNamesReversed[pointer], modifier);
            
            character.Effects.PointEffects.Add(effect);
            
            if (!character.IsValid)
            {
                await RespondAsync(embed: EmbedView.Error(character.Errors), ephemeral: true);
                return;
            }

            await depot.UpdateCharacterAsync(character);

            await RespondAsync($"{character.Name} получает эффект {effect.View}");
        }
        
        [AutocompleteCommand("навык", "skill")]
        public async Task SkillNameAutocomplete()
        {
            var autocomplete = new Autocomplete<string>(Context, 
                Glossary.AncorniaSkillNamesReversed.Keys, 
                s => s);
        
            await autocomplete.RespondAsync();
        }

        [SlashCommand("skill", "Эффект изменяющий навык")]
        public async Task SkillAsync(
            [Summary("имя", "Название эффекта")] string name,
            [Summary("навык", "Модифицируемый навык")] [Autocomplete] string skill,
            [Summary("модификатор", "Значение модификатора")] int modifier)
        {
            var depot = new CharacterDepot(Db, Context);
            
            var character = await depot.GetCharacterAsync();
            
            var effect = new SkillEffect<AncorniaSkillType>(name, Glossary.AncorniaSkillNamesReversed[skill], modifier);
            
            character.Effects.SkillEffects.Add(effect);
            
            if (!character.IsValid)
            {
                await RespondAsync(embed: EmbedView.Error(character.Errors), ephemeral: true);
                return;
            }

            await depot.UpdateCharacterAsync(character);

            await RespondAsync($"{character.Name} получает эффект {effect.View}");
        }
    }
    
    [AutocompleteCommand("имя", "remove")]
    public async Task RemoveNameAutocomplete()
    {
        var depot = new CharacterDepot(Db, Context);
        
        var autocomplete = new Autocomplete<string>(Context,  
            (await depot.GetCharacterAsync()).Effects.CoreEffects.Select(e => e.View), 
            s => s);
        
        await autocomplete.RespondAsync();
    }

    [SlashCommand("remove", "Удалить эффект")]
    public async Task RemoveAsync([Summary("имя", "Название эффекта")] [Autocomplete] string name)
    {
        var depot = new CharacterDepot(Db, Context);
            
        var character = await depot.GetCharacterAsync();

        var effect = character.Effects.CoreEffects.First(e => e.Name == name);

        character.Effects.RemoveEffect(effect);
        
        if (!character.IsValid)
        {
            await RespondAsync(embed: EmbedView.Error(character.Errors), ephemeral: true);
            return;
        }

        await depot.UpdateCharacterAsync(character);

        await RespondAsync($"{character.Name} теряет эффект {effect.View}");
    }
}