﻿using System.Text.Json;
using Rnd.Models;
using Rnd.Results;

namespace Rnd.Data;

public static class PredefinedData
{
    static PredefinedData()
    {
        Modules = CreateModels(LoadData<ModuleData>(nameof(Modules).ToLower()), Module.Create);
        Users = CreateModels(LoadData<UserData>(nameof(Users).ToLower()), User.Create);
        Units = CreateModels(LoadData<UnitData>(nameof(Units).ToLower()), Unit.Create);
        Fields = CreateModels(LoadData<FieldData>(nameof(Fields).ToLower()), Field.Create);
    }


    public static List<Module> Modules { get; }
    public static List<Unit> Units { get; }
    public static List<User> Users { get; }
    public static List<Field> Fields { get; }

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