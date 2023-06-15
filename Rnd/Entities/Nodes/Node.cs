﻿namespace Rnd.Entities.Nodes;

public abstract class Node : Model
{
    protected Node(string name, string path) : base(name, path) { }
    
    public abstract Prototype Prototype { get; }
    public abstract Guid? ParentId { get; }
    public abstract Node? Parent { get; }
    public abstract IReadOnlyList<Node> Children { get; }
}

public enum Prototype : byte
{
    Module,
    Unit,
    Field,
    Method
}