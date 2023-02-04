using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Rnd.Script.Compiler;
using Rnd.Script.Parser;

namespace Rnd.Script.Tests;

[TestClass]
public class Test
{
    [TestMethod]
    public async Task MyTest()
    {
        var lexer = await Lexer.File.ParseAsync(Filepath, Filename);
        var lexerJson = JsonConvert.SerializeObject(lexer, Formatting.Indented, new Newtonsoft.Json.Converters.StringEnumConverter());
        File.WriteAllText(Path.Combine("../../../" + Filepath, LexerFilename), lexerJson);
        
        var tree = Tree.Parse(lexer);
        var treeJson = JsonConvert.SerializeObject(tree, Formatting.Indented, new Newtonsoft.Json.Converters.StringEnumConverter());
        await File.WriteAllTextAsync(Path.Combine("../../../" + Filepath, TreeFilename), treeJson);

        var builder = new ModuleBuilder();
        var module = await builder.BuildAsync(tree);
        var moduleJson = JsonConvert.SerializeObject(module, Formatting.Indented, new Newtonsoft.Json.Converters.StringEnumConverter());
        await File.WriteAllTextAsync(Path.Combine("../../../" + Filepath, ModuleFilename), moduleJson);
    }

    private const string Filepath = ".modules";
    private const string Filename = "RndCore.rnd";
    private const string LexerFilename = "RndCore.lexer.json";
    private const string TreeFilename = "RndCore.tree.json";
    private const string ModuleFilename = "RndCore.module.json";
}