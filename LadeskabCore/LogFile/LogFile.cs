using System;
using System.Text;
using System.IO;

namespace LadeskabCore.LogFile
{
    class LogFile : ILogFile
    {
        private FileStream fs;
        private string filePath;
        public LogFile(string filePath_)
        {
            filePath = filePath_;
            fs = File.Create(filePath);
            fs.Close();
        } 
        public void LogDoorLocked(int id)
        {
            try
            {
                fs = File.Open(filePath, FileMode.Open, FileAccess.Write, FileShare.None);
                fs.Seek(0, SeekOrigin.End);
                Byte[] infoString = new UTF8Encoding(true).GetBytes(String.Format("RFID: {0} (LOCKED) @ {1}\n", id, DateTime.Now.ToString("HH:mm:ss")));

                fs.Write(infoString, 0, infoString.Length);
                fs.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void LogDoorUnlocked(int id)
        {
            try
            {
                fs = File.Open(filePath, FileMode.Open, FileAccess.Write, FileShare.None);
                fs.Seek(0, SeekOrigin.End);
                Byte[] infoString = new UTF8Encoding(true).GetBytes(String.Format("RFID: {0} (UNLOCKED) @ {1}\n", id, DateTime.Now.ToString("HH:mm:ss")));

                fs.Write(infoString, 0, infoString.Length);
                fs.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
