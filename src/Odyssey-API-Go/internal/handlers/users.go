package handlers

import (
	"encoding/json"
	"net/http"
	"odysseyapi-go/internal/auth"
	"odysseyapi-go/internal/dto"
	"odysseyapi-go/internal/services"
	"odysseyapi-go/internal/utils"
)

type UserHandler struct {
	userService *services.UserService
}

func (handler *UserHandler) Login(w http.ResponseWriter, r *http.Request) {
	var loginInfo dto.Login

	err := json.NewDecoder(r.Body).Decode(&loginInfo)
	if err != nil {
		utils.FormatError(w, map[string]any{
			"message": "Invalid JSON",
		}, http.StatusBadRequest)
		return
	}

	accessToken, refreshToken, apiErr := handler.userService.Login(loginInfo)
	if apiErr.Message != "" {
		utils.FormatError(w, map[string]any{
			"message": apiErr.Message,
		}, apiErr.Code)
		return
	}

	utils.FormatData(w, map[string]any{
		"accessToken":  accessToken,
		"refreshToken": refreshToken,
	}, http.StatusOK)

}

func (handler *UserHandler) Register(w http.ResponseWriter, r *http.Request) {
	var registerInfo dto.Register

	err := json.NewDecoder(r.Body).Decode(&registerInfo)
	if err != nil {
		utils.FormatError(w, map[string]any{
			"message": "Invalid JSON",
		}, http.StatusBadRequest)
		return
	}

	user, apiErr := handler.userService.Register(registerInfo)
	if apiErr.Message != "" {
		utils.FormatError(w, map[string]any{
			"message": apiErr.Message,
		}, apiErr.Code)
		return
	}

	utils.FormatData(w, map[string]any{
		"user": user,
	}, http.StatusOK)
}

func (handler *UserHandler) FetchAccessToken(w http.ResponseWriter, r *http.Request) {
	tokenString := r.Header.Get("Authorization")[len("Bearer "):]

	user, apiErr := auth.ValidateRefreshToken(tokenString)

	if apiErr.Message != "" {
		utils.FormatError(w, map[string]any{
			"message": apiErr.Message,
		}, apiErr.Code)
		return
	}

	tokenString, apiErr = auth.GenerateAccessToken(user.ID)

	if apiErr.Message != "" {
		utils.FormatError(w, map[string]any{
			"message": apiErr.Message,
		}, apiErr.Code)
		return
	}

	utils.FormatData(w, map[string]any{
		"accessToken": tokenString,
	}, apiErr.Code)
}
