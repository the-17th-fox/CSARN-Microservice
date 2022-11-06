FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MessagingDatabase/MessagingDatabase.sqlproj", "MessagingDatabase/"]
RUN dotnet restore "MessagingDatabase/MessagingDatabase.sqlproj"
COPY . .
WORKDIR "/src/MessagingDatabase"
RUN dotnet build "MessagingDatabase.sqlproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MessagingDatabase.sqlproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessagingDatabase.dll"]