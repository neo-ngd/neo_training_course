## homework 4

### 1. 描述节点之间的握手协议

假设两个节点 Local 和 Remote

1. Local 和 Remote 都向对方发送 version 消息
1. Local 和 Remote 接收到 version 消息之后，都向对方返回 verback 消息
1. 如果存在 inv 消息，Local 就向 Remote 发送 getdata 消息
1. 如果当前 header height 小于目标 header height，也就是区块头落后了，就发送 getheaders 来更新
1. 如果当前 block height 小于目标 block height，也就是区块落后了，就发送 getblocks 来更新

---

### 2. 对比以太坊的Gossip协议的优劣

Gossip 优势：

1. 节点的扩展性
1. 容错性强，挂掉的节点无法影响网络
1. 不需要中心节点

Gossip 缺陷：

1. 随即感染，导致消息延迟率非常高
1. 会不断的判断周边节点是否曾受到感染，有巨量的冗余消息

NEO 优势：

1. 有共识节点治理，消息延迟小
1. 数据同步效率高

NEO 劣势：

1. 共识节点挂了可能会对网络产生阻滞等影响