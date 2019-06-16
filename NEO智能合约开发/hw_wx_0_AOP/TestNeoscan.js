//这个作业不会，完全是根据第4节视频猜的
import Neon from "@cityofzion/neon-js";

var myNeoscan = new Neon.api.neoscan.instance("MainNet");
myNeoscan.getBalance("ATgjfbkkgAgpfGe1DiKjLwvGSXZ7MMUjZU").then(res => {
    console.log(res);
});