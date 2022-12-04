using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rnd.Api.Client.Clients;

namespace Rnd.Api.Tests;

public static class AssertExtended
{
    public static void ClientIsReady(ApiClient client)
    {
        Assert.AreEqual(ClientStatus.Ready, client.Status, client.Authorization.Errors?.ToString());
    }

    public static void DateTimeAreClose(DateTimeOffset? expected, DateTimeOffset? actual, long maxDifference = 10)
    {
        DateTimeAreClose(expected.GetValueOrDefault().DateTime, actual.GetValueOrDefault().DateTime, maxDifference);
    }
    
    public static void DateTimeAreClose(DateTime? expected, DateTime? actual, long maxDifference = 10)
    {
        var expectedTicks = expected.GetValueOrDefault().Ticks;
        var actualTicks = actual.GetValueOrDefault().Ticks;
        var difference = expectedTicks - actualTicks;

        if (difference <= maxDifference) actualTicks += difference;
                    
        var expectedApproximate = new DateTime(expectedTicks);
        var actualApproximate = new DateTime(actualTicks);
                    
        Assert.AreEqual(expectedApproximate, actualApproximate);
    }

    public static void CollectionAndElements<T>(IEnumerable<T> expected, IEnumerable<T> actual)
    {
        var expectedList = new List<T>(expected);
        var actualList = new List<T>(actual);
        
        if (expectedList.Count != actualList.Count)
        {
            throw new Exception($"Assert count unsuccessful: expected {expectedList.Count}, actual {actualList.Count}");
        }

        for (int i = 0; i < expectedList.Count; i++)
        {
            AllPropertiesAreEqual(expectedList[i], actualList[i]);
        }
    }
    
    public static void AllPropertiesAreEqual<T>(T expected, T actual)
    {
        var type = typeof(T);
        var properties = type.GetProperties();

        var exceptions = new List<Exception>();
        
        foreach (var property in properties)
        {
            var expectedValue = property.GetValue(expected);
            var actualValue = property.GetValue(actual);
            
            try
            {
                if (expectedValue is not string && expectedValue is IEnumerable enumerable)
                {
                    var elementType = enumerable.GetType().GetElementType();
                    if (elementType == null || !elementType.IsPrimitive && elementType.Name != "String") continue;
                    
                    var expectedArray = enumerable.Cast<object>().ToArray();
                    var actualArray = (actualValue as IEnumerable)?.Cast<object>().ToArray() ?? Array.Empty<object>();

                    Assert.AreEqual(expectedArray.Length , actualArray.Length);
                    
                    for (var i = 0; i < expectedArray.Length; i++)
                    {
                        Assert.AreEqual(expectedArray[i], actualArray[i]);
                    }
                }
                else if (expectedValue == null)
                {
                    Assert.AreEqual(expectedValue, actualValue);
                }
                else if (expectedValue is DateTime dateTime)
                {
                    DateTimeAreClose(dateTime, (DateTime?) actualValue);
                }
                else if (expectedValue is DateTimeOffset dateTimeOffset)
                {
                    DateTimeAreClose(dateTimeOffset, (DateTimeOffset?) actualValue);
                }
                else if (expectedValue is string || !expectedValue.GetType().IsClass)
                {
                    Assert.AreEqual(expectedValue, actualValue);
                }
            }
            catch (Exception e)
            {
                exceptions.Add(new Exception($"Property {property.Name} assertion failed"));
                exceptions.Add(e);
            }
        }

        if (exceptions.Count > 0) throw new AggregateException(exceptions);
    }
}