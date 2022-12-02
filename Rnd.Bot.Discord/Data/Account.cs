namespace Rnd.Bot.Discord.Data;

public class Account
{
    public Guid Id { get; set; }
    public Guid RndId { get; set; }
    public ulong DiscordId { get; set; }
    public DateTimeOffset Authorized { get; set; }
}