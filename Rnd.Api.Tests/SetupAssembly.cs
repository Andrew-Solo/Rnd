using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rnd.Api.Client;
using Rnd.Api.Client.Clients;

namespace Rnd.Api.Tests;

[TestClass]
public class SetupAssembly
{
    [AssemblyInitialize]
    public static async Task AssemblyInitialize(TestContext context)
    {
        var client = Settings.GetBasicClient();
        var result = await client.RegisterAsync(Settings.DefaultUser);

        if (client.Status != ClientStatus.Ready)
        {
            throw new Exception(result.Errors?.ToString());
        }

        Settings.TestClient = client;
    }
    
    [AssemblyCleanup]
    public static async Task AssemblyCleanup()
    {
        await Settings.TestClient.DeleteAccountAsync();
    }
}