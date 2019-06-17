#MerkleRoot 源码

```go
func main()  {
	//0x0000, 0x1111, 0x2222
	hashes := make([]common.Uint256, 3)
	hashes[0] = Uint256FromHexString("0000")
	hashes[1] = Uint256FromHexString("1111")
	hashes[2] = Uint256FromHexString("2222")

	root := BuildMerkleRoot(hashes)
	fmt.Println(root)
}

func Uint256FromHexString(hexHash string) (common.Uint256) {
	hashByte, err := hex.DecodeString(hexHash)
	if err != nil {
		panic(err.Error())
	}

	var hash common.Uint256
	copy(hash[:], hashByte)
	return hash
}

type MerkleTreeNode struct {
	Hash  common.Uint256
	Left  *MerkleTreeNode
	Right *MerkleTreeNode
}

func buildLeaves(hashes []common.Uint256) []*MerkleTreeNode {
	var leaves []*MerkleTreeNode
	for _, d := range hashes {
		node := &MerkleTreeNode{
			Hash: d,
		}
		leaves = append(leaves, node)
	}
	return leaves
}

func BuildMerkleRoot(txHashes []common.Uint256) common.Uint256 {
	nodes := buildLeaves(txHashes)
	if len(nodes) > 0 {
		for (len(nodes) > 1) {
			nodes = build(nodes)
		}
		return nodes[0].Hash
	} else {
		panic("error")
	}
}

func build(nodes []*MerkleTreeNode) []*MerkleTreeNode {
	var nextLevel []*MerkleTreeNode
	for i := 0; i < len(nodes)/2; i++ {
		node := &MerkleTreeNode{
			Hash:  ComputeParent(nodes[i*2].Hash, nodes[i*2+1].Hash),
			Left:  nodes[i*2],
			Right: nodes[i*2+1],
		}
		nextLevel = append(nextLevel, node)
	}
	if len(nodes)%2 == 1 {
		node := &MerkleTreeNode{
			Hash:  ComputeParent(nodes[len(nodes)-1].Hash, nodes[len(nodes)-1].Hash),
			Left:  nodes[len(nodes)-1],
			Right: nodes[len(nodes)-1],
		}
		nextLevel = append(nextLevel, node)
	}
	return nextLevel
}

func ComputeParent(left common.Uint256, right common.Uint256) common.Uint256 {
	var sha [64]byte
	copy(sha[:32], left[:])
	copy(sha[32:], right[:])
	return common.Uint256(common.Sha256D(sha[:]))
}	
```

