using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

namespace Optional;

public class IgnoreEmptyOptionalBsonConvention : IMemberMapConvention
{
    public String Name => "Ignore empty Optional<T>";

    public void Apply(BsonMemberMap memberMap)
    {
        if (memberMap.MemberType.IsGenericType && memberMap.MemberType.GetGenericTypeDefinition() == typeof(Optional<>))
        {
            memberMap.SetIgnoreIfDefault(true);
        }
    }
}