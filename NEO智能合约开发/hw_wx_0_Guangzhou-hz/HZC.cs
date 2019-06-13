using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.ComponentModel;
using System.Numerics;

namespace HZC
{
    public class HZC : SmartContract
    {
        public static readonly byte[] Owner = "AWSuQXpjuY3v22gCbEFL2vHbSLMMVK1QD6".ToScriptHash();
        private static readonly BigInteger total_amount = 100000000;

        [DisplayName("name")]
        public static string Name() => "HZ Coin!";

        [DisplayName("symbol")]
        public static string Symbol() => "HZC";

        [DisplayName("decimals")]
        public static byte Decimals() => 0;

        [DisplayName("totalSupply")]
        public static BigInteger TotalSupply()
        {
            StorageMap contract = Storage.CurrentContext.CreateMap(nameof(contract));
            return contract.Get("totalSupply").AsBigInteger();
        }

        [DisplayName("transfer")]
        public static event Action<byte[], byte[], BigInteger> Transferred;

        public static Object Main(string operation, params object[] args)
        {
            if (Runtime.Trigger == TriggerType.Verification)
            {
                   return Runtime.CheckWitness(Owner);
            }
            if (Runtime.Trigger == TriggerType.Application)
            {
                if (operation == "deploy") return Deploy();
                if (operation == "totalSupply") return TotalSupply();
                if (operation == "name") return Name();
                if (operation == "symbol") return Symbol();
                if (operation == "transfer")
                {
                    if (args.Length != 3) return false;
                    byte[] from = (byte[])args[0];
                    byte[] callscript = ExecutionEngine.CallingScriptHash;
                    byte[] to = (byte[])args[1];
                    BigInteger value = (BigInteger)args[2];
                    return Transfer(from, to, value, callscript);
                }
                if (operation == "balanceOf")
                {
                    if (args.Length != 1) return 0;
                    byte[] account = (byte[])args[0];
                    return BalanceOf(account);
                }
                if (operation == "decimals") return Decimals();
            }
            return false;
        }

        [DisplayName("deploy")]
        public static bool Deploy()
        {
            if (TotalSupply() != 0) return false;
            StorageMap contract = Storage.CurrentContext.CreateMap(nameof(contract));
            contract.Put("totalSupply", total_amount);
            StorageMap balance = Storage.CurrentContext.CreateMap(nameof(balance));
            balance.Put(Owner, total_amount);
            Transferred(null, Owner, total_amount);
            return true;
        }

        public static bool Transfer(byte[] from, byte[] to, BigInteger value, byte[] callscript)
        {
            if (from.Length != 20) return false;
            if (to.Length != 20) return false;
            if (value <= 0) return false;
            if (!Runtime.CheckWitness(from) && from.AsBigInteger() != callscript.AsBigInteger()) return false;

            if (from == to) return true;

            StorageMap balance = Storage.CurrentContext.CreateMap(nameof(balance));

            var fromAmount = balance.Get(from).AsBigInteger();
            if (fromAmount < value) return false;
            if (fromAmount == value) balance.Delete(from);
            else balance.Put(from, fromAmount - value);

            var toAmount = balance.Get(to).AsBigInteger();
            balance.Put(to, toAmount + value);
            Transferred(from, to, value);
            return true;
        }

        [DisplayName("balanceOf")]
        public static BigInteger BalanceOf(byte[] address)
        {
            if (address.Length != 20)
                throw new InvalidOperationException("The parameter address SHOULD be 20-byte addresses.");
            StorageMap balance = Storage.CurrentContext.CreateMap(nameof(balance));
            return balance.Get(address).AsBigInteger();
        }
    }
}
