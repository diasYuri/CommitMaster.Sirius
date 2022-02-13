FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/CommitMaster.Sirius.PaymentService/CommitMaster.Sirius.PaymentService.csproj", "CommitMaster.Sirius.PaymentService/"]
RUN dotnet restore "src/CommitMaster.Sirius.PaymentService/CommitMaster.Sirius.PaymentService.csproj"
COPY . .
WORKDIR "/src/CommitMaster.Sirius.PaymentService"
RUN dotnet build "CommitMaster.Sirius.PaymentService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CommitMaster.Sirius.PaymentService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CommitMaster.Sirius.PaymentService.dll"]
