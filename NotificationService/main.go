package main
// import (
// 	"encoding/json"
// 	"net/http"
// 	"sync"
// 	"time"
// 	"github.com/gin-gonic/gin"

import (
	//"fmt"
	"encoding/json"
	"net/http"
	//"sync"
	"time"
	//"github.com/gin-gonic/gin"
)
type Notification struct {
	ID         string `json:"id"`
	ProviderID string `json:"providerId"`
	Message 	string `json:"message"`
	TimeCreated	time.Time `json:"timeCreated"`
}

// var notifications = []Notification{
// 	{ID:"1", ProviderID:"User_01", Message:"Example1", TimeCreated: time.Now()},
// 	{ID:"2", ProviderID:"User_02", Message:"Example2", TimeCreated: time.Now()},
// 	{ID:"3", ProviderID:"User_01", Message:"Example3", TimeCreated: time.Now()},
// 	{ID:"4", ProviderID:"User_03", Message:"Example4", TimeCreated: time.Now()},
// }

func main() {
	// http.HandleFunc("/notifications", getNotifications)
	// http.HandleFunc("/notifications", postNotification)
	http.HandleFunc("/notifications", notificationsHandler) 
	http.ListenAndServe("0.0.0.0:8080", nil)
	//http.ListenAndServe(":8080", nil)
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