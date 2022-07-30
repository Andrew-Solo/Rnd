using Discord.Interactions;
using Microsoft.EntityFrameworkCore;
using RnDBot.Data;
using RnDBot.Models.Character;
using RnDBot.Models.Common;
using RnDBot.Models.Glossaries;
using RnDBot.Views;
using ValueType = RnDBot.Views.ValueType;

namespace RnDBot.Controllers;

[Group("сharacter", "Команды для управление персонажами")]
public class CharacterController : InteractionModuleBase<SocketInteractionContext>
{
    //Dependency Injections
    public DataContext Db { get; set; } = null!;

    [SlashCommand("list", "Отображает список всех персонажей")]
    public async Task ListAsync([Summary("показать", "Показать сообщение всем?")] bool showAll = false)
    {
        var depot = new CharacterDepot(Db, Context);

        var field = new ListField("Энкорния", await depot.GetCharacterNamesAsync());
        var panel = new CommonPanel("Мои персонажи", field);
        
        await RespondAsync(embed: EmbedView.Build(panel), ephemeral: !showAll);
    }

    [AutocompleteCommand("имя", "chose")]
    public async Task ChoseNameAutocomplete()
    {
        var depot = new CharacterDepot(Db, Context);
        
        var autocomplete = new Autocomplete<string>(Context,  await depot.GetCharacterNamesAsync(), s => s);
        
        await autocomplete.RespondAsync();
    }

    [SlashCommand("chose", "Выбор активного персонажа")]
    public async Task ChoseAsync(
        [Autocomplete] [Summary("имя", "Имя выбираемого персонажа")] string name)
    {
        var depot = new CharacterDepot(Db, Context);

        var character =  await depot.DataCharacters.FirstAsync(c => c.Name == name);
        
        character.Selected = DateTime.Now;

        await Db.SaveChangesAsync();

        await RespondAsync($"Выбран персонаж **{character.Name}**.", ephemeral: true);
    }
    
    [SlashCommand("create", "Создание нового персонажа")]
    public async Task CreateAsync([Summary("имя", "Имя создаваемого персонажа")] string name)
    {
        var newCharacter = CharacterFactory.AncorniaCharacter(name);
        
        var depot = new CharacterDepot(Db, Context);
        
        if (!newCharacter.IsValid)
        {
            await RespondAsync(embed: EmbedView.Error(newCharacter.Errors), ephemeral: true);
            return;
        }

        if ((await depot.GetCharacterNamesAsync()).Contains(newCharacter.Name))
        {
            await RespondAsync(embed: EmbedView.Error(new []{"Персонаж с таким именем уже существует."}), ephemeral: true);
            return;
        }

        await depot.AddCharacterAsync(newCharacter);
        
        await RespondAsync($"Персонаж **{newCharacter.Name}** успешно создан и выбран как активный.", ephemeral: true);
    }

    [SlashCommand("rename", "Изменить имя выбранного персонажа")]
    public async Task RenameAsync([Summary("имя", "Новое имя персонажа")] string name)
    {
        var depot = new CharacterDepot(Db, Context);

        var character = await depot.GetCharacterAsync();
        
        character.Name = name;
        
        if (!character.IsValid)
        {
            await RespondAsync(embed: EmbedView.Error(character.Errors), ephemeral: true);
            return;
        }

        if ((await depot.GetCharacterNamesAsync()).Contains(character.Name))
        {
            await RespondAsync(embed: EmbedView.Error(new []{"Персонаж с таким именем уже существует."}), ephemeral: true);
            return;
        }

        await depot.UpdateCharacterAsync(character);
        
        await RespondAsync($"Персонаж теперь имеет имя **{character.Name}**.");
    }
    
    [SlashCommand("delete", "Удалить выбранного персонажа")]
    public async Task DeleteAsync()
    {
        var depot = new CharacterDepot(Db, Context);

        var character = await depot.GetDataCharacterAsync();

        Db.Characters.Remove(character);

        await Db.SaveChangesAsync();
        
        await RespondAsync($"Персонаж **{character.Name}** удален. Автоматически выбран последний активный персонаж.", ephemeral: true);
    }

    [Group("show", "Команды для отображения параметров текущего персонажа")]
    public class ShowController : InteractionModuleBase<SocketInteractionContext>
    {
        //Dependency Injections
        public DataContext Db { get; set; } = null!;

        [SlashCommand("all", "Отображение всех характеристик пероснажа")]
        public async Task AllAsync([Summary("показать", "Показать сообщение всем?")] bool showAll = false)
        {
            var depot = new CharacterDepot(Db, Context);

            var character = await depot.GetCharacterAsync();
            
            await RespondAsync(embeds: EmbedView.Build(character), ephemeral: !showAll);
        }

