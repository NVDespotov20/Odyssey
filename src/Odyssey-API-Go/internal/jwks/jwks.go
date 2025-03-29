package jwks

import (
	"encoding/json"
	"fmt"
	"log"
	"net/http"
	"odysseyapi-go/internal/auth"
	"os"
)

type JWK struct {
	KeyType   string `json:"kty"`
	KeyID     string `json:"kid"`
	KeyUsage  string `json:"use"`
	Algorithm string `json:"alg"`
	Crv       string `json:"crv"`
	X         string `json:"x"`
}

var keys []JWK
var keyJson []byte

func GenerateKeySet() (err error) {
	keys = nil

	keys = append(keys, JWK{
		KeyType:   "OKP",
		KeyID:     os.Getenv("API_NAME")[:2] + "_access",
		KeyUsage:  "sig",
		Algorithm: "EdDSA",
		Crv:       "Ed25519",
		X:         fmt.Sprintf("%x", auth.GetAccessPublicKey()),
	})

	keys = append(keys, JWK{
		KeyType:   "OKP",
		KeyID:     os.Getenv("API_NAME")[:2] + "_refresh",
		KeyUsage:  "sig",
		Algorithm: "EdDSA",
		Crv:       "Ed25519",
		X:         fmt.Sprintf("%x", auth.GetAccessPublicKey()),
	})

	keyJson, err = json.Marshal(keys)

	if err != nil {
		log.Fatal("Couldn't generate JWKS payload")
	}

	return
}

func FetchKey(w http.ResponseWriter, r *http.Request) {
	w.Write(keyJson)
}
