// ReSharper disable InconsistentNaming
namespace Rnd.Api.Localization;

public static class Lang
{
    public static class Exceptions
    {
        public static class IStorable
        {
            public static string DifferentIds => "The object being synchronized and the entity object have different IDs.";
            public static string NullSave => "A save method returned null when a value was expected.";
            public static string NullLoad => "A load method returned null when a value was expected.";
        }

        public static string JsonNullError => "A json deserialization returned null when a value was expected.";
    }
}