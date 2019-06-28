 //import Neon, { api, wallet } from "@cityofzion/neon-js";
const {default: Neon, wallet, api, rpc }  = require('@cityofzion/neon-js');
const net = 'MainNet';
const client = new api.neoscan.instance(net);
const args = process.argv.splice(2);
const address=  'ATgjfbkkgAgpfGe1DiKjLwvGSXZ7MMUjZU';
const token =  "NEO NAME CREDIT";
const getBalance = (address, token) =>
    client.getBalance(address)
    .then(res=>console.log(`${token} amount is ${res.tokens[token]}`))
    .catch(config => {
    console.log(config);
  });
getBalance(address, token);


execute result: NEO NAME CREDIT amount is 3
