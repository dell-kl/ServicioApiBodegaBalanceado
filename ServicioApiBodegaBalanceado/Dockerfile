
FROM mcr.microsoft.com/dotnet/sdk:10.0
RUN groupadd produccion && useradd -r produccion -g produccion && usermod -s /usr/sbin/nologin produccion
USER produccion
WORKDIR /home/produccion
COPY --chown=produccion:produccion ./Publish .
ENV ASPNETCORE_URLS=http://*:8080
ENV ASPNETCORE_ENVIRONMENT=Docker 
EXPOSE 8081
EXPOSE 8080

CMD [ "dotnet", "ServicioApiBodegaBalanceado.dll" ]

#FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
#USER $APP_UID
#WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081
#
#FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["ServicioApiBodegaBalanceado/ServicioApiBodegaBalanceado.csproj", "ServicioApiBodegaBalanceado/"]
#COPY ["Business/Business.csproj", "Business/"]
#COPY ["Data/Data.csproj", "Data/"]
#COPY ["Domain/Domain.csproj", "Domain/"]
#COPY ["Utility/Utility.csproj", "Utility/"]
#RUN dotnet restore "ServicioApiBodegaBalanceado/ServicioApiBodegaBalanceado.csproj"
#COPY . .
#WORKDIR "/src/ServicioApiBodegaBalanceado"
#RUN dotnet build "./ServicioApiBodegaBalanceado.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./ServicioApiBodegaBalanceado.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "ServicioApiBodegaBalanceado.dll"]
