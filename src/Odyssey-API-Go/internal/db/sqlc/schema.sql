CREATE TABLE users (
    id TEXT DEFAULT gen_random_uuid() PRIMARY KEY,
    first_name TEXT NOT NULL,
    last_name TEXT NOT NULL,
    username TEXT UNIQUE NOT NULL,
    about_me TEXT,
    email TEXT UNIQUE NOT NULL,
    password bytea NOT NULL,
    salt TEXT NOT NULL,
    deleted BOOLEAN DEFAULT FALSE NOT NULL
);

CREATE TABLE refresh_tokens (
    id TEXT DEFAULT gen_random_uuid() PRIMARY KEY,
    token TEXT NOT NULL,
    user_id TEXT NOT NULL,
    CONSTRAINT fk_user_id FOREIGN KEY(user_id) REFERENCES users(id)
);

CREATE TABLE sessions (
    id TEXT DEFAULT gen_random_uuid() PRIMARY KEY,
    start_time TIMESTAMPTZ NOT NULL,
    end_time TIMESTAMPTZ NOT NULL,
    instructor_id TEXT NOT NULL,
    student_id TEXT NOT NULL,
    accepted BOOLEAN DEFAULT FALSE NOT NULL,
    CONSTRAINT fk_instructor_id FOREIGN KEY(instructor_id) REFERENCES users(id),
    CONSTRAINT fk_student_id FOREIGN KEY(student_id) REFERENCES users(id)
);
