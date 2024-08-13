#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Product/Product.API/Product.API.csproj", "Product/Product.API/"]
COPY ["src/Product/Product.Application/Product.Application.csproj", "Product/Product.Application/"]
COPY ["src/Product/Product.Core/Product.Core.csproj", "Product/Product.Core/"]
COPY ["src/Product/Product.Infrastructure/Product.Infrastructure.csproj", "Product/Product.Infrastructure/"]

RUN echo "MESSAGE: Jestem tu podczas budowania obrazu"
RUN dotnet restore "./Product/Product.API/Product.API.csproj"
WORKDIR "/src/Product/Product.API"
COPY . .

RUN dotnet build "./Product.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Product.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Product.API.dll"]