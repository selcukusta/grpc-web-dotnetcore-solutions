# Summary

If you want to call gRPC endpoints from web projects, you have to use proxy server for translating gRPC-Web to gRPC HTTP/2.

With `Grpc.AspNetCore.Web`, you don't need to manage any server. `Kestrel` can do it!

# Configure gRPC-Web in ASP.NET Core

- Add `Grpc.AspNetCore.Web` package via `dotnet add package Grpc.AspNetCore.Web --version 2.28.0-pre2`.

- Set CORS settings (`Startup.cs`) via `services.AddCors()`

- Activate `GrpcWeb` and `Cors` (`Startup.cs`) via `app.UseGrpcWeb()` and `app.UseCors()`

- Configure your gRPC endpoint (`Startup.cs`) can be called via web via `.EnableGrpcWeb()`

- Publish your application to `Vagrant` folder: `dotnet publish -c Release -r linux-x64 -o ../IntroGrpc.Proxy/application`

# Define different CORS policies per GRPC endpoints

- Use `Startup.md` instead of `Startup.cs` _(Don't forget to rename it.)_

# References

- [Microsoft Docs](https://docs.microsoft.com/en-us/aspnet/core/grpc/browser?view=aspnetcore-3.1)

- [gRPC-Web Github](https://github.com/grpc/grpc-web/blob/master/BROWSER-FEATURES.md#cors-support)
