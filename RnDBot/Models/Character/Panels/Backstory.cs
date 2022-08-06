using Newtonsoft.Json;
using RnDBot.Models.Common;
using RnDBot.Views;

namespace RnDBot.Models.Character.Panels;

public class Backstory : IPanel, IValidatable
{
    public Backstory(ICharacter character, IEnumerable<string>? goals = null, string? culture = null, string? society = null, 
        IEnumerable<string>? moral = null, string? traditions = null, string? mentor = null, IEnumerable<string>? lifepath = null, 
        IEnumerable<string>? habits = null)
    {
        Character = character;
        
        Goals = new ListField("Цели", goals);
        Outlook = new ListField("Нравственность", moral);
        
        Culture = new TextField<string?>("Культура", culture);
        Society = new TextField<string?>("Социум", society);
        Traditions = new TextField<string?>("Традиции", traditions);
        Mentor = new TextField<string?>("Наставник", mentor);
        
        Lifepath = new ListField("Жизненный путь", lifepath);
        Habits = new ListField("Привычки", habits);
    }
    
    [JsonConstructor]
    public Backstory(ICharacter character, ListField goals, ListField outlook, TextField<string?> culture, TextField<string?> society, 
        TextField<string?> traditions, TextField<string?> mentor, ListField lifepath, ListField habits)
    {
        Character = character;
        
        Goals = goals;
        Outlook = outlook;
        
        Culture = culture;
        Society = society;
        Traditions = traditions;
        Mentor = mentor;
        
        Lifepath = lifepath;
        Habits = habits;
    }

    [JsonIgnore]
    public ICharacter Character { get; }
    
    public ListField Goals { get; }
    public ListField Outlook { get; }

    public TextField<string?> Culture { get; }
    public TextField<string?> Society { get; }
    public TextField<string?> Traditions { get; }
    public TextField<string?> Mentor { get; }
    
    public ListField Lifepath { get; }
    
    public ListField Habits { get; }

    public void SetBackstory(string? goals = null, string? outlook = null, string? culture = null, string? society = null, 
        string? traditions = null, string? mentor = null, string? lifepath = null, string? habits = null)
    {
        if (goals != null) Goals.Values = GetList(goals);
        if (outlook != null) Outlook.Values = GetList(outlook);

        if (culture != null) Culture.TValue = culture;
        if (society != null) Society.TValue = society;
        if (traditions != null) Traditions.TValue = traditions;
        if (mentor != null) Mentor.TValue = mentor;
        
        if (lifepath != null) Lifepath.Values = GetList(lifepath);
        if (habits != null) Habits.Values = GetList(habits);
    }

    private List<string> GetList(string input)
    {
        return input.Split(",").Select(i => i.Trim()).ToList();
    }

    [JsonIgnore]
    public string Title => "Предыстория";

    [JsonIgnore] 
    public string Footer => Character.GetFooter;

    [JsonIgnore]
    public List<IField> Fields
    {
        get
        {
            var result = new List<IField>();

            if (Goals.Values?.Count > 0) result.Add(Goals);
            if (Outlook.Values?.Count > 0) result.Add(Outlook);
            
            if (Culture.TValue != null) result.Add(Culture);
            if (Society.TValue != null) result.Add(Society);
            if (Traditions.TValue != null) result.Add(Traditions);
            if (Mentor.TValue != null) result.Add(Mentor);
            
            if (Lifepath.Values?.Count > 0) result.Add(Lifepath);
            if (Habits.Values?.Count > 0) result.Add(Habits);
                
            return result;
        }
    }

    [JsonIgnore] 
    public bool IsValid => true;

    [JsonIgnore] 
    public string[] Errors => Array.Empty<string>();
}