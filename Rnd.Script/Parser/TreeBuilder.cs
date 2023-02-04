using Rnd.Script.Lexer;
using File = Rnd.Script.Lexer.File;

namespace Rnd.Script.Parser;

public class TreeBuilder
{
    public Tree Build(File file)
    {
        return new Tree(file.Filepath, file.Filename, ParseNodes(file.Lines));
    }

    private List<Node> ParseNodes(List<Line> lines, Node? parent = null, int tabulation = 0)
    {
        var rootLines = lines
            .Where(l => !l.CheckPattern(LexemeType.Newline))
            .Where(l => !l.CheckPattern(LexemeType.Tabulation, LexemeType.Newline))
            .Where(l => (l.GetLexeme(LexemeType.Tabulation)?.Width ?? 0) == tabulation)
            .ToList();

        return rootLines.Select(line => ParseNode(line, parent, tabulation)).ToList();
    }

    private Node ParseNode(Line line, Node? parent = null, int tabulation = 0)
    {
        //TODO Check pattern
        
        var (type, name) = ParseIdentifiers(line);
        
        var node = new Node(parent)
        {
            Name = name?.ToProperty(),
            CustomType = type?.ToProperty(),
            Type = line.GetLexeme(LexemeType.Type)?.ToProperty(),
            Role = line.GetLexeme(LexemeType.Role)?.ToProperty(),
            Value = line.GetLexeme(LexemeType.Value)?.ToProperty(),
            Title = line.GetLexeme(LexemeType.Title)?.ToProperty(),
            Access = line.GetLexeme(LexemeType.Accessor)?.ToProperty(),
            ChildrenType = line.GetLexeme(LexemeType.ChildrenType)?.ToProperty(),
            ChildrenCustomType = line.GetLexeme(LexemeType.ChildrenCustomType)?.ToProperty(),
        };

        var attributesGroup = line.Next?.GetTabGroup(tabulation + 2);
        
        ParseAttributes(attributesGroup, node);

        if (line.IsLexemeExist(LexemeType.Declaration))
        {
            var nestedGroup = attributesGroup?.Last().Next?.GetTabGroup(tabulation + 4) 
                              ?? line.Next?.GetTabGroup(tabulation + 4)
                              ?? new List<Line>();

            //TODO Many
            var valueLine = nestedGroup
                .FirstOrDefault(l => l.CheckPattern(
                    LexemeType.Tabulation, 
                    LexemeType.Value, 
                    LexemeType.Newline));


            if (valueLine != null)
            {
                if (node.Value != null) throw new Exception();
                nestedGroup.Remove(valueLine);
                node.Value = valueLine.GetLexeme(LexemeType.Value)?.ToProperty();
            }
            
            ParseNodes(nestedGroup, node, tabulation + 4);
        }

        return node;
    }

    private (Lexeme?, Lexeme?) ParseIdentifiers(Line line)
    {
        var identifiers = line.GetLexemes(l => l.Type == LexemeType.Identifier);

        return identifiers.Count switch
        {
            1 => (null, identifiers.First()),
            2 => (identifiers.First(), identifiers.Last()),
            _ => throw new Exception()
        };
    }

    private void ParseAttributes(List<Line>? lines, Node node)
    {
        if (lines == null) return;

        foreach (var line in lines)
        {
            ParseAttribute(line, node);
        }
    }

    private void ParseAttribute(Line line, Node node)
    {
        if (line.CheckPattern(
                LexemeType.Tabulation, 
                LexemeType.Title, 
                LexemeType.Newline))
        {
            if (node.Title != null) throw new Exception();
            node.Title = line.GetLexeme(LexemeType.Title)?.ToProperty();
        }
        else if (line.CheckPattern(
                     LexemeType.Tabulation, 
                     LexemeType.Attribute,
                     LexemeType.Newline))
        {
            AddAttribute(node, line.GetLexeme(LexemeType.Attribute));
        }
        else if (line.CheckPattern(
                     LexemeType.Tabulation, 
                     LexemeType.Attribute, 
                     LexemeType.Value, 
                     LexemeType.Newline))
        {
            AddAttribute(node, line.GetLexeme(LexemeType.Attribute), line.GetLexeme(LexemeType.Value));
        }
        else
        {
            throw new Exception();
        }
    }

    private void AddAttribute(Node node, Lexeme? attribute, Lexeme? value = null)
    {
        if (attribute?.Value == "title")
        {
            if (node.Title != null) throw new Exception();
            node.Title = value?.ToProperty();
        } 
        else if (attribute?.Value == "description")
        {
            if (node.Description != null) throw new Exception();
            node.Description = value?.ToProperty();
        }
        else if (attribute != null)
        {
            if (attribute == null) throw new Exception();
            node.Attributes.Add(new Node.Attribute(attribute.ToProperty(), value?.ToProperty()));
        }
    }
}
