## homework1 

在上面的 DNS 智能合约(DNS.cs)中，有一个delete方法还未实现。其基本思想是首先检查域名所属者，如果存在并且与合约的调用者相同，则使用 Storage.Delete 方法来删除相应的键值对。请实现这个功能。

### 代码

``` C#
private static bool Delete(string domain)
{
    byte[] owner = Storage.Get(Storage.CurrentContext, domain);
    if (owner == null || !Runtime.CheckWitness(owner))
    {
        return false;
    }
    Storage.Delete(Storage.CurrentContext, domain);
    return true;
}
```