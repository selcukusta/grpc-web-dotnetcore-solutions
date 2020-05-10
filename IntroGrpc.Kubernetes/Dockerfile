# C# gRPC files cannot be generated on "alpine" images...
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build-ENV
COPY ./src /app
WORKDIR /app
RUN dotnet publish -c Release -r linux-musl-x64 -o publish-folder
FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.1-alpine as runtime
COPY --from=build-env /app/publish-folder ./
ENTRYPOINT ["./[YOUR_ASSEMBLY_NAME]]