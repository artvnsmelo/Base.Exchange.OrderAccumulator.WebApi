# IMAGE
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# RUN
RUN apk add --no-cache icu-data-full icu-libs tzdata krb5-libs libgcc libintl libssl3 libstdc++ zlib

# BUILD
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /build
COPY nuget.config ./
COPY ["Genial.B2b.Partner.Customer.Core.WebApi.API/Genial.B2b.Partner.Customer.Core.WebApi.API.csproj", "Genial.B2b.Partner.Customer.Core.WebApi.API/"]
COPY ["Genial.B2b.Partner.Customer.Core.WebApi.Infra/Genial.B2b.Partner.Customer.Core.WebApi.Infra.csproj", "Genial.B2b.Partner.Customer.Core.WebApi.Infra/"]
COPY ["Genial.B2b.Partner.Customer.Core.WebApi.Service/Genial.B2b.Partner.Customer.Core.WebApi.Service.csproj", "Genial.B2b.Partner.Customer.Core.WebApi.Service/"]
COPY ["Genial.B2b.Partner.Customer.Core.WebApi.Domain/Genial.B2b.Partner.Customer.Core.WebApi.Domain.csproj", "Genial.B2b.Partner.Customer.Core.WebApi.Domain/"]
COPY ["Genial.B2b.Partner.Customer.Core.WebApi.Util/Genial.B2b.Partner.Customer.Core.WebApi.Util.csproj", "Genial.B2b.Partner.Customer.Core.WebApi.Util/"]
RUN dotnet restore -s https://pkgs.dev.azure.com/geniallabs/_packaging/geniallabs/nuget/v3/index.json -s https://api.nuget.org/v3/index.json "Genial.B2b.Partner.Customer.Core.WebApi.API/Genial.B2b.Partner.Customer.Core.WebApi.API.csproj"

COPY . .
WORKDIR "/build/Genial.B2b.Partner.Customer.Core.WebApi.API"
RUN dotnet build "Genial.B2b.Partner.Customer.Core.WebApi.API.csproj" -c Release -o /app/build


# PUBLISH
FROM build AS publish
RUN dotnet publish "Genial.B2b.Partner.Customer.Core.WebApi.API.csproj" -c Release -o /app/publish

# RUN
FROM base AS final
WORKDIR /app

# ENVS
ENV DOTNET_USE_POLLING_FILE_WATCHER=true
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV TZ=America/Sao_Paulo

# STARTUP
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Genial.B2b.Partner.Customer.Core.WebApi.API.dll"]
