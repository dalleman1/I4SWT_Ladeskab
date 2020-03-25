using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using LadeskabCore;
using NSubstitute;
using NUnit.Framework;
using LadeskabCore.LogFile;
using System.Collections.Generic;

namespace LadeskabCoreTest
{
    [TestFixture]
    public class LogFileTest
    {

        [Test]
        public void fileCreation_Test()
        {
            string filePath = @"test.txt";

            File.Delete(filePath); // Make sure the file wasn't there before the constructor call
            ILogFile logFile = new LogFile(filePath);

            Assert.True(File.Exists(filePath));
        }

        [Test]
        public void LogDoorLocked_Test()
        {
            string filePath = @"test.txt";
            string time;

            IEnumerable<string> lines;
            ILogFile logFile = new LogFile(filePath);

            time = logFile.LogDoorLocked(123);
            lines = File.ReadLines(filePath);

            foreach(var l in lines) {
                Assert.AreEqual(string.Format("RFID: 123 (LOCKED) @ {0}", time), l);
            }
        }

        [Test]
        public void LogDoorUnlocked_Test()
        {
            string filePath = @"test.txt";
            string time;

            IEnumerable<string> lines;
            ILogFile logFile = new LogFile(filePath);

            time = logFile.LogDoorUnlocked(123);
            lines = File.ReadLines(filePath);

            foreach (var l in lines)
            {
                Assert.AreEqual(string.Format("RFID: 123 (UNLOCKED) @ {0}", time), l);
            }
        }
    }
}