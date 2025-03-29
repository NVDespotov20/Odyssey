// Code generated by sqlc. DO NOT EDIT.
// versions:
//   sqlc v1.28.0
// source: query.sql

package db

import (
	"context"

	"github.com/jackc/pgx/v5/pgtype"
)

const createRefreshToken = `-- name: CreateRefreshToken :one
INSERT INTO refresh_tokens(
    refresh_token, user_id
) VALUES (
    $1, $2
)
RETURNING id, refresh_token, user_id
`

type CreateRefreshTokenParams struct {
	RefreshToken string
	UserID       string
}

func (q *Queries) CreateRefreshToken(ctx context.Context, arg CreateRefreshTokenParams) (RefreshToken, error) {
	row := q.db.QueryRow(ctx, createRefreshToken, arg.RefreshToken, arg.UserID)
	var i RefreshToken
	err := row.Scan(&i.ID, &i.RefreshToken, &i.UserID)
	return i, err
}

const createUsers = `-- name: CreateUsers :one
INSERT INTO users (
   first_name, last_name, username, about_me, email, password, salt
) VALUES (
    $1, $2, $3, $4, $5, $6, $7
)
RETURNING id, first_name, last_name, username, about_me, email, password, salt, deleted
`

type CreateUsersParams struct {
	FirstName string
	LastName  string
	Username  string
	AboutMe   pgtype.Text
	Email     string
	Password  []byte
	Salt      string
}

func (q *Queries) CreateUsers(ctx context.Context, arg CreateUsersParams) (User, error) {
	row := q.db.QueryRow(ctx, createUsers,
		arg.FirstName,
		arg.LastName,
		arg.Username,
		arg.AboutMe,
		arg.Email,
		arg.Password,
		arg.Salt,
	)
	var i User
	err := row.Scan(
		&i.ID,
		&i.FirstName,
		&i.LastName,
		&i.Username,
		&i.AboutMe,
		&i.Email,
		&i.Password,
		&i.Salt,
		&i.Deleted,
	)
	return i, err
}

const deleteRefreshToken = `-- name: DeleteRefreshToken :one

DELETE FROM refresh_tokens
WHERE id = $1
RETURNING id, refresh_token, user_id
`

func (q *Queries) DeleteRefreshToken(ctx context.Context, id string) (RefreshToken, error) {
	row := q.db.QueryRow(ctx, deleteRefreshToken, id)
	var i RefreshToken
	err := row.Scan(&i.ID, &i.RefreshToken, &i.UserID)
	return i, err
}

const deleteUser = `-- name: DeleteUser :one
DELETE FROM users
WHERE id = $1
RETURNING id, first_name, last_name, username, about_me, email, password, salt, deleted
`

func (q *Queries) DeleteUser(ctx context.Context, id string) (User, error) {
	row := q.db.QueryRow(ctx, deleteUser, id)
	var i User
	err := row.Scan(
		&i.ID,
		&i.FirstName,
		&i.LastName,
		&i.Username,
		&i.AboutMe,
		&i.Email,
		&i.Password,
		&i.Salt,
		&i.Deleted,
	)
	return i, err
}

const getRefreshTokenByUserId = `-- name: GetRefreshTokenByUserId :one

SELECT refresh_token FROM refresh_tokens
WHERE user_id = $1
`

func (q *Queries) GetRefreshTokenByUserId(ctx context.Context, userID string) (string, error) {
	row := q.db.QueryRow(ctx, getRefreshTokenByUserId, userID)
	var refresh_token string
	err := row.Scan(&refresh_token)
	return refresh_token, err
}

const getUserById = `-- name: GetUserById :one
SELECT id, first_name, last_name, username, about_me, email, password, salt, deleted FROM users
WHERE id = $1 LIMIT 1
`

func (q *Queries) GetUserById(ctx context.Context, id string) (User, error) {
	row := q.db.QueryRow(ctx, getUserById, id)
	var i User
	err := row.Scan(
		&i.ID,
		&i.FirstName,
		&i.LastName,
		&i.Username,
		&i.AboutMe,
		&i.Email,
		&i.Password,
		&i.Salt,
		&i.Deleted,
	)
	return i, err
}

const getUserByUsername = `-- name: GetUserByUsername :one
SELECT id, first_name, last_name, username, about_me, email, password, salt, deleted FROM users
WHERE username = $1 LIMIT 1
`

func (q *Queries) GetUserByUsername(ctx context.Context, username string) (User, error) {
	row := q.db.QueryRow(ctx, getUserByUsername, username)
	var i User
	err := row.Scan(
		&i.ID,
		&i.FirstName,
		&i.LastName,
		&i.Username,
		&i.AboutMe,
		&i.Email,
		&i.Password,
		&i.Salt,
		&i.Deleted,
	)
	return i, err
}

const listUsers = `-- name: ListUsers :many
SELECT id, first_name, last_name, username, about_me, email, password, salt, deleted FROM users
ORDER BY id
`

func (q *Queries) ListUsers(ctx context.Context) ([]User, error) {
	rows, err := q.db.Query(ctx, listUsers)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var items []User
	for rows.Next() {
		var i User
		if err := rows.Scan(
			&i.ID,
			&i.FirstName,
			&i.LastName,
			&i.Username,
			&i.AboutMe,
			&i.Email,
			&i.Password,
			&i.Salt,
			&i.Deleted,
		); err != nil {
			return nil, err
		}
		items = append(items, i)
	}
	if err := rows.Err(); err != nil {
		return nil, err
	}
	return items, nil
}

const updateRefreshToken = `-- name: UpdateRefreshToken :one
UPDATE refresh_tokens
    SET refresh_token = $2
WHERE id = $1
RETURNING id, refresh_token, user_id
`

type UpdateRefreshTokenParams struct {
	ID           string
	RefreshToken string
}

func (q *Queries) UpdateRefreshToken(ctx context.Context, arg UpdateRefreshTokenParams) (RefreshToken, error) {
	row := q.db.QueryRow(ctx, updateRefreshToken, arg.ID, arg.RefreshToken)
	var i RefreshToken
	err := row.Scan(&i.ID, &i.RefreshToken, &i.UserID)
	return i, err
}

const updateUser = `-- name: UpdateUser :one
UPDATE users
    SET first_name = $2,
    last_name = $3,
    username = $4,
    about_me = $5,
    email = $6,
    password = $7,
    salt = $8,
    deleted = $9
WHERE id = $1
RETURNING id, first_name, last_name, username, about_me, email, password, salt, deleted
`

type UpdateUserParams struct {
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

func (q *Queries) UpdateUser(ctx context.Context, arg UpdateUserParams) (User, error) {
	row := q.db.QueryRow(ctx, updateUser,
		arg.ID,
		arg.FirstName,
		arg.LastName,
		arg.Username,
		arg.AboutMe,
		arg.Email,
		arg.Password,
		arg.Salt,
		arg.Deleted,
	)
	var i User
	err := row.Scan(
		&i.ID,
		&i.FirstName,
		&i.LastName,
		&i.Username,
		&i.AboutMe,
		&i.Email,
		&i.Password,
		&i.Salt,
		&i.Deleted,
	)
	return i, err
}
