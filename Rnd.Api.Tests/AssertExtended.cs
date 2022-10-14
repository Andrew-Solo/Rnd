using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rnd.Api.Client;

namespace Rnd.Api.Tests;

public static class AssertExtended
{
    public static void IsReady(BasicClient client)
    {
        Assert.AreEqual(ClientStatus.Ready, client.Status, client.Authorization.Errors?.ToString());
    }
}