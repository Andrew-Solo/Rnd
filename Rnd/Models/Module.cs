using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using FluentValidation;
using Rnd.Constants;
using Rnd.Core;

// EF Proxies
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace Rnd.Models;

//TODO Module overriding
public class Module : ValidatableModel<Module, Module.Form, Module.UpdateValidator, Module.ClearValidator>
{
    public virtual List<Unit> Units { get; protected set; } = new();

    [MaxLength(TextSize.Tiny)]
    public string Name { get; protected set; }

    [MaxLength(TextSize.Tiny)]
    public string Version { get; protected set; }

    [MaxLength(TextSize.Small)]
    public string? Title { get; protected set; }

    [MaxLength(TextSize.Medium)]
    public string? Description { get; protected set; }

    public Dictionary<string, dynamic> Attributes { get; protected set; }

    #region Navigation

    public List<Character> UsingCharacters { get; protected set; } = new();
    public List<Game> UsingGames { get; protected set; } = new();
    
    #endregion

    #region Factories

    protected Module(
        string name,
        string version,
        string? title,
        string? description,
        Dictionary<string, dynamic>? attributes
    )
    {
        Name = name;
        Version = version;
        Title = title;
        Description = description;
        Attributes = attributes ?? new Dictionary<string, dynamic>();
    }

    public class Factory : ValidatingFactory<Module, Form, CreateValidator>
    {
        public override Module Create(Form form)
        {
            Guard.Against.NullOrWhiteSpace(form.Name, nameof(form.Name));
            
            return new Module(
                form.Name,
                form.Version ?? "0.0.1",
                form.Title,
                form.Description,
                form.Attributes
            );
        }
    }

    public static Factory New => new();

    #endregion

    #region Updaters

    public override Module Update(Form form)
    {
        if (form.Name != null) Name = form.Name;
        if (form.Version != null) Version = form.Version;
        if (form.Title != null) Title = form.Title;
        if (form.Description != null) Description = form.Description;
        if (form.Attributes != null) Attributes.Merge(form.Attributes);
        return this;
    }

    public override Module Clear(Form form)
    {
        Guard.Against.NullOrWhiteSpace(form.Name, nameof(form.Name));
        Guard.Against.NullOrWhiteSpace(form.Version, nameof(form.Version));
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
        string? Name,
        string? Version,
        string? Title,
        string? Description,
        Dictionary<string, dynamic>? Attributes
    );
    
    public readonly record struct View(
        Guid _id,
        string Name,
        string Version,
        string? Title,
        string? Description,
        Dictionary<string, dynamic> Attributes,
        Guid[] _unitIds,
        string[] Units
    );

    public View GetView()
    {        
        return new View(
            Id,
            Name,
            Version,
            Title,
            Description,
            Attributes,
            Units.Select(u => u.Id).ToArray(),
            Units.Select(u => u.Title ?? u.Name).ToArray()
        );
    }

    #endregion
}