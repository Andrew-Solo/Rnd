namespace Rnd.Core;

public abstract class Factory<TModel, TForm>
    where TModel : FormModel<TForm> 
    where TForm : struct
{
    public abstract TModel Create(TForm form);
}