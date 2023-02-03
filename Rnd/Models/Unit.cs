using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using FluentValidation;
using Rnd.Constants;
using Rnd.Core;

// EF Proxies
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
#pragma warning disable CS8618

namespace Rnd.Models;

public class Unit : ValidatableModel<Unit, Unit.Form, Unit.UpdateValidator, Unit.ClearValidator>
{
    public virtual Module Module { get; protected set; }
    public virtual Unit? Parent { get; protected set; }
    public virtual List<Unit> Children { get; protected set; } = new();
    
    [MaxLength(TextSize.Small)]
    public string Name { get; protected set; }
    public UnitAccess Access { get; protected set; }
    public UnitType Type { get; protected set; }
    public UnitRole Role { get; protected set; }
    public dynamic? Value { get; protected set; }

    [MaxLength(TextSize.Small)]
    public string? Title { get; protected set; }
    
    [MaxLength(TextSize.Medium)]
    public string? Description { get; protected set; }
    
    public Dictionary<string, dynamic> Attributes { get; protected set; }

    #region Navigation

    public Guid ModuleId { get; protected set; }
    public Guid? ParentId { get; protected set; }
    public virtual List<Token> UsingTokens { get; protected set; } = new();
    
    #endregion

    #region Factories

    protected Unit(
        Guid moduleId,
        Guid? parentId,
        string name,
        UnitAccess access,
        UnitType type,
        UnitRole role,
        dynamic? value,
        string? title,
        string? description,
        Dictionary<string, dynamic>? attributes
    )
    {
        ModuleId = moduleId;
        ParentId = parentId;
        Name = name;
        Access = access;
        Type = type;
        Role = role;
        Value = value;
        Title = title;
        Description = description;
        Attributes = attributes ?? new Dictionary<string, dynamic>();
    }

    public class Factory : ValidatingFactory<Unit, Form, CreateValidator>
    {
        public override Unit Create(Form form)
        {
            Guard.Against.Null(form.ModuleId);
            Guard.Against.NullOrWhiteSpace(form.Name);
            Guard.Against.Null(form.Access);
            Guard.Against.Null(form.Type);
            Guard.Against.Null(form.Role);
            
            return new Unit(
                form.ModuleId.Value,
                form.ParentId,
                form.Name,
                form.Access.Value,
                form.Type.Value,
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
        return this;
    }

    public override Unit Clear(Form form)
    {
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
        string? Name,
        UnitAccess? Access,
        UnitType? Type,
        UnitRole? Role,
        dynamic? Value,
        string? Title,
        string? Description,
        Dictionary<string, dynamic>? Attributes
    );
    
    public readonly record struct View(
        Guid _id
    );

    public View GetView()
    {
        return new View(
            Id
        );
    }

    #endregion
}