using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;
using System.ComponentModel;

namespace NeoContract1
{
    public class Contract1 : SmartContract
    {
        private static readonly byte[] Owner = "Ad1HKAATNmFT5buNgSxspbW68f4XVSssSw".ToScriptHash();

        //private static readonly byte[] Owner = "ba3a920a13eac0f8ea1f1067d8168d2608cee3e8";


           // "Ad1HKAATNmFT5buNgSxspbW68f4XVSssSw".ToScriptHash()="ba3a920a13eac0f8ea1f1067d8168d2608cee3e8";????????


        private static readonly BigInteger TotalSupplyVaule = 10000000000000000;
        [DisplayName("name")]
        public static string Name() => "NeoCosmos";
        [DisplayName("decimals")]
        public static byte Decimals() => 8;
        [DisplayName("symbol")]
        public static string Symbol() => "NCT";
        [DisplayName("transfer")]
        public static event Action<byte[], byte[], BigInteger> Transferred;
        [DisplayName("deploy")]
        public static bool Deploy()
        {
            if (TotalSupply() != 0) return false;
            StorageMap contract = Storage.CurrentContext.CreateMap(nameof(contract));
            contract.Put("totalSupply", TotalSupplyVaule);
            StorageMap asset = Storage.CurrentContext.CreateMap(nameof(asset));
            //合约所有者所拥有的所有nep-5通证
            asset.Put(Owner, TotalSupplyVaule);
            //当发生nep-5资产转账时触发该事件
            Transferred(null, Owner, TotalSupplyVaule);
            return true;
        }

        [DisplayName("totalSupply")]
        public static BigInteger TotalSupply()
        {
            StorageMap contract = Storage.CurrentContext.CreateMap(nameof(contract));
            return contract.Get("totalSupply").AsBigInteger();
        }
        [DisplayName("balanceOf")]
        public static BigInteger BalanceOf(byte[] account)
        {
            //参数校验
            if (account.Length != 20)
                throw new InvalidOperationException("The parameter account SHOULD be 20-byte addresses.");
            StorageMap asset = Storage.CurrentContext.CreateMap(nameof(asset));
            return asset.Get(account).AsBigInteger();
        }
        //public static bool Transfer(byte[] from, byte[] to, BigInteger amount, byte[] callscript)
            public static bool Transfer(byte[] from, byte[] to, BigInteger amount)
        {
            if (from.Length != 20 || to.Length != 20)
                throw new InvalidOperationException("The parameters from and to SHOULD be 20-byte address.");
            if (amount <= 0)
                throw new InvalidOperationException("The parameter amount MUST be greater than 0.");
            if (!Runtime.CheckWitness(from))
                return false;
            StorageMap asset = Storage.CurrentContext.CreateMap(nameof(asset));
            var fromAmount = asset.Get(from).AsBigInteger();
            if (fromAmount < amount)
                return false;
            if (from == to)
                return true;
            //减少发送方的账户余额
            if (fromAmount == amount)
                asset.Delete(from);
            else
                asset.Put(from, fromAmount - amount);
            //增加接收方的账户余额
            var toAmount = asset.Get(to).AsBigInteger();
            asset.Put(to, toAmount + amount);

            Transferred(from, to, amount);
            return true;






                }





        public static Object Main(string method, object[] args)
        {
            if(Runtime.Trigger==TriggerType.Verification)
            {
                return Runtime.CheckWitness(Owner);
            }
            else if(Runtime.Trigger == TriggerType.Application)
            {
                if (method == "balanceof") return BalanceOf((byte[])args[0]);
                if (method == "decimals") return Decimals();
                if (method == "name") return Name();
                if (method == "symbol") return Symbol();
                if (method == "deploy") return Deploy();
                //if (method == "supportedStandards") return SupportedStandards();
                if (method == "totalSupply") return TotalSupply();
                //if (method == "transfer") return Transfer((byte[])args[0], (byte[])args[1], (BigInteger)args[2],(byte[])args[3]);
                if (method == "transfer") return Transfer((byte[])args[0], (byte[])args[1], (BigInteger)args[2]);
            }
            return false;
        }
        
    }
}