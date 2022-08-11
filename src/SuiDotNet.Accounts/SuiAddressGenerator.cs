using HDWallet.Core;
using Org.BouncyCastle.Crypto.Digests;

namespace SuiDotNet.Accounts;

public class SuiAddressGenerator : IAddressGenerator
{
    public static IAddressGenerator AccountAddressGenerator = new SuiAddressGenerator();

    public string GenerateAddress(byte[] pubKeyBytes)
    {
        byte[] output = new byte[32];
        var sha3 = new Sha3Digest(256);
        sha3.BlockUpdate(pubKeyBytes, 0, 32);
        sha3.DoFinal(output, 0);
        var hash = output.Take(20).ToArray();
        string address = hash.ToHexString();
        return address;
    }
}