using Rnd.Models;

namespace Rnd.Bot.Discord.Sessions;

public class Session
{
    private Session(Guid? userId)
    {
        _userId = userId;
        Created = DateTime.Now;
    }

    public static Session Create(Guid? userId)
    {
        return new Session(userId);
    }

    public Guid UserId => _userId ?? throw new NullReferenceException("Not Authorized");


    public DateTimeOffset Created { get; }

    public bool IsAuthorized => _userId != null;

    public void Login(User user)
    {
        _userId = user.Id;
    }

    public void Logout()
    {
        _userId = null;
    }

    private Guid? _userId;
}