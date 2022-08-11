using SuiDotNet.Client.Requests;

namespace SuiDotNet.Client.Types;

[MoveType(PackageId = "0x2", Module = "coin", Struct = "TreasuryCap")]
public class TreasuryCap<T>
{
    public ObjectInfo Info { get; set; }
    public Supply<T> TotalSupply { get; set; }

    public TreasuryCap(ObjectInfo info, Supply<T> totalSupply)
    {
        Info = info;
        TotalSupply = totalSupply;
    }
}