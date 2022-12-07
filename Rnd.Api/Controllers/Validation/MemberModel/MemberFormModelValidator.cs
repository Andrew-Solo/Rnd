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
            .Length(6, 32).WithMessage("Длина цветового кода должна быть от 6 до 32 символов, сейчас {TotalLength}")
            .Must(c =>
            {
                try
                {
                    var color = ColorTranslator.FromHtml(c);
                }
                catch (Exception e)
                {
                    return false;
                }

                return true;
            }).WithMessage("Цвет не распознан. Цвет должен иметь Html-формат. ");
        
        RuleFor(u => u.Role)
            .Must(r =>
            {
                try
                {
                    var json = JsonConvert.DeserializeObject<MemberRole>($"\"{r}\"");
                }
                catch (Exception e)
                {
                    return false;
                }

                return true;
            }).WithMessage("Данная роль не существует. Используйте одну из ролей: Owner, Admin, Guide, Player");
    }
}