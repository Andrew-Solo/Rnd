namespace RnDBot.Models.Glossaries;

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

    public static Dictionary<string, AttributeType> AttributeAbbreviationsReversed =>
        AttributeAbbreviations.ReverseKeyToValue();
    
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
    
    public static Dictionary<string, AttributeType> AttributeNamesReversed =>
        AttributeNames.ReverseKeyToValue();
    
    public static readonly Dictionary<PointerType, string> PointerNames = new()
    {
        [PointerType.Body] = "Тело",
        [PointerType.Will] = "Воля",
        [PointerType.Armor] = "Броня",
        [PointerType.Barrier] = "Барьер",
        [PointerType.Ability] = "Очки способностей",
        [PointerType.Drama] = "Очки драмы",
    };
    
    public static Dictionary<string, PointerType> PointerNamesReversed =>
        PointerNames.ReverseKeyToValue();
    
    public static readonly Dictionary<DamageType, string> DamageNames = new()
    {
        [DamageType.Pure] = "Чистый",
        [DamageType.Physical] = "Физический",
        [DamageType.Piercing] = "Пронзающий",
        [DamageType.Mental] = "Ментальный",
        [DamageType.Psychic] = "Психический",
        [DamageType.Elemental] = "Стихийный",
    };

    public static Dictionary<string, DamageType> DamageNamesReversed =>
        DamageNames.ReverseKeyToValue();
    
    public static readonly Dictionary<DamageType, PointerType?> DamageArmor = new()
    {
        [DamageType.Pure] = null,
        [DamageType.Physical] = PointerType.Armor,
        [DamageType.Piercing] = null,
        [DamageType.Mental] = PointerType.Barrier,
        [DamageType.Psychic] = null,
        [DamageType.Elemental] = PointerType.Barrier,
    };
    
    public static readonly Dictionary<DamageType, PointerType?> DamageHit = new()
    {
        [DamageType.Pure] = null,
        [DamageType.Physical] = PointerType.Body,
        [DamageType.Piercing] = PointerType.Body,
        [DamageType.Mental] = PointerType.Will,
        [DamageType.Psychic] = PointerType.Will,
        [DamageType.Elemental] = PointerType.Body,
    };
    
    public static readonly Dictionary<TraumaState, string> TraumaStateName = new()
    {
        [TraumaState.Unstable] = "Нестабильная",
        [TraumaState.Stable] = "Стабильная",
        [TraumaState.Chronic] = "Хроническая",
    };

    public static Dictionary<string, TraumaState> TraumaStateNameReversed => TraumaStateName.ReverseKeyToValue();

    public static readonly Dictionary<TraumaType, string> TraumaTypeName = new()
    {
        [TraumaType.Deadly] = "Смертельная",
        [TraumaType.Critical] = "Критическая",
        [TraumaType.Heavy] = "Тяжелая",
        [TraumaType.Light] = "Легкая",
    };
    
    public static readonly Dictionary<DamageType, string> TraumaDamageTypeName = new()
    {
        [DamageType.Pure] = "Внутренняя",
        [DamageType.Physical] = "Механическая",
        [DamageType.Piercing] = "Колотая",
        [DamageType.Mental] = "Ментальная",
        [DamageType.Psychic] = "Психическая",
        [DamageType.Elemental] = "Стихийная",
    };

    public static string GetTraumaName(TraumaType traumaType, DamageType damageType, TraumaState traumaState)
    {
        return $"{TraumaStateName[traumaState]} {TraumaTypeName[traumaType].ToLower()} " +
               $"{TraumaDamageTypeName[damageType].ToLower()} травма";
    }
    
    public static string GetTraumaEffectName(DamageType damageType, AttributeType attributeType, int fine)
    {
        var name = (attributeType, damageType, fine) switch
        {
            //TODO перечислить всё
            (AttributeType.Str, DamageType.Physical or DamageType.Piercing, -1) => "Рана",
            (AttributeType.Str, DamageType.Physical or DamageType.Piercing, -2) => "Вывих руки",
            (AttributeType.Str, DamageType.Physical or DamageType.Piercing, -3) => "Глубокая рана",
            (AttributeType.Str, DamageType.Physical or DamageType.Piercing, -4) => "Перелом руки",
            (AttributeType.Str, DamageType.Physical or DamageType.Piercing, -5) => "Дробление руки",
            (AttributeType.Str, DamageType.Physical or DamageType.Piercing, -6) => "Открытый перелом",
            (AttributeType.Str, DamageType.Physical or DamageType.Piercing, -7) => "Потеря руки",
            (AttributeType.Str, DamageType.Physical or DamageType.Piercing, -8) => "Потеря руки и плеча",
            (AttributeType.End, DamageType.Physical or DamageType.Piercing, -1) => "Инородный объект",
            (AttributeType.End, DamageType.Physical or DamageType.Piercing, -2) => "Треснувшие ребра",
            (AttributeType.End, DamageType.Physical or DamageType.Piercing, -3) => "Сломанные ребра",
            (AttributeType.End, DamageType.Physical or DamageType.Piercing, -4) => "Разрыв селезенки",
            (AttributeType.End, DamageType.Physical or DamageType.Piercing, -5) => "Распоротый живот",
            (AttributeType.End, DamageType.Physical or DamageType.Piercing, -6) => "Коллапс легкого",
            (AttributeType.End, DamageType.Physical or DamageType.Piercing, -7) => "Септический шок",
            (AttributeType.End, DamageType.Physical or DamageType.Piercing, -8) => "Травма сердца",
            (AttributeType.Dex, DamageType.Physical or DamageType.Piercing, -1) => "Вывих кисти",
            (AttributeType.Dex, DamageType.Physical or DamageType.Piercing, -2) => "Вывих ноги",
            (AttributeType.Dex, DamageType.Physical or DamageType.Piercing, -3) => "Перелом пальцев",
            (AttributeType.Dex, DamageType.Physical or DamageType.Piercing, -4) => "Перелом ноги",
            (AttributeType.Dex, DamageType.Physical or DamageType.Piercing, -5) => "Потеря пальцев",
            (AttributeType.Dex, DamageType.Physical or DamageType.Piercing, -6) => "Открытый перелом ноги",
            (AttributeType.Dex, DamageType.Physical or DamageType.Piercing, -7) => "Потеря кисти",
            (AttributeType.Dex, DamageType.Physical or DamageType.Piercing, -8) => "Потеря ноги",
            (AttributeType.Per, DamageType.Physical or DamageType.Piercing, -1) => "Боль",
            (AttributeType.Per, DamageType.Physical or DamageType.Piercing, -2) => "Контузия",
            (AttributeType.Per, DamageType.Physical or DamageType.Piercing, -3) => "Кровотечение уха",
            (AttributeType.Per, DamageType.Physical or DamageType.Piercing, -4) => "Повреждение глаза",
            (AttributeType.Per, DamageType.Physical or DamageType.Piercing, -5) => "Смещение позвонкив",
            (AttributeType.Per, DamageType.Physical or DamageType.Piercing, -6) => "Потеря уха",
            (AttributeType.Per, DamageType.Physical or DamageType.Piercing, -7) => "Перелом позвоночника",
            (AttributeType.Per, DamageType.Physical or DamageType.Piercing, -8) => "Потеря глаза",
            (AttributeType.Int, DamageType.Physical or DamageType.Piercing, -1) => "Боль",
            (AttributeType.Int, DamageType.Physical or DamageType.Piercing, -2) => "Контузия",
            (AttributeType.Int, DamageType.Physical or DamageType.Piercing, -3) => "Невыносимая боль",
            (AttributeType.Int, DamageType.Physical or DamageType.Piercing, -4) => "Травма головы",
            (AttributeType.Int, DamageType.Physical or DamageType.Piercing, -5) => "Сотрясение мозга",
            (AttributeType.Int, DamageType.Physical or DamageType.Piercing, -6) => "Кровоизлияние в мозг",
            (AttributeType.Int, DamageType.Physical or DamageType.Piercing, -7) => "Кровоизлияние в мозг",
            (AttributeType.Int, DamageType.Physical or DamageType.Piercing, -8) => "Повреждение мозга",
            (AttributeType.Wis, DamageType.Physical or DamageType.Piercing, -1) => "Боль",
            (AttributeType.Wis, DamageType.Physical or DamageType.Piercing, -2) => "Контузия",
            (AttributeType.Wis, DamageType.Physical or DamageType.Piercing, -3) => "Невыносимая боль",
            (AttributeType.Wis, DamageType.Physical or DamageType.Piercing, -4) => "Травма головы",
            (AttributeType.Wis, DamageType.Physical or DamageType.Piercing, -5) => "Сотрясение мозга",
            (AttributeType.Wis, DamageType.Physical or DamageType.Piercing, -6) => "Кровоизлияние в мозг",
            (AttributeType.Wis, DamageType.Physical or DamageType.Piercing, -7) => "Кровоизлияние в мозг",
            (AttributeType.Wis, DamageType.Physical or DamageType.Piercing, -8) => "Лоботомия",
            (AttributeType.Cha, DamageType.Physical or DamageType.Piercing, -1) => "Огромный фингал",
            (AttributeType.Cha, DamageType.Physical or DamageType.Piercing, -2) => "Треснувшая челюсть",
            (AttributeType.Cha, DamageType.Physical or DamageType.Piercing, -3) => "Уродующий шрам",
            (AttributeType.Cha, DamageType.Physical or DamageType.Piercing, -4) => "Выбит зуб",
            (AttributeType.Cha, DamageType.Physical or DamageType.Piercing, -5) => "Стесанный нос",
            (AttributeType.Cha, DamageType.Physical or DamageType.Piercing, -6) => "Выбиты передние зубы",
            (AttributeType.Cha, DamageType.Physical or DamageType.Piercing, -7) => "Стесанное лицо",
            (AttributeType.Cha, DamageType.Physical or DamageType.Piercing, -8) => "Потеря челюсти",
            (AttributeType.Det, DamageType.Physical or DamageType.Piercing, -1) => "Боль",
            (AttributeType.Det, DamageType.Physical or DamageType.Piercing, -2) => "Контузия",
            (AttributeType.Det, DamageType.Physical or DamageType.Piercing, -3) => "Невыносимая боль",
            (AttributeType.Det, DamageType.Physical or DamageType.Piercing, -4) => "Травма головы",
            (AttributeType.Det, DamageType.Physical or DamageType.Piercing, -5) => "Сотрясение мозга",
            (AttributeType.Det, DamageType.Physical or DamageType.Piercing, -6) => "Кровоизлияние в мозг",
            (AttributeType.Det, DamageType.Physical or DamageType.Piercing, -7) => "Делирий",
            (AttributeType.Det, DamageType.Physical or DamageType.Piercing, -8) => "Кома",
            _ => "Эффект травмы"
        };

        return name;
    }

    public static readonly Dictionary<AncorniaDomainType, string> AncorniaDomainNames = new()
    {
        [AncorniaDomainType.War] = "Война",
        [AncorniaDomainType.Mist] = "Чудо",
        [AncorniaDomainType.Way] = "Путь",
        [AncorniaDomainType.Word] = "Слово",
        [AncorniaDomainType.Lore] = "Знание",
        [AncorniaDomainType.Work] = "Работа",
        [AncorniaDomainType.Art] = "Искусство",
    };
    
    public static Dictionary<string, AncorniaDomainType> AncorniaDomainNamesReversed =>
        AncorniaDomainNames.ReverseKeyToValue();
    
    public static readonly Dictionary<AncorniaSkillType, string> AncorniaSkillNames = new()
    {
        [AncorniaSkillType.Bruteforce] = "Грубая сила",
        [AncorniaSkillType.Struggle] = "Борьба",
        [AncorniaSkillType.Blocking] = "Блокирование",
        [AncorniaSkillType.Fortitude] = "Стойкость",
        [AncorniaSkillType.Fencing] = "Фехтование",
        [AncorniaSkillType.Throwing] = "Метание",
        [AncorniaSkillType.Shooting] = "Стрельба",
        [AncorniaSkillType.Riding] = "Езда",
        [AncorniaSkillType.Alchemy] = "Алхимия",
        [AncorniaSkillType.Magic] = "Волшебство",
        [AncorniaSkillType.Enchantment] = "Зачарование",
        [AncorniaSkillType.Priesthood] = "Жречество",
        [AncorniaSkillType.Necromancy] = "Некромантия",
        [AncorniaSkillType.Demonology] = "Демонология",
        [AncorniaSkillType.Metamorphism] = "Метаморфизм",
        [AncorniaSkillType.SleightOfHand] = "Ловкость рук",
        [AncorniaSkillType.Acrobatics] = "Акробатика",
        [AncorniaSkillType.Stealth] = "Скрытность",
        [AncorniaSkillType.Reaction] = "Реакция",
        [AncorniaSkillType.Tracking] = "Следопытство",
        [AncorniaSkillType.Navigation] = "Навигация",
        [AncorniaSkillType.Streets] = "Улицы",
        [AncorniaSkillType.Survival] = "Выживание",
        [AncorniaSkillType.Empathy] = "Эмпатия",
        [AncorniaSkillType.Rhetoric] = "Риторика",
        [AncorniaSkillType.Manipulation] = "Манипуляции",
        [AncorniaSkillType.Networking] = "Связи",
        [AncorniaSkillType.Authority] = "Авторитет",
        [AncorniaSkillType.SelfControl] = "Самообладание",
        [AncorniaSkillType.Research] = "Исследование",
        [AncorniaSkillType.Engineering] = "Инженерия",
        [AncorniaSkillType.Medicine] = "Медицина",
        [AncorniaSkillType.Nature] = "Природа",
        [AncorniaSkillType.History] = "История",
        [AncorniaSkillType.Society] = "Общество",
        [AncorniaSkillType.Science] = "Наука",
        [AncorniaSkillType.Metallurgy] = "Металлургия",
        [AncorniaSkillType.Farming] = "Фермерство",
        [AncorniaSkillType.Mining] = "Шахтерское дело",
        [AncorniaSkillType.Construction] = "Строительство",
        [AncorniaSkillType.Craft] = "Ремесло",
        [AncorniaSkillType.Mechanisms] = "Механизмы",
        [AncorniaSkillType.Chemistry] = "Химия",
        [AncorniaSkillType.Jewelry] = "Ювелирное дело",
        [AncorniaSkillType.Culture] = "Культура",
        [AncorniaSkillType.Creation] = "Творчество",
        [AncorniaSkillType.Inspiration] = "Вдохновение",
        [AncorniaSkillType.Performance] = "Выступление",
        [AncorniaSkillType.Artistry] = "Артистизм",
    };
    
    public static Dictionary<string, AncorniaSkillType> AncorniaSkillNamesReversed =>
        AncorniaSkillNames.ReverseKeyToValue();

    public static readonly Dictionary<AncorniaSkillType, AttributeType> AncorniaSkillCoreAttributes = new()
    {
        [AncorniaSkillType.Bruteforce] = AttributeType.Str,
        [AncorniaSkillType.Struggle] = AttributeType.Str,
        [AncorniaSkillType.Blocking] = AttributeType.End,
        [AncorniaSkillType.Fortitude] = AttributeType.End,
        [AncorniaSkillType.Fencing] = AttributeType.Dex,
        [AncorniaSkillType.Throwing] = AttributeType.Dex,
        [AncorniaSkillType.Shooting] = AttributeType.Per,
        [AncorniaSkillType.Riding] = AttributeType.Per,
        [AncorniaSkillType.Alchemy] = AttributeType.Per,
        [AncorniaSkillType.Magic] = AttributeType.Int,
        [AncorniaSkillType.Enchantment] = AttributeType.Int,
        [AncorniaSkillType.Priesthood] = AttributeType.Wis,
        [AncorniaSkillType.Necromancy] = AttributeType.Wis,
        [AncorniaSkillType.Demonology] = AttributeType.Wis,
        [AncorniaSkillType.Metamorphism] = AttributeType.Det,
        [AncorniaSkillType.SleightOfHand] = AttributeType.Dex,
        [AncorniaSkillType.Acrobatics] = AttributeType.Dex,
        [AncorniaSkillType.Stealth] = AttributeType.Dex,
        [AncorniaSkillType.Reaction] = AttributeType.Per,
        [AncorniaSkillType.Tracking] = AttributeType.Per,
        [AncorniaSkillType.Navigation] = AttributeType.Per,
        [AncorniaSkillType.Streets] = AttributeType.Wis,
        [AncorniaSkillType.Survival] = AttributeType.Det,
        [AncorniaSkillType.Empathy] = AttributeType.Wis,
        [AncorniaSkillType.Rhetoric] = AttributeType.Cha,
        [AncorniaSkillType.Manipulation] = AttributeType.Cha,
        [AncorniaSkillType.Networking] = AttributeType.Cha,
        [AncorniaSkillType.Authority] = AttributeType.Det,
        [AncorniaSkillType.SelfControl] = AttributeType.Det,
        [AncorniaSkillType.Research] = AttributeType.Per,
        [AncorniaSkillType.Engineering] = AttributeType.Int,
        [AncorniaSkillType.Medicine] = AttributeType.Int,
        [AncorniaSkillType.Nature] = AttributeType.Int,
        [AncorniaSkillType.History] = AttributeType.Int,
        [AncorniaSkillType.Society] = AttributeType.Int,
        [AncorniaSkillType.Science] = AttributeType.Int,
        [AncorniaSkillType.Metallurgy] = AttributeType.Str,
        [AncorniaSkillType.Farming] = AttributeType.End,
        [AncorniaSkillType.Mining] = AttributeType.End,
        [AncorniaSkillType.Construction] = AttributeType.End,
        [AncorniaSkillType.Craft] = AttributeType.Dex,
        [AncorniaSkillType.Mechanisms] = AttributeType.Dex,
        [AncorniaSkillType.Chemistry] = AttributeType.Per,
        [AncorniaSkillType.Jewelry] = AttributeType.Dex,
        [AncorniaSkillType.Culture] = AttributeType.Int,
        [AncorniaSkillType.Creation] = AttributeType.Wis,
        [AncorniaSkillType.Inspiration] = AttributeType.Wis,
        [AncorniaSkillType.Performance] = AttributeType.Cha,
        [AncorniaSkillType.Artistry] = AttributeType.Cha,
    };

    public static string GetDomainName<TDomain>(TDomain domain)
    {
        return domain switch
        {
            AncorniaDomainType ancornia => AncorniaDomainNames[ancornia],
            _ => throw new ArgumentOutOfRangeException(nameof(domain), domain, null)
        };
    }
    
    public static string GetSkillName<TSkill>(TSkill skill)
    {
        return skill switch
        {
            AncorniaSkillType ancornia => AncorniaSkillNames[ancornia],
            _ => throw new ArgumentOutOfRangeException(nameof(skill), skill, null)
        };
    }
    
    public static AttributeType GetSkillCoreAttribute<TSkill>(TSkill skill)
    {
        return skill switch
        {
            AncorniaSkillType ancornia => AncorniaSkillCoreAttributes[ancornia],
            _ => throw new ArgumentOutOfRangeException(nameof(skill), skill, null)
        };
    }

    private static Dictionary<T1, T2> ReverseKeyToValue<T1, T2>(this Dictionary<T2, T1> dictionary) 
        where T1 : notnull 
        where T2 : notnull
    {
        return dictionary.ToDictionary(pair => pair.Value, pair => pair.Key);
    }
}