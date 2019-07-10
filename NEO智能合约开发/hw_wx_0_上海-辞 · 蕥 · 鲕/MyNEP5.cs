using System;
using System.ComponentModel;
using System.Numerics;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;


namespace MyNEP5
{
    public class NEP5 : SmartContract
    {
        public static event Action<byte[], byte[], BigInteger> Transferred;

        private static readonly byte[] Owner = "AR4iFxFdbjTkHKtStSKRvu5bgXXBcpZx9D".ToScriptHash();
        private static readonly BigInteger TotalSupplyValue = 996;

        public static object Main(string method, object[] args)
        {
            if (Runtime.Trigger == TriggerType.Verification)
            {
                return Runtime.CheckWitness(Owner);
            }
            else if (Runtime.Trigger == TriggerType.Application)
            {
                if (method == "name")
                {
                    return Name();
                }

                if (method == "symbol")
                {
                    return Symbol();
                }

                if (method == "decimals")
                {
                    return Decimals();
                }

                if (method == "totalSupply")
                {
                    return TotalSupply();
                }

                if (method == "supportedStandards")
                {
                    return SupportedStandards();
                }

                if (method == "balanceOf")
                {
                    return BalanceOf((byte[])args[0]);
                }

                if (method == "deploy")
                {
                    return Deploy();
                }

                if (method == "transfer")
                {
                    var callscript = ExecutionEngine.CallingScriptHash;
                    return Transfer((byte[])args[0], (byte[])args[1], (BigInteger)args[2], callscript);
                }
            }

            return false;
        }


        // Token 名字
        [DisplayName("name")]
        public static string Name()
        {
            return "MyNEP5Token";
        }

        // Token 简称
        [DisplayName("symbol")]
        public static string Symbol()
        {
            return "M5T";
        }

        // Token 精度（0~8）
        [DisplayName("decimals")]
        public static byte Decimals()
        {
            return 3;
        }

        // Token 适用的标准
        [DisplayName("supportedStandards")]
        public static string[] SupportedStandards() => new string[] { "NEP-5", "NEP-7", "NEP-10" };


        // Token 的总发行量
        [DisplayName("totalSupply")]
        public static BigInteger TotalSupply()
        {
            StorageMap contract = Storage.CurrentContext.CreateMap(nameof(contract));
            return contract.Get("totalSupply").AsBigInteger();
        }

        // 账户余额
        [DisplayName("balanceOf")]
        public static BigInteger BalanceOf(byte[] account)
        {
            if (account.Length != 20)
            {
                throw new InvalidOperationException("The parameter account SHOULD be 20-byte addresses.");
            }

            StorageMap asset = Storage.CurrentContext.CreateMap(nameof(asset));
            return asset.Get(account).AsBigInteger();
        }

        // 部署合约
        [DisplayName("deploy")]
        public static bool Deploy()
        {
            // 阻止已部署过的合约再次部署
            if (TotalSupply() != 0)
            {
                return false;
            } 

            // 设置发行总量
            StorageMap contract = Storage.CurrentContext.CreateMap(nameof(contract));
            contract.Put("totalSupply", TotalSupplyValue);

            // 把发行总量记录在发行者的账户下
            StorageMap asset = Storage.CurrentContext.CreateMap(nameof(asset));
            asset.Put(Owner, TotalSupplyValue);

            // 广播事件，让链记录下来
            Transferred(null, Owner, TotalSupplyValue);

            return true;
        }

        // 检测这个合约可以 收/提 NEO/GAS 不。在部署合约的时候，会有这个选项。
        private static bool IsPayable(byte[] to)
        {
            var c = Blockchain.GetContract(to);
            return c == null || c.IsPayable;
        }


#if DEBUG
        // ABI 文件测试的转账方法
        [DisplayName("transfer")]
        public static bool Transfer(byte[] from, byte[] to, BigInteger amount) => true;
#endif
        // 实际执行的转账方法
        [DisplayName("transfer")]
        private static bool Transfer(byte[] from, byte[] to, BigInteger amount, byte[] callscript)
        {
            // 检查地址
            if (from.Length != 20 || to.Length != 20)
            {
                throw new InvalidOperationException("The parameters from and to SHOULD be 20-byte addresses.");
            }

            // 检查金额
            if (amount <= 0)
            {
                throw new InvalidOperationException("The parameter amount MUST be greater than 0.");
            }

            // 检测这个合约可以 收/提 NEO/GAS 不。在部署合约的时候，会有这个选项。
            // 比如，在进行 ICO 募集的时候，需要将 NEO 打到一个合约地址，换取投资的 Token。
            // 当然，也可以反过来操作，取回 NEO   
            if (!IsPayable(to))
            {
                return false;
            }

            // 检查转账者 from ，必须是当前交易的发起者。 另外，该交易不能通过合约触发。
            if (!Runtime.CheckWitness(from) && from.AsBigInteger() != callscript.AsBigInteger())
            {
                return false;
            }
                
            // 获取发款方的金额
            StorageMap asset = Storage.CurrentContext.CreateMap(nameof(asset));
            var fromAmount = asset.Get(from).AsBigInteger();
            // 发款方金额小于转账金额
            if (fromAmount < amount)
            {
                return false;
            }

            // 如果是自己转给自己，默认成功    
            if (from == to) {
                return true;
            }
                
            // 减少发款方的金额
            if (fromAmount == amount) 
            {
                asset.Delete(from);
            } 
            else 
            {
                asset.Put(from, fromAmount - amount);
            }

            // 增加收款方的金额
            var toAmount = asset.Get(to).AsBigInteger();
            asset.Put(to, toAmount + amount);

            // 广播事件，让链记录转账日志
            Transferred(from, to, amount);

            return true;
        }
    }
}
