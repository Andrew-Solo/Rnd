using System.Dynamic;
using Rnd.Constants;

namespace Rnd.Bot.Discord.Models;

public class Roll
{
    public Roll(
        int skill, 
        int advantage = 0,
        int bonusDamage = 0,
        int drama = 0
    )
    {
        Skill = skill;
        Advantage = advantage;
        BonusDamage = bonusDamage;
        Drama = drama;
        Dices = new List<int>();
        Next();
    }
    
    public int Skill { get; }
    public int Advantage { get; }
    public int BonusDamage { get; }
    public int Drama { get; }

    public List<int> Dices { get; }
    public int Tricks => Dices.Where(d => d > 10).Select(d => (int) Math.Ceiling((double) (d - 10) / 10)).Sum();
    public int Price => Dices.Where(d => d < 1).Select(d => (int) Math.Ceiling((double) (d - 1) / -10)).Sum();
    public int Result => Dices.Sum() + Skill - 18;
    public int Damage => Result + BonusDamage;

    public void Next()
    {
        Dices.Clear();
        var dices = Rand.Roll(3, 10, Advantage, Drama);
        Dices.AddRange(dices.Select(d => Explode(d)));
    }

    private int Explode(int value, int direction = 0)
    {
        return value switch
        {
            10 when direction >= 0 => value + Explode(Rand.Roll(10), 1),
            1 when direction <= 0 => value - Explode(Rand.Roll(10), 1),
            _ => value
        };
    }
    
    public dynamic GetView()
    {
        dynamic result = new ExpandoObject();

        result.Title = $"Бросок {Skill}";
        
        if (Advantage != 0) result.Title += $" Пр{(Advantage > 0 ? "+" : "")}{Advantage}";
        if (BonusDamage != 0) result.Title += $" Ур{(BonusDamage > 0 ? "+" : "")}{BonusDamage}";
        if (Drama != 0) result.Title += $" ОД{(Drama > 0 ? "+" : "")}{Drama}";
        
        result.Результат = Result;
        if (Damage != Result) result.Урон = Damage;
        if (Tricks != 0) result.Трюки = Tricks;
        if (Price != 0) result.Цена = Price;

        return result;
    }
}