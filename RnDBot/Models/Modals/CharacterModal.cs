using Discord;
using Discord.Interactions;

namespace RnDBot.Models.Modals;

public class CharacterModal : IModal
{
    public string Title => "Создание персонажа";
    
    [InputLabel("Имя")]
    [ModalTextInput("name", TextInputStyle.Short, "Рафаэль Амброзиус Кусто", 1, 50)]
    public string? Name { get; set; }
    
    [InputLabel("Уровень")]
    [ModalTextInput("level", TextInputStyle.Short, "от -80 до 80", 1, 3, "0")]
    public string? Level { get; set; }
    
    [InputLabel("Описание")]
    [RequiredInput(false)]
    [ModalTextInput("description", TextInputStyle.Paragraph, "Время концептуализировать!", 0, 800)]
    public string? Description { get; set; }
    
    [InputLabel("Культура")]
    [ModalTextInput("culture", TextInputStyle.Short, "Ваше происхождение", 1, 100)]
    public string? Culture { get; set; }
    
    [InputLabel("Возраст")]
    [ModalTextInput("age", TextInputStyle.Short, "от 0 до 999", 1, 3)]
    public string? Age { get; set; }
    
    [InputLabel("Идеалы")]
    [ModalTextInput("ideals", TextInputStyle.Paragraph, "Перечислите ваши принципы через запятую.", 1, 400)]
    public string? Ideals { get; set; }
    
    [InputLabel("Пороки")]
    [ModalTextInput("vices", TextInputStyle.Paragraph, "Перечислите ваши пороки через запятую.", 1, 400)]
    public string? Vices { get; set; }
    
    [InputLabel("Черты")]
    [ModalTextInput("traits", TextInputStyle.Paragraph, "Перечислите ваши черты через запятую.", 1, 400)]
    public string? Traits { get; set; }
}