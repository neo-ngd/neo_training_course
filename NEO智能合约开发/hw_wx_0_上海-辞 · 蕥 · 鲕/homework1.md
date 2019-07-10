
#### homework1 

在上面的 DNS 智能合约(DNS.cs)中，有一个delete方法还未实现。其基本思想是首先检查域名所属者，如果存在并且与合约的调用者相同，则使用 Storage.Delete 方法来删除相应的键值对。请实现这个功能。



#### The code

```C#
private static bool Delete(string domain)
{
    // Check if  the owner is the same as the one who invoke the contract
    if (!Runtime.CheckWitness(owner)) return false;
    byte[] value = Storage.Get(Storage.CurrentContext, domain);
    if (value == null) return false;
    Storage.Delete(Storage.CurrentContext, domain);
    return true;
}
```
