namespace Rnd.Model;

public abstract class Factory<TEntity, TForm>
    where TEntity : Entity<TForm> 
    where TForm : struct
{
    public abstract TEntity Create(TForm form);
}