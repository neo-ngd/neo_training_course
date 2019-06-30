
### homework 7

#####1. 理解智能合约的生命周期
![res](lifecircle.pdf)


2. 尝试本地发送一笔 Invocation的NEP5资产转账，并计算其Gas消耗，并再区块链浏览器上查看其Script字段，并翻译NEO的NVM的字节码

布署交易消耗500GAS,实际是490GAS，因为有10个免费额度.在调用合约的时候，NeoGui一直报错，但在neoray上可以的。估计NeoGui是有错误的。





#####3. *阅读《NEO智能合约开发（一）不可能完成的任务》，理解Verification与Application触发器区别。(附加题) https://www.cnblogs.com/crazylights/p/8427761.html*

Verification：转账触发，是用于转账时执行合约，如果合约返回true,代表执行成功，可以转账,合约执行失败或返回false,则交易执行失败，转账也就失败了.

Application: 发送InvocationTx触发,先打包交易，在区块被确认后，执行合约，如果合约失败，不影响交易。