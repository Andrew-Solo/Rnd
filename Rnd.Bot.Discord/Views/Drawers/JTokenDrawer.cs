using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Rnd.Bot.Discord.Views.Drawers;

public class JTokenDrawer : Drawer<JToken>
{
    public override string Draw(JToken? value)
    {
        return value?.Type switch
        {
            JTokenType.Object => Drawer.Draw(ViewData.ToDictionary(value)),
            JTokenType.Array => Drawer.Draw(JsonConvert.DeserializeObject<List<string?>>(ViewData.ToJson(value)) ?? new List<string?>()), //TODO Cannot cast Newtonsoft.Json.Linq.JArray to Newtonsoft.Json.Linq.JToken.
            JTokenType.Date => Drawer.Draw(value.Value<DateTimeOffset>()),
            JTokenType.TimeSpan => Drawer.Draw(value.Value<TimeSpan>()),
            JTokenType.String => value.Value<string>(),
            JTokenType.Uri => value.Value<string>(),
            JTokenType.Integer => value.Value<long>().ToString(),
            JTokenType.Float => value.Value<double>().ToString(CultureInfo.CurrentCulture),
            JTokenType.Bytes => value.Value<byte[]>()?.ToString(),
            JTokenType.Boolean => value.Value<bool>().ToString(),
            JTokenType.Guid => value.Value<Guid>().ToString(),
            JTokenType.Undefined => null,
            JTokenType.Null => null,
            null => null,
            // JTokenType.None => expr,
            // JTokenType.Constructor => expr,
            // JTokenType.Property => expr,
            // JTokenType.Comment => expr,
            // JTokenType.Raw => expr,
            _ => throw new ArgumentOutOfRangeException()
        } ?? "—";
    }
}