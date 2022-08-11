using System.Diagnostics;
using Newtonsoft.Json;

namespace SuiDotNet.Client.Requests;

public class SuiObject
{
    [JsonProperty("data")]
    public SuiObjectData Data { get; }
    [JsonProperty("owner")]
    public ObjectOwner Owner { get; }
    [JsonProperty("previousTransaction")]
    public string PreviousTransaction { get; }
    [JsonProperty("storageRebate")]
    public int StorageRebate { get; }
    [JsonProperty("reference")]
    public SuiObjectReference Reference { get; }

    public SuiObject(SuiObjectData data, ObjectOwner owner, string previousTransaction, int storageRebate, SuiObjectReference reference)
    {
        Data = data;
        Owner = owner;
        PreviousTransaction = previousTransaction;
        StorageRebate = storageRebate;
        Reference = reference;
    }
}