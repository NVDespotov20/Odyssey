package services
//
// import (
// 	"net/http"
// 	"odysseyapi-go/internal/dto"
// 	"odysseyapi-go/internal/utils"
// )
//
// type SessionService struct {
// }
//
// func (service *SessionService) CreateSession(session dto.Session) (apiErr utils.APIError) {
//
// 	if session.StudentID != user.ID {
// 	    apiErr.Message = "Missing permissions"
//         apiErr.Code = http.StatusForbidden
// 		return
// 	}
//
// 	startTime, err := time.Parse("2025-03-29 14:30:00 GMT", session.StartTime)
// 	if err != nil {
// 	    apiErr.Message = "Invalid time format"
//         apiErr.Code = http.
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
// }
