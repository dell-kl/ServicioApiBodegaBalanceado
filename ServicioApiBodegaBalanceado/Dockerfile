
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