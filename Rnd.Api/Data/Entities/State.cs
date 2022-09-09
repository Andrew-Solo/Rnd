namespace Rnd.Api.Data.Entities;

public class State
{
    public Guid Id { get; set; }
    public StateType Type { get; set; }
    public int Value { get; set; }
}