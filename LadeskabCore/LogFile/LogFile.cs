using System;
using System.Text;
using System.IO;

namespace LadeskabCore.LogFile
{
    public class LogFile : ILogFile
    {
        private FileStream fs;
        private string filePath;
        public LogFile(string filePath_)
        {
            filePath = filePath_;
            fs = File.Create(filePath);
            fs.Close();
        } 
        public string LogDoorLocked(int id)
        {
            string time = DateTime.Now.ToString("HH:mm:ss");

            try
            {
                fs = File.Open(filePath, FileMode.Open, FileAccess.Write, FileShare.None);
                fs.Seek(0, SeekOrigin.End);
                Byte[] infoString = new UTF8Encoding(true).GetBytes(String.Format("RFID: {0} (LOCKED) @ {1}\n", id, time));

                fs.Write(infoString, 0, infoString.Length);
                fs.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return time;
        }

        public string LogDoorUnlocked(int id)
        {
            string time = DateTime.Now.ToString("HH:mm:ss");

            try
            {
                fs = File.Open(filePath, FileMode.Open, FileAccess.Write, FileShare.None);
                fs.Seek(0, SeekOrigin.End);
                Byte[] infoString = new UTF8Encoding(true).GetBytes(String.Format("RFID: {0} (UNLOCKED) @ {1}\n", id, time));

                fs.Write(infoString, 0, infoString.Length);
                fs.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return time;
        }
    }
}
