using System.Collections.Generic;
using Newtonsoft.Json;

namespace SuiDotNet.Client.Requests
{
public class SuiObjectData
{
    [JsonProperty("dataType")]
    public string DataType { get; }
    [JsonProperty("type")]
    public string Type { get; }
    [JsonProperty("has_public_transfer")]
    public bool HasPublicTransfer { get; }
    [JsonProperty("fields")]
    public Dictionary<string, object> Fields { get; }

    public SuiObjectData(string dataType, string type, bool hasPublicTransfer, Dictionary<string, object> fields)
    {
        DataType = dataType;
        Type = type;
        HasPublicTransfer = hasPublicTransfer;
        Fields = fields;
    }
}
}