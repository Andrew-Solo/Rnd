using Rnd.Constants;
using Rnd.Models;
using Rnd.Results;
using Rnd.Script.Compiler.LexemeParsers;
using Rnd.Script.Parser;

namespace Rnd.Script.Compiler;

public class ModuleBuilder
{
    public async Task<Result<Module>> BuildAsync(Tree tree)
    {
        var nodes = new List<Node>(tree.Nodes);

        var moduleResult = ExcludeModuleNode(nodes);
        if (moduleResult.IsFailed) return Result.Fail<Module>(moduleResult.Message);

        var moduleNode = moduleResult.Value;

        var version = moduleNode.Attributes
            .Extract(a => a.Name.Value == "version");
        
        var form = new Module.Form
        {
            Name = moduleNode.Name?.Value,
            Version = version.Value?.Value,
            Title = moduleNode.Title?.Value,
            Description = moduleNode.Description?.Value,
            Attributes = moduleNode.Attributes.ToDictionary(
                a => a.Name.Value, 
                a => LexemeParser.Value.Parse(a.Value) ?? true),
        };
        
        var module = await Module.New.TryCreateAsync(form);
        if (module.IsFailed) return module;

        var units = await ParseUnitsAsync(nodes, module.Value.Id);
        if (units.IsFailed) return Result.Fail<Module>(units.Message);
        module.Value.Units.AddRange(units.Value);
        
        return module;
    }

    private Result<Node> ExcludeModuleNode(List<Node> nodes)
    {
        var moduleNodes = nodes
            .Where(n => LexemeParser.Role.Parse(n.Role) == UnitRole.Module)
            .ToList();
        
        if (moduleNodes.Count != 1) return Result.Fail<Node>("Модуль не определен");
        
        nodes.Remove(moduleNodes.First());
        
        return Result.Success(moduleNodes.First(), "Модуль");
    }

    private async Task<Result<List<Unit>>> ParseUnitsAsync(List<Node> nodes, Guid moduleId, Guid? parentId = null)
    {
        var result = new List<Unit>();
        
        foreach (var node in nodes)
        {
            var unit = await ParseUnitAsync(node, moduleId, parentId);
            if (unit.IsFailed) return Result.Fail<List<Unit>>(unit.Message);
            result.Add(unit.Value);
        }

        return Result.Success(result, "Элементы");
    }

    private async Task<Result<Unit>> ParseUnitAsync(Node node, Guid moduleId, Guid? parentId = null)
    {
        var form = new Unit.Form
        {
            ModuleId = moduleId,
            ParentId = parentId,
            Name = node.Name?.Value,
            Access = LexemeParser.Access.Parse(node.Access),
            Type = LexemeParser.Type.Parse(node.Type),
            Role = LexemeParser.Role.Parse(node.Role),
            Value = LexemeParser.Value.Parse(node.Value),
            Title = node.Title?.Value,
            Description = node.Description?.Value,
            Attributes = node.Attributes.ToDictionary(
                a => a.Name.Value, 
                a => LexemeParser.Value.Parse(a.Value) ?? true),
        };

        var unit = await Unit.New.TryCreateAsync(form);
        if (unit.IsFailed) return unit;
        
        var units = await ParseUnitsAsync(node.Children, moduleId, unit.Value.Id);
        if (units.IsFailed) return Result.Fail<Unit>(units.Message);
        unit.Value.Children.AddRange(units.Value);
        
        return unit;
    }
}