using System.Drawing;
using Rnd.Api.Data;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Games;
using Rnd.Api.Modules.Basic.Users;
using Character = Rnd.Api.Data.Entities.Character;

namespace Rnd.Api.Modules.Basic.Members;

public class Member : IStorable<Data.Entities.Member>
{
    public Member(IEntity entity)
    {
        Id = entity.Id;
        
        Game = null!;
        User = null!;
        Nickname = null!;
        
        Characters = new List<ICharacter>();
    }
    
    public Member(Game game, User user)
    {
        Game = game;
        User = user;
        Nickname = user.Login;
        
        Id = Guid.NewGuid();
        Role = MemberRole.Player;
        Color = ColorHelper.PickRandomDefault();
        Characters = new List<ICharacter>();
        LastActivity = DateTimeOffset.Now.UtcDateTime;
        
        game.Members.Add(this);
        user.Members.Add(this);
    }

    public Guid Id { get; }
    
    public Game Game { get; private set; }
    public User User { get; private set; }
    
    // ReSharper disable once CollectionNeverUpdated.Global
    public List<ICharacter> Characters { get; private set; }

    public MemberRole Role { get; set; }
    public string Nickname { get; set; }
    public Color Color { get; set; }
    
    public DateTimeOffset LastActivity { get; set; }
    
    #region IStorable

    public IStorable<Data.Entities.Member> AsStorable => this;
    
    public Data.Entities.Member? Save(Data.Entities.Member? entity, Action<IEntity>? setAddedState = null, bool upcome = true)
    {
        entity ??= new Data.Entities.Member {Id = Id};
        if (AsStorable.NotSave(entity)) return null;

        entity.Game = Game.AsStorable.SaveNotNull(entity.Game, setAddedState, false);
        if (entity.Game.Members.All(m => m.Id != Id)) entity.Game.Members.Add(entity);
        entity.User = User.AsStorable.SaveNotNull(entity.User, setAddedState, false);
        if (entity.User.Members.All(m => m.Id != Id)) entity.User.Members.Add(entity);
        
        entity.GameId = Game.Id;
        entity.UserId = User.Id;
        entity.Characters.SaveList(Characters.Cast<IStorable<Character>>().ToList(), setAddedState);
        entity.Role = Role;
        entity.Nickname = Nickname;
        entity.ColorHex = ColorTranslator.ToHtml(Color);
        entity.LastActivity = LastActivity;

        return entity;
    }

    public IStorable<Data.Entities.Member>? Load(Data.Entities.Member entity, bool upcome = true)
    {
        if (AsStorable.NotLoad(entity)) return null;

        Game = (Game) new Game(entity.Game).AsStorable.LoadNotNull(entity.Game, false);
        if (Game.Members.All(m => m.Id != entity.GameId)) Game.Members.Add(this);
        User = (User) new User(entity.User).AsStorable.LoadNotNull(entity.User, false);
        if (User.Members.All(m => m.Id != entity.UserId)) User.Members.Add(this);
        
        Characters = Characters
            .Cast<IStorable<Character>>().ToList()
            .LoadList(entity.Characters, new CharacterFactory())
            .Cast<ICharacter>().ToList();
        
        Role = entity.Role;
        Nickname = entity.Nickname;
        Color = ColorTranslator.FromHtml(entity.ColorHex);
        LastActivity = entity.LastActivity;

        return AsStorable;
    }

    #endregion
}