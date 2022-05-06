FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY ["AspNetTutorial.sln", "./"]
COPY ["AspNet.Tutorial.Testing/AspNet.Tutorial.Testing.csproj", "AspNet.Tutorial.Testing/"]
COPY ["AspNet.Tutorial.Core/AspNet.Tutorial.Core.csproj", "AspNet.Tutorial.Core/"]
COPY ["AspNet.Tutorial.Infrastructure/AspNet.Tutorial.Infrastructure.csproj", "AspNet.Tutorial.Infrastructure/"]
COPY ["AspNet.Tutorial.WebApi/AspNet.Tutorial.WebApi.csproj", "AspNet.Tutorial.WebApi/"]
RUN dotnet restore
COPY . .
RUN dotnet build -o /app/build

FROM build AS publish
RUN dotnet publish -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS run
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspNet.Tutorial.WebApi.dll"]
