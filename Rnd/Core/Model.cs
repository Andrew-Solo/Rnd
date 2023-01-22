namespace Rnd.Core;

public abstract class Model
{
    public Guid Id { get; set; } = Guid.NewGuid();
}