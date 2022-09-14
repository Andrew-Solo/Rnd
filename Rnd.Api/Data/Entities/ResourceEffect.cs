namespace Rnd.Api.Data.Entities;

public class ResourceEffect : IEntity
{
    public Guid Id { get; set; }
    public Guid EffectId { get; set; }
    public string ResourceFullname { get; set; } = null!;
    public decimal? ValueModifier { get; set; }
    public decimal? MinModifier { get; set; }
    public decimal? MaxModifier { get; set; }
    
    #region Navigation

    public virtual Effect Effect { get; set; } = null!;

    #endregion
}