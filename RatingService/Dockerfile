#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

#FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
#WORKDIR /src
#COPY ["Service Provider Ratings and Notifications.csproj", "."]
#RUN dotnet restore "./Service Provider Ratings and Notifications.csproj"
#COPY . .
#WORKDIR "/src/."
#RUN dotnet build "Service Provider Ratings and Notifications.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "Service Provider Ratings and Notifications.csproj" -c Release -o /app/publish /p:UseAppHost=false
#
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
##EXPOSE 5000
#ENTRYPOINT ["dotnet", "RatingService.dll"]


FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY . .
RUN ./build.sh

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Service Provider Ratings and Notifications.dll"]