using System.Text.Json.Serialization;

namespace Optional;

[JsonConverter(typeof(OptionalJsonConverter))]
public readonly record struct Optional<T> : IOptional
{
    private readonly bool _hasValue;
    private readonly T _value;

    public bool HasValue => _hasValue;
    public T Value => HasValue ? _value : throw new InvalidOperationException("Optional has no value");

    private Optional(T value)
    {
        _value = value;
        _hasValue = true;
    }

    public static implicit operator Optional<T>(T value) => new(value);
    public static implicit operator T(Optional<T> optional) => optional.Value;

    public override String? ToString() => HasValue ? Value?.ToString() : "Empty";
}