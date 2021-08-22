using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Triggergram.Core.Services.Contracts;

namespace Triggergram.Core.Services.Implementation
{
    public class MediaContainer : IMediaContainer
    {
        private readonly BlobContainerClient _blobContainer;

        public MediaContainer(string connectionString)
        {
            var blobClient = new BlobServiceClient(connectionString);
            _blobContainer = blobClient.GetBlobContainerClient("mediastorage");
        }

        public async Task SaveMediaAsync(string name, Stream fileStream, CancellationToken token)
        {
            await _blobContainer.UploadBlobAsync(name, fileStream, token);
        }

        public async Task<Stream> DownloadMediaAsync(string name, CancellationToken token)
        {
            var blob = _blobContainer.GetBlobClient(name);
            var stream = new MemoryStream();

            await blob.DownloadToAsync(stream, token);

            return stream;
        }
    }
}
