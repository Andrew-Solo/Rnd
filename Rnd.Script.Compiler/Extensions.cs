using Rnd.Models;
using Rnd.Results;

namespace Rnd.Script.Compiler;

public static class Extensions
{
    public static async Task<Result<Module>> ParseAsync(this Module.Factory factory, string path)
    {
        var builder = new ModuleBuilder();
        return await builder.BuildAsync(path);
    }
}