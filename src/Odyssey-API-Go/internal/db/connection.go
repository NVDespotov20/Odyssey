package db

import (
	"context"
	"log"
	"os"
	"time"

	"github.com/jackc/pgx/v5"
)

type DB struct {
	conn    *pgx.Conn
	Queries *Queries
}

var database DB

func ConnectDB() {

	ctx, cancel := context.WithTimeout(context.Background(), 10*time.Second)
	defer cancel()

	connChannel := make(chan *pgx.Conn)
	go func() {
		connection, err := pgx.Connect(ctx, os.Getenv("DB_CONN"))
		connChannel <- connection

		if err != nil {
			log.Fatal("Failed to establish database connection: ", err)
		}
	}()

	select {
	case <-ctx.Done():
		log.Fatal("Database connection attempt timed out")
	case database.conn = <-connChannel:
		database.Queries = New(database.conn)
		return
	}
}

func DisconnectDB() {

	ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer cancel()

	database.conn.Close(ctx)

	select {
	case <-ctx.Done():
		log.Fatal("Database disconnect attemp timed out")
	default:
		return
	}
}

func GetQueries() *Queries {
	return database.Queries
}
