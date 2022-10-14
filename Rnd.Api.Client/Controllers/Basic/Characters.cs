using Rnd.Api.Client.Models.Basic.Character;

namespace Rnd.Api.Client.Controllers.Basic;

public class Characters : Controller<CharacterModel, CharacterFormModel, CharactersSelector>
{
    public Characters(HttpClient client, Uri uri, bool suppressEmbedding = false) : base(client, uri, suppressEmbedding) { }

    protected override string Name => nameof(Characters);
}