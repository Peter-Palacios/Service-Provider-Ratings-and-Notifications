FROM golang:1.20 AS build 
WORKDIR /app
COPY . .
RUN ./build.sh

FROM gcr.io/distroless/base-debian10
WORKDIR /app
COPY --from=build /app/notification-service .
CMD ["./notification-service"]