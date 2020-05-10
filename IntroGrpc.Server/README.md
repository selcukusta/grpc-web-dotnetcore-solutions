# Summary

This project isn't different from standard .NET gRPC template (`dotnet new grpc`). You don't need to change anything. Only publish your application to `Vagrant` folder: `dotnet publish -c Release -r linux-x64 -o ../IntroGrpc.Proxy/application`

If you want to consume your gRPC endpoints from web projects, you need to setup a proxy server such as `Envoy`. It's ready-to-use in `IntroGrpc.Proxy` folder.
