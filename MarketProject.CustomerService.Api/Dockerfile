#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MarketProject.CustomerService.Api/MarketProject.CustomerService.Api.csproj", "MarketProject.CustomerService.Api/"]
COPY ["MarketProject.CustomerService.Domain/MarketProject.CustomerService.Domain.csproj", "MarketProject.CustomerService.Domain/"]
COPY ["MarketProject.CustomerService.Application/MarketProject.CustomerService.Application.csproj", "MarketProject.CustomerService.Application/"]
RUN dotnet restore "MarketProject.CustomerService.Api/MarketProject.CustomerService.Api.csproj"
COPY . .
WORKDIR "/src/MarketProject.CustomerService.Api"
RUN dotnet build "MarketProject.CustomerService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MarketProject.CustomerService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MarketProject.CustomerService.Api.dll"]