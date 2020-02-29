using WordCounter.Core.Interfaces;

namespace WordCounter.Core
{
    public class FileRepository : IFileRepository
    {
        readonly IFileManager _fileManager;

        public FileRepository(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public string LoadFile(string filePath)
        {
            using (var reader = _fileManager.StreamReader(filePath))
            {
                return reader.ReadToEnd();
            }
        }
    }
}