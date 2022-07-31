using Discord.Interactions;
using Newtonsoft.Json;
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
        
        [SlashCommand("power", "Эффект изменяющий мощь")]
        public async Task PowerAsync(
            [Summary("имя", "Название эффекта")] string name,
            [Summary("лимит", "Модификатор лимита мощи")] int maxModifier = 0,
            [Summary("мощь", "Модификатор текщей мощи")] int currentModifier = 0)
        {
            var depot = new CharacterDepot(Db, Context);
            
            var character = await depot.GetCharacterAsync();

            var effect = new PowerEffect(name, maxModifier, currentModifier);
            
            character.Effects.PowerEffects.Add(effect);
            
            if (!character.IsValid)
            {
                await RespondAsync(embed: EmbedView.Error(character.Errors), ephemeral: true);
                return;
            }

            await depot.UpdateCharacterAsync(character);

            await RespondAsync($"{character.Name} получает эффект {effect.View}");
        }

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
        
        [AutocompleteCommand("домен", "domain")]
        public async Task DomainNameAutocomplete()
        {
            var autocomplete = new Autocomplete<string>(Context, 
                Glossary.AncorniaDomainNamesReversed.Keys, 
                s => s);
        
            await autocomplete.RespondAsync();
        }

        [SlashCommand("domain", "Эффект изменяющий уровень домена")]
        public async Task DomainAsync(
            [Summary("имя", "Название эффекта")] string name,
            [Summary("домен", "Модифицируемый домен")] [Autocomplete] string domain,
            [Summary("модификатор", "Значение модификатора")] int modifier)
        {
            var depot = new CharacterDepot(Db, Context);
            
            var character = await depot.GetCharacterAsync();
            
            var effect = new DomainEffect<AncorniaDomainType>(name, Glossary.AncorniaDomainNamesReversed[domain], modifier);
            
            character.Effects.DomainEffects.Add(effect);
            
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
        
        [SlashCommand("aggregate", "Эффект состоящий из нескольких эффектов")]
        public async Task AggregateAsync(
            [Summary("имя", "Название эффекта")] string name,
            [Summary("мощь", "Эффекты изменяющие мощь (JSON)")] string? powerEffects = null,
            [Summary("аттрибуты", "Эффекты изменяющие мощь (JSON)")] string? attributeEffects = null,
            [Summary("состояния", "Эффекты изменяющие мощь (JSON)")] string? pointEffects = null,
            [Summary("домены", "Эффекты изменяющие мощь (JSON)")] string? domainEffects = null,
            [Summary("способности", "Эффекты изменяющие мощь (JSON)")] string? skillEffects = null)
        {
            var depot = new CharacterDepot(Db, Context);
            
            var character = await depot.GetCharacterAsync();
            
            var effect = new AggregateEffect(name);

            try
            {
                if (!string.IsNullOrWhiteSpace(powerEffects))
                {
                    effect.PowerEffects.AddRange(JsonConvert.DeserializeObject<List<PowerEffect>>(powerEffects) 
                                                 ?? new List<PowerEffect>());
                }
            
                if (!string.IsNullOrWhiteSpace(attributeEffects))
                {
                    effect.AttributeEffects.AddRange(JsonConvert.DeserializeObject<List<AttributeEffect>>(attributeEffects) 
                                                     ?? new List<AttributeEffect>());
                }
            
                if (!string.IsNullOrWhiteSpace(pointEffects))
                {
                    effect.PointEffects.AddRange(JsonConvert.DeserializeObject<List<PointEffect>>(pointEffects) 
                                                 ?? new List<PointEffect>());
                }
            
                if (!string.IsNullOrWhiteSpace(domainEffects))
                {
                    effect.DomainEffects.AddRange(JsonConvert.DeserializeObject<List<DomainEffect<AncorniaDomainType>>>(domainEffects) 
                                                  ?? new List<DomainEffect<AncorniaDomainType>>());
                }
            
                if (!string.IsNullOrWhiteSpace(skillEffects))
                {
                    effect.SkillEffects.AddRange(JsonConvert.DeserializeObject<List<SkillEffect<AncorniaSkillType>>>(skillEffects) 
                                                 ?? new List<SkillEffect<AncorniaSkillType>>());
                }
            }
            catch (Exception e)
            {
                await RespondAsync(embed: EmbedView.Error("Ошибка парсинга JSON параметра", e.Message), ephemeral: true);
                throw;
            }

            character.Effects.AggregateEffects.Add(effect);
            
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
            (await depot.GetCharacterAsync()).Effects.CoreEffects.Select(e => e.Name), 
            s => s);
        
        await autocomplete.RespondAsync();
    }

    [SlashCommand("remove", "Удалить эффект")]
    public async Task RemoveAsync([Summary("имя", "Название эффекта")] [Autocomplete] string name)
    {
        var depot = new CharacterDepot(Db, Context);
            
        var character = await depot.GetCharacterAsync();

        var effect = character.Effects.CoreEffects.First(e => e.Name == name);

        ((IEffectAggregator) character.Effects).RemoveEffect(effect);
        
        if (!character.IsValid)
        {
            await RespondAsync(embed: EmbedView.Error(character.Errors), ephemeral: true);
            return;
        }

        await depot.UpdateCharacterAsync(character);

        await RespondAsync($"{character.Name} теряет эффект {effect.View}");
    }
}