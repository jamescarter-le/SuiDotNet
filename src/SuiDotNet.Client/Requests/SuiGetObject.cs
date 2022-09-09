using System;
using System.Threading.Tasks;
using Nethereum.JsonRpc.Client;

namespace SuiDotNet.Client.Requests
{
public class SuiGetObject : RpcRequestResponseHandler<ObjectDataResponse>
{
    public SuiGetObject(IClient client) : base(client, "sui_getObject")
    {
    }


    public Task<ObjectDataResponse> SendRequestAsync(string address, object? id = null)
    {
        if (address == null) throw new ArgumentNullException(nameof(address));
        return SendRequestAsync(id, address);
    }

    public RpcRequest BuildRequest(string address, object? id = null)
    {
        if (address == null) throw new ArgumentNullException(nameof(address));
        return BuildRequest(id, address);
    }
}
}