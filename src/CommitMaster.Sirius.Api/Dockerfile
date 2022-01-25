FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/CommitMaster.Sirius.Api/CommitMaster.Sirius.Api.csproj", "CommitMaster.Sirius.Api/"]
RUN dotnet restore "src/CommitMaster.Sirius.Api/CommitMaster.Sirius.Api.csproj"
COPY . .
WORKDIR "/src/CommitMaster.Sirius.Api"
RUN dotnet build "CommitMaster.Sirius.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CommitMaster.Sirius.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CommitMaster.Sirius.Api.dll"]
