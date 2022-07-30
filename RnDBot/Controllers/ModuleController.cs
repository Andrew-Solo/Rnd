using Discord.Interactions;

namespace RnDBot.Controllers;

public class ModuleController : InteractionModuleBase<SocketInteractionContext>
{
    #region CharacterController

    // [ModalInteraction("character_create")]
    // public async Task ModalResponse(CharacterModal modal)
    // {
    //     await RespondAsync($"Персонаж **{modal.Name}**.\n{modal.Description}");
    // }

    #endregion
}