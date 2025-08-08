FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY NewGolfingApplication.sln ./
COPY GolfingAppUI/GolfingAppUI.csproj GolfingAppUI/
COPY GolfingDataAccessLib/GolfingDataAccessLib.csproj GolfingDataAccessLib/
RUN dotnet restore NewGolfingApplication.sln
COPY . ./
RUN dotnet publish NewGolfingApplication.sln -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "GolfingAppUI.dll"]