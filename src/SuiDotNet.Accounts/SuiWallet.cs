using System;
using System.Linq;
using HDWallet.Core;
using HDWallet.Ed25519;

namespace SuiDotNet.Accounts
{
    public class SuiWallet : Wallet
    {
        protected override IAddressGenerator GetAddressGenerator() => SuiAddressGenerator.AccountAddressGenerator;

        public SuiWallet() {}

        public SuiWallet(string privateKeyBase64) : base(Convert.FromBase64String(privateKeyBase64).Take(32).ToArray())
        {

        }

        public SuiWallet(byte[] privateKey) : base(privateKey) { }
    }
}