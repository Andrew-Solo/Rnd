namespace Rnd.Model;

public abstract class Entity<TForm> where TForm : struct 
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public abstract void Update(TForm form);
    public abstract void Clear(TForm form);
}