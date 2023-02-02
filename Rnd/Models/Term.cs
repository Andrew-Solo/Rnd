using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Rnd.Constants;
using Rnd.Core;

// EF Proxies
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace Rnd.Models;

public class Term : ValidatableModel<Term, Term.Form, Term.UpdateValidator, Term.ClearValidator>
{
    public virtual Module Module { get; protected set; }
    
    [MaxLength(TextSize.Small)]
    public string Name { get; protected set; }
    public TermAccess Access { get; protected set; }
    public TermType Type { get; protected set; }
    public TermRole Role { get; protected set; }
    public dynamic Value { get; protected set; }

    [MaxLength(TextSize.Small)]
    public string? Title { get; protected set; }
    
    [MaxLength(TextSize.Medium)]
    public string? Description { get; protected set; }
    
    public Dictionary<string, dynamic?> Attributes { get; protected set; } = new();

    #region Navigation

    public Guid ModuleId { get; protected set; }
    public virtual List<Token> UsingTokens { get; protected set; } = new();
    
    #endregion

    #region Factories

    protected Term(
        
    )
    {
        
    }

    public class Factory : ValidatingFactory<Term, Form, CreateValidator>
    {
        public override Term Create(Form form)
        {
            return new Term();
        }
    }

    public static Factory New => new();

    #endregion

    #region Updaters

    public override Term Update(Form form)
    {
        return this;
    }

    public override Term Clear(Form form)
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