FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
FROM build AS publish

######## Publishing SharedLib
COPY ["CSARN.SharedLib/", "./CSARN.SharedLib/"]
RUN dotnet publish "CSARN.SharedLib/CSARN.SharedLib.csproj" -c Release -o /app/publish /p:UseAppHost=false
########

######## Publishing Infrastructure
COPY ["CSARN.AccountsMicroservice/Infrastructure/", "./CSARN.AccountsMicroservice/Infrastructure/"]
RUN dotnet publish "CSARN.AccountsMicroservice/Infrastructure/Infrastructure.csproj" -c Release -o /app/publish /p:UseAppHost=false
######## 

######## Publishing AccountMsvc
COPY ["CSARN.AccountsMicroservice/Web/", "./CSARN.AccountsMicroservice/Web/"]
RUN dotnet publish "CSARN.AccountsMicroservice/Web/Web.csproj" -c Release -o /app/publish /p:UseAppHost=false
########

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]