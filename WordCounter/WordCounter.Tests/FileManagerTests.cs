using System.IO;
using System.Text;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordCounter.Core;
using WordCounter.Core.Interfaces;

namespace WordCounter.Tests
{
    [TestClass]
    public class FileManagerTests
    {
        private ILogger<FileRepository> _logger;

        private IFileManager _fileManager;

        private IFileRepository _fileRepository;

        [TestInitialize]
        public void TestInit()
        {
            _logger = A.Fake<ILogger<FileRepository>>();

            _fileManager = A.Fake<IFileManager>();

            _fileRepository = new FileRepository(_logger, _fileManager);
        }

        [TestMethod]
        public void FileManagerTests_StreamReaderReturnsTheCorrectText()
        {
            // Arrange
            const string fileText = "The quick brown fox jumps over the lazy dog.";

            var fileTextBytes = Encoding.UTF8.GetBytes(fileText);

            var memoryStream = new MemoryStream(fileTextBytes);

            const string expectedResult = "The quick brown fox jumps over the lazy dog.";

            A.CallTo(() => _fileManager.StreamReader(A<string>.Ignored)).Returns(new StreamReader(memoryStream));

            // Act
            var actualResult = _fileRepository.LoadFile(@"c:\file.txt");

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void FileManagerTests_ThrowsAnExceptionWhenTheFileIsNotFound()
        {
            // Arrange
            A.CallTo(() => _fileManager.StreamReader(A<string>.Ignored)).Throws(() => new FileNotFoundException());

            // Act
            _fileRepository.LoadFile(@"c:\filedoesnotexist.txt");

            // Assert - Expects Exception
        }
    }
}