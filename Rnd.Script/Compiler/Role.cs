using Rnd.Constants;

namespace Rnd.Script.Compiler;

public static class Role
{
    public static UnitRole Parse(string role)
    {
        return role switch
        {
            
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
        };
    }
}