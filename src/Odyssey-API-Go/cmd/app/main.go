package main

import (
	"os"
	"os/signal"

	"odysseyapi-go/internal/auth"
	"odysseyapi-go/internal/db"
	"odysseyapi-go/internal/jwks"
	"odysseyapi-go/internal/server"
)

func main() {
	exitSignal := make(chan os.Signal, 1)
	signal.Notify(exitSignal, os.Interrupt)

	db.ConnectDB()
	server.Init()
	server.BindRoutes()
	auth.GenerateKeys()
	jwks.GenerateKeySet()

	<-exitSignal
	db.DisconnectDB()
	server.Shutdown()

}
