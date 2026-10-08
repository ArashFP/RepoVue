# Build: docker build -t repovue-api api/

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY RepoVue.Api/RepoVue.Api.csproj RepoVue.Api/
RUN dotnet restore RepoVue.Api/RepoVue.Api.csproj
COPY RepoVue.Api/ RepoVue.Api/
RUN dotnet publish RepoVue.Api/RepoVue.Api.csproj -c Release -o /app --no-restore

# Chiseled = a minimal Ubuntu image with no shell or package manager, which runs as a non-root user.
# Less inside the image means fewer things that can have vulnerabilities.
FROM mcr.microsoft.com/dotnet/aspnet:10.0-noble-chiseled
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "RepoVue.Api.dll"]
