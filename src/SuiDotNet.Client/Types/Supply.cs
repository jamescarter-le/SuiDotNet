namespace SuiDotNet.Client.Types;

[MoveType(PackageId = "0x2", Module = "balance", Struct = "Supply")]
public class Supply<T>
{
    public long Value { get; }

    public Supply(long value)
    {
        Value = value;
    }
}