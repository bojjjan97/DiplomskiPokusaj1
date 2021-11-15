#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DiplomskiPokusaj1/DiplomskiPokusaj1.csproj", "DiplomskiPokusaj1/"]
RUN dotnet restore "DiplomskiPokusaj1/DiplomskiPokusaj1.csproj"
COPY . .
WORKDIR "/src/DiplomskiPokusaj1"
RUN dotnet build "DiplomskiPokusaj1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DiplomskiPokusaj1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DiplomskiPokusaj1.dll"]
