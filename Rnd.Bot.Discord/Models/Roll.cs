using System.Dynamic;
using Rnd.Constants;

namespace Rnd.Bot.Discord.Models;

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