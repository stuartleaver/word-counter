using System.IO;
using WordCounter.Core.Interfaces;

namespace WordCounter.Core
{
    public class FileManager : IFileManager
    {
        public StreamReader StreamReader(string path)
        {
            return new StreamReader(path);
        }
    }
}