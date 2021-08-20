using System;
using System.IO;
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
    public static class MediaPost
    {
        [FunctionName("MediaPost")]
        public static async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log,
            CancellationToken token)
        {
            var blobClient = new BlobServiceClient(
                Environment.GetEnvironmentVariable("STORAGE_CONNECTION_STRING")
            );
            var blobContainer = blobClient.GetBlobContainerClient("mediastorage");

            log.LogInformation($"Requested {req.Method} /api/MediaPost");

            if (!req.HasFormContentType)
                return new BadRequestObjectResult(new { Message = "Incorrect input data." });

            if (req.Form.Files.GetFile("media") is null)
                return new BadRequestObjectResult(new { Message = "Media content is missing." });

            var mediaFileName = Guid.NewGuid().ToString();

            await using var fileStream = new MemoryStream();
            await req.Form.Files["media"].CopyToAsync(fileStream, token);

            fileStream.Position = 0;
            await blobContainer.UploadBlobAsync($"photo/{mediaFileName}", fileStream, token);

            return new OkObjectResult(new { Message = mediaFileName });
        }
    }
}
