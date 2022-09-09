namespace Rnd.Api.Data.Entities;

public class Skill
{
    public Guid Id { get; set; }
    public SkillType Type { get; set; }
    public int Value { get; set; }
}