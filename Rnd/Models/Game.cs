using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using FluentValidation;
using Rnd.Constants;
using Rnd.Core;

// EF Proxies
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace Rnd.Models;

public class Game : ValidatableModel<Game.Form, Game.UpdateValidator, Game.ClearValidator>
{
    [MaxLength(TextSize.Tiny)]
    public string Name { get; protected set; }

    [MaxLength(TextSize.Small)]
    public string? Title { get; protected set; }
    
    [MaxLength(TextSize.Medium)]
    public string? Description { get; protected set; }
    
    //public virtual Module? Module { get; protected set; }

    public virtual List<Member> Members { get; protected set; } = new();

    public DateTimeOffset Created { get; protected set; }
    
    public readonly record struct Form(
        string? Name = null, 
        string? Title = null, 
        string? Description = null,
        Guid? ModuleId = null);
    
    #region Navigation
    
    public Guid? ModuleId { get; protected set; }

    #endregion
    
    #region Factories

    protected Game(
        string name,
        string? title,
        string? description,
        Guid? moduleId)
    {
        Name = name;
        Title = title;
        Description = description;
        ModuleId = moduleId;
        Created = Time.Now;
    }
    
    public class Factory : ValidatingFactory<Game, Form, CreateValidator>
    {
        public override Game Create(Form form)
        {
            Guard.Against.NullOrWhiteSpace(form.Name, nameof(form.Name));
            
            return new Game(form.Name, form.Title, form.Description, form.ModuleId);
        }
    }

    public static Factory New => new();
    
    #endregion
    
    #region Updaters
    
    public override void Update(Form form)
    {
        if (form.Name != null) Name = form.Name;
        if (form.Title != null) Title = form.Title;
        if (form.Description != null) Description = form.Description;
        if (form.ModuleId != null) ModuleId = form.ModuleId;
    }

    public override void Clear(Form form)
    {
        Guard.Against.Null(form.Name, nameof(form.Name));
        
        if (form.Title == null) Title = null;
        if (form.Description == null) Description = null;
        if (form.ModuleId == null) ModuleId = null;
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
    
    
    
    #endregion
}