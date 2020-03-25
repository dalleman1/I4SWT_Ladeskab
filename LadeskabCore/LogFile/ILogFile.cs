namespace LadeskabCore
{
    public interface ILogFile
    {
        string LogDoorLocked(int id);
        string LogDoorUnlocked(int id);
    }
}
