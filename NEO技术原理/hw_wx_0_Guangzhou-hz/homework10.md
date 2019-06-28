
### homework 10 大作业


1. 尝试本地搭建私链，请参考步骤：https://docs.neo.org/zh-cn/network/private-chain/private-chain2.html

2. 将合约 Calculator.cs 部署在私链上。合约编译：
   1. 方法一，采用visual studio 2017进行编译，详情参考：https://docs.neo.org/zh-cn/sc/quickstart/getting-started-csharp.html
   2. 方法二，采用在线网页编译器，参考：https://neocompiler.io/#!/ecolab/compilers

3. 尝试从合约代码的AVM Opcode指令集中，定位出 `div`逻辑的代码段。（请使用 https://neocompiler.io/#!/ecolab/compilers 编译后的Opcode指令集 ）

   > ```
   > DUPFROMALTSTACK
   > PUSH0
   > PICKITEM
   > PUSHBYTES3: 0x646976
   > EQUAL
   > JMPIFNOT: 103
   > DUPFROMALTSTACK
   > PUSH1
   > PICKITEM
   > ARRAYSIZE
   > PUSH2
   > NUMEQUAL
   > JMPIF: 8
   > PUSH0
   > NOP
   > FROMALTSTACK
   > DROP
   > RET
   > DUPFROMALTSTACK
   > PUSH1
   > PICKITEM
   > PUSH0
   > PICKITEM
   > DUPFROMALTSTACK
   > PUSH11
   > PUSH2
   > ROLL
   > SETITEM
   > DUPFROMALTSTACK
   > PUSH1
   > PICKITEM
   > PUSH1
   > PICKITEM
   > DUPFROMALTSTACK
   > PUSH12
   > PUSH2
   > ROLL
   > SETITEM
   > DUPFROMALTSTACK
   > PUSH11
   > PICKITEM
   > DUPFROMALTSTACK
   > PUSH12
   > PICKITEM
   > DIV
   > DUPFROMALTSTACK
   > PUSH13
   > PUSH2
   > ROLL
   > SETITEM
   > NOP
   > DUPFROMALTSTACK
   > PUSH11
   > PICKITEM
   > DUPFROMALTSTACK
   > PUSH12
   > PICKITEM
   > DUPFROMALTSTACK
   > PUSH13
   > PICKITEM
   > NOP
   > PUSH2
   > XSWAP
   > PUSHBYTES11: 0x726573756c744576656e74
   > PUSH4
   > PACK
   > SYSCALLPUSHBYTES18: error parsing syscall fn arg
   > DUPFROMALTSTACK
   > PUSH13
   > PICKITEM
   > NOP
   > FROMALTSTACK
   > DROP
   > RET
   > PUSH1
   > NOP
   > FROMALTSTACK
   > DROP
   > RET
   > SYSCALLPUSHBYTES19: error parsing syscall fn arg
   > ```

4. 在私链上调用该合约，并进行 加、减、乘、除的四个操作，并通过 ApplicationLog 插件拿到输出结果。（ApplicationLog插件下载地址：https://github.com/neo-project/neo-plugins/releases）

* add 操作Log
```json
{
    "jsonrpc": "2.0",
    "id": 1,
    "result": {
        "txid": "0x3afcdf0f144d5fd80e10d503839f637ddc4ed000c370c33adff0b71e26aa12db",
        "executions": [
            {
                "trigger": "Application",
                "contract": "0xe901b1cf9db1fd4fdbd379ea55c705ece9da96f9",
                "vmstate": "HALT",
                "gas_consumed": "0.073",
                "stack": [
                    {
                        "type": "Integer",
                        "value": "2"
                    }
                ],
                "notifications": [
                    {
                        "contract": "0x59aa61642cdb54e2b5c3bdbe878b7d9ca6f027f1",
                        "state": {
                            "type": "Array",
                            "value": [
                                {
                                    "type": "ByteArray",
                                    "value": "726573756c744576656e74"
                                },
                                {
                                    "type": "Integer",
                                    "value": "1"
                                },
                                {
                                    "type": "Integer",
                                    "value": "1"
                                },
                                {
                                    "type": "Integer",
                                    "value": "2"
                                }
                            ]
                        }
                    }
                ]
            }
        ]
    }
}
```
* Sub操作log

