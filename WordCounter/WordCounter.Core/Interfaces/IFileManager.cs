using System.IO;

namespace WordCounter.Core.Interfaces
{
    public interface IFileManager
    {
        StreamReader StreamReader(string path);
    }
}