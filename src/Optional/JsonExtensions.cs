using System.Text.Json.Serialization.Metadata;

namespace Optional;

public static class OptionalJsonExtensions
{
    public static void IgnoreEmptyOptionalModifier(JsonTypeInfo typeInfo)
    {
        if (typeInfo.Kind is not JsonTypeInfoKind.Object) return;

        foreach (var property in typeInfo.Properties)
        {
            if (property.PropertyType.IsAssignableTo(typeof(IOptional)))
            {
                property.ShouldSerialize = (_, obj) => obj is IOptional { HasValue: true };
            }
        }
    }
}