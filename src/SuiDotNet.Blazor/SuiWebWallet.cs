using Microsoft.JSInterop;
using SuiDotNet.Blazor.DTOs;

namespace SuiDotNet.Blazor;

public class SuiWebWallet : ISuiWebWallet
{
    public const string ProgrammedAndBuiltAgainstSuiWalletVersion = "0.0.3";

    private const string WALLET_GLOBAL = "suiWallet";
    private const string IDENTIFIER_HAS_PERMISSIONS = WALLET_GLOBAL + ".hasPermissions";
    private const string IDENTIFIER_REQUEST_PERMISSIONS = WALLET_GLOBAL + ".requestPermissions";
    private const string IDENTIFIER_GET_ACCOUNTS = WALLET_GLOBAL + ".getAccounts";
    private const string IDENTIFIER_EXECUTE_MOVE_CALL = WALLET_GLOBAL + ".executeMoveCall";
    private const string IDENTIFIER_EXECUTE_SERIALIZED_MOVE_CALL = WALLET_GLOBAL + ".executeSerializedMoveCall";

    private bool _connected;
    private bool _connecting;

    private readonly IJSRuntime _js;

    public SuiWebWallet(IJSRuntime js)
    {
        _js = js;
    }

    public bool Connecting => _connecting;
    public bool Connected => _connected;

    public async ValueTask<bool> Connect()
    {
        if (await WalletConnectorExists())
        {
            _connecting = true;
            _connected = await HasPermissions("viewAccount");
            
            if(!_connected)
            {
                await RequestPermissions();
                _connected = await HasPermissions("viewAccount");
            }

            _connecting = false;
        }
        else
        {
            _connecting = false;
            _connected = false;
        }
        return _connected;
    }

    public async ValueTask<bool> WalletConnectorExists()
    {
        try
        {
            await HasPermissions("viewAccount");
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public ValueTask Disconnect()
    {
        _connected = false;
        return ValueTask.CompletedTask;
    }

    public ValueTask<bool> RequestPermissions()
    {
        return _js.InvokeAsync<bool>(IDENTIFIER_REQUEST_PERMISSIONS);
    }

    public ValueTask<bool> HasPermissions(params string[] permissionTypes)
    {
        return _js.InvokeAsync<bool>(IDENTIFIER_HAS_PERMISSIONS, new object[] { permissionTypes });
    }

    public ValueTask<string[]> GetAccounts()
    {
        return _js.InvokeAsync<string[]>(IDENTIFIER_GET_ACCOUNTS);
    }

    public ValueTask<TransactionResponse> ExecuteMoveCall(MoveCallTransaction transaction)
    {
        return _js.InvokeAsync<TransactionResponse>(IDENTIFIER_EXECUTE_MOVE_CALL, new object[] { transaction });
    }

    public ValueTask<TransactionResponse> ExecuteSerializedMoveCall(byte[] transactionBytes)
    {
        return _js.InvokeAsync<TransactionResponse>(IDENTIFIER_EXECUTE_SERIALIZED_MOVE_CALL, new object[] { transactionBytes });
    }
}

/*



public class TransactionResponse
{
    public TransactionEffectsResponse EffectsResponse { get; set; }
}

public class TransactionEffectsResponse
{
    public CertifiedTransaction Certificate { get; set; }
    public TransactionEffects Effects { get; set; }
    public long TimestampMs { get; set; }
}

public class OwnedObjectRef
{
    public string AddressOwner { get; set; }
    public string ObjectOwner { get; set; }
    public string SingleOwner { get; set; }
}

public enum ExecutionStatusType
{
    Success,
    Failure
}

public class TransactionEffects
{
    public ExecutionStatusType Status { get; set; }
    public string Error { get; set; }
}

public class CertifiedTransaction
{
    public string TransactionDigest { get; set; }
    public string 
}

public class TransactionData
{
    public 
}

public class SuiTransactionKind
{
    public TransferObject TransferObject { get; set; }
    public SuiMovePackage Publish { get; set; }
}

public class SuiMovePackage
{

}

public class MoveCall
{
    public string Package { get; set; }
    public string Module { get; set; }
    public string Function { get; set; }
    public string[]? TypeArguments { get; set; }
    public object[]? Arguments { get; set; }
}

public class TransferObject
{
    public string Receipient { get; set; }
    public string ObjectRef { get; set; }
}

*/