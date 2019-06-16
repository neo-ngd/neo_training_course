using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace Nep5Contract
{
    public class Nep5Token : SmartContract
    {
        [System.ComponentModel.DisplayName("transfer")]
        public static event Action<byte[], byte[], BigInteger> Transfered;
        // public static event deleTransfer Transfered;
        public delegate void deleTransfer(byte[] from, byte[] to, BigInteger value);

        private const ulong decimals = 100000000;
        private const string name = "zcoin";
        private const string symbol = "z";
        public static readonly byte[] superAdmin = Neo.SmartContract.Framework.Helper.ToScriptHash("AJS1nc3e8QAwbvzzoRNiymVbXCFYHaBjG5");
        public static Object Main(string method, object[] args)
        {
            if (Runtime.Trigger == TriggerType.Application)
            {
                if (method == "deploy")
                {
                    //发行资产
                    return Deploy();
                }
                else if (method == "name")
                {
                    return Name();
                }
                else if (method == "symbol")
                {
                    return Symbol();
                }
                else if (method == "totalSupply")
                {
                    return TotalSupply();
                }
                else if (method == "transfer")
                {
                    return Transfer((byte[])args[0], (byte[])args[1], (BigInteger)args[2]);
                }
                else if (method == "balanceOf")
                {
                    return BalanceOf((byte[])args[0]);
                }
                else if (method == "decimals")
                {
                    return Decimals();
                }
                return "true";
            }
            return "not application contract";
        }

        public static bool Deploy()
        {
            if (!Runtime.CheckWitness(superAdmin))
            {
                return false;
            }
            StorageMap asset = Storage.CurrentContext.CreateMap("asset");
            BigInteger totalSupply = asset.Get("totalSupply").AsBigInteger();
            if (!totalSupply.Equals(0))
            {
                return false;
            }
            totalSupply = 3000000;
            BigInteger totalDeploy = totalSupply * decimals;
            asset.Put("totalSupply", totalDeploy);
            asset.Put(superAdmin, totalSupply);
            Transfered(null, superAdmin, totalDeploy);
            return true;
        }

        public static String Name()
        {
            return name;
        }

        public static BigInteger TotalSupply()
        {
            StorageMap asset = Storage.CurrentContext.CreateMap("asset");
            return asset.Get("totalSupply").AsBigInteger();
        }

        public static String Symbol()
        {
            return symbol;
        }

        public static BigInteger BalanceOf(byte[] account)
        {
            if (!account.Length.Equals(20))
            {
                throw new InvalidOperationException("the parameter account should be 21-bytes address");
            }

            StorageMap asset = Storage.CurrentContext.CreateMap("asset");
            return asset.Get(account).AsBigInteger();
        }

        public static BigInteger Decimals()
        {
            return 8;
        }

        public static bool Transfer(byte[] from, byte[] to, BigInteger value)
        {
            if (!from.Length.Equals(20))
            {
                throw new InvalidOperationException("the parameter from and to should be 21-bytes address");
            }
            if (value < 0)
            {
                throw new InvalidOperationException("the parameter amount must bigger than 0");
            }
            if (!Runtime.CheckWitness(from))
            {
                return false;
            }
            StorageMap asset = Storage.CurrentContext.CreateMap("asset");
            BigInteger fromValue = asset.Get(from).AsBigInteger();
            if (fromValue < value)
            {
                return false;
            }
            asset.Put(from, fromValue - value);
            BigInteger toValue = asset.Get(to).AsBigInteger();
            asset.Put(to, toValue + value);
            Transfered(from, to, value);
            return true;
        }

    }
}