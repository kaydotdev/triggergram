using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Triggergram.Core
{
    public class MediaPost
    {
        private readonly BlobContainerClient _blobContainer;
        private readonly string[] _allowedExtensions = new[] { "image/jpg", "image/png" };
        
        public MediaPost()
        {
            var blobClient = new BlobServiceClient(
                Environment.GetEnvironmentVariable("STORAGE_CONNECTION_STRING")
            );
            _blobContainer = blobClient.GetBlobContainerClient("mediastorage");
        }
        
        [FunctionName("MediaPost")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log,
            CancellationToken token)
        {
            log.LogInformation($"Requested {req.Method} /api/MediaPost");

            return req.Method switch
            {
                "GET" => new NotFoundResult(),
                "POST" => await SaveMediaToStorage(req, token),
                _ => new NotFoundResult()
            };
        }

        private async Task<IActionResult> SaveMediaToStorage(HttpRequest req, CancellationToken token)
        {
            if (!req.HasFormContentType)
                return new BadRequestObjectResult(new { Message = "Incorrect input data." });

            if (req.Form.Files.GetFile("media") is null)
                return new BadRequestObjectResult(new { Message = "Media content is missing." });

            var mediaFileName = Guid.NewGuid().ToString();
            var mediaFile = req.Form.Files["media"];
            
            if (!_allowedExtensions.Contains(mediaFile.ContentType))
                return new BadRequestObjectResult(new { Message = "Only *.jpg and *.png media types allowed." });

            await using var fileStream = new MemoryStream();
            await mediaFile.CopyToAsync(fileStream, token);

            fileStream.Position = 0;
            await _blobContainer.UploadBlobAsync($"photo/{mediaFileName}{Path.GetExtension(mediaFile.FileName)}",
                                                 fileStream, token);

            return new OkObjectResult(new { Message = mediaFileName });
        }
    }
}
