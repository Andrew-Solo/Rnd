using Rnd.Models;

namespace Rnd.Bot.Discord.Sessions;

public class Session
{
    private Session(User.View? user)
    {
        User = user;
        Created = DateTime.Now;
    }

    public static Session Create(User.View? user)
    {
        return new Session(user);
    }
    
    public User.View? User { get; private set; }
    public DateTimeOffset Created { get; }

    public bool IsAuthorized => User != null;

    public void Login(User user)
    {
        User = user.GetView();
    }
    
    public void Logout()
    {
        User = null;
    }
}