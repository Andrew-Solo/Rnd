namespace Rnd.Data;

public class UserData : ModelData
{
    public string? Password => Data[nameof(Password)].GetStringOrNull();
}