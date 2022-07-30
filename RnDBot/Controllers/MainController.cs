using Discord.Interactions;

namespace RnDBot.Controllers;

public class MainController : InteractionModuleBase<SocketInteractionContext>
{
    public Task CharacterAttributesAsync() { return Task.CompletedTask; }
    public Task CharacterPointsAsync() { return Task.CompletedTask; }
    public Task CharacterSkillsAsync() { return Task.CompletedTask; }
    public Task RollSkillAsync() { return Task.CompletedTask; }
    public Task RollDiceAsync() { return Task.CompletedTask; }
    public Task PointAlterAsync() { return Task.CompletedTask; }
    public Task PointRestAsync() { return Task.CompletedTask; }
}