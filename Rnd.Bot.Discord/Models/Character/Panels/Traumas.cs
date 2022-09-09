using Newtonsoft.Json;
using Rnd.Bot.Discord.Models.Character.Panels.Effect;
using Rnd.Bot.Discord.Views;
using ValueType = Rnd.Bot.Discord.Views.ValueType;

namespace Rnd.Bot.Discord.Models.Character.Panels;

public class Traumas : IPanel, IValidatable, IEffectProvider
{
    public Traumas(ICharacter character)
    {
        Character = character;
        TraumaEffects = new List<TraumaEffect>();
    }
    
    [JsonConstructor]
    public Traumas(ICharacter character, List<TraumaEffect> traumaEffects)
    {
        Character = character;
        TraumaEffects = traumaEffects;
    }

    [JsonIgnore]
    public readonly ICharacter Character;
    
    public List<TraumaEffect> TraumaEffects { get; }
    
    public IEnumerable<IEffect> GetEffects()
    {
        return TraumaEffects;
    }
    
    [JsonIgnore]
    public string Title => "Травмы";
    
    [JsonIgnore]
    public string Description => EmbedView.Build(TraumaEffects.Select(e => e.View).ToArray(), ValueType.List);

    [JsonIgnore] 
    public string Footer => Character.GetFooter;

    [JsonIgnore]
    public bool IsValid
    {
        get
        {
            var valid = true;
            // var errors = new List<string>();

            //No validation events

            // Errors = errors.ToArray();
            return valid;
        }
    }

    [JsonIgnore] 
    public string[]? Errors { get; } = Array.Empty<string>();
}