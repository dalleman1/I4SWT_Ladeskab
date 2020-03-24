using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using LadeskabCore;
using NSubstitute;
using NUnit.Framework;
using LadeskabCore.LogFile;

namespace LadeskabCoreTest
{
    [TestFixture]
    public class LogFileTest
    {
        private ILogFile _logFile;

        [SetUp]
        public void SetUp()
        {
            _logFile = Substitute.For<ILogFile>();
        }

    //    [Test]
    //    public void LogDoorLocked_Test_Called()
    //    {
    //        var mockFileSystem = new MockFileSystem();

    //        var mockInputFile = new MockFileData("line1\nline2\nline3");

    //        mockFileSystem.AddFile(@"D:\Repo\test.txt", mockInputFile);

    //        var sut = new FileProcessorTestable(mockFileSystem);
    //        sut.ConvertFirstLineToUpper(@"D:\Repo\test.txt");

    //        MockFileData mockOutputFile = mockFileSystem.GetFile(@"D:\Repo\test.txt");

    //        string[] outputLines = mockOutputFile.TextContents.Split();

    //        Assert.Equals("LINE1", outputLines[0]);
    //        Assert.Equals("line2", outputLines[1]);
    //        Assert.Equals("line3", outputLines[2]);
       }

    //}
    //public class FileProcessorTestable
    //{
    //    private readonly IFileSystem _fileSystem;

    //    public FileProcessorTestable() : this(new FileSystem()) { }

    //    public FileProcessorTestable(IFileSystem fileSystem)
    //    {
    //        _fileSystem = fileSystem;
    //    }

    //    public void ConvertFirstLineToUpper(string inputFilePath)
    //    {
    //        string outputFilePath = Path.ChangeExtension(inputFilePath, ".out.txt");

    //        using (StreamReader inputReader = _fileSystem.File.OpenText(inputFilePath))
    //        using (StreamWriter outputWriter = _fileSystem.File.CreateText(outputFilePath))
    //        {
    //            bool isFirstLine = true;

    //            while (!inputReader.EndOfStream)
    //            {
    //                string line = inputReader.ReadLine();

    //                if (isFirstLine)
    //                {
    //                    line = line.ToUpperInvariant();
    //                    isFirstLine = false;
    //                }

    //                outputWriter.WriteLine(line);
    //            }
    //        }
    //    }
    //}

}