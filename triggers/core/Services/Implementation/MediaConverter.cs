using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using Triggergram.Core.Services.Contracts;

namespace Triggergram.Core.Services.Implementation
{
    public class MediaConverter : IMediaConverter
    {
        public async Task<Stream> ConvertMediaFormatAsync(Stream mediaStream, CancellationToken token = default)
        {
            mediaStream.Position = 0;
            var convertedMediaStream = new MemoryStream();
            var image = await Image.LoadAsync(mediaStream);
            await image.SaveAsPngAsync(convertedMediaStream, token);
            convertedMediaStream.Position = 0;
            return convertedMediaStream;
        }
    }
}
