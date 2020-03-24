using System;

namespace LadeskabCore.StationControl
{
    public interface IStationControl
    {
        bool Isconnected();
        void CheckID(int oldID, int ID);
        void ChargeMessage();
        void LockDoor();
        void UnlockDoor();
        void HandleDetectEventRFID(object sender, EventArgs e);
        void HandleDetectEventDoor(object sender, EventArgs e);
        void HandleDetectedEventCharge(object sender, EventArgs e);
    }

}