using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
namespace Neo.SmartContract
{
    public class Domain : SmartContract
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
            byte[] owner = Storage.Get(Storage.CurrentContext, domain);//get owner
            if (owner != null) return false;//check if owner exist
            if (!Runtime.CheckWitness(owner)) return false;//check if owner own the domain
            Storage.Delete(Storage.CurrentContext, domain, owner);//delete
            return true;
        }
    }
}
