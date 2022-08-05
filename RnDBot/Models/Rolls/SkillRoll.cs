using System.Collections.ObjectModel;
using System.Text;
using Dice;
using Discord;
using RnDBot.Models.Character.Fields;
using RnDBot.Models.Common;
using RnDBot.Views;
using Attribute = RnDBot.Models.Character.Fields.Attribute;
using ValueType = RnDBot.Views.ValueType;

namespace RnDBot.Models.Rolls;

public class SkillRoll<TSkill> : IPanel where TSkill : struct
{
    public SkillRoll(Attribute attribute, Skill<TSkill> skill, bool isNearDeath, string? footer, int advantages = 0, int modifier = 0)
    {
        Attribute = attribute;
        Skill = skill;
        IsNearDeath = isNearDeath;
        Footer = footer;
        Advantages = advantages;
        Modifier = modifier;

        _results = new List<int>();
    }

    public Attribute Attribute { get; }
    public Skill<TSkill> Skill { get; }
    public int Advantages { get; set; }
    public int Modifier { get; set; }
    public bool IsNearDeath { get; set; }

    public IReadOnlyCollection<int> Results => new ReadOnlyCollection<int>(_results);
    public int SkillResult { get; private set; }
    public int Crits { get; private set; }
    public int Misscrits { get; private set; }
    
    public int Result { get; private set; }
    public int Tricks { get; private set; }
    public int Price { get; private set; }
    
    public bool IsCrit => Crits == 2;
    public bool IsSuperCrit => Crits == 3;
    public bool IsMisscrit => Misscrits == 2;
    public bool IsSuperMisscrit => Misscrits == 3;
    public bool WithAdvantage => Advantages > 0;
    public bool WithDisadvantage => Advantages < 0;
    public bool WithModifier => Modifier != 0;

    public void Roll()
    {
        _results.Clear();
        
        _results.Add((int) Roller.Roll("1d6").Value);
        _results.Add((int) Roller.Roll("1d6").Value);

        Crits = _results.Count(x => x == 6);

        if (!IsNearDeath)
        {
            var diceNumber = 1 + Math.Abs(Advantages);

            if (Skill.Value < 2)
            {
                SkillResult = 0;
            }
            else
            {
                SkillResult = (int) Roller.Roll($"{diceNumber}d{Skill.Value}k{(Advantages >= 0 ? "h" : "l")}1").Value;

                if (Skill.Value < 4)
                {
                    SkillResult--;
                    if (Roller.Roll("1d4").Value == 4) Crits++;
                }
                else
                {
                    if (SkillResult == Skill.Value) Crits++;
                }
            }
            
            _results.Add(SkillResult);
        }

        Misscrits = _results.Count(x => x == 1); 

        if (Skill.Value > 1 && Crits >= 2)
        {
            _results.Add((int) Roller.Roll($"1d{Skill.Value}").Value);
            
            if (IsSuperCrit) _results.Add((int) Roller.Roll($"1d{Skill.Value}").Value);
        }
        else if (Misscrits >= 2)
        {
            _results.Add((int) Roller.Roll($"1d6").Value * -1);
            
            if (IsSuperMisscrit) _results.Add((int) Roller.Roll($"1d6").Value * -1);
        }

        Result = _results.Sum() + Attribute.Modifier + Modifier;
        
        Price = _results.Count(x => x <= 1);

        Tricks = _results.Count(x => x is >= 6 and < 12) + 
                     2 * _results.Count(x => x is >= 12 and < 18) +
                     3 * _results.Count(x => x is >= 18 and < 24) + 
                     4 * _results.Count(x => x is >= 24 and < 100) +
                     5 * _results.Count(x => x > 100);
    }
    
    public string Title => $"Проверка **{Skill.Name}** `{Skill.Value}`";
    public string Description
    {
        get
        {
            var sb = new StringBuilder();
            
            sb.AppendLine($"{Attribute.Name} {EmbedView.Build(Attribute.Value, ValueType.InlineModifier)}");

            if (IsCrit) sb.AppendLine("**Критический!**");
            if (IsSuperCrit) sb.AppendLine("**Cуперкритический!**");
        
            if (IsMisscrit) sb.AppendLine("**Провальный!**");
            if (IsSuperMisscrit) sb.AppendLine("**Суперпровальный!**");
        
            if (WithAdvantage) sb.AppendLine($"Преимущества: `{Math.Abs(Advantages)}`");
            if (WithDisadvantage) sb.AppendLine($"Помехи: `{Math.Abs(Advantages)}`");

            if (WithModifier) sb.AppendLine($"Модификатор {EmbedView.Build(Modifier, ValueType.InlineModifier)}");
        
            if (IsNearDeath) sb.AppendLine("*Персонаж присмерти!*");

            return sb.ToString();
        }
    }

    public List<IField> Fields  => new()
    {
        new TextField<int>("Результат", Result),
        new TextField<int>("Трюки", Tricks),
        new TextField<int>("Цена", Price),
    };

    public Color? Color => Discord.Color.Blue;

    public string? Footer { get; }

    private readonly List<int> _results;
}