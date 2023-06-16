namespace Rnd.Models.Nodes.Methods;

public class ActionMethod : Method
{
    public ActionMethod(string name, string path, Guid unitId) 
        : base(name, path, unitId) { }

    public override Methodology Methodology => Methodology.Action;
}