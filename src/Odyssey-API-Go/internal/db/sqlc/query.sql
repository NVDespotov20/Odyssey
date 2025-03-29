-- name: GetUserById :one
SELECT * FROM users
WHERE id = $1 LIMIT 1;

-- name: GetUserByUsername :one
SELECT * FROM users
WHERE username = $1 LIMIT 1;

-- name: ListUsers :many
SELECT * FROM users
ORDER BY id;

-- name: CreateUsers :one
INSERT INTO users (
   first_name, last_name, username, about_me, email, password, salt
) VALUES (
    $1, $2, $3, $4, $5, $6, $7
)
RETURNING *;

-- name: UpdateUser :one
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
RETURNING *;

-- name: DeleteUser :one
DELETE FROM users
WHERE id = $1
RETURNING *;

-- name: CreateRefreshToken :one
INSERT INTO refresh_tokens(
    refresh_token, user_id
) VALUES (
    $1, $2
)
RETURNING *;

-- name: UpdateRefreshToken :one
UPDATE refresh_tokens
    SET refresh_token = $2
WHERE id = $1
RETURNING *;

-- name: DeleteRefreshToken :one

DELETE FROM refresh_tokens
WHERE id = $1
RETURNING *;

-- name: GetRefreshTokenByUserId :one

SELECT refresh_token FROM refresh_tokens
WHERE user_id = $1;

-- name: CreateSession :one

INSERT INTO sessions(
    start_time, end_time, instructor_id, student_id
) VALUES (
    $1, $2, $3, $4
)
RETURNING *;

-- name: GetSessionsByInstructorId :many

SELECT start_time, end_time FROM sessions
WHERE instructor_id = $1;

-- name: GetSessionsByInstructorIdAuth :many

SELECT start_time, end_time, student_id FROM sessions 
WHERE instructor_id = $1;

-- name: DeleteSession :one

DELETE FROM sessions
WHERE id = $1
RETURNING *;
