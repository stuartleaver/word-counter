namespace WordCounter.Core.Interfaces
{
    public interface IFileRepository
    {
        string LoadFile(string filePath);
    }
}