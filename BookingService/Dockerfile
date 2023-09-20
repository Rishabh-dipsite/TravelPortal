#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
RUN apt-get -y update && apt-get install -y curl
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BookingService/BookingService.csproj", "BookingService/"]
COPY ["SharedClassLibrary/SharedClassLibrary.csproj", "SharedClassLibrary/"]
RUN dotnet restore "BookingService/BookingService.csproj"

WORKDIR "/src/SharedClassLibrary"
COPY SharedClassLibrary .
WORKDIR "/src/BookingService"
COPY BookingService .
RUN dotnet build "BookingService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookingService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookingService.dll"]