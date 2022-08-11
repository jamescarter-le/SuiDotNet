namespace SuiDotNet.Blazor.DTOs;

public class MoveCallTransaction
{
    public string PackageObjectId { get; set; }
    public string Module { get; set; }
    public string Function { get; set; }
    public string[] TypeArguments { get; set; }
    public object[] Arguments { get; set; }
    public string? GasPayment { get; set; }
    public int GasBudget { get; set; }

    public MoveCallTransaction(string packageId, string module, string function, string[] typeArgs, object[] args, string gasCoinId, int gasBudget)
    {
        PackageObjectId = packageId;
        Module = module;
        Function = function;
        TypeArguments = typeArgs;
        Arguments = args;
        GasPayment = gasCoinId;
        GasBudget = gasBudget;
    }
}