## homework 7

### 1. 理解智能合约的生命周期

#### 第一种情况：

1. 编写合约代码
1. 编译合约代码
1. 发送交易，部署合约代码
1. 转账交易前后，出发合约代码

#### 第二种情况

1. 编写合约代码
1. 编译合约代码
1. 发送交易，部署合约代码
1. 发送交易，调用合约定义的方法
1. 发送交易，调用升级方法或者销毁的方法，来升级或销毁合约

---

### 2. 尝试本地发送一笔 Invocation的NEP5资产转账，并计算其Gas消耗，并再区块链浏览器上查看其Script字段，并翻译NEO的NVM的字节码

使用本地私链进行操作
我的私链已经发行了代币：YouLoveApple，它的 ScriptHash为：0xa851e953730374ce64411ad2f01957f3dcd8e441

地址1：AbG2ky7Zb7YGdMMSVRg5aLYfSZeB5juyqk
地址2：AM5Sec3AEdDQRPVdcFfwWSJQxcZKXfhHy9

从地址1给地址2转过去 1 个 YouLoveApple token，预计消耗 Gas 2.669，优惠过后实际消耗 Gas 0.001

NEO-GUI 计算得到的原始 ByteCode
0400e1f505143a2b3fb26767674a7738ddf8511939d47666aaef14d5bda48c063f583aa93df8585c671fb8059e1f2753c1087472616e736665726741e4d8dcf35719f0d21a4164ce74037353e951a8f1

按照字节码的规则，拆分原始 ByteCode 得到如下结果
04 | 00e1f505 | 14 | 3a2b3fb26767674a7738ddf8511939d47666aaef | 14 | d5bda48c063f583aa93df8585c671fb8059e1f27 | 53 | c1 | 08 | 7472616e73666572 | 67 | 41e4d8dcf35719f0d21a4164ce74037353e951a8f1

那么我们知道操作码和数据之间有这么一些对应关系：

- 04 -> PUSHBYTES4
- 14 -> PUSHBYTES20
- 53 -> PUSH3
- c1 -> PACK
- 08 -> PUSHBYTES8
- 67 -> APPCALL

转换成 OpCode：

- PUSHBYTES4: 0x00e1f505  【压入一个4位数组】
- PUSHBYTES20: 0x3a2b3fb26767674a7738ddf8511939d47666aaef  【压入一个20位数组】
- PUSHBYTES20: 0xd5bda48c063f583aa93df8585c671fb8059e1f27  【压入一个20位数组】
- PUSH3  【压入大整数 3】
- PACK  【将计算栈栈顶的 3 个元素打包成数组】
- PUSHBYTES8: 0x7472616e73666572  【压入一个8位数组】
- APPCALL: a851e953730374ce64411ad2f01957f3dcd8e441  【调用指定地址的函数，这里是合约ScriptHash】