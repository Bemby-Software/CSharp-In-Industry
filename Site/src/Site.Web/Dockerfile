#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
RUN curl -sL https://deb.nodesource.com/setup_15.x | bash -
RUN apt-get install --assume-yes nodejs
WORKDIR /src
COPY [".", "Site.Web/"]
RUN dotnet restore "Site.Web/Site.Web.csproj"
COPY . .
WORKDIR "/src/Site.Web"
RUN dotnet build "Site.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Site.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Site.Web.dll"]
