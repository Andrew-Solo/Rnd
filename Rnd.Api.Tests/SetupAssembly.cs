using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rnd.Api.Client;

namespace Rnd.Api.Tests;

[TestClass]
public class SetupAssembly
{
    [AssemblyInitialize]
    public static async Task AssemblyInitialize(TestContext context)
    {
        var client = Settings.GetBasicClient();
        await client.RegisterAsync(Settings.DefaultUser);

        if (client.Status != ClientStatus.Ready)
        {
            throw new Exception("Test api client registration error");
        }

        Settings.TestClient = client;
    }
    
    [AssemblyCleanup]
    public static async Task AssemblyCleanup()
    {
        await Settings.TestClient.DeleteAccountAsync();
    }
}