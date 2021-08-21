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

using Triggergram.Core.Services.Contracts;

namespace Triggergram.Core
{
    public class MediaPost
    {
        private readonly BlobContainerClient _blobContainer;
        private readonly IMediaConverter _mediaConverter;
        private readonly string[] _allowedExtensions = { "image/jpeg", "image/png" };
        
        public MediaPost(IMediaConverter mediaConverter)
        {
            var blobClient = new BlobServiceClient(
                Environment.GetEnvironmentVariable("STORAGE_CONNECTION_STRING")
            );
            _blobContainer = blobClient.GetBlobContainerClient("mediastorage");
            _mediaConverter = mediaConverter;
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
            await using (var convertedFileStream = await _mediaConverter.ConvertMediaFormatAsync(fileStream, token))
            {
                await _blobContainer.UploadBlobAsync($"{Guid.Empty}/{mediaFileName}.png",
                    convertedFileStream, token);
            }

            return new OkObjectResult(new { Message = mediaFileName });
        }
    }
}
