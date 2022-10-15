using Rnd.Api.Client.Models.Basic.Character;

namespace Rnd.Api.Client.Controllers.Basic;

public class CharactersSelector : Selector<CharacterModel, CharacterFormModel>
{
    public CharactersSelector(HttpClient client, Uri path, IController<CharacterModel, CharacterFormModel> controller) 
        : base(client, path, controller) { }
    
    public Fields Fields => new(Client, Path);
    public Parameters Parameters => new(Client, Path);
    public Resources Resources => new(Client, Path);
    public Effects Effects => new(Client, Path);
}