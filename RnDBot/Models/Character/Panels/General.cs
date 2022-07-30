﻿using Newtonsoft.Json;
using RnDBot.Models.Common;
using RnDBot.Views;

namespace RnDBot.Models.Character.Panels;

public class General : IPanel, IValidatable
{
    public General(ICharacter character, string? description = null,
        string? culture = null, string? age = null, 
        List<string>? ideals = null, List<string>? vices = null, List<string>? traits = null)
    {
        Character = character;

        Description = description;
        Culture = new TextField<string?>("Культура", culture);
        Age = new TextField<string?>("Возраст", age);
        
        Ideals = new ListField("Идеалы", ideals);
        Vices = new ListField("Пороки", vices);
        Traits = new ListField("Черты", traits);
    }

    [JsonConstructor]
    public General(ICharacter character, string? description, TextField<string?> culture, TextField<string?> age, 
        ListField ideals, ListField vices, ListField traits)
    {
        Character = character;
        Description = description;
        Culture = culture;
        Age = age;
        Ideals = ideals;
        Vices = vices;
        Traits = traits;
    }

    [JsonIgnore]
    public ICharacter Character { get; }
    
    public string? Description { get; set; }
    public TextField<string?> Culture { get; }
    public TextField<string?> Age { get; }
    public ListField Ideals { get; }
    public ListField Vices { get; }
    public ListField Traits { get; }

    [JsonIgnore]
    public string Title => Character.GetFooter;

    [JsonIgnore]
    public List<IField> Fields => new()
    {
        Culture,
        Age,
        Ideals,
        Vices,
        Traits
    };

    public bool IsValid
    {
        get
        {
            var valid = true;
            var errors = new List<string>();

            if (Age.Value != null && !Int32.TryParse(Age.TValue, out int _))
            {
                valid = false;
                errors.Add("Возраст должен быть записан цифрой.");
            }

            Errors = errors.ToArray();
            return valid;
        }
    }

    public string[]? Errors { get; private set; }
}