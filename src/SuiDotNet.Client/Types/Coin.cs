using System.Numerics;
using SuiDotNet.Client.Requests;

namespace SuiDotNet.Client.Types
{
    [MoveType(PackageId = "0x2", Module = "coin", Struct = "Coin")]
    public class Coin<T>
    {
        public ObjectInfo Info { get; }
        public BigInteger Balance { get; }

        public Coin(ObjectInfo info, BigInteger balance)
        {
            Info = info;
            Balance = balance;
        }
    }
}