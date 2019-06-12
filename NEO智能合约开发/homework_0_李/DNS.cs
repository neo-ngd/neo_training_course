using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace DNS
{
    public class Domain : SmartContract
    {
        public static object Main(string operation, params object[] args)
        {
            //在链上运行时 Trriger =16
            ///application 触发器
            if (Runtime.Trigger == TriggerType.Application)
            {
                switch (operation)
                {
                    case "query":
                        ///查询入参 数组的第一个参数
                        return Query((string)args[0]);
                    case "register":
                        ///注册 数组的第一和第二个参数
                        return Register((string)args[0], (byte[])args[1]);
                    case "delete":
                        ///需要数组的第一个参数
                        return Delete((string)args[0]);
                    default:
                        return false;
                }
            }
            return true;
        }

        private static byte[] Query(string domain)
        {
            ///query方法  domain 是入参 查询domain域名拥有者的地址 
            return Storage.Get(Storage.CurrentContext, domain);
        }


        private static bool Register(string domain, byte[] owner)
        {
            // Check if  the owner is the same as the one who invoke the contract
            //查找owner 存在返回false
            if (!Runtime.CheckWitness(owner)) return false;
            //owner不存在 查询domain这个键的是是不是空，非空 就返回错误 为空就赋值
            byte[] value = Storage.Get(Storage.CurrentContext, domain);
            //不为空 返回错误
            if (value != null) return false;
            //最后注册
            Storage.Put(Storage.CurrentContext, domain, owner);
            return true;
        }

        private static bool Delete(string domain)
        {
            // To do  判断存在则delete  //其基本思想是首先检查域名所属者，
            //如果存在并且与合约的调用者相同，
            //则使用 Storage.Delete 方法来删除相应的键值对
            byte[] owner = Storage.Get(Storage.CurrentContext, domain);
            if (owner != null && Runtime.CheckWitness(owner)) {
                Storage.Delete(Storage.CurrentContext, domain);
                return true;
            }
            return false;
            

        }
    }
}