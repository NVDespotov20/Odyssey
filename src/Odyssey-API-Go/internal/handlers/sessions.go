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
// 	if session.StudentID != user.ID {
// 		utils.FormatError(w, map[string]any{
// 			"message": "Missing permissions",
// 		}, http.StatusUnauthorized)
// 		return
// 	}
//
// 	startTime, err := time.Parse("2025-03-29 14:30:00 GMT", session.StartTime)
// 	if err != nil {
// 		utils.FormatError(w, map[string]any{
// 			"message": "Invalid time format",
// 		}, http.StatusBadRequest)
// 		return
// 	}
// 	var startTimePg pgtype.Timestamptz
// 	startTimePg.Scan(startTime)
//
// 	endTime, err := time.Parse("2025-03-29 14:30:00 GMT", session.EndTime)
// 	if err != nil {
// 		utils.FormatError(w, map[string]any{
// 			"message": "Invalid time format",
// 		}, http.StatusBadRequest)
// 		return
// 	}
// 	var endTimePg pgtype.Timestamptz
// 	endTimePg.Scan(endTime)
//
// 	_, err = db.GetQueries().CreateSession(ctx, db.CreateSessionParams{
// 		StartTime:    startTimePg,
// 		EndTime:      endTimePg,
// 		InstructorID: session.InstructorID,
// 		StudentID:    session.StudentID,
// 	})
// 	if err != nil {
// 		utils.FormatError(w, map[string]any{
// 			"message": "Internal error",
// 		}, http.StatusInternalServerError)
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
//
// }
