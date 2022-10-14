using System.Web;
using Newtonsoft.Json;

namespace Rnd.Api.Client.Controllers;

public static class Extensions
{
    public static Uri WithParameters(this Uri uri, object parameters)
    {
        var json = JsonConvert.SerializeObject(parameters);
        var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json) 
                         ?? throw new InvalidOperationException("Json deserialization error");
        
        var builder = new UriBuilder(uri);
        var query = HttpUtility.ParseQueryString(builder.Query);
        
        foreach (var (key, value) in dictionary)
        {
            query[key] = value as string ?? JsonConvert.SerializeObject(value);
        }

        builder.Query = query.ToString();
        return builder.Uri;
    }
}