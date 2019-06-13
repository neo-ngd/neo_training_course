import Neon from "@cityofzion/neon-js";

var myNeoscan = new Neon.api.neoscan.instance("MainNet");
myNeoscan.getBalance("ATgjfbkkgAgpfGe1DiKjLwvGSXZ7MMUjZU").then(res => {
    console.log(res);
});