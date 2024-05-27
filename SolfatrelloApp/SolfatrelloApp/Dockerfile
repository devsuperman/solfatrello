FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5030

ENV ASPNETCORE_URLS=http://+:5030

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["SolfatrelloApp/SolfatrelloApp/SolfatrelloApp.csproj", "SolfatrelloApp/SolfatrelloApp/"]
RUN dotnet restore "SolfatrelloApp/SolfatrelloApp/SolfatrelloApp.csproj"
COPY . .
WORKDIR "/src/SolfatrelloApp/SolfatrelloApp"
RUN dotnet build "SolfatrelloApp.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "SolfatrelloApp.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SolfatrelloApp.dll"]
