using Discord;
using Discord.Interactions;

namespace RnDBot.Modules;

public class TestModule : InteractionModuleBase<SocketInteractionContext>
{
    //test hello world -> hello world
    [SlashCommand("test", "Echo an input")]
    public Task TestAsync([Summary("Text", "The text to echo")] string echo)
        => RespondAsync(echo);
    
    //test hello world -> hello world
    [SlashCommand("embed", "Embed an input")]
    public Task EmbedAsync([Summary("Text", "The text to embed")] string echo)
    {
        var user = Context.User;

        var embedBuilder = new EmbedBuilder()
            .WithAuthor(user.Username, user.GetAvatarUrl() ?? user.GetDefaultAvatarUrl())
            .WithColor(255, 20, 20)
            .WithDescription($"Сообщение: {echo}")
            .WithFooter(new EmbedFooterBuilder().WithText("Footer").WithIconUrl(user.GetAvatarUrl()))
            .WithTimestamp(new DateTimeOffset(2000, 01, 01, 0, 0, 0, TimeSpan.Zero))
            .WithTitle("Заголовок")
            .WithUrl("https://www.google.com/")
            .WithCurrentTimestamp()
            .WithImageUrl(user.GetAvatarUrl())
            .WithThumbnailUrl(user.GetDefaultAvatarUrl())
            .WithFields(new List<EmbedFieldBuilder>
            {
                new EmbedFieldBuilder().WithName("Домен [4]").WithValue(@"```md
[ИНТ Воображение](4)
[СИЛ Навык](4)
[ЛОВ Грубая ловкость](12)
[ЛОВ Юфелирное дело](8)
[ИНТ Знания](6)
[ИНТ Воображение](4)
                    ```"),
                new EmbedFieldBuilder().WithName("Домен [4]").WithValue(@"```md
[ИНТ Воображение](4)
[СИЛ Навык](4)
[ЛОВ Грубая ловкость](12)
[ЛОВ Юфелирное дело](8)
[ИНТ Знания](6)
[ИНТ Воображение](4)
                    ```"),
            })
            .WithFields(new List<EmbedFieldBuilder>
            {
                new EmbedFieldBuilder().WithName("Тело").WithValue("```md\n<_8 / _12>\n```").WithIsInline(true),
                new EmbedFieldBuilder().WithName("Сила").WithValue("```md\n# +3\n```").WithIsInline(true),
                new EmbedFieldBuilder().WithName("Телосложение").WithValue("```md\n# -2\n```").WithIsInline(true),
                new EmbedFieldBuilder().WithName("Ловкость").WithValue("```md\n# +0\n```").WithIsInline(true),
                new EmbedFieldBuilder().WithName("Восприятие").WithValue("```md\n# -1\n```").WithIsInline(true),
            });
        
            // .WithAuthor(guildUser.ToString(), guildUser.GetAvatarUrl() ?? guildUser.GetDefaultAvatarUrl())
            // .WithTitle("Roles")
            // .WithDescription(roleList)
            // .WithColor(Color.Green)
            // .WithCurrentTimestamp();
        
        return RespondAsync(embed: embedBuilder.Build());
    }
}