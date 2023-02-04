using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using FluentValidation;
using Rnd.Constants;
using Rnd.Core;

// EF Proxies
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace Rnd.Models;

public class Game : ValidatableModel<Game, Game.Form, Game.UpdateValidator, Game.ClearValidator>
{
    //TODO Title#Name view
    [MaxLength(TextSize.Tiny)]
    public string Name { get; protected set; }

    public virtual Module Module { get; protected set; }

    public virtual List<Member> Members { get; protected set; } = new();

    [MaxLength(TextSize.Small)]
    public string? Title { get; protected set; }

    [MaxLength(TextSize.Medium)]
    public string? Description { get; protected set; }

    public DateTimeOffset Created { get; protected set; }
    
    #region Navigation
    
    public Guid ModuleId { get; protected set; }

    #endregion
    
    #region Factories

    protected Game(
        string name,
        Guid moduleId,
        string? title,
        string? description)
    {
        Name = name;
        ModuleId = moduleId;
        Title = title;
        Description = description;
        Created = Time.Now;
    }
    
    public class Factory : ValidatingFactory<Game, Form, CreateValidator>
    {
        public override Game Create(Form form)
        {
            Guard.Against.NullOrWhiteSpace(form.Name, nameof(form.Name));
            Guard.Against.Null(form.ModuleId, nameof(form.ModuleId));
            
            return new Game(
                form.Name, 
                form.ModuleId.Value,
                form.Title, 
                form.Description
            );
        }
    }

    public static Factory New => new();
    
    #endregion
    
    #region Updaters
    
    public override Game Update(Form form)
    {
        if (form.Name != null) Name = form.Name;
        if (form.ModuleId != null) ModuleId = form.ModuleId.Value;
        if (form.Title != null) Title = form.Title;
        if (form.Description != null) Description = form.Description;
        return this;
    }

    public override Game Clear(Form form)
    {
        Guard.Against.Null(form.Name, nameof(form.Name));
        Guard.Against.Null(form.ModuleId, nameof(form.ModuleId));
        if (form.Title == null) Title = null;
        if (form.Description == null) Description = null;
        return this;
    }
    
    #endregion
    
    #region Validators

    public class UpdateValidator : AbstractValidator<Form> 
    {
        public UpdateValidator()
        {
            RuleFor(u => u.Name)
                .Matches("^[A-Za-z0-9_]*$").WithMessage("Имя содержит запрещенные символы, разрешены только латинские буквы, " +
                                                        "цифры и нижнее подчеркивание")
                .Length(TextSize.Word, TextSize.Tiny).WithMessage("Длина имени должна быть от {MinLength} до {MaxLength} символов, " +
                                                                  "сейчас {TotalLength}");
        
            RuleFor(u => u.Title)
                .Length(TextSize.Symbol, TextSize.Small).WithMessage("Длина названия должна быть от {MinLength} до {MaxLength} символов, " +
                                                                     "сейчас {TotalLength}");
        
            RuleFor(u => u.Description)
                .Length(TextSize.Symbol, TextSize.Medium).WithMessage("Длина описания должна быть от {MinLength} до {MaxLength} символов," +
                                                                      " сейчас {TotalLength}");
        }
    }
    
    public class CreateValidator : UpdateValidator
    {
        public CreateValidator()
        {
            RuleFor(u => u.Name).NotNull().WithMessage("Имя игры должно быть указано");
        }
    }
    
    public class ClearValidator : AbstractValidator<Form> 
    {
        public ClearValidator()
        {
            RuleFor(u => u.Name).NotNull().WithMessage("Нельзя очистить имя игры");
        }
    }

    #endregion
    
    #region Views
    
    public readonly record struct Form(
        string? Name = null, 
        Guid? ModuleId = null,
        string? Title = null, 
        string? Description = null
    );
    
    public readonly record struct View(
        Guid _id,
        string Name,
        Guid _moduleId,
        string Module,
        Guid[] _memberIds,
        string[] Members,
        string? Title,
        string? Description,
        DateTimeOffset Created
    );
    
    public View GetView()
    {
        var members = Members
            .OrderByDescending(m => m.Selected)
            .ToDictionary(m => m.Id, g => g.Nickname);
        
        return new View(
            Id, 
            Name, 
            ModuleId,
            (Module.Title ?? Module.Name) + " v" + Module.Version,
            members.Keys.ToArray(), 
            members.Values.ToArray(), 
            Title, 
            Description, 
            Created
        );
    } 
    
    #endregion
}