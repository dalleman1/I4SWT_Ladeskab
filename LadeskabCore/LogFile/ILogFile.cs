namespace LadeskabCore
{
    public interface ILogFile
    {
        void LogDoorLocked(int id);
        void LogDoorUnlocked(int id);
    }
}
