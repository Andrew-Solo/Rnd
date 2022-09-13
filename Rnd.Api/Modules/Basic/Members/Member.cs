using System.Drawing;
using Rnd.Api.Data;
using Rnd.Api.Helpers;
using Rnd.Api.Localization;
using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Games;
using Rnd.Api.Modules.Basic.Users;
using Character = Rnd.Api.Data.Entities.Character;

namespace Rnd.Api.Modules.Basic.Members;

public class Member : IStorable<Data.Entities.Member>
{
    public Member(Game game, User user)
    {
        Game = game;
        User = user;

        Id = Guid.NewGuid();
        Role = MemberRole.Player;
        Nickname = user.Login;
        Color = ColorHelper.PickRandomDefault();
        Characters = new List<ICharacter>();
        LastActivity = DateTime.Now;
    }

    public Guid Id { get; }
    
    public Game Game { get; private set; }
    public User User { get; private set; }
    public List<ICharacter> Characters { get; }
    
    public MemberRole Role { get; set; }
    public string Nickname { get; set; }
    public Color Color { get; set; }
    
    public DateTime LastActivity { get; set; }
    
    #region IStorable

    public IStorable<Data.Entities.Member> AsStorable => this;
    
    public Data.Entities.Member? Save(Data.Entities.Member? entity)
    {
        entity ??= new Data.Entities.Member {Id = Id};
        if (AsStorable.NotSave(entity)) return null;
        
        entity.Game = Game.AsStorable.SaveNotNull(entity.Game);
        entity.User = User.AsStorable.SaveNotNull(entity.User);
        entity.Characters.SaveList(Characters.Cast<IStorable<Character>>().ToList());
        entity.Role = Role;
        entity.Nickname = Nickname;
        entity.ColorHex = Color.ToHex();
        entity.LastActivity = LastActivity;

        return entity;
    }

    public void Load(Data.Entities.Member entity)
    {
        if (AsStorable.NotLoad(entity)) return;

        Game = GameFactory.ByEntity(entity.Game);
        User = UserFactory.ByEntity(entity.User);

        Characters.Clear();
        Characters.AddRange(entity.Characters.Select(CharacterFactory.ByEntity));
        
        Role = entity.Role;
        Nickname = entity.Nickname;
        Color = ColorTranslator.FromHtml(entity.ColorHex);
        entity.LastActivity = LastActivity;
    }

    #endregion
}