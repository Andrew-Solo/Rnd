using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Rnd.Constants;
using Rnd.Core;

// EF Proxies
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace Rnd.Models;

public class Character : ValidatableModel<Character, Character.Form, Character.UpdateValidator, Character.ClearValidator>
{
    public virtual Member Owner { get; protected set; }
    public virtual Module Module { get; protected set; }
    public virtual List<Token> Tokens { get; protected set; }
    
    [MaxLength(TextSize.Tiny)]
    public string Name { get; protected set; }
    
    [MaxLength(TextSize.Small)]
    public string? Title { get; protected set; }
    
    [MaxLength(TextSize.Medium)]
    public string? Description { get; protected set; }
    
    [MaxLength(TextSize.Tiny)]
    public string? ColorHtml { get; protected set; }
    
    public DateTimeOffset Created { get; protected set; }
    public DateTimeOffset Selected { get; protected set; }
    
    #region Navigation
    
    public Guid OwnerId { get; protected set; }
    public Guid ModuleId { get; protected set; }

    #endregion

    #region Factories

    protected Character(
        
    )
    {
        
    }

    public class Factory : ValidatingFactory<Character, Form, CreateValidator>
    {
        public override Character Create(Form form)
        {
            return new Character();
        }
    }

    public static Factory New => new();

    #endregion

    #region Updaters

    public override Character Update(Form form)
    {
        return this;
    }

    public override Character Clear(Form form)
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