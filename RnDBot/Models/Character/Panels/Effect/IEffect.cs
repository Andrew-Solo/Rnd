using RnDBot.Models.Character.Fields;
using Attribute = RnDBot.Models.Character.Fields.Attribute;

namespace RnDBot.Models.Character.Panels.Effect;

public interface IEffect
{
    string Name { get; }
    string View { get; }
    void ModifyAttribute(Attribute attribute) {}
    void ModifyPointer(Pointer pointer) {}
    void ModifySkill<TSkill>(Skill<TSkill> skill) where TSkill : struct {}
}