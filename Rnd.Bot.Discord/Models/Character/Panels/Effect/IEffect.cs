using Rnd.Bot.Discord.Models.Character.Fields;
using Rnd.Bot.Discord.Models.Common;
using Attribute = Rnd.Bot.Discord.Models.Character.Fields.Attribute;

namespace Rnd.Bot.Discord.Models.Character.Panels.Effect;

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