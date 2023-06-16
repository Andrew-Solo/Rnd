namespace Rnd.Models.Nodes.Methods;

public class FunctionMethod : Method
{
    public FunctionMethod(string name, string path, Guid unitId) 
        : base(name, path, unitId) { }

    public override Methodology Methodology => Methodology.Function;
}