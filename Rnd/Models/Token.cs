using FluentValidation;
using Rnd.Core;

// EF Proxies
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace Rnd.Models;

public class Token : ValidatableModel<Token, Token.Form, Token.UpdateValidator, Token.ClearValidator>
{
    public virtual Unit Unit { get; protected set; }
    
    public virtual Character Character { get; protected set; }
    
    public dynamic Value { get; protected set; }
    
    #region Navigation

    public Guid UnitId { get; protected set; }
    public Guid? CharacterId { get; protected set; }
    
    #endregion

    #region Factories

    protected Token(
        
    )
    {
        
    }

    public class Factory : ValidatingFactory<Token, Form, CreateValidator>
    {
        public override Token Create(Form form)
        {
            return new Token();
        }
    }

    public static Factory New => new();

    #endregion

    #region Updaters

    public override Token Update(Form form)
    {
        return this;
    }

    public override Token Clear(Form form)
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