```json
{
    "jsonrpc": "2.0",
    "id": 1,
    "result": {
        "txid": "0xf0b6ddac9ec1e30a721792b417a4575ee16dcceb060c96cdd3051cc4f4835069",
        "executions": [
            {
                "trigger": "Application",
                "contract": "0x38e0f769c56356746e93db7c5a3514374fb2750c",
                "vmstate": "HALT",
                "gas_consumed": "0.077",
                "stack": [
                    {
                        "type": "Integer",
                        "value": "1"
                    }
                ],
                "notifications": [
                    {
                        "contract": "0x59aa61642cdb54e2b5c3bdbe878b7d9ca6f027f1",
                        "state": {
                            "type": "Array",
                            "value": [
                                {
                                    "type": "ByteArray",
                                    "value": "726573756c744576656e74"
                                },
                                {
                                    "type": "Integer",
                                    "value": "2"
                                },
                                {
                                    "type": "Integer",
                                    "value": "1"
                                },
                                {
                                    "type": "Integer",
                                    "value": "1"
                                }
                            ]
                        }
                    }
                ]
            }
        ]
    }
}
```

* Multi 操作log

```json
{
    "jsonrpc": "2.0",
    "id": 1,
    "result": {
        "txid": "0x291577682719c673ad5d4c9a98db013084332e4e7256cd51b6abc0cf5b17b1f5",
        "executions": [
            {
                "trigger": "Application",
                "contract": "0xc88b5afed2a2fca940b9e1042ef5f082fa6850ec",
                "vmstate": "HALT",
                "gas_consumed": "0.081",
                "stack": [
                    {
                        "type": "Integer",
                        "value": "6"
                    }
                ],
                "notifications": [
                    {
                        "contract": "0x59aa61642cdb54e2b5c3bdbe878b7d9ca6f027f1",
                        "state": {
                            "type": "Array",
                            "value": [
                                {
                                    "type": "ByteArray",
                                    "value": "726573756c744576656e74"
                                },
                                {
                                    "type": "Integer",
                                    "value": "2"
                                },
                                {
                                    "type": "Integer",
                                    "value": "3"
                                },
                                {
                                    "type": "Integer",
                                    "value": "6"
                                }
                            ]
                        }
                    }
                ]
            }
        ]
    }
}
```



* div操作log

```json
{
    "jsonrpc": "2.0",
    "id": 1,
    "result": {
        "txid": "0x4f6bc5332e7a869694452be25bb9265543aa25002446d0e9d780fc88c30d0597",
        "executions": [
            {
                "trigger": "Application",
                "contract": "0x718d4f3f2f929311276feb67dd0fea3b7b523ff8",
                "vmstate": "HALT",
                "gas_consumed": "0.085",
                "stack": [
                    {
                        "type": "Integer",
                        "value": "3"
                    }
                ],
                "notifications": [
                    {
                        "contract": "0x59aa61642cdb54e2b5c3bdbe878b7d9ca6f027f1",
                        "state": {
                            "type": "Array",
                            "value": [
                                {
                                    "type": "ByteArray",
                                    "value": "726573756c744576656e74"
                                },
                                {
                                    "type": "Integer",
                                    "value": "6"
                                },
                                {
                                    "type": "Integer",
                                    "value": "2"
                                },
                                {
                                    "type": "Integer",
                                    "value": "3"
                                }
                            ]
                        }
                    }
                ]
            }
        ]
    }
}
```



5. 调用合约，并进行除以0的除法调用操作，尝试分析返回值，并通过ApplicationLog 观察VM执行结果，包括虚拟机状态，栈数据情况

```json
{
    "jsonrpc": "2.0",
    "id": 1,
    "result": {
        "txid": "0x5d80160fee9e7a28282faaf9fdb79aa216d53d4a9adcac5e5ae96d973431383f",
        "executions": [
            {
                "trigger": "Application",
                "contract": "0x5252bfe5ddefac870b9e080751d31629f7218817",
                "vmstate": "FAULT",
                "gas_consumed": "0.068",
                "stack": [],
                "notifications": []
            }
        ]
    }
}
```

调用合约时无返回值，因为在执行合约过程中出现了异常，虚拟机直接退出。因此虚拟机状态是FAULT，栈中无数据