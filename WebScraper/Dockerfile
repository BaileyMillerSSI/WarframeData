FROM microsoft/dotnet:2.2-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
RUN ls
WORKDIR /src
COPY ["WebScraper/WebScraper.csproj", "WebScraper/"]
COPY ["SharedLibs/SharedLibs.csproj", "SharedLibs/"]
RUN dotnet restore "WebScraper/WebScraper.csproj"
COPY . .
WORKDIR "/src/WebScraper"
RUN dotnet build "WebScraper.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebScraper.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebScraper.dll"]
