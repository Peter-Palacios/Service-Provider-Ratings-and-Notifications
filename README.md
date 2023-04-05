# Service-Provider-Ratings-and-Notifications

How to Use Project:

1) Build and run services 
	
	cd/path
	
	docker-compose build
	
	docker-compose up -d
	
	
2) Test Ratings Services

	Going to URL:

	http://localhost:5000/api/ratings/11111111-1111-1111-1111-111111111111
	
	shows output of 4.5
	since the data seeded is initialized at rating of 4 and another rating of 5, returning their average
	
	the same can be done for
	http://localhost:5000/api/ratings/22222222-2222-2222-2222-222222222222
	
	which returns an average rating of 4
	
	Additional Ratings can be added using xcurl:
	
	curl -X POST -H "Content-Type: application/json" -d '{"ProviderId": "22222222-2222-2222-2222-222222222222", "Value": 1}' http://localhost:5000/api/ratings
	
	after running the average rating now returns a value of 3
	
3)Test Notifcation Services

	Since the Notification Services only reflects new ratings you can see the new rating that was just added by going to the URL:
	http://localhost:8080/notifications
	
	after refreshing the page again the new notification is gone
	
	Additional testing of Notification services can be done by changing the value of the ProviderId and Value to different values and then refreshing the localhost 	again:
	
	curl -X POST -H "Content-Type: application/json" -d '{"ProviderId": "33333333-3333-3333-3333-333333333333", "Value": 1}' http://localhost:5000/api/ratings
	curl -X POST -H "Content-Type: application/json" -d '{"ProviderId": "33333333-3333-3333-3333-333333333333", "Value": 5}' http://localhost:5000/api/ratings
	curl -X POST -H "Content-Type: application/json" -d '{"ProviderId": "44444444-4444-4444-4444-444444444444", "Value": 4}' http://localhost:5000/api/ratings
	curl -X POST -H "Content-Type: application/json" -d '{"ProviderId": "55555555-5555-5555-5555-555555555555", "Value": 3}' http://localhost:5000/api/ratings



Documention:

	Maintainability:
		Project divided into two separate services: Ratings and Notifications
		This separation can make it easier to manage or update the services without effecting the other

	Reliability:
		Error Handling in the Ratings service includes handling and logging of errors during runtime
		HTTP response codes: services provide feedback on success or failure of requests

	Scalability:
		Project Uses Docker to containerize the services, making it wasier to scale by deploying additional instances

		Notification service is statless, which improves scalability by handling mutliple requests at the same time without
		mainting internal state between requests. It also makes it easier to scale by adding more services

		Uses Decoupled Services: Ratings and Notifications servies communicate via HTTP, so they scale independently


Additional notes:

	Before this assignment I never touched Docker or Go, so both technologies were completely new, but being a backend engineer is something I strive for so it was         a great challenge and fun from my side.

	Submitting project as-is for the sake of time.

	Possible improvements :

	Additional unit/integrations testing can be added manually or using additonal xUnit

	Proper logging in Notification Service simliar to those in Rating Service

	Timout handling for http requests

	Caching for Rating service to reduce load on the data source




