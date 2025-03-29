package utils

import "time"

type APIResponse struct {
	APIVersion string            `json:"apiVersion,omitempty"`
	Context    string            `json:"context,omitempty"`
	ID         string            `json:"id,omitempty"`
	Method     string            `json:"method,omitempty"`
	Params     *Params           `json:"params,omitempty"`
	Data       *Data             `json:"data,omitempty"`
	Error      *APIError         `json:"error,omitempty"`
}

type Params struct {
	ID string `json:"id,omitempty"`
}

type Data struct {
	Kind             string     `json:"kind,omitempty"`
	Fields           string     `json:"fields,omitempty"`
	ETag             string     `json:"etag,omitempty"`
	ID               string     `json:"id,omitempty"`
	Lang             string     `json:"lang,omitempty"`
	Updated          *time.Time `json:"updated,omitempty"`
	Deleted          bool       `json:"deleted,omitempty"`
	CurrentItemCount int        `json:"currentItemCount,omitempty"`
	ItemsPerPage     int        `json:"itemsPerPage,omitempty"`
	StartIndex       int        `json:"startIndex,omitempty"`
	TotalItems       int        `json:"totalItems,omitempty"`
	PageIndex        int        `json:"pageIndex,omitempty"`
	TotalPages       int        `json:"totalPages,omitempty"`
	PageLinkTemplate string     `json:"pageLinkTemplate,omitempty"`
	Next             *PageLink  `json:"next,omitempty"`
	NextLink         string     `json:"nextLink,omitempty"`
	Previous         *PageLink  `json:"previous,omitempty"`
	PreviousLink     string     `json:"previousLink,omitempty"`
	Self             *PageLink  `json:"self,omitempty"`
	SelfLink         string     `json:"selfLink,omitempty"`
	Edit             *PageLink  `json:"edit,omitempty"`
	EditLink         string     `json:"editLink,omitempty"`
	Items            []Item     `json:"items,omitempty"`
}

type PageLink struct {
	URL string `json:"url,omitempty"`
}

type Item struct {
	ID    string `json:"id,omitempty"`
	Title string `json:"title,omitempty"`
	Data  string `json:"data,omitempty"`
}

type APIError struct {
	Code    int              `json:"code,omitempty"`
	Message string           `json:"message,omitempty"`
	Errors  []APIErrorDetail `json:"errors,omitempty"`
}

type APIErrorDetail struct {
	Domain       string `json:"domain,omitempty"`
	Reason       string `json:"reason,omitempty"`
	Message      string `json:"message,omitempty"`
	Location     string `json:"location,omitempty"`
	LocationType string `json:"locationType,omitempty"`
	ExtendedHelp string `json:"extendedHelp,omitempty"`
	SendReport   string `json:"sendReport,omitempty"`
}
