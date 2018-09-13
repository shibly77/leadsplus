FROM microsoft/dotnet:2.1.300

WORKDIR /dotnetapp
EXPOSE 80

COPY ./bin/Docker .

ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT ["dotnet", "Agent.API.dll"]
