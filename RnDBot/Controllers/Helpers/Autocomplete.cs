using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace RnDBot.Controllers.Helpers;

public class Autocomplete<T>
{
    public Autocomplete(SocketInteractionContext context, Dictionary<string, T> elements)
    {
        _context = context;

        Elements = elements;
    }
    
    public Autocomplete(SocketInteractionContext context, IEnumerable<string> collection, Func<string, T> valueSelector) 
        : this(context, collection.ToDictionary(k => k, valueSelector))
    {}

    public List<string> Collection => Elements.Keys.ToList();
    public Dictionary<string, T> Elements { get; }

    public IEnumerable<AutocompleteResult> Result => GetResult().Select(name => new AutocompleteResult(name, Elements[name]));

    public async Task RespondAsync() => await Interaction.RespondAsync(Result);
    
    private IEnumerable<string> GetResult()
    {
        var result = new List<string>(25);
        
        result.AddRange(Collection.Where(c => c.ToLower().StartsWith(UserInput)));

        if (result.Count > 25) return result.Take(25);
        
        result.AddRange(Collection.Where(c => c.ToLower().Contains(UserInput)).Except(result));

        return result.Take(25);
    }

    private string UserInput => Interaction.Data.Current.Value.ToString()?.ToLower() ?? String.Empty;

    private SocketAutocompleteInteraction Interaction =>
        _context.Interaction as SocketAutocompleteInteraction 
        ?? throw new InvalidOperationException();
    
    private readonly SocketInteractionContext _context;
}