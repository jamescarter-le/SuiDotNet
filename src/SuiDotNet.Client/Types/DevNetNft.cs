using SuiDotNet.Client.Requests;

namespace SuiDotNet.Client.Types
{
    [MoveType(PackageId = "0x2", Module = "devnet_nft", Struct = "DevNetNFT")]
    public class DevNetNft
    {
        public ObjectInfo Info { get; }
        public string Name { get; }
        public string Description { get; }
        public string Url { get; }

        public DevNetNft(ObjectInfo info, string name, string description, string url)
        {
            Info = info;
            Name = name;
            Description = description;
            Url = url;
        }
    }
}
