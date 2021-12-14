FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /sln

# Copy the source code and restore project
COPY . .

RUN dotnet restore WishingWell.Api/WishingWell.Api.csproj

# Test Project
RUN dotnet test "WishingWell.UnitTests/WishingWell.UnitTests.csproj"

# Build Project
RUN dotnet publish "WishingWell.Api/WishingWell.Api.csproj" -c Release -o /app/publish

# FINAL image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WishingWell.Api.dll"]
