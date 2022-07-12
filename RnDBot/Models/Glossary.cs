using RnDBot.Models.Settings;

namespace RnDBot.Models;

public static class Glossary
{
    public static readonly Dictionary<AttributeType, string> AttributeAbbreviations = new()
    {
        [AttributeType.Str] = "СИЛ",
        [AttributeType.End] = "ТЕЛ",
        [AttributeType.Dex] = "ЛОВ",
        [AttributeType.Per] = "ВОС",
        [AttributeType.Int] = "ИНТ",
        [AttributeType.Wis] = "МУД",
        [AttributeType.Cha] = "ХАР",
        [AttributeType.Det] = "РЕШ",
    };
    
    public static readonly Dictionary<AttributeType, string> AttributeNames = new()
    {
        [AttributeType.Str] = "Сила",
        [AttributeType.End] = "Телосложение",
        [AttributeType.Dex] = "Ловкость",
        [AttributeType.Per] = "Восприятие",
        [AttributeType.Int] = "Интеллект",
        [AttributeType.Wis] = "Мудрость",
        [AttributeType.Cha] = "Харизма",
        [AttributeType.Det] = "Решимость",
    };
    
    public static readonly Dictionary<ConditionType, string> ConditionNames = new()
    {
        [ConditionType.Body] = "Тело",
        [ConditionType.Will] = "Воля",
        [ConditionType.Armor] = "Броня",
        [ConditionType.Barrier] = "Барьер",
        [ConditionType.Ap] = "Очки способностей",
    };

    public static readonly Dictionary<AncorniaDomainType, string> AncorniaDomainNames = new()
    {

    };
    
    public static readonly Dictionary<AncorniaSkillType, string> AncorniaSkillNames = new()
    {

    };

    public static string GetDomainDictionaryValue<TDomain>(TDomain domain)
    {
        return domain switch
        {
            AncorniaDomainType ancornia => AncorniaDomainNames[ancornia],
            _ => throw new ArgumentOutOfRangeException(nameof(domain), domain, null)
        };
    }
    
    public static string GetSkillDictionaryValue<TSkill>(TSkill skill)
    {
        return skill switch
        {
            AncorniaSkillType ancornia => AncorniaSkillNames[ancornia],
            _ => throw new ArgumentOutOfRangeException(nameof(skill), skill, null)
        };
    }
}