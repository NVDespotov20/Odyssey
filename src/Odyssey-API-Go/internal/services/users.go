package services

import (
	"bytes"
	"context"
	"crypto/rand"
	"net/http"
	"odysseyapi-go/internal/auth"
	"odysseyapi-go/internal/db"
	"odysseyapi-go/internal/dto"
	"odysseyapi-go/internal/utils"
	"time"

	"github.com/dlclark/regexp2"
	"golang.org/x/crypto/argon2"
)

type UserService struct {
}

func validateInput(registerInfo dto.Register) (apiErr utils.APIError) {
	regex, err := regexp2.Compile(`^\p{L}+.{2,24}$`, 0)
	if err != nil {
		apiErr.Message = "Internal error ANM"
		apiErr.Code = http.StatusInternalServerError
		return
	}

	valid, _ := regex.MatchString(registerInfo.FirstName)
	if !valid {
		apiErr.Message = "Invalid name"
		apiErr.Code = http.StatusBadRequest
		return
	}

	valid, _ = regex.MatchString(registerInfo.LastName)
	if !valid {
		apiErr.Message = "Invalid name"
		apiErr.Code = http.StatusBadRequest
		return
	}

	regex, err = regexp2.Compile(`^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,32}$`, 0)
	if err != nil {
		apiErr.Message = err.Error()
		apiErr.Code = http.StatusInternalServerError
		return
	}

	valid, _ = regex.MatchString(registerInfo.Password)
	if !valid {
		apiErr.Message = "Invalid password"
		apiErr.Code = http.StatusBadRequest
		return
	}

	regex, err = regexp2.Compile(`^(?=.{8,32}$)[A-Za-z0-9._%+-]+@[A-Za-z0-9-]+\.[A-Za-z]{2,}$`, 0)
	if err != nil {
		apiErr.Message = "Internal error EMAIl"
		apiErr.Code = http.StatusInternalServerError
		return
	}

	valid, _ = regex.MatchString(registerInfo.Email)
	if !valid {
		apiErr.Message = "Invalid email"
		apiErr.Code = http.StatusBadRequest
		return
	}

	regex, err = regexp2.Compile(`^[A-Za-z0-9._-]{3,32}$`, 0)
	if err != nil {
		apiErr.Message = "Internal error USERNAME"
		apiErr.Code = http.StatusInternalServerError
		return
	}

	valid, _ = regex.MatchString(registerInfo.Username)
	if !valid {
		apiErr.Message = "Invalid email"
		apiErr.Code = http.StatusBadRequest
		return
	}

	return

}

func (service *UserService) Login(loginInfo dto.Login) (accessToken string, refreshToken string, apiErr utils.APIError) {
	ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer cancel()

	user, err := db.GetQueries().GetUserByUsername(ctx, loginInfo.Username)
	if err != nil {
		apiErr.Code = http.StatusUnauthorized
		apiErr.Message = "Invalid username or password"
		return
	}

	hashedPassword := argon2.IDKey([]byte(loginInfo.Password), []byte(user.Salt), 2, 1024*19, 1, uint32(len(loginInfo.Password)))
	if !bytes.Equal(hashedPassword, user.Password) {
		apiErr.Code = http.StatusUnauthorized
		apiErr.Message = "Invalid username or password"
		return
	}

	refreshToken, apiErr = auth.GenerateRefreshToken(user.ID)
	if apiErr.Message != "" {
		apiErr.Code = http.StatusInternalServerError
		apiErr.Message = "Token generation failed"
		return
	}

	accessToken, apiErr = auth.GenerateAccessToken(user.ID)
	if apiErr.Message != "" {
		apiErr.Code = http.StatusInternalServerError
		apiErr.Message = "Token generation failed"
		return
	}

	return
}

func (service *UserService) Register(registerInfo dto.Register) (user db.User, apiErr utils.APIError) {
	ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer cancel()
	var err error

	apiErr = validateInput(registerInfo)
	if apiErr.Message != "" {
		return
	}

	user, err = db.GetQueries().GetUserByUsername(ctx, registerInfo.Username)
	if err == nil {
		apiErr.Code = http.StatusBadRequest
		apiErr.Message = "User already exists"
		return
	}

	salt := rand.Text()[:24]
	user, err = db.GetQueries().CreateUsers(ctx, db.CreateUsersParams{
		FirstName: registerInfo.FirstName,
		LastName:  registerInfo.LastName,
		Username:  registerInfo.Username,
		Email:     registerInfo.Email,
		Salt:      salt,
		Password:  argon2.IDKey([]byte(registerInfo.Password), []byte(salt), 2, 1024*19, 1, uint32(len(registerInfo.Password))),
	})

	if err != nil {
		apiErr.Code = http.StatusInternalServerError
		apiErr.Message = "User creation failed"
	}

	_, err = db.GetQueries().CreateRefreshToken(ctx, db.CreateRefreshTokenParams{
		RefreshToken: rand.Text()[:24],
		UserID:       user.ID,
	})

	if err != nil {
		apiErr.Code = http.StatusInternalServerError
		apiErr.Message = "User creation failed"
	}

	return
}
