# Optional

`Optional<T>` for C#.

Supports `System.Text.Json` and `MongoDB.Bson` serialization.

## Motivation

The need for a truly optional (may be nullable or not) property or parameter.

For example, `Optional<int?>` can have 3 different values
- empty (not exist)
- null
- int

## Setup

### MongoDB.Bson

Register generic `OptionalBsonSerializer` in `BsonSerializer` class

```csharp
BsonSerializer.RegisterGenericSerializerDefinition(typeof(Optional<>), typeof(OptionalBsonSerializer<>));
```

## Examples

You can ignore empty `Optional<T>` types in serialization by 2 ways

### 1. Using Attributes

#### 1.1. System.Text.Json

```csharp
public class User
{
    public required String Id { get; set; }
    
    public required String Name { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Optional<String> PhoneNumber { get; set; }
}
```

#### 1.2. MongoDB.Bson

```csharp
public class User
{
    public required String Id { get; set; }
    
    public required String Name { get; set; }
    
    [BsonIgnoreIfDefault]
    public Optional<String> PhoneNumber { get; set; }
}
```

### 2. Using Modifiers and Conventions

#### 2.1. System.Text.Json

```csharp
jsonSerializerOptions.TypeInfoResolver = new DefaultJsonTypeInfoResolver
{
    Modifiers = { OptionalJsonExtensions.IgnoreEmptyOptionalModifier }
};
```

#### 2.2. MongoDB.Bson

```csharp
var pack = new ConventionPack();
pack.Add(new IgnoreEmptyOptionalBsonConvention());
ConventionRegistry.Register("Optional Conventions", pack, type => type.IsAssignableTo(typeof(IOptional)));
```