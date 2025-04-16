FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src
RUN apt update && apt install -y nodejs

COPY ["Medium/Medium.Web/Medium.Web.csproj", "Medium/Medium.Web/"]
COPY ["Medium/Medium.Domain/Medium.Domain.csproj", "Medium/Medium.Domain/"]
COPY ["Medium/Medium.Application/Medium.Application.csproj", "Medium/Medium.Application/"]
COPY ["Medium/Medium.Infrastructure/Medium.Infrastructure.csproj", "Medium/Medium.Infrastructure/"]

RUN dotnet restore "./Medium/Medium.Web/Medium.Web.csproj"
COPY . .

WORKDIR "/src/Medium/Medium.Web"
RUN dotnet build "./Medium.Web.csproj" -c Release -o /app

FROM build AS publish
WORKDIR "/src/Medium/Medium.Web"
RUN dotnet publish "./Medium.Web.csproj" -c Release -o /app

#FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
FROM build AS final
WORKDIR /app
COPY --from=publish /app .

EXPOSE 80
ENTRYPOINT ["dotnet", "Medium.Web.dll"]