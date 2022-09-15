using System;
using System.Linq;
using System.Threading.Tasks;
using Nethereum.JsonRpc.Client;
using SuiDotNet.Client.Requests;

namespace SuiDotNet.Client
{
    public class SuiJsonClient : ISuiClient
    {
        private readonly SuiClientSettings _settings;
        private readonly RpcClient _rpcClient;

        public SuiJsonClient(SuiClientSettings settings) : this(new Uri(settings.BaseUri))
        {
            _settings = settings;
        }

        public SuiJsonClient(Uri rpcEndpoint)
        {
            _settings = new SuiClientSettings();
            var serializerOptions = DefaultJsonSerializerSettingsFactory.BuildDefaultJsonSerializerSettings();
            _rpcClient = new RpcClient(rpcEndpoint, jsonSerializerSettings: serializerOptions);
        }

        public async Task SyncAccountState(string address)
        {
            await _rpcClient.SendRequestAsync("sui_syncAccountState", null, address);
        }

        public async Task<SuiObjectInfo[]> GetObjectsOwnedByAddress(string address)
        {
            var objects = await _rpcClient.SendRequestAsync<SuiObjectInfo[]>("sui_getObjectsOwnedByAddress", null, address);
            return objects;
        }

        public Task<SuiObject?> GetObject(SuiObjectInfo objectInfo)
        {
            return GetObject(objectInfo.ObjectId);
        }

        public async Task<SuiObject?> GetObject(string objectId)
        {
            var obj = await _rpcClient.SendRequestAsync<ObjectDataResponse>("sui_getObject", null, objectId);
            if (obj.Status == SuiObjectStatus.Exists)
            {
                return obj.Details;
            }

            return null;
        }

        public async Task<T?> GetObject<T>(string objectId) where T : class
        {
            var obj = await GetObject(objectId, typeof(T));
            if (obj == null)
                return null;

            return (T)obj;
        }

        public async Task<object?> GetObject(string objectId, Type objectType)
        {
            var suiObject = await GetObject(objectId);
            if (suiObject == null)
                return null;

            if (!SuiEx.IsOfSuiType(objectType, suiObject.Data.Type, _settings.PackageIdOverrides))
                throw new Exception($"Found SuiObject with DataType {suiObject.Data.Type} does not match annotated type.");

            return SuiEx.ObjectFromDictionary(suiObject.Data.Fields, objectType);
        }

        public Task<SuiObject[]> GetObjects(params SuiObjectInfo[] objectInfos)
        {
            return GetObjects(objectInfos.Select(x => x.ObjectId).ToArray());
        }

        public async Task<SuiObject[]> GetObjects(params string[] objectIds)
        {
            if (!objectIds.Any())
                return Array.Empty<SuiObject>();

            var request = new SuiGetObject(_rpcClient);

            var batchRequest = new RpcRequestResponseBatch();
            for (int i = 0; i < objectIds.Length; i++)
            {
                batchRequest.BatchItems.Add(
                    new RpcRequestResponseBatchItem<SuiGetObject, ObjectDataResponse>(
                        request, request.BuildRequest(objectIds[i], i))
                );
            }

            var response = await _rpcClient.SendBatchRequestAsync(batchRequest);

            var responses = response.BatchItems.OfType<RpcRequestResponseBatchItem<SuiGetObject, ObjectDataResponse>>()
                .Select(x => x.Response).ToArray();

            return responses.Where(x => x.Status == SuiObjectStatus.Exists).Select(x => x.Details).ToArray()!;
        }

        public async Task<T[]> GetObjects<T>(params SuiObjectInfo[] objectInfos) where T : class
        {
            return (await GetObjects(typeof(T), objectInfos)).OfType<T>().ToArray();
        }

        public async Task<object[]> GetObjects(Type objectType, params SuiObjectInfo[] objectInfos)
        {
            objectInfos = objectInfos.Where(x => SuiEx.IsOfSuiType(objectType, x.Type, _settings.PackageIdOverrides)).ToArray();
            var objects = await GetObjects(objectInfos);
            var typedObjects = objects.Select(x => SuiEx.ObjectFromDictionary(x.Data.Fields, objectType)).ToArray();
            return typedObjects;
        }

        public async Task<T[]> GetObjects<T>(params string[] objectIds) where T : class
        {
            var objects = await GetObjects(typeof(T), objectIds);
            return objects.OfType<T>().ToArray();
        }

        public async Task<object[]> GetObjects(Type objectType, params string[] objectIds)
        {
            var objects = await GetObjects(objectIds);
            return objects
                .Where(x => SuiEx.IsOfSuiType(objectType, x.Data.Type, _settings.PackageIdOverrides))
                .Select(x => SuiEx.ObjectFromDictionary(x.Data.Fields, objectType))
                .ToArray();
        }
    }
}