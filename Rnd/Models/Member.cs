using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Ardalis.GuardClauses;
using FluentValidation;
using Rnd.Constants;
using Rnd.Core;

// EF Proxies
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
#pragma warning disable CS8618

namespace Rnd.Models;

public class Member : ValidatableModel<Member, Member.Form, Member.UpdateValidator, Member.ClearValidator>
{
    public virtual Game Game { get; protected set; }
    
    public virtual User User { get; protected set; }
    
    [MaxLength(TextSize.Small)]
    public string Nickname { get; protected set; }

    [MaxLength(TextSize.Tiny)] 
    public MemberRole Role { get; protected set; }
    
    [MaxLength(TextSize.Tiny)]
    public string ColorHtml { get; protected set; }

    //public virtual List<Character> Characters { get; protected set; } = new();
    
    public DateTimeOffset Created { get; protected set; }
    
    public DateTimeOffset Selected { get; protected set; }
    
    public readonly record struct Form(
        Guid? GameId = null,
        Guid? UserId = null,
        MemberRole? Role = null, 
        string? Nickname = null,
        string? ColorHtml = null);
    
    #region Navigation

    public Guid GameId { get; protected set; }
    
    public Guid UserId { get; protected set; }

    #endregion
    
    #region Factories
    
    protected Member(
        Guid gameId,
        Guid userId,
        string nickname,
        MemberRole? role,
        string? colorHtml)
    {
        GameId = gameId;
        UserId = userId;
        Nickname = nickname;
        Role = role ?? MemberRole.Player;
        ColorHtml = colorHtml ?? Colors.GetRandomHtml();
        Selected = Time.Zero;
        Created = Time.Now;
    }
    
    public class Factory : ValidatingFactory<Member, Form, CreateValidator>
    {
        public override Member Create(Form form)
        {
            Guard.Against.NullOrEmpty(form.GameId, nameof(GameId));
            Guard.Against.NullOrEmpty(form.UserId, nameof(UserId));
            Guard.Against.NullOrEmpty(form.Nickname, nameof(Nickname));
            
            return new Member(form.GameId.Value, form.UserId.Value, form.Nickname, form.Role, form.ColorHtml);
        }
    }

    public static Factory New => new();
    
    #endregion
    
    #region Updaters
    
    public override Member Update(Form form)
    {
        if (form.GameId != null) GameId = form.GameId.Value;
        if (form.UserId != null) UserId = form.UserId.Value;
        if (form.Nickname != null) Nickname = form.Nickname;
        if (form.Role != null) Role = form.Role.Value;
        if (form.ColorHtml != null) ColorHtml = form.ColorHtml;

        return this;
    }

    public void Select()
    {
        Selected = Time.Now;
    }

    public override Member Clear(Form form)
    {
        Guard.Against.Null(form.GameId);
        Guard.Against.Null(form.UserId);
        Guard.Against.Null(form.Nickname);
        Guard.Against.Null(form.Role);
        Guard.Against.Null(form.ColorHtml);
        
        return this;
    }
    
    #endregion
    
    #region Validators

    public class UpdateValidator : AbstractValidator<Form> 
    {
        public UpdateValidator()
        {
            RuleFor(u => u.Nickname)
                .Length(TextSize.Symbol, TextSize.Small).WithMessage("Длина никнейма должна быть от {MinLength} до {MaxLength} символов, " +
                                                                     "сейчас {TotalLength}");
            
            RuleFor(u => u.ColorHtml)
                .Length(TextSize.Symbol, TextSize.Tiny).WithMessage("Длина цветового кода должна быть от {MinLength} до {MaxLength} " +
                                                                    "символов, сейчас {TotalLength}")
                .Must(c =>
                {
                    if (c == null) return true;
                
                    try
                    {
                        ColorTranslator.FromHtml(c);
                    }
                    catch (Exception)
                    {
                        return false;
                    }

                    return true;
                }).WithMessage("Цвет не распознан. Цвет должен иметь Html-формат");
        }
    }
    
    public class CreateValidator : UpdateValidator
    {
        public CreateValidator()
        {
            RuleFor(u => u.GameId).NotNull().WithMessage("Игра должна быть указана");
            RuleFor(u => u.UserId).NotNull().WithMessage("Пользователь должен быть указан");
            RuleFor(u => u.Nickname).NotNull().WithMessage("Прозвище должно быть указано");

        }
    }
    
    public class ClearValidator : AbstractValidator<Form> 
    {
        public ClearValidator()
        {
            RuleFor(u => u.GameId).NotNull().WithMessage("Нельзя очистить игру");
            RuleFor(u => u.UserId).NotNull().WithMessage("Нельзя очистить пользователя");
            RuleFor(u => u.Nickname).NotNull().WithMessage("Нельзя очистить прозвище");
            RuleFor(u => u.Role).NotNull().WithMessage("Нельзя очистить роль");
            RuleFor(u => u.ColorHtml).NotNull().WithMessage("Нельзя очистить цвет");
        }
    }

    #endregion
    
    #region Views
    
    
    
    #endregion
}