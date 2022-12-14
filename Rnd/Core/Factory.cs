namespace Rnd.Core;

public abstract class Factory<TModel, TForm>
    where TModel : Model<TForm> 
    where TForm : struct
{
    public abstract TModel Create(TForm form);
}