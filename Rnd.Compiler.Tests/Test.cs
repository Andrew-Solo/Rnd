using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }

    private const string Filepath = ".modules";
    private const string Filename = "RndCore.rnd";
}