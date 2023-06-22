namespace Rnd.Data;

public class Request
{
    protected Request(string user)
    {
        User = new Path(user);
        Users = new Path("");
        Modules = new Path("");
        Units = new Path("");
        Fields = new Path("");
    }

    public Path User { get; }
    public Path Users { get; }
    public Path Modules { get; }
    public Path Units { get; }
    public Path Fields { get; }

    public static Request Create(string user)
    {
        return new Request(user);
    }

    public Request WithUsers(string value)
    {
        Users.Set(value);
        return this;
    }

    public Request WithModules(string value)
    {
        Modules.Set(value);
        return this;
    }

    public Request WithUnits(string value)
    {
        Units.Set(value);
        return this;
    }

    public Request WithFields(string value)
    {
        Fields.Set(value);
        return this;
    }
}