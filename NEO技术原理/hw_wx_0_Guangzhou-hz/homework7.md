
### homework 7

1. 理解智能合约的生命周期

  1. 用高级语言(如：C#, Java等)编写合约代码
  2. 将合约编译成AVM码
  3. 部署合约
  4. 用户可以发起交易调用执行合约
  4. 转账或接收转账时触发合约
  5. 可以对已部署的合约进行合约升级
  6. 可以对已部署的合约进行销毁

2. 尝试本地发送一笔 Invocation的NEP5资产转账，并计算其Gas消耗，并再区块链浏览器上查看其Script字段，并翻译NEO的NVM的字节码

Gas消耗 2.782

Scrip字段：

> 02e803148e568bcbf3bab3d524f556a78148d462c0be738314a0f373b5d8b717fb944179f104084b01e9dd3ab853c1087472616e73666572671e13baaefaf3f272d2be804eb3cf18f6556f1902f1



VM字节码：

> ```
> PUSHBYTES2: 0xe803 # 转账数量1000 
> PUSHBYTES20: 0x8e568bcbf3bab3d524f556a78148d462c0be7383 # 接收者
> PUSHBYTES20: 0xa0f373b5d8b717fb944179f104084b01e9dd3ab8 # 转账者
> PUSH3    # 数组的数量
> PACK     # 数组
> PUSHBYTES8: 0x7472616e73666572 # transfer
> APPCALL: 02196f55f618cfb34e80bed272f2f3faaeba131e #调用合约
> THROWIFNOT
> ```


3. *阅读《NEO智能合约开发（一）不可能完成的任务》，理解Verification与Application触发器区别。(附加题) https://www.cnblogs.com/crazylights/p/8427761.html*



