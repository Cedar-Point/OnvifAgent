#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OnvifAgent/OnvifAgent.csproj", "OnvifAgent/"]
RUN dotnet restore "OnvifAgent/OnvifAgent.csproj"
COPY . .
WORKDIR "/src/OnvifAgent"
RUN dotnet build "OnvifAgent.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OnvifAgent.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnvifAgent.dll"]