FROM mcr.micsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY GolfingApplication.sln ./
COPY GolfingAppUI/GolfingAppUI.csproj GolfingAppUI/
COPY GolfingDataAccessLib/GolfingDataAccessLib.csproj GolfingDataAccessLib/
RUN dotnet restore GolfingApplication.sln
COPY . ./
RUN dotnet publish GolfingApplication.sln -c Release -o /app/publish

FROM mcr.micsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "GolfingAppUI.dll"]