FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
FROM build AS publish

WORKDIR /src
######## Publishing SharedLib
COPY CSARN.SharedLib/ ./CSARN.SharedLib/
RUN dotnet publish "CSARN.SharedLib/CSARN.SharedLib.csproj" -c Release -o /app/publish /p:UseAppHost=false
########

######## Publishing Core.ApplicationServicesAbstractions
COPY CSARN.MessagingMicroservice/Core/ApplicationServicesAbstractions/ ./CSARN.MessagingMicroservice/Core/ApplicationServicesAbstractions/
RUN dotnet publish "CSARN.MessagingMicroservice/Core/ApplicationServicesAbstractions/ApplicationServiceAbstractions.csproj" -c Release -o /app/publish /p:UseAppHost=false
########

######## Publishing Core.DomainServicesAbstractions
COPY CSARN.MessagingMicroservice/Core/DomainServicesAbstractions/ ./CSARN.MessagingMicroservice/Core/DomainServicesAbstractions/
RUN dotnet publish "CSARN.MessagingMicroservice/Core/DomainServicesAbstractions/DomainServicesAbstractions.csproj" -c Release -o /app/publish /p:UseAppHost=false
########

######## Publishing Infrastructure
COPY CSARN.MessagingMicroservice/Infrastructure/ ./CSARN.MessagingMicroservice/Infrastructure/
RUN dotnet publish "CSARN.MessagingMicroservice/Infrastructure/Infrastructure.csproj" -c Release -o /app/publish /p:UseAppHost=false
########

######## Publishing Web
COPY CSARN.MessagingMicroservice/Web/ ./CSARN.MessagingMicroservice/Web/
RUN dotnet publish "CSARN.MessagingMicroservice/Web/Web.csproj" -c Release -o /app/publish /p:UseAppHost=false
######## 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]