FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["KekAuth/KekAuth.csproj", "KekAuth/"]
RUN dotnet restore "KekAuth/KekAuth.csproj"
COPY . .
WORKDIR "/src/KekAuth"
RUN dotnet build "KekAuth.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KekAuth.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KekAuth.dll"]
