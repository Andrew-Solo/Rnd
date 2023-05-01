using System.Dynamic;
using Newtonsoft.Json;
using Rnd.Constants;

namespace Rnd.Bot.Discord.Models;

public class Roll
{
    public Roll(
        int skill, 
        int advantage = 0,
        int bonusDamage = 0,
        DamageType damageType = DamageType.Бонусный,
        int rock = 0
    )
    {
        Skill = skill;
        Advantage = advantage;
        BonusDamage = bonusDamage;
        DamageType = damageType;
        Rock = rock;
        Dices = new List<int>();
        Next();
    }
    
    public int Skill { get; }
    public int Advantage { get; }
    public int BonusDamage { get; }
    public DamageType DamageType { get; }
    public int Rock { get; }

    public List<int> Dices { get; }
    public int Tricks => Dices.Count(d => d == 10);
    public int Price => Dices.Count(d => d < 0);
    public int Result => Dices.Sum() + Skill - 16;
    public int Damage => Result + GetDamage();

    public void Next()
    {
        Dices.Clear();
        var dices = Rand.Roll(3, 10, Advantage, Rock);
        
        foreach (var dice in dices)
        {
            Dices.AddRange(Explode(dice));
        }

        if (Rock > 3)
        {
            Dices.AddRange(Rand.Range(Rock - 3, 10));
        }
    }

    private List<int> Explode(int value, int direction = 0, List<int>? added = null)
    {
        var dices = added ?? new List<int>();
        dices.Add(value * (direction < 0 ? -1 : 1));
        
        if (direction >= 0 && value == 10)
        {
            return Explode(Rand.Roll(10), 1, dices);
        }
        
        if (direction <= 0 && value == 1)
        {
            return Explode(Rand.Roll(10), -1, dices);
        }

        return dices;
    }

    private int GetDamage()
    {
        return DamageType switch
        {
            DamageType.Бонусный => BonusDamage,
            DamageType.Рубящий => BonusDamage,
            DamageType.Колющий => (int) Math.Ceiling(BonusDamage * 1.3),
            DamageType.Дробящий => (int) Math.Ceiling(BonusDamage * 0.7),
            DamageType.Взрывной => BonusDamage,
            DamageType.Стихийный => BonusDamage,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    private string GetArmorDamage()
    {
        return DamageType switch
        {
            DamageType.Бонусный => "",
            DamageType.Рубящий => "1 При попадании",
            DamageType.Колющий => "0",
            DamageType.Дробящий => "1 Надежно",
            DamageType.Взрывной => "1 Надежно",
            DamageType.Стихийный => "0",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    public dynamic GetView()
    {
        dynamic result = new ExpandoObject();

        result.Title = "";
        if (Damage == Result) result.Title += "Результат ";
        result.Title += $"{Damage}";
        if (Damage != Result) result.Title += $" {DamageType} урон";
        
        if (Tricks != 0) result.Трюки = Tricks;
        if (Price != 0) result.Цена = Price;
        if (DamageType != DamageType.Бонусный) result.Урон_броне = GetArmorDamage();
        if (Damage != Result) result.Результат = Result;
        
        result.Description = $"Навык {Skill}";
        if (BonusDamage != 0) result.Description += $" {(BonusDamage > 0 ? "+" : "")}{BonusDamage}";
        if (Advantage != 0) result.Description += Advantage > 0 ? $"\nПремущества +{Advantage}" : $"\nПомехи {Advantage}";
        if (Rock != 0) result.Description += $"\nРок {(Rock > 0 ? "+" : "")}{Rock}";
        result.Description += $"\nКости ({String.Join(", ", Dices)})";

        return result;
    }
}