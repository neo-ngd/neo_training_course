
#### homework1 

在上面的 DNS 智能合约(DNS.cs)中，有一个delete方法还未实现。其基本思想是首先检查域名所属者，如果存在并且与合约的调用者相同，则使用 Storage.Delete 方法来删除相应的键值对。请实现这个功能。


using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
namespace Neo.SmartContract
{
    public class Domain : SmartContract
    {
        public static object Main(string operation, params object[] args)
        {
	        if (Runtime.Trigger == TriggerType.Application){
		            switch (operation){
		                case "query":
		                    return Query((string)args[0]);
		                case "register":
		                    return Register((string)args[0], (byte[])args[1]);
		                case "delete":
		                    return Delete((string)args[0]);
		                default:
		                    return false;
		            }
	        }
        }

        private static byte[] Query(string domain)
        {
            return Storage.Get(Storage.CurrentContext, domain);
        }


        private static bool Register(string domain, byte[] owner)
        {
	        // Check if  the owner is the same as the one who invoke the contract
            if (!Runtime.CheckWitness(owner)) return false;
            byte[] value = Storage.Get(Storage.CurrentContext, domain);
            if (value != null) return false;
            Storage.Put(Storage.CurrentContext, domain, owner);
            return true;
        }

        private static bool Delete(string domain)
        {
        	// To do
        	byte[] owner = Storage.Get(Storage.CurrentContext, domain);
            if (owner == null) return false;
            if (!Runtime.CheckWitness(owner)) return false;
            Storage.Delete(Storage.CurrentContext, domain);
            return true;
        }
    }
}
