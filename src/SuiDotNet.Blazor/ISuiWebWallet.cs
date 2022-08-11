using SuiDotNet.Blazor.DTOs;

namespace SuiDotNet.Blazor
{
    public interface ISuiWebWallet
    {
        bool Connecting { get; }
        bool Connected { get; }
        ValueTask<bool> Connect();
        ValueTask Disconnect();
        ValueTask<bool> RequestPermissions();
        ValueTask<bool> HasPermissions(params string[] permissionType);
        ValueTask<string[]> GetAccounts();
        ValueTask<TransactionResponse> ExecuteMoveCall(MoveCallTransaction transaction);
        ValueTask<TransactionResponse> ExecuteSerializedMoveCall(byte[] transactionBytes);
        ValueTask<bool> WalletConnectorExists();
    }
}