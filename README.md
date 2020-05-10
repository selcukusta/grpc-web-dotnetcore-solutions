# How to use gRPC-Web with .NET Core Solutions

## Summary

With this repository, we'll trying to consume our .NET Core based gRPC applications from web clients.

Common way is use a proxy server, generally Envoy, but there is a new alternative for that: **Support gRPC-Web alongside gRPC HTTP/2 in ASP.NET Core**.

The repository includes all of these scenarios.

## Projects

Each projects have `README.md` file. All details are written in that.

| Project Name             | Description                                                                                                  |
| ------------------------ | ------------------------------------------------------------------------------------------------------------ |
| IntroGrpc.Server         | .NET gRPC project template. Details are provided in the README.md file.                                      |
| IntroGrpc.CoreServer     | .NET gRPC project template with `grpc-web` support. Details are provided in the README.md file.              |
| IntroGrpc.Proxy          | It creates a virtual machine depends on gRPC project deployment. Details are provided in the README.md file. |
| IntroGrpc.BasicWebClient | It's a basic `HTML` project which is consume your gRPC application.                                          |
| IntroGrpc.ReactWebClient | It's a basic `React` project which is consume your gRPC application.                                         |
| IntroGrpc.BackendClient  | It's a basic `Console` project which is consume your gRPC application.                                       |
| IntroGrpc.Kubernetes     | It includes basic manifest files about how to deploy gRPC server application to the Kubernetes cluster.      |

## Prerequests

- To develop, build and publish server applications you need to have `.NET Core 3.1 SDK`.

- To deploy and publish gGRPC-web applications and envoy proxy you need to have `Vagrant`.

- To run `protoc` commands you need `protobuf` release. It can be downloaded from [this](https://github.com/protocolbuffers/protobuf/releases) address or if you're a MacOS user, you can get it via `brew install protobuf`.

- To generate the protobuf messages and client service stub class you need `protoc-gen-grpc-web` plugin. It can be downloaded from [this](https://github.com/grpc/grpc-web/releases) address or if you're a MacOS user, you can get it via `brew install protoc-gen-grpc-web`.

- To run client applications, you need to have `Node.js`.

## Run via Proxy

```
cd IntroGrpc.Server
rm -rf ../IntroGrpc.Proxy/application && dotnet publish -c Release -r linux-x64  -o ../IntroGrpc.Proxy/application
cd ../IntroGrpc.Proxy
# Remove comment dashes from 30. and 34. lines in Vagrantfile, add to the 29. and 33. lines.
vagrant up
```

## Run without Proxy (via `Grpc.AspNetCore.Web`)

```
cd IntroGrpc.CoreServer
rm -rf ../IntroGrpc.Proxy/application && dotnet publish -c Release -r linux-x64  -o ../IntroGrpc.Proxy/application
cd ../IntroGrpc.Proxy
# Remove comment dashes from 29. and 33. lines in Vagrantfile, add to the 30. and 34. lines.
vagrant up
```
