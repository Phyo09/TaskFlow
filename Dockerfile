# Use the official .NET SDK image to build and publish the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . ./
RUN dotnet publish -c Release -o /out

# Use the ASP.NET runtime image for the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

# Let Render bind to the PORT env variable
ENV ASPNETCORE_URLS=http://+:$PORT

EXPOSE 10000

COPY TaskFlow.db ./

ENTRYPOINT ["dotnet", "TaskFlow.dll"]
