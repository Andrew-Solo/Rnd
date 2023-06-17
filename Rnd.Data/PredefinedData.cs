using System.Text.Json;
using Rnd.Models;
using Rnd.Models.Nodes;
using Rnd.Results;

namespace Rnd.Data;

public static class PredefinedData
{
    static PredefinedData()
    {
        Modules = CreateModels(LoadData<ModuleData>("modules"), Module.Create);
        Users = CreateModels(LoadData<UserData>("users"), User.Create);
    }

    public static List<User> Users { get; }
    public static List<Module> Modules { get; }

    private static List<TModel> CreateModels<TModel, TData>(List<TData> dataList, Func<TData, Result<TModel>> factory)
        where TModel : Model where TData : ModelData
    {
        var models = new List<TModel>();
        
        foreach (var data in dataList)
        {
            var user = factory(data);
            if (user.Failed) continue;
            models.Add(user.Value);
        }

        return models;
    }

    private static List<TData> LoadData<TData>(string filename) where TData : ModelData
    {
        var json = File.ReadAllText($"./data/{filename}.json");
        var dataList = JsonSerializer.Deserialize<List<TData>>(json);
        return dataList ?? new List<TData>();
    }
}