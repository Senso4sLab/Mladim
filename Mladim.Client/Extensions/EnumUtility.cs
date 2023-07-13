using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Mladim.Client.Extensions;

public static class EnumUtility
{
    public static string GetDisplayAttributeString(this Enum value) =>
        TryGetDisplayAttribute(value) is DisplayAttribute displayAttribute ?
            displayAttribute.Name ?? string.Empty : string.Empty;
    private static Attribute? TryGetDisplayAttribute(this Enum value) =>
        value.GetType().GetField(value.ToString())?.GetCustomAttribute(typeof(DisplayAttribute));
    public static IEnumerable<T> ToEnums<T>(this T value) where T : struct, Enum =>
        Enum.GetValues<T>()
            .Where(val => value.HasFlag(val))
            .ToList() ?? Enumerable.Empty<T>();
}
