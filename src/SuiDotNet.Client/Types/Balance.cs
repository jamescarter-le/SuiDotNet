namespace SuiDotNet.Client.Types
{
    [MoveType(PackageId = "0x2", Module = "balance", Struct = "Balance")]
    public class Balance<T>
    {
        public long Value { get; }

        public Balance(long value)
        {
            Value = value;
        }
    }
}