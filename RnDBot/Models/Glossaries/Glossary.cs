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
        [DamageType.Physical] = "Физический",
        [DamageType.Mental] = "Ментальный",
        [DamageType.Magic] = "Магический",
    };

    public static Dictionary<string, DamageType> DamageNamesReversed =>
        DamageNames.ReverseKeyToValue();
    
    public static readonly Dictionary<DamageType, PointerType> DamageArmor = new()
    {
        [DamageType.Physical] = PointerType.Armor,
        [DamageType.Mental] = PointerType.Barrier,
        [DamageType.Magic] = PointerType.Barrier,
    };
    
    public static readonly Dictionary<DamageType, PointerType> DamageHit = new()
    {
        [DamageType.Physical] = PointerType.Body,
        [DamageType.Mental] = PointerType.Will,
        [DamageType.Magic] = PointerType.Body,
    };
    
    public static readonly Dictionary<TraumaState, string> TraumaStateName = new()
    {
        [TraumaState.Unstable] = "Нестабильная",
        [TraumaState.Stable] = "Стабильная",
        [TraumaState.Chronic] = "Хроническая",
    };

    public static readonly Dictionary<TraumaType, string> TraumaTypeName = new()
    {
        [TraumaType.Deadly] = "Смертельная",
        [TraumaType.Critical] = "Критическая",
        [TraumaType.Heavy] = "Тяжелая",
        [TraumaType.Light] = "Легкая",
    };
    
    public static readonly Dictionary<DamageType, string> TraumaDamageTypeName = new()
    {
        [DamageType.Physical] = "Механическая",
        [DamageType.Mental] = "Психическая",
        [DamageType.Magic] = "Магическая",
    };

    public static string GetTraumaName(TraumaType traumaType, DamageType damageType, TraumaState traumaState)
    {
        return $"{TraumaStateName[traumaState]} {TraumaTypeName[traumaType].ToLower()} " +
               $"{TraumaDamageTypeName[damageType].ToLower()} травма";
    }
    
    public static string GetTraumaEffectName(DamageType damageType, AttributeType attributeType, int fine)
    {
        var traumaType = fine switch
        {
            < -6 => TraumaType.Deadly,
            < -4 => TraumaType.Critical,
            < -2 => TraumaType.Heavy,
            < 0 => TraumaType.Light,
            _ => throw new InvalidOperationException()
        };

        var name = (attributeType, damageType, traumaType) switch
        {
            //TODO перечислить всё
            (AttributeType.Str, DamageType.Physical, TraumaType.Light) => fine % 2 == 0 ? "Вывих руки" : "Рана",
            (AttributeType.Str, DamageType.Physical, TraumaType.Heavy) => fine % 2 == 0 ? "Перелом руки" : "Глубокая рана",
            (AttributeType.Str, DamageType.Physical, TraumaType.Critical) => fine % 2 == 0 ? "Открытый перелом" : "Дробление руки",
            (AttributeType.Str, DamageType.Physical, TraumaType.Deadly) => fine % 2 == 0 ? "Потеря руки и плеча" : "Потеря руки",
            (AttributeType.End, DamageType.Physical, TraumaType.Light) => fine % 2 == 0 ? "Треснувшие ребра" : "Инородный объект",
            (AttributeType.End, DamageType.Physical, TraumaType.Heavy) => fine % 2 == 0 ? "Разрыв селезенки" : "Сломанные ребра",
            (AttributeType.End, DamageType.Physical, TraumaType.Critical) => fine % 2 == 0 ? "Коллапс легкого" : "Распоротый живот",
            (AttributeType.End, DamageType.Physical, TraumaType.Deadly) => fine % 2 == 0 ? "Травма сердца" : "Септический шок",
            (AttributeType.Dex, DamageType.Physical, TraumaType.Light) => fine % 2 == 0 ? "Вывих ноги" : "Вывих кисти",
            (AttributeType.Dex, DamageType.Physical, TraumaType.Heavy) => fine % 2 == 0 ? "Перелом ноги" : "Перелом пальцев",
            (AttributeType.Dex, DamageType.Physical, TraumaType.Critical) => fine % 2 == 0 ? "Открытый перелом ноги" : "Потеря пальцев",
            (AttributeType.Dex, DamageType.Physical, TraumaType.Deadly) => fine % 2 == 0 ? "Потеря ноги" : "Потеря кисти",
            (AttributeType.Per, DamageType.Physical, TraumaType.Light) => fine % 2 == 0 ? "Контузия" : "Боль",
            (AttributeType.Per, DamageType.Physical, TraumaType.Heavy) => fine % 2 == 0 ? "Повреждение глаза" : "Кровотечение уха",
            (AttributeType.Per, DamageType.Physical, TraumaType.Critical) => fine % 2 == 0 ? "Потеря уха" : "Смещение позвонкив",
            (AttributeType.Per, DamageType.Physical, TraumaType.Deadly) => fine % 2 == 0 ? "Потеря глаза" : "Перелом позвоночника",
            (AttributeType.Int, DamageType.Physical, TraumaType.Light) => fine % 2 == 0 ? "Контузия" : "Боль",
            (AttributeType.Int, DamageType.Physical, TraumaType.Heavy) => fine % 2 == 0 ? "Травма головы" : "Невыносимая боль",
            (AttributeType.Int, DamageType.Physical, TraumaType.Critical) => fine % 2 == 0 ? "Кровоизлияние в мозг" : "Сотрясение мозга",
            (AttributeType.Int, DamageType.Physical, TraumaType.Deadly) => fine % 2 == 0 ? "Повреждение мозга" : "Кровоизлияние в мозг",
            (AttributeType.Wis, DamageType.Physical, TraumaType.Light) => fine % 2 == 0 ? "Контузия" : "Боль",
            (AttributeType.Wis, DamageType.Physical, TraumaType.Heavy) => fine % 2 == 0 ? "Травма головы" : "Невыносимая боль",
            (AttributeType.Wis, DamageType.Physical, TraumaType.Critical) => fine % 2 == 0 ? "Кровоизлияние в мозг" : "Сотрясение мозга",
            (AttributeType.Wis, DamageType.Physical, TraumaType.Deadly) => fine % 2 == 0 ? "Лоботомия" : "Кровоизлияние в мозг",
            (AttributeType.Cha, DamageType.Physical, TraumaType.Light) => fine % 2 == 0 ? "Треснувшая челюсть" : "Огромный фингал",
            (AttributeType.Cha, DamageType.Physical, TraumaType.Heavy) => fine % 2 == 0 ? "Выбит зуб" : "Уродующий шрам",
            (AttributeType.Cha, DamageType.Physical, TraumaType.Critical) => fine % 2 == 0 ? "Выбиты передние зубы" : "Стесанный нос",
            (AttributeType.Cha, DamageType.Physical, TraumaType.Deadly) => fine % 2 == 0 ? "Потеря челюсти" : "Стесанное лицо",
            (AttributeType.Det, DamageType.Physical, TraumaType.Light) => fine % 2 == 0 ? "Контузия" : "Боль",
            (AttributeType.Det, DamageType.Physical, TraumaType.Heavy) => fine % 2 == 0 ? "Травма головы" : "Невыносимая боль",
            (AttributeType.Det, DamageType.Physical, TraumaType.Critical) => fine % 2 == 0 ? "Кровоизлияние в мозг" : "Сотрясение мозга",
            (AttributeType.Det, DamageType.Physical, TraumaType.Deadly) => fine % 2 == 0 ? "Кома" : "Делирий",
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
        [AncorniaDomainType.Craft] = "Ремесло",
        [AncorniaDomainType.Art] = "Искусство",
    };
    
    public static Dictionary<string, AncorniaDomainType> AncorniaDomainNamesReversed =>
        AncorniaDomainNames.ReverseKeyToValue();
    
    public static readonly Dictionary<AncorniaSkillType, string> AncorniaSkillNames = new()
    {
        [AncorniaSkillType.Bruteforce] = "Грубая сила",
        [AncorniaSkillType.Struggle] = "Борьба",
        [AncorniaSkillType.Fortitude] = "Стойкость",
        [AncorniaSkillType.Fencing] = "Фехтование",
        [AncorniaSkillType.HandToHandCombat] = "Рукопашный бой",
        [AncorniaSkillType.Throwing] = "Метание",
        [AncorniaSkillType.Shooting] = "Стрельба",
        [AncorniaSkillType.Riding] = "Верховая езда",
        [AncorniaSkillType.Alchemy] = "Алхимия",
        [AncorniaSkillType.Pyrokinetics] = "Пирокинетика",
        [AncorniaSkillType.Geomancy] = "Геомантия",
        [AncorniaSkillType.Aeroturgy] = "Аэротургия",
        [AncorniaSkillType.Hydrosophistry] = "Гидрософистика",
        [AncorniaSkillType.Enchantment] = "Зачарование",
        [AncorniaSkillType.Priesthood] = "Жречество",
        [AncorniaSkillType.Necromancy] = "Некромантия",
        [AncorniaSkillType.Demonology] = "Демонология",
        [AncorniaSkillType.Metamorphism] = "Метаморфизм",
        [AncorniaSkillType.Climbing] = "Лазанье",
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
        [AncorniaSkillType.Manipulation] = "Манипуляция",
        [AncorniaSkillType.Networking] = "Связи",
        [AncorniaSkillType.Authority] = "Авторитет",
        [AncorniaSkillType.SelfControl] = "Самообладание",
        [AncorniaSkillType.Research] = "Исследование",
        [AncorniaSkillType.Engineering] = "Инженерия",
        [AncorniaSkillType.Medicine] = "Медицина",
        [AncorniaSkillType.Nature] = "Природа",
        [AncorniaSkillType.History] = "История",
        [AncorniaSkillType.Society] = "Общество",
        [AncorniaSkillType.Blacksmith] = "Кузнечное дело",
        [AncorniaSkillType.Farming] = "Фермерство",
        [AncorniaSkillType.Mining] = "Шахтерское дело",
        [AncorniaSkillType.Construction] = "Строительство",
        [AncorniaSkillType.Leatherworking] = "Кожевничество",
        [AncorniaSkillType.Tailoring] = "Портняжное дело",
        [AncorniaSkillType.Jewelry] = "Ювелирное дело",
        [AncorniaSkillType.Music] = "Музыка",
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
        [AncorniaSkillType.Fortitude] = AttributeType.End,
        [AncorniaSkillType.Fencing] = AttributeType.Dex,
        [AncorniaSkillType.HandToHandCombat] = AttributeType.Dex,
        [AncorniaSkillType.Throwing] = AttributeType.Dex,
        [AncorniaSkillType.Shooting] = AttributeType.Per,
        [AncorniaSkillType.Riding] = AttributeType.Per,
        [AncorniaSkillType.Alchemy] = AttributeType.Per,
        [AncorniaSkillType.Pyrokinetics] = AttributeType.Int,
        [AncorniaSkillType.Geomancy] = AttributeType.Int,
        [AncorniaSkillType.Aeroturgy] = AttributeType.Int,
        [AncorniaSkillType.Hydrosophistry] = AttributeType.Int,
        [AncorniaSkillType.Enchantment] = AttributeType.Int,
        [AncorniaSkillType.Priesthood] = AttributeType.Wis,
        [AncorniaSkillType.Necromancy] = AttributeType.Wis,
        [AncorniaSkillType.Demonology] = AttributeType.Cha,
        [AncorniaSkillType.Metamorphism] = AttributeType.Det,
        [AncorniaSkillType.Climbing] = AttributeType.Str,
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
        [AncorniaSkillType.Blacksmith] = AttributeType.Str,
        [AncorniaSkillType.Farming] = AttributeType.End,
        [AncorniaSkillType.Mining] = AttributeType.End,
        [AncorniaSkillType.Construction] = AttributeType.End,
        [AncorniaSkillType.Leatherworking] = AttributeType.Dex,
        [AncorniaSkillType.Tailoring] = AttributeType.Cha,
        [AncorniaSkillType.Jewelry] = AttributeType.Dex,
        [AncorniaSkillType.Music] = AttributeType.Per,
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