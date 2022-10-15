using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rnd.Api.Client;
using Rnd.Api.Client.Clients;
using Rnd.Api.Client.Models.Basic.User;

namespace Rnd.Api.Tests;

[TestClass]
public class SetupAssembly
{
    [AssemblyInitialize]
    public static async Task AssemblyInitialize(TestContext context)
    {
        var client = Settings.GetBasicClient();
        var result = await client.RegisterAsync(Settings.TestUserForm);

        if (client.Status != ClientStatus.Ready)
        {
            throw new Exception(result.Errors?.ToString());
        }

        Settings.TestClient = client;
        Settings.TestUser = client.User;
    }
    
    [AssemblyCleanup]
    public static async Task AssemblyCleanup()
    {
        await Settings.TestClient.DeleteAccountAsync();
    }
}