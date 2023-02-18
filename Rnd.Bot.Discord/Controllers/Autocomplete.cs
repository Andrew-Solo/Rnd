using Discord;
using Discord.Interactions;

namespace Rnd.Bot.Discord.Controllers;

public class Autocomplete<T>
{
    public Autocomplete(IEnumerable<T> collection, Func<T, string> getKey, Func<T, object>? getValue = null)
    {
        Elements = collection.ToDictionary(getKey, getValue ?? getKey);
    }

    public List<string> Collection => Elements.Keys.ToList();
    public Dictionary<string, object> Elements { get; }
    
    public async Task RespondAsync(SocketInteractionContext context)
    {
        await context.Interaction.AsAutocomplete().RespondAsync(GetResult(context));
    }

    public IEnumerable<AutocompleteResult> GetResult(SocketInteractionContext context)
    {
        return GetKeys(context).Select(key => new AutocompleteResult(key, Elements[key]));
    }
    
    public IEnumerable<string> GetKeys(SocketInteractionContext context)
    {
        var input = context.Interaction.AsAutocomplete().Data.Current.Value.ToString()?.ToLower() ?? String.Empty;
        var result = new List<string>(25);
        
        result.AddRange(Collection.Where(c => c.ToLower().StartsWith(input)));

        if (result.Count > 25) return result.Take(25);
        
        result.AddRange(Collection.Where(c => c.ToLower().Contains(input)).Except(result));

        return result.Take(25);
    }
}