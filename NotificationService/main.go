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

var notifications = []Notification{
	{ID:"1", ProviderID:"User_01", Message:"Example1", TimeCreated: time.Now()},
	{ID:"2", ProviderID:"User_02", Message:"Example1", TimeCreated: time.Now()},
	{ID:"3", ProviderID:"User_01", Message:"Example1", TimeCreated: time.Now()},
	{ID:"4", ProviderID:"User_03", Message:"Example1", TimeCreated: time.Now()},
}

func main() {
	http.HandleFunc("/notifications", getNotifications)
	http.ListenAndServe(":8080", nil)
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