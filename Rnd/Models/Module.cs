using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Rnd.Constants;
using Rnd.Core;

// EF Proxies
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace Rnd.Models;

//TODO Module overriding
public class Module : ValidatableModel<Module, Module.Form, Module.UpdateValidator, Module.ClearValidator>
{
    [MaxLength(TextSize.Tiny)]
    public string Name { get; protected set; }

    [MaxLength(TextSize.Tiny)]
    public string Version { get; protected set; }

    [MaxLength(TextSize.Small)]
    public string? Title { get; protected set; }

    [MaxLength(TextSize.Medium)]
    public string? Description { get; protected set; }
    
    public List<Term> Terms { get; protected set; } = new();

    #region Navigation

    public List<Character> UsingCharacters { get; protected set; } = new();
    public List<Game> UsingGames { get; protected set; } = new();
    
    #endregion

    #region Factories

    protected Module(
        
    )
    {
        
    }

    public class Factory : ValidatingFactory<Module, Form, CreateValidator>
    {
        public override Module Create(Form form)
        {
            return new Module();
        }
    }

    public static Factory New => new();

    #endregion

    #region Updaters

    public override Module Update(Form form)
    {
        return this;
    }

    public override Module Clear(Form form)
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