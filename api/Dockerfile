FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG BUILDOS
ARG BUILDARCH

WORKDIR /app

COPY . .

RUN dotnet publish --os $BUILDOS --arch $BUILDARCH --framework net8.0 --configuration Release --output ./pub

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS run

WORKDIR /app

ENV ASPNETCORE_URLS=http://+:80

COPY --from=build /app/pub .

ENTRYPOINT ["dotnet", "./TourOfHeroes.API.dll"]
