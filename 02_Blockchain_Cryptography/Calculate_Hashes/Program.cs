using System;
using Nethereum.Signer;
using Nethereum.Signer.Crypto;
using Nethereum.Util;
using Nethereum.Hex.HexConvertors.Extensions;
using System.Text;

namespace Calculate_Hashes
{
    class Program
    {
        static void Main(string[] args)
        {
            string msg = "blockchain";
            byte[] msgBytes = Encoding.UTF8.GetBytes(msg);
            Console.WriteLine("Msg hash: {0}", msgHash.ToHex());
        }
    }
}
