using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;

namespace Neo.SmartContract
{
    public class Domain : Framework.SmartContract
    {
        public static object Main(string operation, params object[] args)
        {
	        if (Runtime.Trigger == TriggerType.Application)
	        {
		            switch (operation)
		            {
		                case "query":
		                    return Query((string)args[0]);
		                case "register":
		                    return Register((string)args[0], (byte[])args[1]);
		                case "delete":
		                    return Delete((string)args[0]);
		                default:
		                    return false;
		            }
	        } else
	        {
	            return false;
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
        	byte[] value = Storage.Get(Storage.CurrentContext, domain);
            if (value == null) return false;

            byte[] caller = ExecutionEngine.CallingScriptHash;
            if (!Equals(value, caller)) return false;

            Storage.Delete(Storage.CurrentContext, domain);
            return true;
        }

         private static bool Equals(byte[] b1, byte[] b2)
         {
            if (b1.Length != b2.Length) return false;
            if (b1 == null || b2 == null) return false;
            for (int i = 0; i < b1.Length; i++)
                if (b1[i] != b2[i])  return false;
            return true;
         }
    }
}
