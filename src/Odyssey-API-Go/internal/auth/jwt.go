package auth

import (
	"context"
	"crypto/rand"
	"crypto/rsa"
	"crypto/x509"
	"encoding/pem"
	"fmt"
	"log"
	"net/http"
	"odysseyapi-go/internal/db"
	"odysseyapi-go/internal/utils"
	"os"
	"time"

	"github.com/golang-jwt/jwt/v5"
)

const (
	rTokenDuration time.Duration = time.Hour * 24 * 1825
	aTokenDuration time.Duration = time.Hour * 24 * 365
)

var (
	rTokenPrivateKey string
	aTokenPrivateKey string
	rTokenPublicKey  string
	aTokenPublicKey  string
)

func GenerateKeys() {
	//seed := sha256.Sum224([]byte(os.Getenv("JWT_ACCESS_GEN_SECRET")))
	aTokenKey, err := rsa.GenerateKey(rand.Reader, 2048)
	if err != nil {
		log.Fatalf("Failed to generate RSA key: %v", err)
	}

	aTokenPrivateKey = string(pem.EncodeToMemory(&pem.Block{
		Type:  "RSA PRIVATE KEY",
		Bytes: x509.MarshalPKCS1PrivateKey(aTokenKey),
	}))

	fmt.Println("")

	aTokenPublicKey = string(pem.EncodeToMemory(&pem.Block{
		Type:  "RSA PUBLIC KEY",
		Bytes: x509.MarshalPKCS1PublicKey(&aTokenKey.PublicKey),
	}))

	//seed = sha256.Sum256([]byte(os.Getenv("JWT_REFRESH_GEN_SECRET")))
	rTokenKey, err := rsa.GenerateKey(rand.Reader, 2048)
	if err != nil {
		log.Fatalf("Failed to generate RSA key: %v", err)
	}

	aTokenPrivateKey = string(pem.EncodeToMemory(&pem.Block{
		Type:  "RSA PRIVATE KEY",
		Bytes: x509.MarshalPKCS1PrivateKey(rTokenKey),
	}))

	aTokenPrivateKey = string(pem.EncodeToMemory(&pem.Block{
		Type:  "RSA PUBLIC KEY",
		Bytes: x509.MarshalPKCS1PublicKey(&rTokenKey.PublicKey),
	}))
}

func GetAccessPublicKey() []byte {
	return aTokenPublicKey
}

func GetRefreshPublicKey() []byte {
	return rTokenPublicKey
}

func GenerateAccessToken(userid string) (string, utils.APIError) {
	return generateToken(userid, aTokenPrivateKey, aTokenDuration, false)
}

func GenerateRefreshToken(userid string) (string, utils.APIError) {
	return generateToken(userid, rTokenPrivateKey, rTokenDuration, true)
}

func ValidateAccessToken(tokenString string) (db.User, utils.APIError) {
	return validateToken(tokenString, GetAccessPublicKey(), false)
}

func ValidateRefreshToken(tokenString string) (db.User, utils.APIError) {
	return validateToken(tokenString, GetRefreshPublicKey(), true)
}

func validateToken(tokenString string, publicKey []byte, checkJti bool) (user db.User, apiErr utils.APIError) {
	ctx, cancel := context.WithTimeout(context.Background(), 15*time.Second)
	defer cancel()

	token, err := jwt.Parse(tokenString, func(token *jwt.Token) (any, error) {
		return publicKey, nil
	},
		//jwt.WithValidMethods([]string{jwt.SigningMethodRS256.Alg()}),
		jwt.WithLeeway(time.Second*5),
		jwt.WithIssuer(os.Getenv("API_NAME")),
		jwt.WithAudience(os.Getenv("API_NAME")))

	if err != nil {
		apiErr.Code = http.StatusUnauthorized
		apiErr.Message = "Invalid JWT Token PUBLIC KEY IS KILLING ME"

		return
	}

	if claims, ok := token.Claims.(jwt.MapClaims); ok && token.Valid {
		var userid string
		userid, err = token.Claims.GetSubject()
		if err != nil {
			apiErr.Code = http.StatusUnauthorized
			apiErr.Message = "Invalid JWT Token"

			return
		}

		user, err = db.GetQueries().GetUserById(ctx, userid)
		if err != nil {
			apiErr.Code = http.StatusUnauthorized
			apiErr.Message = "Invalid JWT Token"

			return
		}

		if !checkJti {
			return
		}

		jti, exists := claims["jti"]
		if !exists {

			apiErr.Code = http.StatusUnauthorized
			apiErr.Message = "Invalid JWT Token"

			return
		}

		if userJti, _ := db.GetQueries().GetRefreshTokenByUserId(ctx, userid); jti != userJti {

			apiErr.Code = http.StatusUnauthorized
			apiErr.Message = "Invalid JWT Token"

			return
		}
	}

	return
}

func generateToken(userid string, key []byte, duration time.Duration, applyJti bool) (tokenString string, apiErr utils.APIError) {
	ctx, cancel := context.WithTimeout(context.Background(), 15*time.Second)
	defer cancel()

	refreshToken := ""
	if applyJti {
		var err error
		refreshToken, err = db.GetQueries().GetRefreshTokenByUserId(ctx, userid)
		if err != nil {
			apiErr.Code = http.StatusNotFound
			apiErr.Message = "User Not Found"
			return
		}
	}

	claims := &jwt.RegisteredClaims{
		Issuer:    os.Getenv("API_NAME"),
		Subject:   userid,
		Audience:  []string{os.Getenv("API_NAME")},
		ExpiresAt: jwt.NewNumericDate(time.Now().Add(duration)),
		NotBefore: jwt.NewNumericDate(time.Now()),
		IssuedAt:  jwt.NewNumericDate(time.Now()),
		ID:        refreshToken,
	}

	token := jwt.NewWithClaims(jwt.SigningMethodRS256, claims)

	if applyJti {
		token.Header["kid"] = os.Getenv("API_NAME")[:2] + "_refresh"
	} else {
		token.Header["kid"] = os.Getenv("API_NAME")[:2] + "_access"
	}
	token.Header["jku"] = "/jwks"

	tokenString, err := token.SignedString(key)

	if err != nil {
		apiErr.Code = http.StatusInternalServerError
		apiErr.Message = "Token signature failed " + err.Error()
		return
	}

	return tokenString, apiErr
}
