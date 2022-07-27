using Discord;
using Discord.Interactions;

namespace RnDBot.Models.Modals;

// Defines the modal that will be sent.
public class FooodModal : IModal
{
    public string Title => "Fav Food";

    // Strings with the ModalTextInput attribute will automatically become components.
    [InputLabel("What??")]
    [ModalTextInput("food_name", placeholder: "Pizza", maxLength: 20)]
    public string Food { get; set; } = null!;

    // Additional paremeters can be specified to further customize the input.
    [InputLabel("Why??")]
    [ModalTextInput("food_reason", TextInputStyle.Paragraph, "Kuz it's tasty", maxLength: 500)]
    public string Reason { get; set; } = null!;
}