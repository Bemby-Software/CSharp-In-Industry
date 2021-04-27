FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
RUN curl -sL https://deb.nodesource.com/setup_15.x | bash -
RUN apt-get install --assume-yes nodejs
COPY . .
RUN dotnet publish "Site.Web/Site.Web.csproj" -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /app/out .
ENV ASPNETCORE_ENVIRONMENT Production
ENTRYPOINT dotnet Site.Web.dll