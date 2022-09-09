using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SuiDotNet.Client.Requests;

namespace SuiDotNet.Client.Converters
{

public class ObjectOwnerConverter : JsonConverter<ObjectOwner>
{
    public override bool CanWrite
    {
        get { return false; }
    }

    public override void WriteJson(JsonWriter writer, ObjectOwner value, JsonSerializer serializer)
    {
    }

    public override ObjectOwner ReadJson(JsonReader reader, Type objectType, ObjectOwner existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.String)
        {
            if (reader.Value.ToString() == "Shared")
            {
                return new ObjectOwner(OwnerType.Shared);
            }
        }
        else
        {
            var jObject = serializer.Deserialize<JObject>(reader);
            if (jObject.ContainsKey("AddressOwner"))
            {
                return new ObjectOwner(OwnerType.Address, jObject["AddressOwner"].Value<string>());
            }
            else if (jObject.ContainsKey("ObjectOwner"))
            {
                return new ObjectOwner(OwnerType.Object, jObject["AddressOwner"].Value<string>());
            }
        }

        return new ObjectOwner(OwnerType.Unknown);
    }
}

}