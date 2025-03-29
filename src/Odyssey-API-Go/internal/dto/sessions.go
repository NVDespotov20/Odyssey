package dto

type Session struct {
	StartTime string `json:"startTime"`
	EndTime   string `json:"endTime"`
	InstructorID string `json:"instructorId"`
	StudentID string `json:"studentId"`
}
