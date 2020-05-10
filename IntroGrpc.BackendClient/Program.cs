using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using IntroGrpc.CoreServer;

namespace IntroGrpc.BackendClient
{
    class Program
    {
        static async Task Main(string[] args)
        {

            /* Pass authorization header */
            // Get Token -> https://grpc.local/api/authority

            // var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE1ODg5NDc4NjEsImlzcyI6ImdycGMubG9jYWwiLCJhdWQiOiJncnBjLmxvY2FsIn0._PPeqqnzSV90PgBqN-oJZzVrEfQdB37ZnjsxGQ6NGGI";
            // var credentials = CallCredentials.FromInterceptor((context, metadata) =>
            // {
            //     if (!string.IsNullOrEmpty(token))
            //     {
            //         metadata.Add("Authorization", $"Bearer {token}");
            //     }
            //     return Task.CompletedTask;
            // });

            using var channel = GrpcChannel.ForAddress("https://grpc.local/", new GrpcChannelOptions
            {
                //Credentials = ChannelCredentials.Create(new SslCredentials(), credentials)
            });
            // Unary 
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Reply from gRPC Server: " + reply.Message);
            // Server Streaming
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));
            var photoClient = new Photo.PhotoClient(channel);
            var photoReply = photoClient.SayHello(new PhotoRequest { Name = "GreeterClient" });
            var stream = photoReply.ResponseStream;
            var result = new List<byte>();
            while (await stream.MoveNext(cts.Token))
            {
                result.AddRange(stream.Current.Message.ToByteArray());
            }
            using var fStream = new FileStream("output.png", FileMode.OpenOrCreate, FileAccess.Write);
            await fStream.WriteAsync(result.ToArray(), 0, result.Count, cts.Token);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
