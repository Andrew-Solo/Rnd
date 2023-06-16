using Rnd.Models;
using Rnd.Models.Nodes;

namespace Rnd.Data;

public static class PredefinedData
{
    public static User? Owner => null;
    
    public static List<Module> Modules => new()
    {
        
    };

    public static Module? UserModule => null;
    public static Module? SpaceModule => null;
    public static Module? MemberModule => null;
    public static Module? GroupModule => null;
    public static Module? PluginModule => null;
    public static Module? ModuleModule => null;
}