using Microsoft.Extensions.Logging;

namespace WebPhotoAlbum.Logging
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private string localFilePath;
        public FileLoggerProvider(string path) { localFilePath = path; }

        public ILogger CreateLogger(string categoryName) { return new FileLogger(localFilePath, categoryName); }

        public void Dispose() { }
    }
}
