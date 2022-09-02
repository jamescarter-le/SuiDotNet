using Newtonsoft.Json;

namespace SuiDotNet.Client.Requests
{
public class ObjectDataResponse
{
    [JsonProperty("status")]
    public SuiObjectStatus Status { get; set; }
    [JsonProperty("details")] // Need to set JsonConverter to ignore deserializing this if it is a string.
    public SuiObject? Details { get; set; }
}
}