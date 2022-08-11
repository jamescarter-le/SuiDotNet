using SuiDotNet.Client.Requests;

namespace SuiDotNet.Client
{
    public interface ISuiClient
    {
        Task<SuiObjectInfo[]> GetObjectsOwnedByAddress(string address);
        
        Task<SuiObject?> GetObject(SuiObjectInfo objectInfo);
        Task<SuiObject?> GetObject(string objectId);
        Task<T?> GetObject<T>(string objectId) where T : class;
        Task<object?> GetObject(string objectId, Type objectType);

        Task<SuiObject[]> GetObjects(params SuiObjectInfo[] objectInfos);
        Task<SuiObject[]> GetObjects(params string[] objectIds);
        Task<T[]> GetObjects<T>(params SuiObjectInfo[] objectInfos) where T : class;
        Task<object[]> GetObjects(Type objectType, params SuiObjectInfo[] objectInfos);
        Task<T[]> GetObjects<T>(params string[] objectIds) where T : class;
        Task<object[]> GetObjects(Type objectType, params string[] objectIds);
    }
}