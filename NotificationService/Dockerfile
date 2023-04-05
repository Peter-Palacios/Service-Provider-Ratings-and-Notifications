FROM golang:1.20 AS build 
WORKDIR /app
COPY . .
RUN go get github.com/gin-gonic/gin
RUN go build -o notification-service

FROM gcr.io/distroless/base-debian10
WORKDIR /app
COPY --from=build /app/notification-service .
CMD ["./notification-service"]
