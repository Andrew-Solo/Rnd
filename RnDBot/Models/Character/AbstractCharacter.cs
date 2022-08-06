using System.Text.RegularExpressions;
using Newtonsoft.Json;
using RnDBot.Models.Character.Panels;
using RnDBot.Views;

namespace RnDBot.Models.Character;

public class AbstractCharacter : ICharacter
{
    public AbstractCharacter(ICharacter character)
    {
        Name = character.Name;
        General = character.General;
        Effects = character.Effects;
        Traumas = character.Traumas;
        Attributes = character.Attributes;
        Pointers = character.Pointers;
        Backstory = character.Backstory;

        ValidateErrors = new List<string>();
    }
    
    public AbstractCharacter(string name)
    {
        Name = name;
        General = new General(this);
        Effects = new Effects(this);
        Traumas = new Traumas(this);
        Attributes = new Attributes(this);
        Backstory = new Backstory(this);
        Pointers = new Pointers(this);
        
        ValidateErrors = new List<string>();
    }

    [JsonConstructor]
    public AbstractCharacter(string name, General general, Attributes attributes, Pointers pointers, Effects effects, Traumas traumas,
        Backstory? backstory)
    {
        Name = name;
        
        General = new General(this, general.Description, general.Culture, general.Age, 
            general.Ideals, general.Vices, general.Traits);
        
        Effects = new Effects(this, effects.PowerEffects, effects.AttributeEffects, effects.PointEffects, 
            effects.DomainEffects, effects.SkillEffects, effects.AggregateEffects);

        Traumas = new Traumas(this, traumas.TraumaEffects);
        Attributes = new Attributes(this, attributes.CoreAttributes);
        Pointers = new Pointers(this, pointers.PointersCurrent);

        ValidateErrors = new List<string>();

        //TODO это код для совместимости
        if (backstory != null)
        {
            Backstory = new Backstory(this, backstory.Goals, backstory.Outlook, backstory.Culture, backstory.Society, 
                backstory.Traditions, backstory.Mentor, backstory.Lifepath, backstory.Habits);
        }
        else
        {
            Backstory = new Backstory(this);
        }
    }

    public string Name { get; set; }
    public General General { get; }
    public Attributes Attributes { get; }
    public Pointers Pointers { get; }
    public Effects Effects { get; }
    public Traumas Traumas { get; }
    public Backstory Backstory { get; }

    [JsonIgnore]
    public bool IsValid => Validate();

    [JsonIgnore] 
    public string[] Errors => ValidateErrors.ToArray();

    [JsonIgnore]
    public virtual int GetPower => 0;

    [JsonIgnore]
    public virtual List<IPanel> Panels
    {
        get
        {
            var panels = new List<IPanel>
            {
                General,
                Pointers,
                Attributes,
            };
            
            if (Effects.CoreEffects.Count > 0) panels.Add(Effects);
            if (Traumas.TraumaEffects.Count > 0) panels.Add(Traumas);
            if (Backstory.Fields.Count > 0) panels.Add(Backstory);

            return panels;
        }
    }

    [JsonIgnore]
    protected List<string> ValidateErrors { get; }

    protected bool Validate()
    {
        var valid = true;
        
        if (!Regex.IsMatch(Name, @"^[a-zA-Zа-яА-Я0-9 '""-]*$"))
        {
            valid = false;
            ValidateErrors.Add("Имя персонажа должно состоять из латиницы, кириллицы, цифр, пробелов, кавычек, апострофов или дефизов.");
        }

        var panels = Panels.Cast<IValidatable>();

        foreach (var panel in panels)
        {
            if (!panel.IsValid)
            {
                valid = false;
                ValidateErrors.AddRange(panel.Errors ?? Array.Empty<string>());
            }
        }
        
        return valid;
    }
}