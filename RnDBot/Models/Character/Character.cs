using Newtonsoft.Json;
using RnDBot.Models.Character.Fields;
using RnDBot.Models.Character.Panels;
using RnDBot.Views;

namespace RnDBot.Models.Character;

public class Character<TDomain, TSkill> : AbstractCharacter 
    where TDomain : struct 
    where TSkill : struct
{
    public Character(ICharacter character, List<Domain<TDomain, TSkill>> domains) : base(character)
    {
        Domains = new Domains<TDomain, TSkill>(this, domains);
    }
    
    [JsonConstructor]
    public Character(string name, General general, Attributes attributes, Pointers pointers, Effects effects, Traumas traumas, 
        Domains<TDomain, TSkill> domains) 
        : base(name, general, attributes, pointers, effects, traumas)
    {
        Domains = new Domains<TDomain, TSkill>(this, domains.CoreDomains);
    }

    public Domains<TDomain, TSkill> Domains { get; }

    [JsonIgnore]
    public override int GetPower => Domains.CoreSkills.Sum(s => s.Value);

    [JsonIgnore]
    public override List<IPanel> Panels
    {
        get
        {
            var panels = new List<IPanel>
            {
                General,
                Pointers,
                Attributes,
                Domains
            };
            
            if (Effects.CoreEffects.Count > 0) panels.Add(Effects);
            if (Traumas.TraumaEffects.Count > 0) panels.Add(Traumas);

            return panels;
        }
    }
}