## homework 10

### 1. 尝试从合约代码的AVM Opcode指令集中，定位出 div逻辑的代码段。（请使用 https://neocompiler.io/#!/ecolab/compilers 编译后的Opcode指令集）

- 6a 386: DUPFROMALTSTACK  #
- 00 387: PUSH0  #An empty array of bytes is pushed onto the stack
- c3 388: PICKITEM  #
- 03 389: PUSHBYTES3 646976 # div
- 87 393: EQUAL  # Returns 1 if the inputs are exactly equal, 0 otherwise.
- 64 394: JMPIFNOT 6700 # 103
- 6a 397: DUPFROMALTSTACK  #
- 51 398: PUSH1  # The number 1 is pushed onto the stack.
- c3 399: PICKITEM  #
- c0 400: ARRAYSIZE  #
- 52 401: PUSH2  # The number 2 is pushed onto the stack.
- 9c 402: NUMEQUAL  # Returns 1 if the numbers are equal, 0 otherwise.
- 63 403: JMPIF 0800 # 8
- 00 406: PUSH0  #An empty array of bytes is pushed onto the stack
- 6c 408: FROMALTSTACK  # Puts the input onto the top of the main stack. Removes it from the alt stack.
- 75 409: DROP  # Removes the top stack item.
- 66 410: RET  #
- 6a 411: DUPFROMALTSTACK  #
- 51 412: PUSH1  # The number 1 is pushed onto the stack.
- c3 413: PICKITEM  #
- 00 414: PUSH0  #An empty array of bytes is pushed onto the stack
- c3 415: PICKITEM  #
- 6a 416: DUPFROMALTSTACK  #
- 5b 417: PUSH11  # The number 11 is pushed onto the stack.
- 52 418: PUSH2  # The number 2 is pushed onto the stack.
- 7a 419: ROLL  # The item n back in the stack is moved to the top.
- c4 420: SETITEM  #
- 6a 421: DUPFROMALTSTACK  #
- 51 422: PUSH1  # The number 1 is pushed onto the stack.
- c3 423: PICKITEM  #
- 51 424: PUSH1  # The number 1 is pushed onto the stack.
- c3 425: PICKITEM  #
- 6a 426: DUPFROMALTSTACK  #
- 5c 427: PUSH12  # The number 12 is pushed onto the stack.
- 52 428: PUSH2  # The number 2 is pushed onto the stack.
- 7a 429: ROLL  # The item n back in the stack is moved to the top.
- c4 430: SETITEM  #
- 6a 431: DUPFROMALTSTACK  #
- 5b 432: PUSH11  # The number 11 is pushed onto the stack.
- c3 433: PICKITEM  #
- 6a 434: DUPFROMALTSTACK  #
- 5c 435: PUSH12  # The number 12 is pushed onto the stack.
- c3 436: PICKITEM  #
- 96 437: DIV  # a is divided by b.
- 6a 438: DUPFROMALTSTACK  #
- 5d 439: PUSH13  # The number 13 is pushed onto the stack.
- 52 440: PUSH2  # The number 2 is pushed onto the stack.
- 7a 441: ROLL  # The item n back in the stack is moved to the top.
- c4 442: SETITEM  #
- 6a 444: DUPFROMALTSTACK  #
- 5b 445: PUSH11  # The number 11 is pushed onto the stack.
- c3 446: PICKITEM  #
- 6a 447: DUPFROMALTSTACK  #
- 5c 448: PUSH12  # The number 12 is pushed onto the stack.
- c3 449: PICKITEM  #
- 6a 450: DUPFROMALTSTACK  #
- 5d 451: PUSH13  # The number 13 is pushed onto the stack.
- c3 452: PICKITEM  #
- 52 454: PUSH2  # The number 2 is pushed onto the stack.
- 72 455: XSWAP  #
- 0b 456: PUSHBYTES11 726573756c744576656e74 # resultEvent
- 54 468: PUSH4  # The number 4 is pushed onto the stack.
- c1 469: PACK  #
- 68 470: SYSCALL 124e656f2e52756e74696d652e4e6f74696679 # Neo.Runtime.Notify
- 6a 490: DUPFROMALTSTACK  #
- 5d 491: PUSH13  # The number 13 is pushed onto the stack.
- c3 492: PICKITEM  #
- 6c 494: FROMALTSTACK  # Puts the input onto the top of the main stack. Removes it from the alt stack.
- 75 495: DROP  # Removes the top stack item.
- 66 496: RET  #

