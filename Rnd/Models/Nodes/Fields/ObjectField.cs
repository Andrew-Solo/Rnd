namespace Rnd.Models.Nodes.Fields;

public class ObjectField : Field
{
    protected ObjectField(string name, string path, Guid? unitId, Guid? methodId) 
        : base(name, path, unitId, methodId) { }

    public override Type Type => Type.Object;
}