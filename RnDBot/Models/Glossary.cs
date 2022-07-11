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
}