---

### 2. 在私链上调用该合约，并进行 加、减、乘、除的四个操作，并通过 ApplicationLog 插件拿到输出结果。（ApplicationLog插件下载地址：https://github.com/neo-project/neo-plugins/releases）\

加法的ApplicationLog

```json
{
    "jsonrpc": "2.0",
    "id": 1,
    "result": {
        "txid": "0x313a7be699e7bf865ec279452dcbc9081fbc0feae5dd73639ec35756c73db961",
        "executions": [
            {
                "trigger": "Application",
                "contract": "0x87c7156c6cf3e30b51c7fe3806e58b02be302b62",
                "vmstate": "HALT",
                "gas_consumed": "0.073",
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
                                    "value": "1"
                                },
                                {
                                    "type": "Integer",
                                    "value": "5"
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

减法的ApplicationLog

``` json
{
    "jsonrpc": "2.0",
    "id": 1,
    "result": {
        "txid": "0x610c6737962e25408366e1c37106fea5eb6c8eeeb7f85c06b9a1dd5fbce2189b",
        "executions": [
            {
                "trigger": "Application",
                "contract": "0x9eb99305cc9f9770dcd14e43bfc9323ffceb2ee7",
                "vmstate": "HALT",
                "gas_consumed": "0.077",
                "stack": [
                    {
                        "type": "Integer",
                        "value": "7"
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
                                    "value": "10"
                                },
                                {
                                    "type": "Integer",
                                    "value": "3"
                                },
                                {
                                    "type": "Integer",
                                    "value": "7"
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

乘法的ApplicationLog

``` json
{
    "jsonrpc": "2.0",
    "id": 1,
    "result": {
        "txid": "0x860a21a52f886e8ff328e8069e9a86f5646bf9d1190d9ca6bd34508c57a1d2b0",
        "executions": [
            {
                "trigger": "Application",
                "contract": "0xb7a794fc6aabcdae9a68154fa4dee15051566c38",
                "vmstate": "HALT",
                "gas_consumed": "0.081",
                "stack": [
                    {
                        "type": "Integer",
                        "value": "32"
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
                                    "value": "8"
                                },
                                {
                                    "type": "Integer",
                                    "value": "4"
                                },
                                {
                                    "type": "Integer",
                                    "value": "32"
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

除法的ApplicationLog

``` json
{
    "jsonrpc": "2.0",
    "id": 1,
    "result": {
        "txid": "0x8d37fc2ee65de3e28cf14f69a5a07c782bc97e6fb3f36345521c542b7d374fd7",
        "executions": [
            {
                "trigger": "Application",
                "contract": "0xe811bdc05bd15107be5323353e7e3dfd3a4c882a",
                "vmstate": "HALT",
                "gas_consumed": "0.085",
                "stack": [
                    {
                        "type": "Integer",
                        "value": "16"
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
                                    "type": "ByteArray",
                                    "value": "20"
                                },
                                {
                                    "type": "Integer",
                                    "value": "2"
                                },
                                {
                                    "type": "Integer",
                                    "value": "16"
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

---

### 3. 调用合约，并进行除以0的除法调用操作，尝试分析返回值，并通过ApplicationLog 观察VM执行结果，包括虚拟机状态，栈数据情况。

- vm给出的状态为：FAULT
- 栈为空