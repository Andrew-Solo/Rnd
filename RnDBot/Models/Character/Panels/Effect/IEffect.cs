using RnDBot.Models.Character.Fields;
using RnDBot.Models.Common;
using Attribute = RnDBot.Models.Character.Fields.Attribute;

namespace RnDBot.Models.Character.Panels.Effect;

public interface IEffect
{
    string Name { get; }
    string View { get; }
    void ModifyPower(CounterField power) {}
    void ModifyAttribute(Attribute attribute) {}
    void ModifyPointer(Pointer pointer) {}
    void ModifyDomain<TDomain, TSkill>(Domain<TDomain, TSkill> domain) where TDomain : struct where TSkill : struct {}
    void ModifySkill<TSkill>(Skill<TSkill> skill) where TSkill : struct {}
}