## homework 4

在本地使用neon-js查询主网上 ATgjfbkkgAgpfGe1DiKjLwvGSXZ7MMUjZU 这个地址上NNC (NEO NAME Credit)这个token的余额

### 执行结果

[result](https://assets.myjoit.com/images/neo/neo_homework/neo_homework_4.png)

### 代码

``` javascript
import { api } from "@cityofzion/neon-js";

let neoscan = new api.neoscan.instance('MainNet')

neoscan.getBalance('ATgjfbkkgAgpfGe1DiKjLwvGSXZ7MMUjZU').then(res => {
  var result = res.tokens['NEO NAME CREDIT']
  if (result) {
    console.log('the balance of NNS is', result.c[0])
  } else {
    console.log('the balance of NNS is NONE')
  }
})
```