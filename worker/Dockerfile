FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build

COPY . /src

WORKDIR /src
RUN dotnet publish -c release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:5.0-alpine AS runtime
COPY --from=build /app/publish .
ENTRYPOINT [ "dotnet", "worker.dll" ]

