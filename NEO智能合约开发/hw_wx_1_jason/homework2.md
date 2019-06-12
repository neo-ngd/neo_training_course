```


using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace NeoNep5
{
    public class NeoNep5 : SmartContract
    {
        private static readonly BigInteger TotalSupplyValue = 10000000000000000;

         private static readonly byte[] Owner = "Ad1HKAATNmFT5buNgSxspbW68f4XVSssSw".ToScriptHash();
        
        public static event Action<byte[], byte[], BigInteger> Transferred;
        public static object Main(string method, object[] args)
        {
            if (Runtime.Trigger == TriggerType.Verification)
                return Runtime.CheckWitness(Owner);
            if (Runtime.Trigger == TriggerType.Application)
            {
                    if( method == "balanceOf") return Balanceof((byte[])args[0]);
                    if( method == "decimals") return Decimals();
                    if( method == "name") return Name();
                    if( method == "deploy") return Deploy();
                    if( method == "symbol") return Symbol();
                    if( method == "supportedStandards") return SupportedStandards();
                    if( method == "totalSupply") return TotalSupply();
                    if( method == "transfer") return Transfer((byte[])args[0], (byte[])args[1], (BigInteger)args[2]);
            }
            return false;
        }
        public static string Name() => "jason5";
        public static string Symbol() => "JE";
        public static byte Decimals() => 8;
        public static string SupportedStandards() => "nep-5";


        public static bool Deploy()
        {
            if (TotalSupply()!= 0) return false;
            StorageMap contract = Storage.CurrentContext.CreateMap(nameof(contract));
            contract.Put("totalSupply", TotalSupplyValue);
            StorageMap asset = Storage.CurrentContext.CreateMap(nameof(asset));
            asset.Put(Owner, TotalSupplyValue);
            Transferred(null, Owner, TotalSupplyValue);
            return true;
        }

        public static BigInteger TotalSupply()
        {
            StorageMap contract = Storage.CurrentContext.CreateMap(nameof(contract));
            return contract.Get("totalSupply").AsBigInteger();
        }

        public static BigInteger Balanceof(byte[] account)
        {
            if (account.Length != 20)
                throw new InvalidOperationException("Must be 20-byte address.");
            StorageMap asset = Storage.CurrentContext.CreateMap(nameof(asset));
            return asset.Get(account).AsBigInteger();
        }

        public static bool Transfer(byte[] from, byte[] to, BigInteger amount)
        {
            if (from.Length != 20 || to.Length != 20)
                throw new InvalidOperationException("Address must be 2--byte.");
            if (amount <= 0)
                throw new InvalidOperationException("Amount must be greater than 0.");
            if (!Runtime.CheckWitness(from))
                return false;
            StorageMap asset = Storage.CurrentContext.CreateMap(nameof(asset));
            var fromAmount = asset.Get(from).AsBigInteger();
            if (fromAmount < amount)
                return false;
            if (from == to)
                return true;
            if (fromAmount == amount)
                asset.Delete(from);
            else
                asset.Put(from, fromAmount - amount);

            var toAmount = asset.Get(to).AsBigInteger();
            asset.Put(to, toAmount + amount);

            Transferred(from, to, amount);
            return true;
        }

    }
}


```