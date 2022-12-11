using System.Drawing;
using FluentValidation;
using Newtonsoft.Json;
using Rnd.Api.Client.Models.Basic.Member;
using Rnd.Api.Data.Entities;

namespace Rnd.Api.Controllers.Validation.MemberModel;

public class MemberFormModelValidator : AbstractValidator<MemberFormModel> 
{
    public MemberFormModelValidator()
    {
        RuleFor(u => u.Nickname)
            .Length(1, 50).WithMessage("Длина никнейма должна быть от 1 до 50 символов, сейчас {TotalLength}");
        
        RuleFor(u => u.ColorHtml)
            .Length(3, 32).WithMessage("Длина цветового кода должна быть от 3 до 32 символов, сейчас {TotalLength}")
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
        
        RuleFor(u => u.Role)
            .Must(r =>
            {
                if (r == null) return true;
                
                try
                {
                    JsonConvert.DeserializeObject<MemberRole>($"\"{r}\"");
                }
                catch (Exception)
                {
                    return false;
                }

                return true;
            }).WithMessage("Данная роль не существует. Используйте одну из ролей: Admin, Guide, Player")
            .NotEqual(MemberRole.Owner.ToString())
            .WithMessage("Невозможно присваивать участнику роль Owner");
    }
}