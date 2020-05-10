using System.IO;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace IntroGrpc.CoreServer
{
    public class PhotoService : Photo.PhotoBase
    {
        private readonly ILogger<PhotoService> _logger;
        public PhotoService(ILogger<PhotoService> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public override async Task SayHello(PhotoRequest request, IServerStreamWriter<PhotoReply> responseStream, ServerCallContext context)
        {
            var path = Path.Combine("Assets", "image.png");
            using (FileStream file = new FileStream(path, FileMode.Open, System.IO.FileAccess.Read))
            {
                PhotoReply reply = new PhotoReply();
                reply.Message = await Google.Protobuf.ByteString.FromStreamAsync(file, context.CancellationToken);
                await responseStream.WriteAsync(reply);
            }
        }
    }
}
