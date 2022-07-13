﻿namespace RnDBot.Models;

public class Skill<TSkill> where TSkill : struct
{
    public Skill(AttributeType coreAttribute, TSkill skillType, int value = 0)
    {
        CoreAttribute = coreAttribute;
        SkillType = skillType;
        Value = value;
    }
    
    public AttributeType CoreAttribute { get; set; }
    public TSkill SkillType { get; set; }
    public int Value { get; set; }
}