        [SlashCommand("general", "Отображение основной информации о персонаже")]
        public async Task GeneralAsync([Summary("показать", "Показать сообщение всем?")] bool showAll = false)
        {
            var depot = new CharacterDepot(Db, Context);

            var character = await depot.GetCharacterAsync();
            
            await RespondAsync(embed: EmbedView.Build(character.General), ephemeral: !showAll);
        }

        [SlashCommand("attributes", "Отображение атрибутов и развития персонажа")]
        public async Task AttributesAsync([Summary("показать", "Показать сообщение всем?")] bool showAll = false)
        {
            var depot = new CharacterDepot(Db, Context);

            var character = await depot.GetCharacterAsync();
            
            await RespondAsync(embed: EmbedView.Build(character.Attributes), ephemeral: !showAll);
        }

        [SlashCommand("points", "Отображение состояний и очков персонажа")]
        public async Task PointsAsync([Summary("показать", "Показать сообщение всем?")] bool showAll = false)
        {
            var depot = new CharacterDepot(Db, Context);

            var character = await depot.GetCharacterAsync();
            
            await RespondAsync(embed: EmbedView.Build(character.Pointers), ephemeral: !showAll);
        }

        [SlashCommand("skills", "Отображение навыков персонажа")]
        public async Task SkillsAsync([Summary("показать", "Показать сообщение всем?")] bool showAll = false)
        {
            var depot = new CharacterDepot(Db, Context);

            var character = await depot.GetCharacterAsync();
            
            await RespondAsync(embed: EmbedView.Build(character.Domains), ephemeral: !showAll);
        }

        //TODO abilities, items, reputation, backstory
    }
    
    [Group("set", "Команды для редактирования персонажа")]
    public class SetController : InteractionModuleBase<SocketInteractionContext>
    {
        //Dependency Injections
        public DataContext Db { get; set; } = null!;

        [SlashCommand("general", "Изменение основной информации о персонаже")]
        public async Task GeneralAsync(
            [Summary("описание","Краткая памятка до 800 знаков")] string? description = null,
            [Summary("культура","Культурные особенности персонажа")] string? culture = null,
            [Summary("возраст","Цифра от 0 до 999")] string? age = null,
            [Summary("идеалы", "Разделяйте иделы запятыми")] string? ideals = null,
            [Summary("пороки", "Разделяйте пороки запятыми")] string? vices = null,
            [Summary("черты", "Разделяйте черты запятыми")] string? traits = null
            )
        {
            var depot = new CharacterDepot(Db, Context);

            var character = await depot.GetCharacterAsync();

            if (description != null) character.General.Description = description;
            if (culture != null) character.General.Culture.TValue = culture;
            if (age != null) character.General.Age.TValue = age;
            
            if (ideals != null) character.General.Ideals.Values = ideals.Split(",").Select(i => i.Trim()).ToList();
            if (vices != null) character.General.Vices.Values = vices.Split(",").Select(i => i.Trim()).ToList();
            if (traits != null) character.General.Traits.Values = traits.Split(",").Select(i => i.Trim()).ToList();
            
            if (!character.IsValid)
            {
                await RespondAsync(embed: EmbedView.Error(character.Errors), ephemeral: true);
                return;
            }
            
            await depot.UpdateCharacterAsync(character);
            
            await RespondAsync("Основная информация отредактирована.", embed: EmbedView.Build(character.General), ephemeral: true);
        }

        [SlashCommand("attributes", "Изменение атрибутов и уровня персонажа")]
        public async Task AttributesAsync(
            [Summary("сил","Сила")] int? str = null,
            [Summary("тел","Телосложение")] int? end = null,
            [Summary("лов","Ловкость")] int? dex = null,
            [Summary("вос","Восприятие")] int? per = null,
            [Summary("инт","Интеллект")] int? intl = null,
            [Summary("муд","Мудрость")] int? wis = null,
            [Summary("хар","Харизма")] int? cha = null,
            [Summary("реш","Решимость")] int? det = null)
        {
            var depot = new CharacterDepot(Db, Context);

            var character = await depot.GetCharacterAsync();

            character.Attributes.SetCoreAttributes(str, end, dex, per, intl, wis, cha, det);
            
            if (!character.IsValid)
            {
                await RespondAsync(embed: EmbedView.Error(character.Errors), ephemeral: true);
                return;
            }
            
            await depot.UpdateCharacterAsync(character);
            
            await RespondAsync("Атрибуты успешно отредактированы.", embed: EmbedView.Build(character.Attributes), ephemeral: true);
        }

