using System.Dynamic;
using Discord.Interactions;
using Rnd.Bot.Discord.Views.Panels;
using Rnd.Constants;

namespace Rnd.Bot.Discord.Controllers;

public class MainController : InteractionModuleBase<SocketInteractionContext>
{
    // public MainController()
    // {
    //     var config = Setup.Configuration;
    //     Data = new AirtableBase(config.AirtableToken, config.Games[config.DefaultGame]);
    // }
    //
    // ~MainController()
    // {
    //     Data.Dispose();
    // }
    //
    // public AirtableBase Data { get; private set; }
    //
    // public async Task GameAutocomplete()
    // {
    //     var autocomplete = new Autocomplete<KeyValuePair<string, string>>(
    //         Setup.Configuration.Games, 
    //         s => s.Key,
    //         s => s.Value
    //     );
    //     
    //     await autocomplete.RespondAsync(Context);
    // }
    //
    // public async Task CharacterAutocomplete()
    // {
    //     var response = await Data.ListAsync(Table.Characters);
    //     
    //     if (response.IsFailed) return;
    //
    //     var first = response.Value.First();
    //
    //     var value = first.Get<string>(Character.Name);
    //     
    //     var values = response.Value
    //         .Select(r => r.Get<string>(Character.Name))
    //         .Where(s => s != null)
    //         .Cast<string>()
    //         .ToList();
    //     
    //     var autocomplete = new Autocomplete<string>(
    //         values, 
    //         s => s
    //     );
    //     
    //     await autocomplete.RespondAsync(Context);
    // }
    //
    // [AutocompleteCommand("игра", "выбор_игры")]
    // public async Task ChoseGameAutocomplete() => await GameAutocomplete();
    //
    // [SlashCommand("выбор_игры", "Выбрать активную игру")]
    // public async Task ChoseGameAsync(
    //     [Summary("игра", "Имя игры"), Autocomplete] string game
    // )
    // {
    //     Data.Dispose();
    //     Data = new AirtableBase(Setup.Configuration.AirtableToken, game);
    //     await this.EmbedResponseAsync(PanelBuilder.WithTitle("Игра изменена").AsSuccess());
    // }
    //
    // [AutocompleteCommand("персонаж", "бросок")]
    // public async Task RollAutocomplete() => await CharacterAutocomplete();

    [SlashCommand("бросок", "Выполнить проверку навыка")]
    public async Task RollAsync(
        //[Summary("персонаж", "Персонаж выполнящий проверку"), Autocomplete] string character,
        [Summary("атрибут", "Значение атрибута")] int attribute,
        [Summary("профессия", "Значение профессии")] int profession,
        [Summary("навык", "Значение навыка")] int skill,
        [Summary("преимущество", "Количество преимуществ, или помех, если меньше нуля")] int advantage = 0,
        [Summary("урон", "Бонус к урону от оружия или способности")] int damage = 0,
        [Summary("драма", "Количество вложенных очков драмы")] int drama = 0
    )
    {
        var role = Context.Guild.Users.First(u => u.Id == Context.User.Id).Roles.Last();
        var roll = new Roll(attribute, profession, skill, advantage, damage, drama);
        PanelBuilder panel = PanelBuilder.ByObject(roll.GetView());
        await this.EmbedResponseAsync(panel.WithColor(role.Color), false);
    } 
}

public class Roll
{
    public Roll(
        int attribute,
        int profession,
        int skill, 
        int advantage = 0,
        int bonusDamage = 0,
        int drama = 0
    )
    {
        Attribute = attribute;
        Profession = profession;
        Skill = skill;
        Advantage = advantage;
        BonusDamage = bonusDamage;
        Drama = drama;
        Dices = Rand.Roll(3, 20, advantage, drama);
        CritDices = Rand.Roll(Crits, 6).Union(Rand.Roll(Misscrits, 6).Select(d => d * -1)).ToList();
    }
    
    public int Attribute { get; }
    public int Profession { get; }
    public int Skill { get; }
    public int Advantage { get; }
    public int BonusDamage { get; }
    public int Drama { get; }

    public List<int> Dices { get; }
    public List<int> CritDices { get; }
    public int Crits => Dices.Count(d => d == 20);
    public int Misscrits => Dices.Count(d => d == 1);
    public int CritValue => CritDices.Sum();

    public List<int> Values => new()
    {
        Dices[0] + Attribute + CritValue - 14,
        Dices[1] + Profession + CritValue - 14,
        Dices[2] + Skill + CritValue - 14,
    };

    public int Sum => Result + Price;
    public int Result => Values.Where(v => v > 0).Sum();
    public int Price => Values.Where(v => v < 0).Sum();
    public int Damage => Result + BonusDamage;
    
    
    public dynamic GetView()
    {
        dynamic result = new ExpandoObject();

        result.Title = $"Бросок {Attribute}/{Profession}/{Skill}";
        
        if (Advantage != 0) result.Title += $" Пр{(Advantage > 0 ? "+" : "")}{Advantage}";
        if (BonusDamage != 0) result.Title += $" Ур{(BonusDamage > 0 ? "+" : "")}{BonusDamage}";
        if (Drama != 0) result.Title += $" ОД{(Drama > 0 ? "+" : "")}{Drama}";
        
        result.Итог = Sum;
        result.Цена = Price;
        result.Урон = Damage;

        return result;
    }
}