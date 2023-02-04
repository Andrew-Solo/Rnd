using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;
using FluentValidation;
using Rnd.Constants;
using Rnd.Core;

// EF Proxies
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
#pragma warning disable CS8618

namespace Rnd.Models;

public class Character : ValidatableModel<Character, Character.Form, Character.UpdateValidator, Character.ClearValidator>
{
    public virtual Member Owner { get; protected set; }
    public virtual Module Module { get; protected set; }

    [MaxLength(TextSize.Small)]
    public string Title { get; protected set; }
    
    [MaxLength(TextSize.Medium)]
    public string? Description { get; protected set; }
    
    [MaxLength(TextSize.Tiny)]
    public string? ColorHtml { get; protected set; }
    
    public DateTimeOffset Created { get; protected set; }
    public DateTimeOffset Selected { get; protected set; }
    
    
    #region Navigation
    
    public Guid OwnerId { get; protected set; }
    public Guid ModuleId { get; protected set; }
    public virtual List<Token> Tokens { get; protected set; } = new();

    #endregion

    #region Factories

    protected Character(
        Guid ownerId,
        Guid moduleId,
        string title,
        string? description,
        string? colorHtml
    )
    {
        OwnerId = ownerId;
        ModuleId = moduleId;
        Title = title;
        Description = description;
        ColorHtml = colorHtml;
        Created = Time.Now;
        Selected = Time.Zero;
    }

    public class Factory : ValidatingFactory<Character, Form, CreateValidator>
    {
        public override Character Create(Form form)
        {
            Guard.Against.Null(form.OwnerId, nameof(form.OwnerId));
            Guard.Against.Null(form.ModuleId, nameof(form.ModuleId));
            Guard.Against.Null(form.Title, nameof(form.Title));
            
            return new Character(
                form.OwnerId.Value, 
                form.ModuleId.Value,
                form.Title, 
                form.Description, 
                form.ColorHtml
            );
        }
    }

    public static Factory New => new();

    #endregion

    #region Updaters

    public override Character Update(Form form)
    {
        if (form.OwnerId != null) OwnerId = form.OwnerId.Value;
        if (form.ModuleId != null) ModuleId = form.ModuleId.Value;
        if (form.Title != null) Title = form.Title;
        if (form.Description != null) Description = form.Description;
        if (form.ColorHtml != null) ColorHtml = form.ColorHtml;
        return this;
    }

    public override Character Clear(Form form)
    {
        Guard.Against.Null(form.OwnerId, nameof(form.OwnerId));
        Guard.Against.Null(form.ModuleId, nameof(form.ModuleId));
        Guard.Against.Null(form.Title, nameof(form.Title));
        if (form.Description == null) Description = null;
        if (form.ColorHtml == null) ColorHtml = null;
        return this;
    }
    
    public void Select()
    {
        Selected = Time.Now;
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
        Guid? OwnerId,
        Guid? ModuleId,
        string? Title,
        string? Description,
        string? ColorHtml
    );
    
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
    public readonly record struct View(
        Guid _id,
        Guid _ownerId,
        string Owner,
        Guid _moduleId,
        string Module,
        string? Title,
        string? Description,
        string? ColorHtml,
        DateTimeOffset Created,
        DateTimeOffset Selected
    );

    public View GetView()
    {
        return new View(
            Id,
            OwnerId,
            Owner.Nickname,
            ModuleId,
            Module.VersionedTitle,
            Title,
            Description,
            ColorHtml ?? Owner.ColorHtml,
            Created,
            Selected
        );
    }

    #endregion
}