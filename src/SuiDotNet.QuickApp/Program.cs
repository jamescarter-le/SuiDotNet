using SuiDotNet.Accounts;
using SuiDotNet.Client;
using SuiDotNet.Client.Types;

//
// Write your test Sui program here:
//

var client = new SuiJsonClient(new Uri("https://gateway.devnet.sui.io"));
var privateKey = "y9EMttq2NY3FGgakETYKdfs2ju/ROMnsq+k8SNx8CzezE50rIAlhV3E+13PhiaV0V1dcDJ4mFp24lM+xCqYE+g==";
var suiWallet = new SuiWallet(privateKey);

Console.WriteLine($"SuiDotNet Test App\r\n\r\n");
Console.WriteLine($"Sui Wallet Address: {suiWallet.Address}\r\n\r\n");

await client.SyncAccountState(suiWallet.Address);
var accountRefs = await client.GetObjectsOwnedByAddress(suiWallet.Address);

Console.WriteLine($"Your Objects:\r\n");
var accountObjs = await client.GetObjects(accountRefs);
foreach (var obj in accountObjs)
{
    Console.WriteLine($"ObjectId: {obj.Reference.ObjectId}\tType: {obj.Data.Type}");
}

var suiCoins = await client.GetObjects<Coin<SUI>>(accountRefs);
Console.WriteLine($"\r\n\r\n");
Console.WriteLine("Sui Balance: " + suiCoins.Sum(x => (int)x.Balance));
Console.WriteLine($"\r\n\r\n");
Console.WriteLine("Press key to enter...");
Console.ReadLine();