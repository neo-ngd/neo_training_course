using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;

namespace SmartContractDemo {
    public class Domain : SmartContract {
        public static object Main(string operation, params object[] args) {
            switch (operation) {
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

        private static byte[] Query(string domain) {
            return Storage.Get(Storage.CurrentContext, domain);
        }

        private static bool Register(string domain, byte[] owner) {
            if (!Runtime.CheckWitness(owner)) {
                return false;
            }
            byte[] value = Storage.Get(Storage.CurrentContext, domain);
            if (value != null) {
                return false;
            }
            Storage.Put(Storage.CurrentContext, domain, owner);
            return true;
        }

        private static bool Delete(string domain) {
            byte[] owner = Storage.Get(Storage.CurrentContext, domain);
            if (owner == null) {
                return false;
            }
            if (!Runtime.CheckWitness(owner)) {
                return false;
            }
            Storage.Delete(Storage.CurrentContext, domain);
            return true;
        }
    }
}
