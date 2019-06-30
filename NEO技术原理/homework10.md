
### homework 10 大作业


1. 尝试本地搭建私链，请参考步骤：https://docs.neo.org/zh-cn/network/private-chain/private-chain2.html

2. 将合约 Calculator.cs 部署在私链上。合约编译：
   1. 方法一，采用visual studio 2017进行编译，详情参考：https://docs.neo.org/zh-cn/sc/quickstart/getting-started-csharp.html
   2. 方法二，采用在线网页编译器，参考：https://neocompiler.io/#!/ecolab/compilers

3. 尝试从合约代码的AVM Opcode指令集中，定位出 `div`逻辑的代码段。（请使用 https://neocompiler.io/#!/ecolab/compilers 编译后的Opcode指令集）

4. 在私链上调用该合约，并进行 加、减、乘、除的四个操作，并通过 ApplicationLog 插件拿到输出结果。（ApplicationLog插件下载地址：https://github.com/neo-project/neo-plugins/releases）

5. 调用合约，并进行除以0的除法调用操作，尝试分析返回值，并通过ApplicationLog 观察VM执行结果，包括虚拟机状态，栈数据情况。
/Users/apple/Documents/neoSmartContract/homework/neo_training_course/NEO技术原理/homework1.md