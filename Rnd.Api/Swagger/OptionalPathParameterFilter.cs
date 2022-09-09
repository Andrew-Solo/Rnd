using System.Text.RegularExpressions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Rnd.Api.Swagger;

// ReSharper disable once ClassNeverInstantiated.Global
public class OptionalPathParameterFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var httpMethodAttributes = context.MethodInfo
            .GetCustomAttributes(true)
            .OfType<Microsoft.AspNetCore.Mvc.Routing.IRouteTemplateProvider>();

        var httpMethodWithOptional = httpMethodAttributes
            .FirstOrDefault(m => m.Template?.Contains('?') ?? false);
        
        if (String.IsNullOrWhiteSpace(httpMethodWithOptional?.Template)) return;

        const string captureName = "routeParameter";
        const string regex = $"{{(?<{captureName}>\\w+):?\\w*\\?}}";

        var matches = Regex.Matches(httpMethodWithOptional.Template, regex);

        foreach (Match match in matches)
        {
            var name = match.Groups[captureName].Value;

            var parameter = operation.Parameters
                .FirstOrDefault(p => p.In == ParameterLocation.Path && p.Name == name);

            if (parameter == null) continue;
            
            parameter.AllowEmptyValue = true;
            parameter.Required = false;
            parameter.Schema.Nullable = true;
        }
    }
}