package handlers
//
// import (
// 	"context"
// 	"encoding/json"
// 	"net/http"
// 	"odysseyapi-go/internal/auth"
// 	"odysseyapi-go/internal/db"
// 	"odysseyapi-go/internal/dto"
// 	"odysseyapi-go/internal/utils"
// 	"time"
//
// 	"github.com/jackc/pgx/v5/pgtype"
// )
//
// type SessionHandler struct {
// }
//
// func (handler *SessionHandler) CreateSession(w http.ResponseWriter, r *http.Request) {
// 	ctx, cancel := context.WithTimeout(context.Background(), 10*time.Second)
// 	defer cancel()
//
// 	tokenString := r.Header.Get("Authorization")[len("Bearer "):]
// 	user, apiErr := auth.ValidateAccessToken(tokenString)
// 	if apiErr.Message != "" {
// 		utils.FormatError(w, map[string]any{
// 			"message": apiErr.Message,
// 		}, http.StatusUnauthorized)
// 		return
// 	}
//
// 	var session dto.Session
// 	err := json.NewDecoder(r.Body).Decode(&session)
// 	if err != nil {
// 		utils.FormatError(w, map[string]any{
// 			"message": "Invalid JSON",
// 		}, http.StatusBadRequest)
// 		return
// 	}
//
// 	marshalledSession, err := json.Marshal(session)
// 	if err != nil {
// 		utils.FormatError(w, map[string]any{
// 			"message": "Internal error",
// 		}, http.StatusInternalServerError)
// 		return
// 	}
//
// 	utils.FormatData(w, map[string]any{
// 		"session": marshalledSession,
// 	}, http.StatusCreated)
// }
//
// func (handler *SessionHandler) GetSessions(w http.ResponseWriter, r *http.Request) {
//
// }
