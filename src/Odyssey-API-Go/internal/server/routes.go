package server

import (
	"net/http"
	"odysseyapi-go/internal/handlers"
	"odysseyapi-go/internal/jwks"
)

var userHandler *handlers.UserHandler = new(handlers.UserHandler)

func BindUserRoutes(mux *http.ServeMux) {
	mux.HandleFunc("POST /users/login", userHandler.Login)
	mux.HandleFunc("POST /users/register", userHandler.Register)
	mux.HandleFunc("GET /users/refresh", userHandler.FetchAccessToken)
}

func BindJWKS(mux *http.ServeMux) {
	mux.HandleFunc("GET /jwks", jwks.FetchKey)
}
