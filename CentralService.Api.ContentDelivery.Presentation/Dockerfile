#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CentralService.Api.ContentDelivery.Presentation/CentralService.Api.ContentDelivery.Presentation.csproj", "CentralService.Api.ContentDelivery.Presentation/"]
RUN dotnet restore "CentralService.Api.ContentDelivery.Presentation/CentralService.Api.ContentDelivery.Presentation.csproj"
COPY . .
WORKDIR "/src/CentralService.Api.ContentDelivery.Presentation"
RUN dotnet build "CentralService.Api.ContentDelivery.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CentralService.Api.ContentDelivery.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CentralService.Api.ContentDelivery.Presentation.dll"]