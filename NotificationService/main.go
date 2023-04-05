package main

import (
	"encoding/json"
	"net/http"
	"time"
)
type Notification struct {
	ID         string `json:"id"`
	ProviderID string `json:"providerId"`
	Message 	string `json:"message"`
	TimeCreated	time.Time `json:"timeCreated"`
}


func main() {

	http.HandleFunc("/notifications", notificationsHandler) 
	http.ListenAndServe("0.0.0.0:8080", nil)

}


type NotificationStore struct{

	notifications []Notification
}


func (s *NotificationStore) Add(notification Notification){
	s.notifications = append(s.notifications, notification)
}

func(s *NotificationStore) GetAllClear() []Notification {
	notificationsCopy :=make([]Notification, len(s.notifications))
	copy(notificationsCopy, s.notifications)
	s.notifications=nil

	return notificationsCopy

}

var store = &NotificationStore{}

func getNotifications(w http.ResponseWriter, r (*http.Request)){
	notifications :=store.GetAllClear()

	json.NewEncoder(w).Encode(notifications)
}

func postNotification(w http.ResponseWriter, r *http.Request) {
    if r.Method != http.MethodPost {
        w.WriteHeader(http.StatusMethodNotAllowed)
        return
    }

    var notification Notification
    err := json.NewDecoder(r.Body).Decode(&notification)
    if err != nil {
        w.WriteHeader(http.StatusBadRequest)
        return
    }

    notification.TimeCreated = time.Now()
    store.Add(notification)

    w.WriteHeader(http.StatusCreated)
}

func notificationsHandler(w http.ResponseWriter, r *http.Request) {
    switch r.Method {
    case http.MethodGet:
        getNotifications(w, r)
    case http.MethodPost:
        postNotification(w, r)
    default:
        w.WriteHeader(http.StatusMethodNotAllowed)
    }
}