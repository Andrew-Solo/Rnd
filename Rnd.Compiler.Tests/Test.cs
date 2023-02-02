using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Rnd.Compiler.Lexer;
using File = Rnd.Compiler.Lexer.File;

namespace Rnd.Compiler.Tests;

[TestClass]
public class Test
{
    [TestMethod]
    public async Task MyTest()
    {
        var lexer = await File.Parse(Filepath, Filename);
        var json = JsonConvert.SerializeObject(lexer);
        Console.WriteLine(json);
    }

    private const string Filepath = ".modules";
    private const string Filename = "RndCore.rnd";
}