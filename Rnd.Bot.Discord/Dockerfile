FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Rnd.Bot.Discord/Rnd.Bot.Discord.csproj", "Rnd.Bot.Discord/"]
RUN dotnet restore "Rnd.Bot.Discord/Rnd.Bot.Discord.csproj"
COPY . .
WORKDIR "/src/Rnd.Bot.Discord"
RUN dotnet build "Rnd.Bot.Discord.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rnd.Bot.Discord.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rnd.Bot.Discord.dll"]
