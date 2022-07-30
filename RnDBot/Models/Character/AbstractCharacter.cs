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
        Attributes = character.Attributes;
        Pointers = character.Pointers;

        ValidateErrors = new List<string>();
    }
    
    public AbstractCharacter(string name)
    {
        Name = name;
        General = new General(this);
        Effects = new Effects(this);
        Attributes = new Attributes(this);
        Pointers = new Pointers(this);
        
        ValidateErrors = new List<string>();
    }

    [JsonConstructor]
    public AbstractCharacter(string name, General general, Attributes attributes, Pointers pointers, Effects effects)
    {
        Name = name;
        General = new General(this, general.Description, general.Culture, general.Age, general.Ideals, general.Vices, general.Traits);
        Effects = new Effects(this, effects.AttributeEffects, effects.PointEffects, effects.SkillEffects);
        Attributes = new Attributes(this, attributes.CoreAttributes);
        Pointers = new Pointers(this, pointers.CorePointers);
        
        ValidateErrors = new List<string>();
    }

    public string Name { get; set; }
    public General General { get; }
    public Attributes Attributes { get; }
    public Pointers Pointers { get; }
    public Effects Effects { get; }

    [JsonIgnore]
    public bool IsValid => Validate();

    [JsonIgnore] 
    public string[] Errors => ValidateErrors.ToArray();

    [JsonIgnore]
    public virtual int GetPower => 0;

    [JsonIgnore]
    public virtual List<IPanel> Panels => new()
    {
        General,
        Pointers,
        Attributes,
        Effects,
    };

    protected List<string> ValidateErrors { get; }

    protected virtual bool Validate()
    {
        var valid = true;
        
        if (!Regex.IsMatch(Name, @"^[a-zA-Zа-я-А-Я 0-9]*$"))
        {
            valid = false;
            ValidateErrors.Add("Имя персонажа должно состоять из латиницы, кириллицы, цифр или пробелов.");
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