using Discord;
using Discord.Interactions;

namespace RnDBot.Models.Modals;

public class CharacterModal : IModal
{
    public string Title => "Создание персонажа";
    
    [InputLabel("Имя")]
    [ModalTextInput("name", TextInputStyle.Paragraph, "placeholder", 1, 4000, "Хей")]
    public string? Name { get; set; }
    
    [InputLabel("Описание")]
    [ModalTextInput("description", placeholder: "Описание персонажа", maxLength: 200)]
    public string? Description { get; set; }
}