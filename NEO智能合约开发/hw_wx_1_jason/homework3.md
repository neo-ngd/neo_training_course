
#### homework 3

neo主网查询nns资产

```


const {default:Neon, wallet, rpc, api }  = require('@cityofzion/neon-js')

const net = 'MainNet'
const client = new api.neoscan.instance(net)

const args = process.argv.splice(2)
const address= args[0] || 'ATgjfbkkgAgpfGe1DiKjLwvGSXZ7MMUjZU'
const token = args[1] ||  "NEO NAME CREDIT"

const getBalance = (address, token) =>
  client.getBalance(address)
    .then(res=>console.log(`${token} is: ${res.tokens[token]}`))
    .catch(err=>console.error("error"))

getBalance(address, token)


```