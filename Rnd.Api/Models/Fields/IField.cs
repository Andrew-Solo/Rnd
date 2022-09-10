using Microsoft.VisualBasic.FileIO;

namespace Rnd.Api.Models.Fields;

public interface IField
{
    public string Group { get; }
    public string Name { get; }
    public FieldType Type { get; }
    public object? Value { get; set; }
}