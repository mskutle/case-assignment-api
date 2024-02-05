FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
ENV DOTNET_URLS=http://+:8080
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CaseAssignment.Api/CaseAssignment.Api.csproj", "CaseAssignment.Api/"]
RUN dotnet restore "CaseAssignment.Api/CaseAssignment.Api.csproj"
COPY . .
WORKDIR "/src/CaseAssignment.Api"
RUN dotnet build "CaseAssignment.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CaseAssignment.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CaseAssignment.Api.dll"]
