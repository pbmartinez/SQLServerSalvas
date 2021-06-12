#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["WebTemplate.NetCore/WebTemplate.NetCore.csproj", "WebTemplate.NetCore/"]
COPY ["Core.Application/Core.Application.csproj", "Core.Application/"]
COPY ["Core.Domain/Core.Domain.csproj", "Core.Domain/"]
COPY ["Core.Language/Core.Language.csproj", "Core.Language/"]
COPY ["Infrastructure.Shared/Infrastructure.Shared.csproj", "Infrastructure.Shared/"]
COPY ["Infrastructure.Persistence/Infrastructure.Persistence.csproj", "Infrastructure.Persistence/"]
RUN dotnet restore "WebTemplate.NetCore/WebTemplate.NetCore.csproj"
COPY . .
WORKDIR "/src/WebTemplate.NetCore"
RUN dotnet build "WebTemplate.NetCore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebTemplate.NetCore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebTemplate.NetCore.dll"]
