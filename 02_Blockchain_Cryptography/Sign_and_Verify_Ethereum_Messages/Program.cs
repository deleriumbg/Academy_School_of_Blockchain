using System;
using Nethereum.Signer;
using Nethereum.Signer.Crypto;
using Nethereum.Util;
using Nethereum.Hex.HexConvertors.Extensions;
using System.Text;

namespace Sign_and_Verify_Ethereum_Messages
{
    class Program
    {
        static void Main(string[] args)
        {
            //var privKey = EthECKey.GenerateKey();
            var privKey = new EthECKey("97ddae0f3a25b92268175400149d65d6887b9cefaf28ea2c078e05cdc15a3c0a");
            byte[] pubKeyCompressed = new ECKey(privKey.GetPrivateKeyAsBytes(), true).GetPubKey(true);
            Console.WriteLine("Private key: {0}", privKey.GetPrivateKey().Substring(4));
            Console.WriteLine("Public key: {0}", privKey.GetPubKey().ToHex().Substring(2));
            Console.WriteLine("Public key (compressed): {0}", pubKeyCompressed.ToHex());

            Console.WriteLine();

            string msg = "Message for signing";
            byte[] msgBytes = Encoding.UTF8.GetBytes(msg);
            byte[] msgHash = new Sha3Keccack().CalculateHash(msgBytes);
            var signature = privKey.SignAndCalculateV(msgHash);
            Console.WriteLine("Msg: {0}", msg);
            Console.WriteLine("Msg hash: {0}", msgHash.ToHex());
            Console.WriteLine("Signature: [v = {0}, r = {1}, s = {2}]",
                signature.V[0] - 27, signature.R.ToHex(), signature.S.ToHex());

            Console.WriteLine();

            var pubKeyRecovered = EthECKey.RecoverFromSignature(signature, msgHash);
            Console.WriteLine("Recovered pubKey: {0}", pubKeyRecovered.GetPubKey().ToHex().Substring(2));

            bool validSig = pubKeyRecovered.Verify(msgHash, signature);
            Console.WriteLine("Signature valid? {0}", validSig);
        }
    }
}
