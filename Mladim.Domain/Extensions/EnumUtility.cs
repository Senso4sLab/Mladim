using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Mladim.Domain.Extensions;

public static class EnumUtility
{
    public static string GetDisplayAttribute(this Enum value) =>
        TryGetDisplayAttribute(value) is DisplayAttribute displayAttribute ?
            displayAttribute.Name ?? string.Empty : string.Empty;
    private static Attribute? TryGetDisplayAttribute(this Enum value) =>
        value.GetType().GetField(value.ToString())?.GetCustomAttribute(typeof(DisplayAttribute));


    public static string GetDescriptionAttributeString(this Enum value) =>
        TryGetDescriptionAttribute(value) is DescriptionAttribute descriptionAttribute ?
            descriptionAttribute.Description ?? string.Empty : string.Empty;
    private static Attribute? TryGetDescriptionAttribute(this Enum value) =>
        value.GetType().GetField(value.ToString())?.GetCustomAttribute(typeof(DescriptionAttribute));



    public static IEnumerable<T> ToEnums<T>(this T value) where T : struct, Enum =>
        Enum.GetValues<T>()
            .Where(val => value.HasFlag(val))
            .ToList() ?? Enumerable.Empty<T>();
}
