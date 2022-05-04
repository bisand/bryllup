FROM bisand/dotnet-runtime-asp AS base
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000

FROM bisand/dotnet-build AS build
RUN mkdir src \
    && mkdir app \
    && mkdir app/build \
    && mkdir app/publish \
    && chown -R $(whoami) src \
    && chown -R $(whoami) app 

COPY --chown=builduser ["bryllup.csproj", ""]
RUN dotnet restore "bryllup.csproj"
COPY --chown=builduser . .
RUN dotnet build "bryllup.csproj" -c Release -o ./app/build

FROM build AS publish
RUN dotnet publish "bryllup.csproj" -c Release -o ./app/publish /p:UseAppHost=false

FROM base AS final
COPY --from=publish /home/builduser/app/publish .
ENTRYPOINT ["dotnet", "bryllup.dll"]
