// Code generated by sqlc. DO NOT EDIT.
// versions:
//   sqlc v1.28.0

package db

import (
	"github.com/jackc/pgx/v5/pgtype"
)

type RefreshToken struct {
	ID           string
	RefreshToken string
	UserID       string
}

type User struct {
	ID        string
	FirstName string
	LastName  string
	Username  string
	AboutMe   pgtype.Text
	Email     string
	Password  []byte
	Salt      string
	Deleted   pgtype.Bool
}
