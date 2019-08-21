using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace WebPhotoAlbum.Logging
{
    public class FileLogger : ILogger
    {
        private string _categoryName;
        private string _localFilePath;
        private object multithreadlocker = new object();
        public FileLogger(string path, string categoryName) {
            _localFilePath = path;
            _categoryName = categoryName;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            lock (multithreadlocker)
            {
                File.AppendAllText(_localFilePath, @"__  ______        ____  _           _          _    _ _                       _                                " + Environment.NewLine);
                File.AppendAllText(_localFilePath, @"\ \/ /  _ \      |  _ \| |__   ___ | |_ ___   / \  | | |__  _   _ _ __ ___   | |    ___   __ _  __ _  ___ _ __ " + Environment.NewLine);
                File.AppendAllText(_localFilePath, @" \  /| | | |_____| |_) | '_ \ / _ \| __/ _ \ / _ \ | | '_ \| | | | '_ ` _ \  | |   / _ \ / _` |/ _` |/ _ \ '__|" + Environment.NewLine);
                File.AppendAllText(_localFilePath, @" /  \| |_| |_____|  __/| | | | (_) | || (_) / ___ \| | |_) | |_| | | | | | | | |__| (_) | (_| | (_| |  __/ |   " + Environment.NewLine);
                File.AppendAllText(_localFilePath, @"/_/\_\____/      |_|   |_| |_|\___/ \__\___/_/   \_\_|_.__/ \__,_|_| |_| |_| |_____\___/ \__, |\__, |\___|_|   " + Environment.NewLine);
                File.AppendAllText(_localFilePath, @"                                                                                         |___/ |___/           " + Environment.NewLine);
                File.AppendAllText(_localFilePath, $"Logging date: {DateTime.Now}" + Environment.NewLine);
                File.AppendAllText(_localFilePath, Environment.NewLine);
            }
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == LogLevel.Information;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                lock (multithreadlocker)
                    File.AppendAllText(_localFilePath, $"(Category: {_categoryName}, Date: {DateTime.Now}): {formatter(state, exception)}{Environment.NewLine}");
            }
        }
    }
}
