using Rnd.Api.Helpers;
using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Parameters.AttributeParameters;

public class FinalAttribute : Attribute
{
    public FinalAttribute(Attribute original) : base(original.AttributeType, original.Value)
    {
        
    }

    public override string Path => PathHelper.Combine(nameof(Final), base.Path);
}