        [SlashCommand("points", "Изменение состояний и очков персонажа")]
        public async Task PointsAsync(
            [Summary("драма","Очки драмы от -3 до 3")] int? drama = null,
            [Summary("способности","Очки способностей")] int? ability = null,
            [Summary("тело","Очки здоровья тела")] int? body = null,
            [Summary("воля","Очки здоровья воли")] int? will = null,
            [Summary("броня","Очки прочности брони")] int? armor = null,
            [Summary("барьер","Очки прочности барьера")] int? barrier = null)
        {
            var depot = new CharacterDepot(Db, Context);

            var character = await depot.GetCharacterAsync();
            
            character.Pointers.SetCorePointers(drama, ability, body, will, armor, barrier);
            
            if (!character.IsValid)
            {
                await RespondAsync(embed: EmbedView.Error(character.Errors), ephemeral: true);
                return;
            }
            
            await depot.UpdateCharacterAsync(character);
            
            await RespondAsync("Состояния успешно отредактированы.", embed: EmbedView.Build(character.Pointers), ephemeral: true);
        }
        
        [AutocompleteCommand("навык", "skill")]
        public async Task SkillNameAutocomplete()
        {
            var autocomplete = new Autocomplete<string>(Context, 
                Glossary.AncorniaSkillNamesReversed.Keys, 
                s => s);
        
            await autocomplete.RespondAsync();
        }

        [SlashCommand("skill", "Изменение уровня навыка персонажа")]
        public async Task SkillsAsync(
            [Summary("навык", "Название изменяемого навыка")] [Autocomplete] string name,
            [Summary("уровень", "Устанавливаемый уровень навыка")] int level = 0)
        {
            var depot = new CharacterDepot(Db, Context);

            var character = await depot.GetCharacterAsync();
            
            var type = Glossary.AncorniaSkillNamesReversed[name];
            var skill = character.Domains.CoreSkills.First(s => s.SkillType == type);
            skill.Value = level;

            if (!character.IsValid)
            {
                await RespondAsync(embed: EmbedView.Error(character.Errors), ephemeral: true);
                return;
            }

            await depot.UpdateCharacterAsync(character);

            var skillLevel = skill.Value;

            await RespondAsync($"Навык **{name}** установлен на уровень `{skillLevel}`.", ephemeral: true);
        }

        //TODO abilities, items, reputation, backstory
    }
    
    [Group("up", "Команды для повышения характеристик персонажа")]
    public class UpController : InteractionModuleBase<SocketInteractionContext>
    {
        //Dependency Injections
        public DataContext Db { get; set; } = null!;
        
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
            [Summary("уровень", "Прибавляемый к навыку уровень")] int level = 1,
            [Summary("показать", "Показать сообщение всем?")] bool showAll = false)
        {
            var depot = new CharacterDepot(Db, Context);

            var character = await depot.GetCharacterAsync();
            
            var type = Glossary.AncorniaSkillNamesReversed[name];
            var skill = character.Domains.CoreSkills.First(s => s.SkillType == type);
            skill.Value += level;

            if (!character.IsValid)
            {
                await RespondAsync(embed: EmbedView.Error(character.Errors), ephemeral: true);
                return;
            }

            await depot.UpdateCharacterAsync(character);

            var domain = character.Domains.CoreDomains
                .First(d => d.Skills.Any(s => s.SkillType == type));

            var skillLevel = domain.DomainLevel + skill.Value;
            var maxSkillLevel = domain.DomainLevel + character.Domains.MaxSkillLevel;
            var power = character.Attributes.Power;

            await RespondAsync($"Навык **{name}** улучшен до уровня `{skillLevel}`.\n" + 
                               $"Максимальный уровень этого навыка `{maxSkillLevel}`.\n" +
                               $"Осталось свободной мощи `{power.Max - power.Current}`.",
                ephemeral: !showAll);
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
        public async Task LevelAsync(
            [Summary("атрибут", "Название атрибута для улучшения")] [Autocomplete] string name,
            [Summary("показать", "Показать сообщение всем?")] bool showAll = false)
        {
            var depot = new CharacterDepot(Db, Context);

            var character = await depot.GetCharacterAsync();
            
            var type = Glossary.AttributeNamesReversed[name];
            var attribute = character.Attributes.CoreAttributes.First(a => a.AttributeType == type);
            attribute.Modifier += 1;
            
            if (!character.IsValid)
            {
                await RespondAsync(embed: EmbedView.Error(character.Errors), ephemeral: true);
                return;
            }
            
            await depot.UpdateCharacterAsync(character);

            var power = character.Attributes.Power;
            var attrLevel = EmbedView.Build(attribute.Modifier, ValueType.InlineModifier);
            var maxAttrLevel = EmbedView.Build(character.Attributes.MaxAttribute, ValueType.InlineModifier);

            await RespondAsync($"Уровень **{character.Name}** увеличен до `{character.Attributes.Level}`\n" +
                               $"Атрибут **{name}** улучшен до уровня {attrLevel}.\n" +
                               $"Максимальный уровень атрибута {maxAttrLevel}.\n" +
                               $"Осталось свободной мощи `{power.Max - power.Current}`.",
                ephemeral: !showAll);
        }
    }
}