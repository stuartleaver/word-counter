using System.IO;
using WordCounter.Core.Interfaces;

namespace WordCounter.Core
{
    public class FileManager : IFileManager
    {
        /// <summary>
        /// Returns a Stream Reader for a given file
        /// </summary>
        /// <param name="path">Path and name of the file</param>
        /// <returns>StreamReader</returns>
        public StreamReader StreamReader(string path)
        {
            return new StreamReader(path);
        }
    }
}