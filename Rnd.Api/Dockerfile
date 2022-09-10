FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Rnd.Api/Rnd.Api.csproj", "Rnd.Api/"]
RUN dotnet restore "Rnd.Api/Rnd.Api.csproj"
COPY . .
WORKDIR "/src/Rnd.Api"
RUN dotnet build "Rnd.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rnd.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rnd.Api.dll"]
