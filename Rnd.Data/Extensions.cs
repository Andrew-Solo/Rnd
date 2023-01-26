using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rnd.Results;

namespace Rnd.Data;

public static class Extensions
{
    public static async Task<ValidationResult> ValidateAsync<T>(
        this DbSet<T> data, 
        string header, 
        params Rule<T>[] rules) 
        where T : class
    {
        var result = new ValidationResult(true, new Message(header));
        
        foreach (var rule in rules)
        {
            if (!await data.AnyAsync(rule.Predicate)) continue;
            
            result.IsValid = false;
                
            if (rule.Name != null)
            {
                result.Message.AddTooltips(rule.Name, rule.Message);
            }
            else
            {
                result.Message.AddDetails(rule.Message);
            }
        }

        return result;
    }
}

public record struct Rule<T>(
    Expression<Func<T, bool>> Predicate,
    string Message,
    string? Name = null
);