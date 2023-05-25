using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Mladim.Client.Extensions;

public static class EnumUtility
{
    public static string GetDisplayAttribute(this Enum value)
    {
        var type = value.GetType();

        var fi = type.GetField(value.ToString());

        if (fi.GetCustomAttribute(typeof(DisplayAttribute)) is DisplayAttribute displayAttribute)
            return displayAttribute.Name;

        return string.Empty;
    }

    public static IEnumerable<T> ToEnums<T>(this T value) where T : struct, Enum =>
        Enum.GetValues<T>()
            .Where(val => value.HasFlag(val))
            .ToList() ?? Enumerable.Empty<T>();
}
