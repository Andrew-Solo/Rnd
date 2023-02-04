using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;
using FluentValidation;
using Rnd.Constants;
using Rnd.Core;
using Rnd.Results;

// EF Proxies
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
#pragma warning disable CS8618

namespace Rnd.Models;

public class Unit : ValidatableModel<Unit, Unit.Form, Unit.UpdateValidator, Unit.ClearValidator>
{
    public virtual Module Module { get; protected set; }
    public virtual Unit? Parent { get; protected set; }

    [MaxLength(TextSize.Paragraph)]
    public string Fullname { get; protected set; }
    
    [MaxLength(TextSize.Tiny)]
    public string Name { get; protected set; }

    [MaxLength(TextSize.Tiny)]
    public UnitAccess Access { get; protected set; }
    
    [MaxLength(TextSize.Tiny)]
    public UnitRole Role { get; protected set; }
    
    [MaxLength(TextSize.Tiny)]
    public UnitType Type { get; protected set; }
    
    [MaxLength(TextSize.Tiny)]
    public string? CustomType { get; protected set; }
    
    [MaxLength(TextSize.Tiny)]
    public UnitType? ChildrenType { get; protected set; }
    
    [MaxLength(TextSize.Tiny)]
    public string? ChildrenCustomType { get; protected set; }
    
    public string Value { get; protected set; }

    [MaxLength(TextSize.Small)]
    public string? Title { get; protected set; }

    [MaxLength(TextSize.Medium)]
    public string? Description { get; protected set; }

    [Column(TypeName = "json")]
    public Dictionary<string, string> Attributes { get; protected set; }
    
    public virtual List<Unit> Children { get; protected set; } = new();

    #region Navigation
    
    public Guid ModuleId { get; protected set; }
    public Guid? ParentId { get; protected set; }
    public virtual List<Token> UsingTokens { get; protected set; } = new();
    
    #endregion

    #region Factories

    protected Unit(
        Guid moduleId,
        Guid? parentId,
        string fullname,
        string name,
        UnitAccess access,
        UnitType type,
        string? customType,
        UnitType? childrenType,
        string? childrenCustomType,
        UnitRole role,
        string value,
        string? title,
        string? description,
        Dictionary<string, string>? attributes
    )
    {
        ModuleId = moduleId;
        ParentId = parentId;
        Fullname = fullname;
        Name = name;
        Access = access;
        Type = type;
        CustomType = customType;
        ChildrenType = childrenType;
        ChildrenCustomType = childrenCustomType;
        Role = role;
        Value = value;
        Title = title;
        Description = description;
        Attributes = attributes ?? new Dictionary<string, string>();
    }

    public class Factory : ValidatingFactory<Unit, Form, CreateValidator>
    {
        public override Unit Create(Form form)
        {
            Guard.Against.Null(form.ModuleId, nameof(form.ModuleId));
            Guard.Against.NullOrWhiteSpace(form.Fullname, nameof(form.Fullname));
            Guard.Against.NullOrWhiteSpace(form.Name, nameof(form.Name));
            Guard.Against.Null(form.Access, nameof(form.Access));
            Guard.Against.Null(form.Type, nameof(form.Type));
            Guard.Against.Null(form.Role, nameof(form.Role));
            Guard.Against.Null(form.Value, nameof(form.Value));
            
            return new Unit(
                form.ModuleId.Value,
                form.ParentId,
                form.Fullname,
                form.Name,
                form.Access.Value,
                form.Type.Value,
                form.CustomType,
                form.ChildrenType,
                form.ChildrenCustomType,
                form.Role.Value,
                form.Value,
                form.Title,
                form.Description,
                form.Attributes
            );
        }
    }

    public static Factory New => new();

    #endregion

    #region Updaters

    public override Unit Update(Form form)
    {
        if (form.ModuleId != null) ModuleId = form.ModuleId.Value;
        if (form.ParentId != null) ParentId = form.ParentId.Value;
        if (form.Fullname != null) Fullname = form.Fullname;
        if (form.Name != null) Name = form.Name;
        if (form.Access != null) Access = form.Access.Value;
        if (form.Type != null) Type = form.Type.Value;
        if (form.Role != null) Role = form.Role.Value;
        if (form.Value != null) Value = form.Value;
        if (form.Title != null) Title = form.Title;
        if (form.Description != null) Description = form.Description;
        if (form.Attributes != null) Attributes.Merge(form.Attributes);
        return this;
    }

    public override Unit Clear(Form form)
    {
        Guard.Against.Null(form.ModuleId, nameof(form.ModuleId));
        if (form.ParentId == null) ParentId = null;
        Guard.Against.Null(form.Fullname, nameof(form.Fullname));
        Guard.Against.Null(form.Name, nameof(form.Name));
        Guard.Against.Null(form.Access, nameof(form.Access));
        Guard.Against.Null(form.Type, nameof(form.Type));
        Guard.Against.Null(form.Role, nameof(form.Role));
        Guard.Against.Null(form.Value, nameof(form.Value));
        if (form.Title == null) Title = null;
        if (form.Description == null) Description = null;
        if (form.Attributes == null) Attributes.Clear();
        return this;
    }
    
    #endregion

    #region Validators

    public class UpdateValidator : AbstractValidator<Form>
    {
        public UpdateValidator()
        {
            
        }
    }

    public class CreateValidator : UpdateValidator
    {
        public CreateValidator()
        {
            
        }
    }

    public class ClearValidator : AbstractValidator<Form>
    {
        public ClearValidator()
        {
            
        }
    }

    #endregion

    #region Views

    public record struct Form(
        Guid? ModuleId,
        Guid? ParentId,
        string? Fullname,
        string? Name,
        UnitAccess? Access,
        UnitType? Type,
        string? CustomType,
        UnitType? ChildrenType,
        string? ChildrenCustomType,
        UnitRole? Role,
        string? Value,
        string? Title,
        string? Description,
        Dictionary<string, string>? Attributes
    );
    
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
    public readonly record struct View(
        Guid _id,
        Guid _moduleId,
        string Module,
        Guid? _parentId,
        string? Parent,
        string Fullname,
        string Name,
        string Access,
        string Role,
        string Type,
        string? CustomType,
        string? ChildrenType,
        string? ChildrenCustomType,
        string Value,
        string? Title,
        string? Description,
        Dictionary<string, string> Attributes,
        Guid[] _childIds,
        string[] Children
    );

    public View GetView()
    {
        return new View(
            Id,
            ModuleId,
            Module.VersionedTitle,
            ParentId,
            Parent?.Name,
            Fullname,
            Name,
            Access.ToString(),
            Role.ToString(),
            Type.ToString(),
            CustomType,
            ChildrenType?.ToString(),
            ChildrenCustomType,
            Value,
            Title,
            Description,
            Attributes,
            Children.Select(c => c.Id).ToArray(),
            Children.Select(c => c.Name).ToArray()
        );
    }
    
    public Task<Result<Unit>> ActAsync(string parameters)
    {
        throw new NotImplementedException();
    }

    #endregion
}