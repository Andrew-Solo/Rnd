namespace Rnd.Core;

public abstract class FormModel<TModel, TForm> : Model 
    where TModel : FormModel<TModel, TForm> 
    where TForm : struct 
{
    public abstract TModel Update(TForm form);
    public abstract TModel Clear(TForm form);
}