using Rnd.Primitives;

namespace Rnd.Data;

public class UserData : ModelData
{
    public string? Password => this[nameof(Password)].GetStringOrNull();
    public Role? Role => this[nameof(Role)].GetEnumOrNull<Role>();
    public List<Association>? Associations => this[nameof(Associations)].GetObject<List<Association>>();
}