namespace Rnd.Core;

public abstract class Factory<TModel, TForm>
    where TModel : FormModel<TModel, TForm> 
    where TForm : struct
{
    public abstract TModel Create(TForm form);
}