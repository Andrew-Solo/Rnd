namespace Rnd.Core;

public abstract class FormModel<TForm> : Model where TForm : struct 
{
    public abstract void Update(TForm form);
    public abstract void Clear(TForm form);
}