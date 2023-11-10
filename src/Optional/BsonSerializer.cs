using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Optional;

public class OptionalBsonSerializer<T> : SerializerBase<Optional<T>>
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Optional<T> value)
    {
        BsonSerializer.Serialize<T>(context.Writer, value, args: args);
    }

    public override Optional<T> Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return BsonSerializer.Deserialize<T>(context.Reader);
    }
}