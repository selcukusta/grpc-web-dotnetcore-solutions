/* This file can be used to define different CORS policies per GRPC endpoints. */

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IntroGrpc.CoreServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            /*
            Source: https://github.com/grpc/grpc-web/blob/master/BROWSER-FEATURES.md#cors-support

            Should follow the CORS spec (Mandatory)
                - Access-Control-Allow-Credentials to allow Authorization headers
                - Access-Control-Allow-Methods to allow POST and (preflight) OPTIONS only
                - Access-Control-Allow-Headers to whatever the preflight request carries
            */
            services.AddCors(cors =>
            {
                cors.AddPolicy("GreeterServicePolicy", builder =>
                {
                    /* Access-Control-Allow-Credentials can't be used with wildcard origins */
                    // builder.WithOrigins("grpc.local")
                    //     .AllowCredentials();

                    builder.WithOrigins("*")
                        .WithMethods("POST", "OPTIONS")
                        .WithHeaders("keep-alive", "user-agent", "cache-control", "content-type", "content-transfer-encoding", "x-accept-content-transfer-encoding", "x-accept-response-streaming", "x-user-agent", "x-grpc-web", "grpc-timeout")
                        .WithExposedHeaders("grpc-status", "grpc-message", "grpc-encoding", "grpc-accept-encoding");
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseGrpcWeb();
            app.UseCors("GreeterServicePolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GreeterService>().EnableGrpcWeb().RequireCors("GreeterServicePolicy");
                endpoints.MapGrpcService<PhotoService>().EnableGrpcWeb().RequireCors("GreeterServicePolicy");

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
