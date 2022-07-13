using Discord;
using Discord.Interactions;
using RnDBot.Models.Character;
using RnDBot.Models.CharacterFields;
using RnDBot.Models.Glossaries;
using RnDBot.View;

namespace RnDBot.Controllers;

public class TestController : InteractionModuleBase<SocketInteractionContext>
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

    //test hello world -> hello world
    [SlashCommand("character", "Return a test character")]
    public async Task CharacterAsync([Summary("Name", "Character name")] string name)
    {
        var domains = new List<Domain<AncorniaDomainType, AncorniaSkillType>>
        {
            new(AncorniaDomainType.War, new List<Skill<AncorniaSkillType>>
            {
                GetSkill(AncorniaSkillType.Bruteforce),
                GetSkill(AncorniaSkillType.Struggle),
                GetSkill(AncorniaSkillType.Fortitude),
                GetSkill(AncorniaSkillType.Fencing),
                GetSkill(AncorniaSkillType.HandToHandCombat),
                GetSkill(AncorniaSkillType.Throwing),
                GetSkill(AncorniaSkillType.Shooting),
                GetSkill(AncorniaSkillType.Riding),
            }, 4),
            new(AncorniaDomainType.Mist, new List<Skill<AncorniaSkillType>>
            {
                GetSkill(AncorniaSkillType.Alchemy),
                GetSkill(AncorniaSkillType.Pyrokinetics),
                GetSkill(AncorniaSkillType.Geomancy),
                GetSkill(AncorniaSkillType.Aeroturgy),
                GetSkill(AncorniaSkillType.Hydrosophistry),
                GetSkill(AncorniaSkillType.Enchantment),
                GetSkill(AncorniaSkillType.Priesthood),
                GetSkill(AncorniaSkillType.Necromancy),
                GetSkill(AncorniaSkillType.Demonology),
                GetSkill(AncorniaSkillType.Metamorphism),
            }, 4),
            new(AncorniaDomainType.Way, new List<Skill<AncorniaSkillType>>
            {
                GetSkill(AncorniaSkillType.Climbing),
                GetSkill(AncorniaSkillType.SleightOfHand),
                GetSkill(AncorniaSkillType.Acrobatics),
                GetSkill(AncorniaSkillType.Stealth),
                GetSkill(AncorniaSkillType.Reaction),
                GetSkill(AncorniaSkillType.Tracking),
                GetSkill(AncorniaSkillType.Navigation),
                GetSkill(AncorniaSkillType.Streets),
                GetSkill(AncorniaSkillType.Survival),
            }, 4),
            new(AncorniaDomainType.Word, new List<Skill<AncorniaSkillType>>
            {
                GetSkill(AncorniaSkillType.Empathy),
                GetSkill(AncorniaSkillType.Rhetoric),
                GetSkill(AncorniaSkillType.Manipulation),
                GetSkill(AncorniaSkillType.Networking),
                GetSkill(AncorniaSkillType.Authority),
                GetSkill(AncorniaSkillType.SelfControl),
            }, 4),
            new(AncorniaDomainType.Lore, new List<Skill<AncorniaSkillType>>
            {
                GetSkill(AncorniaSkillType.Research),
                GetSkill(AncorniaSkillType.Engineering),
                GetSkill(AncorniaSkillType.Medicine),
                GetSkill(AncorniaSkillType.Nature),
                GetSkill(AncorniaSkillType.History),
                GetSkill(AncorniaSkillType.Society),
            }, 4),
            new(AncorniaDomainType.Craft, new List<Skill<AncorniaSkillType>>
            {
                GetSkill(AncorniaSkillType.Blacksmith),
                GetSkill(AncorniaSkillType.Farming),
                GetSkill(AncorniaSkillType.Mining),
                GetSkill(AncorniaSkillType.Construction),
                GetSkill(AncorniaSkillType.Leatherworking),
                GetSkill(AncorniaSkillType.Tailoring),
            }, 4),
            new(AncorniaDomainType.Art, new List<Skill<AncorniaSkillType>>
            {
                GetSkill(AncorniaSkillType.Jewelry),
                GetSkill(AncorniaSkillType.Music),
                GetSkill(AncorniaSkillType.Culture),
                GetSkill(AncorniaSkillType.Creation),
                GetSkill(AncorniaSkillType.Inspiration),
                GetSkill(AncorniaSkillType.Performance),
                GetSkill(AncorniaSkillType.Artistry),
            }, 4),
        };

        var character = new Character<AncorniaDomainType, AncorniaSkillType>(name, domains)
        {
            General =
            {
                Culture = { Text = "Культурный челик"},
                Age = { Text = "22" },
                Ideals = { Values = new List<string> { "Вера в какашки", "Сострадание какашкам" }},
                Vices = { Values = new List<string> { "Капрофилия", "Дермовый чел, в целом" }},
                Traits = { Values = new List<string> { "Люблю поесть", "Люблю поспать", "Ленивый", "Окорочек" }},
            }
        };

        foreach (var panel in character.Panels)
        {
            await ReplyAsync(embed: EmbedView.Build(panel));
        }

        await RespondAsync(name + " сгенерирован!");
    }

    private Skill<AncorniaSkillType> GetSkill(AncorniaSkillType type) => new(Glossary.AncorniaSkillCoreAttributes[type], type, 0);
}