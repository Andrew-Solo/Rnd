﻿using Newtonsoft.Json;
using RnDBot.Models.Character.Fields;
using RnDBot.Models.Glossaries;
using RnDBot.Views;

namespace RnDBot.Models.Character.Panels;

public class Domains<TDomain, TSkill> : IPanel, IValidatable
    where TDomain : struct 
    where TSkill : struct
{
    public Domains(ICharacter character, List<Domain<TDomain, TSkill>> coreDomains)
    {
        Character = character;
        CoreDomains = coreDomains;
    }

    [JsonIgnore]
    public ICharacter Character { get; }

    [JsonIgnore] 
    public int MaxSkillLevel => (int) Math.Floor((double) Character.Attributes.Power.Max / 8) + 6;
    
    //TODO Индексатор
    public List<Domain<TDomain, TSkill>> CoreDomains { get; }
    
    [JsonIgnore]
    public IReadOnlyCollection<Skill<TSkill>> CoreSkills
    {
        get
        {
            var result = new List<Skill<TSkill>>();
            
            CoreDomains.ForEach(d => result.AddRange(d.Skills));

            return result;
        }
    }

    //TODO Items
    [JsonIgnore]
    public IReadOnlyCollection<Domain<TDomain, TSkill>> FinalDomains
    {
        get
        {
            var result = new List<Domain<TDomain, TSkill>>();

            foreach (var domain in CoreDomains.Select(d => 
                         new Domain<TDomain, TSkill>(
                             d.DomainType, 
                             d.Skills.Select(s => new Skill<TSkill>(s.CoreAttribute, s.SkillType, s.Value))
                                 .ToList(), 
                             d.DomainLevel)))
            {
                foreach (var effect in Character.Effects.CoreEffects)
                {
                    effect.ModifyDomain(domain);
                }

                foreach (var skill in domain.Skills)
                {
                    skill.Value += domain.DomainLevel;
                    
                    foreach (var effect in Character.Effects.CoreEffects)
                    {
                        effect.ModifySkill(skill);
                    }
                }
                
                result.Add(domain);
            }

            return result;
        }
    }

    [JsonIgnore]
    public IReadOnlyCollection<Skill<TSkill>> FinalSkills
    {
        get
        {
            var result = new List<Skill<TSkill>>();
            
            FinalDomains.ToList().ForEach(d => result.AddRange(d.Skills));

            return result;
        }
    }

    [JsonIgnore]
    public string Title => "Навыки";
    
    [JsonIgnore]
    public List<IField> Fields => FinalDomains.Select(a => (IField) a).ToList();
    
    [JsonIgnore]
    public string Footer => Character.GetFooter;
    
    [JsonIgnore]
    public bool IsValid
    {
        get
        {
            var valid = true;
            var errors = new List<string>();

            var errorDomains = FinalDomains.Where(d => d.DomainLevel is > 8 or < 0).ToList();

            if (errorDomains.Any())
            {
                valid = false;
                
                var domainsJoin = String.Join(", ", 
                    errorDomains.Select(d => $"{d.Name}"));
                
                errors.Add($"Домены: {domainsJoin} – должны иметь уровень от 0 до 8.");
            }

            var errorSkills = CoreSkills.Where(s => s.Value > MaxSkillLevel).ToList();

            if (errorSkills.Any())
            {
                valid = false;

                var errorFinalSkills = FinalSkills
                    .Where(s => errorSkills.Select(skill => skill.SkillType).Contains(s.SkillType));
                
                var skillsJoin = String.Join(", ", 
                    errorFinalSkills.Select(s => 
                        $"{s.Name} `{s.Value}`/" +
                        $"`{s.Value - (errorSkills.First(skill => Glossary.GetSkillName(skill.SkillType) == Glossary.GetSkillName(s.SkillType)).Value - MaxSkillLevel)}`"));
                
                errors.Add($"Навыки: {skillsJoin} – превышают максимальный уровень.");
            }

            Errors = errors.ToArray();
            return valid;
        }
    }

    [JsonIgnore]
    public string[]? Errors { get; private set; }
}