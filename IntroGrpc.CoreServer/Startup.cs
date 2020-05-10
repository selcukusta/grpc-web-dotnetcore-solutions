using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace IntroGrpc.CoreServer
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddGrpc(options => options.Interceptors.Add<BasicInterceptor>());

            /*
            Source: https://github.com/grpc/grpc-web/blob/master/BROWSER-FEATURES.md#cors-support

            Should follow the CORS spec (Mandatory)
                - Access-Control-Allow-Credentials to allow Authorization headers
                - Access-Control-Allow-Methods to allow POST and (preflight) OPTIONS only
                - Access-Control-Allow-Headers to whatever the preflight request carries
            */
            services.AddCors(cors =>
            {
                cors.AddDefaultPolicy(builder =>
               {
                   /* Access-Control-Allow-Credentials can't be used with wildcard origins */
                   // builder.WithOrigins("grpc.local")
                   //     .AllowCredentials();

                   builder.WithOrigins("*")
                      .WithMethods("POST", "OPTIONS")
                      .WithHeaders("authorization", "keep-alive", "user-agent", "cache-control", "content-type", "content-transfer-encoding", "x-accept-content-transfer-encoding", "x-accept-response-streaming", "x-user-agent", "x-grpc-web", "grpc-timeout", "x-client-id")
                      .WithExposedHeaders("grpc-status", "grpc-message", "grpc-encoding", "grpc-accept-encoding");
               });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _config.GetValue<string>("Issuer"),
                    ValidAudience = _config.GetValue<string>("Issuer"),
                    IssuerSigningKey = new SymmetricSecurityKey
                    (
                        Encoding.UTF8.GetBytes(_config.GetValue<string>("SecretKey"))
                    )
                };
            });
            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseGrpcWeb();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GreeterService>().EnableGrpcWeb();
                endpoints.MapGrpcService<PhotoService>().EnableGrpcWeb();
                endpoints.MapControllers();
            });
        }
    }
}
