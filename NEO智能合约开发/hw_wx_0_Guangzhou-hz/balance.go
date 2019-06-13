package main

import (
	"fmt"
	"github.com/hzxiao/goutil"
	"github.com/hzxiao/goutil/httputil"
)

var seed = "http://seed2.aphelion-neo.com:10332"
var nnc = "fc732edee1efdf968c23c20a9628eaa5a6ccb934"

func main()  {
	reqArgs := goutil.Map{
		"id": 1,
		"jsonrpc": "2.0",
		"method": "getnep5balances",
		"params": []interface{}{"ATgjfbkkgAgpfGe1DiKjLwvGSXZ7MMUjZU"},
	}

	var res goutil.Map
	err := httputil.PostJSON(seed, reqArgs, httputil.ReturnJSON, &res)
	if err != nil {
		fmt.Println(err)
		return
	}

	for _, b := range res.GetMapArrayP("result/balance") {
		if b.GetString("asset_hash") == nnc {
			fmt.Printf("balance: %v\n", b.GetInt64("amount"))
		}
	}
}
