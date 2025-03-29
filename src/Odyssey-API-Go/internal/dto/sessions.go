package dto

type Session struct {
    StartTime string `json:"startTime"`
    EndTime string `json:"endTime"`
	Email     string `json:"instructorId"`
	Username  string `json:"studentId"`
}
