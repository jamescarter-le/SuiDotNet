using Newtonsoft.Json;
using SuiDotNet.Client.Converters;

namespace SuiDotNet.Client.Requests
{
    [JsonConverter(typeof(ObjectOwnerConverter))]
    public class ObjectOwner
    {
        public OwnerType Type { get; }
        public string? Address { get; }

        public ObjectOwner(OwnerType type)
        {
            Type = type;
        }

        public ObjectOwner(OwnerType type, string address)
        {
            Type = type;
            Address = address;
        }
    }
}
