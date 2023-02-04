using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;
using FluentValidation;
using Rnd.Core;

// EF Proxies
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
#pragma warning disable CS8618

namespace Rnd.Models;

public class Token : ValidatableModel<Token, Token.Form, Token.UpdateValidator, Token.ClearValidator>
{
    public virtual Unit Unit { get; protected set; }
    
    public virtual Character Character { get; protected set; }
    
    public string Value { get; protected set; }
    
    #region Navigation

    public Guid UnitId { get; protected set; }
    public Guid CharacterId { get; protected set; }
    
    #endregion

    #region Factories

    protected Token(
        Guid unitId,
        Guid characterId,
        string value
    )
    {
        UnitId = unitId;
        CharacterId = characterId;
        Value = value;
    }

    public class Factory : ValidatingFactory<Token, Form, CreateValidator>
    {
        public override Token Create(Form form)
        {
            Guard.Against.Null(form.UnitId, nameof(form.UnitId));
            Guard.Against.Null(form.CharacterId, nameof(form.CharacterId));
            Guard.Against.Null(form.Value, nameof(form.Value));
            
            return new Token(
                form.UnitId.Value,
                form.CharacterId.Value,
                form.Value
            );
        }
    }

    public static Factory New => new();

    #endregion

    #region Updaters

    public override Token Update(Form form)
    {
        if (form.UnitId != null) UnitId = form.UnitId.Value;
        if (form.CharacterId != null) CharacterId = form.CharacterId.Value;
        if (form.Value != null) Value = form.Value;
        return this;
    }

    public override Token Clear(Form form)
    {
        Guard.Against.Null(form.UnitId, nameof(form.UnitId));
        Guard.Against.Null(form.CharacterId, nameof(form.CharacterId));
        Guard.Against.Null(form.Value, nameof(form.Value));
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
        Guid? UnitId,
        Guid? CharacterId,
        string? Value
    );
    
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
    public readonly record struct View(
        Guid _id,
        Guid _unitId,
        string Unit,
        Guid _characterId,
        string Character,
        string Value
    );

    public View GetView()
    {
        return new View(
            Id,
            UnitId,
            Unit.Title ?? Unit.Name,
            CharacterId,
            Character.Title,
            Value
        );
    }

    #endregion
}