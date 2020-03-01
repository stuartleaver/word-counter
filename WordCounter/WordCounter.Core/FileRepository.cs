using System.IO;
using Microsoft.Extensions.Logging;
using WordCounter.Core.Interfaces;

namespace WordCounter.Core
{
    public class FileRepository : IFileRepository
    {
        private readonly ILogger<FileRepository> _logger;

        readonly IFileManager _fileManager;

        public FileRepository(ILogger<FileRepository> logger, IFileManager fileManager)
        {
            _logger = logger;

            _fileManager = fileManager;
        }

        public string LoadFile(string filePath)
        {
            try
            {
                using (var reader = _fileManager.StreamReader(filePath))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (FileNotFoundException e)
            {
                _logger.LogError($"A file error occured - {e.Message}");

                throw;
            }
        }
    }
}