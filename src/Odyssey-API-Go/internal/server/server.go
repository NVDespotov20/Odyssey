package server

import (
	"context"
	"log"
	"net/http"
	"time"
)

var (
	mux = http.NewServeMux()

	server = &http.Server{
		Addr:    ":8080",
		Handler: mux,
	}
)

func Init() {

	go func() {
		if err := server.ListenAndServe(); err != http.ErrServerClosed {
			log.Fatal("Server error:", err)
		}
	}()
}

func BindRoutes() {
    BindUserRoutes(mux)
    BindJWKS(mux)
}

func Shutdown() {
	ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer cancel()

	server.Shutdown(ctx)
}
