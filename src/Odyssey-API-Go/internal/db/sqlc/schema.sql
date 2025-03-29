CREATE TABLE users (
    id TEXT DEFAULT gen_random_uuid() PRIMARY KEY,
    first_name TEXT NOT NULL,
    last_name TEXT NOT NULL,
    username TEXT UNIQUE NOT NULL,
    about_me TEXT,
    email TEXT UNIQUE NOT NULL,
    password bytea NOT NULL,
    salt TEXT NOT NULL,
    deleted BOOLEAN DEFAULT NULL
);

CREATE TABLE refresh_tokens (
    id TEXT DEFAULT gen_random_uuid() PRIMARY KEY,
    refresh_token TEXT NOT NULL,
    user_id TEXT NOT NULL,
    CONSTRAINT fk_user_id FOREIGN KEY(user_id) REFERENCES users(id)
);
