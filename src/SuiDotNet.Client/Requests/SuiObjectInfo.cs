using Newtonsoft.Json;

namespace SuiDotNet.Client.Requests;

public class SuiObjectInfo
{
    [JsonProperty("objectId")]
    public string ObjectId { get; }
    [JsonProperty("digest")]
    public string Digest { get; }
    [JsonProperty("type")]
    public string Type { get; }
    [JsonProperty("owner")]
    public ObjectOwner Owner { get; }
    [JsonProperty("previousTransaction")]
    public string PreviousTransaction { get; }

    public SuiObjectInfo(string objectId, string digest, string type, ObjectOwner owner, string previousTransaction)
    {
        ObjectId = objectId;
        Digest = digest;
        Type = type;
        Owner = owner;
        PreviousTransaction = previousTransaction;
    }
}