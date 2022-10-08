using Rnd.Api.Client.Models.Basic.Character;

namespace Rnd.Api.Client.Controllers.Basic;

public class Characters : Controller<CharacterModel, CharacterCreateModel, CharacterEditModel, CharactersSelector>
{
    public Characters(HttpClient client, string path, bool suppressEmbedding = false) : base(client, path, suppressEmbedding) { }

    protected override string Name => nameof(Characters);
}