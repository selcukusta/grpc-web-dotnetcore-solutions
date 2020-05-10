using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;

namespace IntroGrpc.CoreServer
{
    public class BasicInterceptor : Interceptor
    {
        private readonly ILogger<BasicInterceptor> _logger;

        public BasicInterceptor(ILogger<BasicInterceptor> logger)
        {
            _logger = logger;
        }

        /* Will be used for gRPC Client. */
        /* 
            grpc-web is not supported interceptors yet! It will be released on v1.1.0.
            https://github.com/grpc/grpc-web/pull/558#issuecomment-620309351 
        */
        public override AsyncServerStreamingCall<TResponse> AsyncServerStreamingCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncServerStreamingCallContinuation<TRequest, TResponse> continuation)
        {

            /*
            var headers = context.Options.Headers;
            if (headers == null)
            {
                headers = new Metadata();
                var options = context.Options.WithHeaders(headers);
                context = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, options);
            }

            headers.Add("caller-user", Environment.UserName);
            headers.Add("caller-machine", Environment.MachineName);
            headers.Add("caller-os", Environment.OSVersion.ToString());
            return continuation(request, context);
            */
            return base.AsyncServerStreamingCall(request, context, continuation);
        }


        /* Will be used for gRPC Server */
        public override Task ServerStreamingServerHandler<TRequest, TResponse>(TRequest request, IServerStreamWriter<TResponse> responseStream, ServerCallContext context, ServerStreamingServerMethod<TRequest, TResponse> continuation)
        {
            _logger.LogTrace($"Starting call. Type: {MethodType.ServerStreaming}. Request: {typeof(TRequest)}. Response: {typeof(TResponse)}");
            LogMetadata(context.RequestHeaders, "caller-user");
            LogMetadata(context.RequestHeaders, "caller-machine");
            LogMetadata(context.RequestHeaders, "caller-os");
            return continuation(request, responseStream, context);
        }
        private void LogMetadata(Metadata headers, string key)
        {
            var headerValue = headers.SingleOrDefault(h => h.Key == key)?.Value;
            _logger.LogTrace($"{key}: {headerValue ?? "(unknown)"}");
        }
    }
}