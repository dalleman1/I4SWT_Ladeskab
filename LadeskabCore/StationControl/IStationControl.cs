namespace LadeskabCore.StationControl
{
    public interface IStationControl
    {
        bool Isconnected();

        void CheckID(int oldID, int ID);

        void ChargeMessage();

        void LockDoor();

        void UnlockDoor();
    }

}