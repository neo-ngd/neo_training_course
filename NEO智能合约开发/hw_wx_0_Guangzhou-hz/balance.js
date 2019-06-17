var neon = require("@cityofzion/neon-js");

client = new neon.rpc.RPCClient("http://seed2.aphelion-neo.com:10332");

nnc = 'fc732edee1efdf968c23c20a9628eaa5a6ccb934';

query = neon.default.create.query({ method: "getnep5balances", params: ['ATgjfbkkgAgpfGe1DiKjLwvGSXZ7MMUjZU'] });
client.execute(query).then(res => {

    for (var b of res.result.balance) {
        if (b.asset_hash == nnc) {
            console.log(b.amount);
        }
    }
});