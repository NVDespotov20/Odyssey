package utils

import (
	"encoding/json"
	"net/http"
)

func FormatData(w http.ResponseWriter, data map[string]any, statusCode int) {
	body, err := json.Marshal(map[string]any{
		"data": data,
	})

	if err != nil {
        w.WriteHeader(http.StatusInternalServerError)
		return
	}

    if statusCode != http.StatusOK {
		w.WriteHeader(statusCode)
	}


	w.Write(body)

	return
}

func FormatError(w http.ResponseWriter, error map[string]any, statusCode int) (err error) {
	body, err := json.Marshal(map[string]any{
		"error": error,
	})

	if err != nil {
		return
	}

	w.WriteHeader(statusCode)
	w.Write(body)

	return
}
