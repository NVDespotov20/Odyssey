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

	go db.ConnectDB()
	go server.Init()
	server.BindRoutes()
	auth.GenerateKeys()
	jwks.GenerateKeySet()

	<-exitSignal
	go db.DisconnectDB()
	go server.Shutdown()

}
